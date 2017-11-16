Ext.define('TianZun.controller.AllEvent', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.allEvent',


    requires: [
       'TianZun.view.eventaudit.AllEventQuery',//查询页面
       'TianZun.view.eventaudit.AuditEvent',//审核页面
       'TianZun.view.eventaudit.DetailEvent'//详情页面
    ],

    onRender: function (obj, eOpts) {
        var isLook = false;
        var isInput = false;
        var isDelete = false;
        //权限控制
        $.each(configs.Permissions, function (key, item) {
            if (item.Code == "EVENT_ALL_LOOK")
                isLook = true;
            if (item.Code == "EVENT_ALL_INPUT")
                isInput = true;
            if (item.Code == "EVENT_ALL_DELETE")
                isDelete = true;
        })
        if (!isLook) {
            this.view.down('[action=look]').hide();
        }
        if (!isInput) {
            this.view.down('[action=input]').hide();
        }
        if (!isDelete) {
            this.view.down('[action=delete]').hide();
        }
    },

    onQuery: function (obj, e) {
        var win = this.getView().child("allEventQuery");

        if (!win) {
            win = Ext.create('widget.allEventQuery');
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
        var ispush = form.getForm().findField("ispush").getValue();
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
        if ($.trim(ispush) != null && $.trim(ispush) != "") {
            filter.push({ property: "ispush", value: ispush });
        }
        var gridStore = this.getView().child('gridpanel').getStore();
        gridStore.clearFilter(true);
        gridStore.filter(filter);
        win.hide();
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
    onDelete: function (obj, e) {
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();
        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }
        var record = sm.getSelection()[0];
        Ext.Msg.confirm("提示", "您确定要作废吗？", function (btn) {
            if (btn == "yes") {
                grid.mask("正在处理中，请稍等");    //马赛克
                PostAjax({
                    url: 'api/Event/DeleteEventCompany',
                    data: record.data,
                    complete: function (jqXHR, textStatus, errorThrown) {
                        grid.unmask();    //马赛克
                        if (textStatus == "success") {
                            //grid.getSelectionModel().clearSelections();
                            store.reload();
                            Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                        } else {
                            store.reload();
                            Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
                        }
                    }
                });
            }
        })
    },
    onInput: function (obj, e) {
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();
        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }
        var record = sm.getSelection()[0];
        Ext.Msg.confirm("提示", "您确定要录入吗？", function (btn) {
            if (btn == "yes") {
                grid.mask("正在处理中，请稍等");    //马赛克
                PostAjax({
                    url: 'api/Event/InputEventCompany',
                    data: record.data,
                    complete: function (jqXHR, textStatus, errorThrown) {
                        grid.unmask();    //马赛克
                        if (textStatus == "success") {
                            //grid.getSelectionModel().clearSelections();
                            store.reload();
                            Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                        } else {
                            store.reload();
                            Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
                        }
                    }
                });
            }
        })
    },


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