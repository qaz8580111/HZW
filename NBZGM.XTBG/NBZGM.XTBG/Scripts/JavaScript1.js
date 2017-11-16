/// <reference path="jquery/jquery-2.2.4.js" />
/// <reference path="zTree_v3-master/js/jquery.ztree.all.js" />
//提交成功信息框
function successMsg() {
    //Messenger().post('提交成功！');
    Messenger().post({ message: '提交成功！' + new Date().toLocaleString(), type: 'success', showCloseButton: true });
}
//提交失败信息框
function errorMsg() {
    //Messenger().post('网络异常，请联系管理员！');
    Messenger().post({ message: '网络异常，请联系管理员！' + new Date().toLocaleString(), type: 'error', showCloseButton: true });
}
//提交失败信息框
function errorTimeMsg() {
    //Messenger().post('网络异常，请联系管理员！');
    Messenger().post({ message: '开始时间不能小于结束时间！' + new Date().toLocaleString(), type: 'error', showCloseButton: true });
}
//生成 Datatables 序号
function GenerateSerialNumber() {
    var api = this.api();
    //获取到本页开始的条数
    var startIndex = api.context[0]._iDisplayStart;
    api.column(0).nodes().each(function (cell, i) {
        cell.innerHTML = startIndex + i + 1;
    });
}
//无刷新提交表单
function submitForm() {
    var $this = $(this);
    var $submit = $this.find('input[type=submit]');
    var $reset = $this.find('input[type=reset]');
    $submit.attr('disabled', 'disabled');
    $.post($this.attr('action'), $this.serializeJson())
        .success(function (data) {
            if (data.StatusID == 1) {
                successMsg();
            }
        })
        .error(errorMsg)
        .complete(function () {
            $submit.removeAttr('disabled');
            //$reset.trigger('click');
        }
        )
    return false;
}
//激活标签页
function activePane(pane) {
    $('a[href="#' + pane + '"]').tab('show');
};
//过去TD内容格式
function getDtContent(innerData, type, rowData, meta) {
    return '<div class="text-overflow" style="min-width:10em;max-width:20em;" title="' + innerData + '">' + innerData + '</div>';
}
//缓存用户和部门数据
var Cache=new Object();
//初始化树
function initTree(treeData, treeUrl, treeId, setting) {
    console.log(new Date().toLocaleString() + ' 加载数据开始！');
    if (Cache[treeData] == undefined) {
        console.log(new Date().toLocaleString() + ' 本地未缓存数据，请求远程数据！');
        $.post(treeUrl, 'json')
        .success(function (data) {
            if (data != null && data != "") {
                Cache[treeData] = data;
                console.log(new Date().toLocaleString() + ' 加载数据成功！');
                $.fn.zTree.init($(treeId), setting, Cache[treeData]);
                console.log(new Date().toLocaleString() + ' 绑定数据成功！');
            }
        })
        .error(errorMsg);
    } else {
        console.log(new Date().toLocaleString() + ' 加载数据成功！');
        $.fn.zTree.init($(treeId), setting, Cache[treeData]);
        console.log(new Date().toLocaleString() + ' 绑定数据成功！');
    }
}
//初始化用户树
function initUserTree(controllers) {
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $(controllers.UserTreeBox).hide();
    });
    console.log(new Date().toLocaleString() + ' 初始化用户树控件开始！');
    //树配置
    var settingUser = {
        check: {
            enable: true
        },
        //回调函数
        callback: {
            onCheck: function zTreeOnCheck(event, treeId, treeNode) {
                var userIds = ',', userNames = ',', userPhones = ',';
                var treeObj = $.fn.zTree.getZTreeObj(controllers.UserTree.substring(1));
                var nodes = treeObj.getCheckedNodes(true);
                $.each(nodes, function (index, value) {
                    if (!value.isParent) {
                        userIds += value.id + ',';
                        userNames += value.name + ',';
                        userPhones += value.phone + ',';
                    }
                });
                if (userIds == ',') {
                    $(controllers.UserIDs).val('');
                    $(controllers.UserNames).val('');
                    $(controllers.UserPhones).val('');
                }
                else {
                    $(controllers.UserIDs).val(userIds);
                    $(controllers.UserNames).val(userNames);
                    $(controllers.UserPhones).val(userPhones);
                }
            },
        },
    };
    //加载数据，初始化树
    initTree('UserTreeData', '/Common/GetUnitUserTree', controllers.UserTree, settingUser);
    console.log(new Date().toLocaleString() + ' 绑定用户添加按钮事件！');
    //绑定选择按钮事件
    $(controllers.UserNames + ',' + controllers.UserAdd).on('click', function () {
        //$(controllers.UserNames).on('click', function () {
        console.log(new Date().toLocaleString() + ' 触发用户选择事件！');
        var $this = $(controllers.UserNames);
        var $thisOffset = $this.offset();
        console.log(new Date().toLocaleString() + ' 获取控件位置及宽度！' + JSON.stringify({
            top: $thisOffset.top + $this.outerHeight(),
            left: $thisOffset.left,
            width: $this.outerWidth(),
        }));
        $(controllers.UserTreeBox).css({
            top: $thisOffset.top + $this.outerHeight(),
            left: $thisOffset.left,
            width: $this.outerWidth(),
        }).toggle('fast');
    });
    console.log(new Date().toLocaleString() + ' 绑定清除按钮事件！');
    //绑定清空按钮事件
    $(controllers.UserEmptied).on('click', function () {
        console.log(new Date().toLocaleString() + ' 触发用户清空按钮！');
        $(controllers.UserIDs).val('');
        $(controllers.UserNames).val('');
        $(controllers.UserPhones).val('');
    });
    console.log(new Date().toLocaleString() + '初始化用户树控件成功，参数如下：');
    console.log(controllers);
}
//初始化用户树模态框
function InitUserTreeModal(controllers) {
    console.log(new Date().toLocaleString() + ' 初始化用户树控件开始！');
    controllers.UserTreeModal = "#UserTreeModal" + Math.ceil(Math.random() * 100000000);
    controllers.UserTree = "#UserTree" + Math.ceil(Math.random() * 100000000);
    $('body').append(
        '<div id="' + controllers.UserTreeModal.substring(1) + '" class="modal fade TreeModal" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">\
            <div class="modal-dialog" role="document">\
                <div class="modal-content">\
                    <div class="modal-header">人员选择</div>\
                    <div class="modal-body">\
                        <ul id="' + controllers.UserTree.substring(1) + '" class="pull-left ztree"></ul>\
                    </div>\
                    <div class="modal-footer">\
                        <button type="button" class="btn btn-primary" data-dismiss="modal">确定</button>\
                        <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>\
                        <button type="button" class="btn btn-danger" data-dismiss="modal">清空</button>\
                    </div>\
                </div>\
            </div>\
        </div>');
    $(controllers.UserNames).on('click', function () {
        $(controllers.UserTreeModal).modal();
    });
    var settingUser = {
        check: {
            enable: true
        },
    };
    //加载数据，初始化树
    initTree('UserTreeData', '/Common/GetUnitUserTree', controllers.UserTree, settingUser);
    $(controllers.UserTreeModal + " .modal-footer .btn-primary").on('click', function () {
        console.log(new Date().toLocaleString() + ' 触发用户确定按钮！');
        var userIds = ',', userNames = '', userPhones = ',';
        var treeObj = $.fn.zTree.getZTreeObj(controllers.UserTree.substring(1));
        var nodes = treeObj.getCheckedNodes(true);
        $.each(nodes, function (index, value) {
            if (!value.isParent) {
                userIds += value.id + ',';
                userNames += value.name + ' ';
                userPhones += value.phone + ',';
            }
        });
        if (userIds == ',') {
            $(controllers.UserIDs).val('');
            $(controllers.UserNames).val('');
            $(controllers.UserPhones).val('');
        }
        else {
            $(controllers.UserIDs).val(userIds);
            $(controllers.UserNames).val(userNames);
            $(controllers.UserPhones).val(userPhones);
        }
    });
    $(controllers.UserTreeModal + " .modal-footer .btn-danger").on('click', function () {
        console.log(new Date().toLocaleString() + ' 触发用户清空按钮！');
        var treeObj = $.fn.zTree.getZTreeObj(controllers.UserTree.substring(1));
        treeObj.checkAllNodes(false);
        $(controllers.UserIDs).val('');
        $(controllers.UserNames).val('');
        $(controllers.UserPhones).val('');
    });
    console.log(new Date().toLocaleString() + ' 初始化用户树控件成功，参数如下：');
    console.log(controllers);
}
//初始化部门树
function initUnitTree(controllers) {
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $(controllers.UserTreeBox).hide('fast');
    });
    console.log(new Date().toLocaleString() + ' 初始化部门树控件开始！');
    var settingUnit = {
        check: {
            enable: true
        },
        callback: {
            onCheck: function zTreeOnCheck(event, treeId, treeNode) {
                var unitIds = ',', unitNames = ',';
                var treeObj = $.fn.zTree.getZTreeObj(controllers.UnitTree.substring(1));
                var nodes = treeObj.getCheckedNodes(true);
                $.each(nodes, function (index, value) {
                    if (value.isParent) {
                        unitIds += value.id + ',';
                        unitNames += value.name + ',';
                    }
                });
                if (unitIds == ',') {
                    $(controllers.UnitIDs).val('');
                    $(controllers.UnitNames).val('');
                } else {
                    $(controllers.UnitIDs).val(unitIds);
                    $(controllers.UnitNames).val(unitNames);
                }
            },
        },
    };
    //加载数据，初始化树
    initTree('UnitTreeData', '/Common/GetUnitTree', controllers.UnitTree, settingUnit);
    console.log(new Date().toLocaleString() + ' 绑定部门添加按钮事件！');
    $(controllers.UnitNames + ',' + controllers.UnitAdd).on('click', function () {
        //$(controllers.UnitNames).on('click', function () {
        console.log(new Date().toLocaleString() + ' 触发部门选择事件！');
        var $this = $(controllers.UnitNames);
        var $thisOffset = $this.offset();
        console.log(new Date().toLocaleString() + ' 获取控件位置及宽度！' + JSON.stringify({
            top: $thisOffset.top + $this.outerHeight(),
            left: $thisOffset.left,
            width: $this.outerWidth(),
        }));
        $(controllers.UnitTreeBox).css({
            top: $thisOffset.top + $this.outerHeight(),
            left: $thisOffset.left,
            width: $this.outerWidth(),
        }).toggle('fast');
    });
    console.log(new Date().toLocaleString() + ' 绑定清除按钮事件！');
    $(controllers.UnitEmptied).on('click', function () {
        $(controllers.UnitIDs).val('');
        $(controllers.UnitNames).val('');
    });
    console.log(new Date().toLocaleString() + '初始化部门树控件成功，参数如下：');
    console.log(controllers);
}
//初始化部门树模态框
function InitUnitTreeModal(controllers) {
    console.log(new Date().toLocaleString() + ' 初始化部门树控件开始！');
    controllers.UnitTreeModal = "#UnitTreeModal" + Math.ceil(Math.random() * 100000000);
    controllers.UnitTree = "#UnitTree" + Math.ceil(Math.random() * 100000000);
    $('body').append(
        '<div id="' + controllers.UnitTreeModal.substring(1) + '" class="modal fade TreeModal" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">\
            <div class="modal-dialog" role="document">\
                <div class="modal-content">\
                    <div class="modal-header">部门选择</div>\
                    <div class="modal-body">\
                        <ul id="' + controllers.UnitTree.substring(1) + '" class="pull-left ztree"></ul>\
                    </div>\
                    <div class="modal-footer">\
                        <button type="button" class="btn btn-primary" data-dismiss="modal">确定</button>\
                        <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>\
                        <button type="button" class="btn btn-danger" data-dismiss="modal">清空</button>\
                    </div>\
                </div>\
            </div>\
        </div>');
    $(controllers.UnitNames).on('click', function () {
        $(controllers.UnitTreeModal).modal();
    });
    var settingUnit = {
        check: {
            enable: true
        },
    };
    //加载数据，初始化树
    initTree('UnitTreeData', '/Common/GetUnitTree', controllers.UnitTree, settingUnit);
    $(controllers.UnitTreeModal + " .modal-footer .btn-primary").on('click', function () {
        console.log(new Date().toLocaleString() + ' 触发部门确定按钮！');
        var UnitIds = ',', UnitNames = '';
        var treeObj = $.fn.zTree.getZTreeObj(controllers.UnitTree.substring(1));
        var nodes = treeObj.getCheckedNodes(true);
        $.each(nodes, function (index, value) {
            if (value.isParent) {
                UnitIds += value.id + ',';
                UnitNames += value.name + ' ';
            }
        });
        if (UnitIds == ',') {
            $(controllers.UnitIDs).val('');
            $(controllers.UnitNames).val('');
        }
        else {
            $(controllers.UnitIDs).val(UnitIds);
            $(controllers.UnitNames).val(UnitNames);
        }
    });
    $(controllers.UnitTreeModal + " .modal-footer .btn-danger").on('click', function () {
        console.log(new Date().toLocaleString() + ' 触发部门清空按钮！');
        var treeObj = $.fn.zTree.getZTreeObj(controllers.UnitTree.substring(1));
        treeObj.checkAllNodes(false);
        $(controllers.UnitIDs).val('');
        $(controllers.UnitNames).val('');
    });
    console.log(new Date().toLocaleString() + ' 初始化部门树控件成功，参数如下：');
    console.log(controllers);
}

$(document).ready(function () {
    //初始化Messenger消息提示插件
    Messenger.options = {
        extraClasses: 'messenger-fixed messenger-on-bottom messenger-on-right',
        theme: 'ice'
    }
});