Ext.define('TianZun.controller.FinishEvent', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.finishEvent',


    requires: [
       'TianZun.view.eventaudit.EventQuery',//查询页面
       'TianZun.view.eventaudit.AuditEvent',//审核页面
       'TianZun.view.eventaudit.DetailEvent'//详情页面
    ],

    onRender: function (obj, eOpts) {
        var isLook = false;
        var isPush = false;

        //权限控制
        $.each(configs.Permissions, function (key, item) {
            if (item.Code == "EVENT_FINISH_LOOK")
                isLook = true;
            if (item.Code == "EVENT_FINISH_PUSH")
                isPush = true;
        })
        if (!isLook) {
            this.view.down('[action=look]').hide();
        }
        if (!isPush) {
            this.view.down('[action=push]').hide();
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

    onPush: function (obj, e) {
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();
        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }
        var record = sm.getSelection()[0];
        if (record.get('isexamine') == '作废') {
            Ext.Msg.alert("提示", "已作废事件不能推送");
            return
        }
        if (record.get('ispush') == '已推送') {
            Ext.Msg.alert("提示", "已推送的事件不能再推送");
            return
        }
        Ext.Msg.confirm("提示", "您确定要推送吗？", function (btn) {
            if (btn == "yes") {
                grid.mask("正在处理中，请稍等");    //马赛克
                PostAjax({
                    url: 'api/Event/PushEventCompany',
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

    onDelete: function () {
        var grid = this.getView().child('gridpanel');
        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }
        console.log(this);
        var record = sm.getSelection()[0];
        if (record.get('isexamine') == '作废') {
            Ext.Msg.alert("提示", "已作废事件不能再次作废");
            return
        }
        var win = Ext.create('widget.auditEvent', { record: record });
        var me = this;
        if (record.get('ispush') == '已推送') {

            Ext.Msg.confirm("提示", "您确定要作废并撤销推送吗？", function (btn) {

                if (btn == "yes") {
                    Ext.getCmp('inputisinvalis').hide();
                    Ext.getCmp('invalisisinvalis').setValue(1);
                    Ext.getCmp('inputcontent').hide();
                    Ext.getCmp('inputcontent').allowBlank = true;
                    me.getView().add(win);
                    win.show();
                }
            })
        } else {

            Ext.getCmp('inputisinvalis').hide();
            //Ext.getCmp('inputisinvalis').checked = false;
            Ext.getCmp('invalisisinvalis').setValue(1);
            Ext.getCmp('inputcontent').hide();
            Ext.getCmp('inputcontent').allowBlank = true;
            //Ext.getCmp('invalisisinvalis').checked = true;

            this.getView().add(win);
            win.show();  
        }
        
        

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
                form.mask("正在处理中，请稍等");    //马赛克
                PostAjax({
                    url: 'api/Event/DeleteEventCompany',
                    data: formData,
                    complete: function (jqXHR, textStatus, errorThrown) {
                        if (textStatus == "success") {
                            grid.getSelectionModel().clearSelections();
                            store.reload();
                            win.close();
                            Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                        } else {
                            store.reload();
                            Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
                        }
                    }
                });
            }
        });
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