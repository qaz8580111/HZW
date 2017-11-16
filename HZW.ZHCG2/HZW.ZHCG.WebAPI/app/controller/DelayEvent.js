/// <reference path="../../Scripts/extjs/ext-all-debug.js" />

Ext.define('TianZun.controller.DelayEvent', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.delayEvent',


    requires: [
       'TianZun.view.eventaudit.EventQuery',//查询页面
       'TianZun.view.eventaudit.AuditEvent',//审核页面
       'TianZun.view.eventaudit.DetailEvent'//详情页面
    ],

    onRender: function (obj, eOpts) {
        var isLook = false;
        var isAudit = false;
        var isDelete = false;

        //权限控制
        $.each(configs.Permissions, function (key, item) {
            if (item.Code == "EVENT_DELAY_LOOK")
                isLook = true;
            if (item.Code == "EVENT_DELAY_AUDIT")
                isAudit = true;
            if (item.Code == "EVENT_DELAY_DELETE")
                isDelete = true;
        })
        if (!isLook) {
            this.view.down('[action=look]').hide();
        }
        if (!isAudit) {
            this.view.down('[action=audit]').hide();
        }
        if (!isDelete) {
            this.view.down('[action=delete]').hide();
        }

    },


    onQuery: function (obj, e) {
        var win = this.getView().child("eventQuery");

        if (!win) {
            win = Ext.create('widget.eventQuery');
            this.getView().add(win);
        }

        win.show();
    },

    onQueryOK: function (button, e) {
        var win = button.up('window');
        var form = win.down('form');

        var Code = form.getForm().findField("Code").getValue();
        var title = form.getForm().findField("title").getValue();
        var STime = form.getForm().findField("STime").getValue();
        var ETime = form.getForm().findField("ETime").getValue();

        var filter = [];

        if ($.trim(Code) != null && $.trim(Code) != "") {
            filter.push({ property: "Code", value: $.trim(Code) });
        }

        if ($.trim(title) != null && $.trim(title) != "") {
            filter.push({ property: "title", value: $.trim(title) });
        }

        if ($.trim(STime) != null && $.trim(STime) != "") {
            filter.push({ property: "STime", value: STime });
        }

        if ($.trim(ETime) != null && $.trim(ETime) != "") {
            filter.push({ property: "ETime", value: ETime });
        }
        var gridStore = this.getView().child('gridpanel').getStore();
        gridStore.clearFilter(true);
        gridStore.filter(filter);
        win.hide();
    },

    onAudit: function (obj, e) {
        var grid = this.getView().child('gridpanel');
        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }

        var record = sm.getSelection()[0];

        var win = Ext.create('widget.auditEvent', { record: record });

        Ext.getCmp('invaliscontent').hide();
        Ext.getCmp('invaliscontent').allowBlank = true;
        this.getView().add(win);
        win.show();


    },


    onAuditOK: function (button, e) {
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();
        var win = button.up('window');
        var form = win.down('form');
        if (!form.isValid()) {
            return;
        }
        var formData = form.getValues();
        
        Ext.Msg.confirm("提示", "您确定要提交吗？", function (btn) {
            if (btn == "yes") {
                var url;
                if (formData.isinvalis == 0) {
                    url = 'api/Event/EditAuditCompany'
                } else if (formData.isinvalis == 1) {
                    url = 'api/Event/DeleteEventCompany';
                }
                form.mask("正在处理中，请稍等");    //马赛克
                PostAjax({
                    url: url,
                    data: formData,
                    complete: function (jqXHR, textStatus, errorThrown) {
                        if (textStatus == "success") {
                            grid.getSelectionModel().clearSelections();
                            store.reload();
                            win.close();
                            Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                        } else {
                            grid.reload();
                            Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
                        }
                    }
                });
                grid.reload();
                Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
            }
        });
    },

    onLook: function (obj, e) {


        var me = this;

        var grid = this.getView().child('gridpanel');

        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }

        var record = sm.getSelection()[0];
        var win = Ext.create('widget.detailEvent', { record: record });
        me.getView().add(win);

        win.show();
    },

    //onDelete: function (obj, e) {
    //    var grid = this.getView().child('gridpanel');
    //    var store = grid.getStore();
    //    var sm = grid.getSelectionModel();
    //    if (sm.getSelection().length == 0) {
    //        Ext.Msg.alert("提示", "请选择一条记录");
    //        return;
    //    }
    //    var record = sm.getSelection()[0];
    //    Ext.Msg.confirm("提示", "您确定要作废吗？", function (btn) {
    //        if (btn == "yes") {
    //            grid.mask();    //马赛克
    //            PostAjax({
    //                url: 'api/Event/DeleteEventCompany',
    //                data: record.data,
    //                complete: function (jqXHR, textStatus, errorThrown) {
    //                    grid.unmask();    //马赛克
    //                    if (textStatus == "success") {
    //                        //grid.getSelectionModel().clearSelections();
    //                        store.reload();
    //                        Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
    //                    } else {
    //                        store.reload();
    //                        Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
    //                    }
    //                }
    //            });
    //        }
    //    })
    //},



    onClear: function (button, e) {
        button.up('form').reset();
        var me = this;
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();
        var filter = [{

        }];
        store.clearFilter(true);
        store.filter(filter);
        store.load();
        var win = button.up('window');
        win.close();
    },
    onClose: function (obj) {
        var win = obj.up('window');
        win.close();
    },

    onHide: function (button) {
        var win = button.up('window');
        win.hide();
    }
});