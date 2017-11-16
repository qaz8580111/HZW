/// <reference path="jquery/jquery-2.2.4.js" />
/// <reference path="zTree_v3-master/js/jquery.ztree.all.js" />
var UserTreeData;
var UnitTreeData;
//初始化用户树
function initUserTree(controllers) {
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
    console.log(new Date().toLocaleString() + ' 加载数据开始！');
    //加载数据，初始化树
    if (UserTreeData == undefined) {
        console.log(new Date().toLocaleString() + ' 本地未缓存数据，请求远程数据！');
        $.post('/Common/GetUnitUserTree', 'json')
        .success(function (data) {
            if (data != null && data != "") {
                UserTreeData = data;
                console.log(new Date().toLocaleString() + ' 加载数据成功！');
                $.fn.zTree.init($(controllers.UserTree), settingUser, data);
                console.log(new Date().toLocaleString() + ' 绑定数据成功！');
            }
        })
        .error(errorMsg);
    } else {
        console.log(new Date().toLocaleString() + ' 加载数据成功！');
        $.fn.zTree.init($(controllers.UserTree), settingUser, UserTreeData);
        console.log(new Date().toLocaleString() + ' 绑定数据成功！');
    }
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
//初始化用户树（自带HTML代码）
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
    if (UserTreeData == undefined) {
        console.log(new Date().toLocaleString() + ' 本地未缓存数据，请求远程数据！');
        $.post('/Common/GetUnitUserTree', 'json')
        .success(function (data) {
            if (data != null && data != "") {
                UserTreeData = data;
                console.log(new Date().toLocaleString() + ' 加载数据成功！');
                $.fn.zTree.init($(controllers.UserTree), settingUser, data);
                console.log(new Date().toLocaleString() + ' 绑定数据成功！');
            }
        })
        .error(errorMsg);
    } else {
        console.log(new Date().toLocaleString() + ' 加载数据成功！');
        $.fn.zTree.init($(controllers.UserTree), settingUser, UserTreeData);
        console.log(new Date().toLocaleString() + ' 绑定数据成功！');
    }
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
    console.log(new Date().toLocaleString() + ' 加载数据开始！');
    if (UnitTreeData == undefined) {
        console.log(new Date().toLocaleString() + ' 本地未缓存数据，请求远程数据！');
        $.post('/Common/GetUnitTree', 'json')
        .success(function (data) {
            if (data != null && data != "") {
                UnitTreeData = data;
                console.log(new Date().toLocaleString() + ' 加载数据成功！');
                $.fn.zTree.init($(controllers.UnitTree), settingUnit, data);
                console.log(new Date().toLocaleString() + ' 绑定数据成功！');
            }
        })
        .error(errorMsg);
    } else {
        console.log(new Date().toLocaleString() + ' 加载数据成功！');
        $.fn.zTree.init($(controllers.UnitTree), settingUnit, UnitTreeData);
        console.log(new Date().toLocaleString() + ' 绑定数据成功！');
    }
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
//初始化部门树（自带HTML代码）
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
    console.log(new Date().toLocaleString() + ' 加载数据开始！');
    if (UnitTreeData == undefined) {
        console.log(new Date().toLocaleString() + ' 本地未缓存数据，请求远程数据！');
        $.post('/Common/GetUnitTree', 'json')
        .success(function (data) {
            if (data != null && data != "") {
                UnitTreeData = data;
                console.log(new Date().toLocaleString() + ' 加载数据成功！');
                $.fn.zTree.init($(controllers.UnitTree), settingUnit, data);
                console.log(new Date().toLocaleString() + ' 绑定数据成功！');
            }
        })
        .error(errorMsg);
    } else {
        console.log(new Date().toLocaleString() + ' 加载数据成功！');
        $.fn.zTree.init($(controllers.UnitTree), settingUnit, UnitTreeData);
        console.log(new Date().toLocaleString() + ' 绑定数据成功！');
    }
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