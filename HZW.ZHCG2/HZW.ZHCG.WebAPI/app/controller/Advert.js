Ext.define('TianZun.controller.Advert', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.advert',

    requires: [
        'TianZun.view.outad.EditAd',
        'TianZun.view.outad.QueryAd',
        'TianZun.view.outad.LookAdvert'
    ],

    onRender: function () {
        var isEdit = false;
        var isDelete = false;

        $.each(configs.Permissions, function (key, item) {
            if (item.Code == "OUTAD_EDIT")
                isEdit = true;
            if (item.Code == "OUTAD_DELETE")
                isDelete = true;
        })
        if (!isEdit) {
            this.getView().down('[action=edit]').hide();
        }
        if (!isDelete) {
            this.getView().down('[action=delete]').hide();
        }
    },

    onGridItemDbClick: function (grid, record) {
        var me = this;

        var grid = this.getView().child('gridpanel');

        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }

        var record = sm.getSelection()[0];
        var win = Ext.create('widget.lookAd', { record: record });
        me.getView().add(win);

        win.show();
    },

    onQuery: function (button, e) {

        var win = this.getView().child("queryAd");

        if (!win) {
            win = Ext.create('widget.queryAd');
            this.getView().add(win);
        }

        win.show();
    },

    onQueryOK: function (button, e) {
        var win = button.up('window');
        var form = win.down('form');

        //var idType = form.getForm().findField("IDType").getValue();
        var adName = form.getForm().findField("AdName").getValue();
        var typeID = form.getForm().findField("TypeID").getValue();
        var state = form.getForm().findField("State").getValue();

        var filter = [];

        //if ($.trim(idType) != null && $.trim(idType) != "") {
        //    filter.push({ property: "IDType", value: $.trim(idType) });
        //}

        if ($.trim(adName) != null && $.trim(adName) != "") {
            filter.push({ property: "AdName", value: $.trim(adName) });
        }

        if (typeof typeID == "number") {
            filter.push({ property: "TypeID", value: typeID });
        }

        if ($.trim(state) != null && $.trim(state) != "") {
            filter.push({ property: "State", value: state });
        }

        var gridStore = this.getView().child('gridpanel').getStore();
        gridStore.clearFilter(true);
        gridStore.filter(filter);
        win.hide();
    },

    onAddOK: function (button, e) {
        var win = button.up('window');
        var form = win.down('form');

        var formData = form.getValues();

        if (!form.isValid()) {
            return;
        }

        form.submit({
            url: 'api/Advert/AddAdvert',
            method: "POST",
            waitTitle: "正在提交",
            waitMsg: "正在提交，请稍候...",
            success: function (form, action) {
                var content = Ext.create("TianZun.view.outad.AdList");
                var view = Ext.getCmp("IndexLeft").up();
                var panel = view.items.getAt(3)
                var gridArr = Ext.getCmp("IndexLeft").query('gridcolumn')[1].up('treepanel').getEl().query('.x-grid-item');
                gridArr[0].className = "x-grid-item x-grid-item-selected";
                gridArr[1].className = "x-grid-item";
                view.remove(panel)
                content.region = 'center';
                view.add(content);
                win.close();
                Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 2000));
            },
            failure: function (form, action) {
                Ext.MessageBox.show({ title: "提示", msg: "操作失败！" }, setTimeout(function () { Ext.Msg.hide(); }, 2000));
            }
        });
    },

    onEdit: function (button, e) {
        var me = this;
        var grid = this.getView().child('gridpanel');
        
        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }

        var record = sm.getSelection()[0];
        var win = Ext.create('widget.editAd', { record: record });
        me.getView().add(win);

        win.show();
    },

    onEditOK: function (button, e) {
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();

        var win = button.up('window');
        var form = win.down('form');

        if (!form.isValid()) {
            return;
        }

        var formData = form.getValues();

        form.submit({
            url: 'api/Advert/EditAdvert',
            method: "POST",
            waitTitle: "正在提交",
            waitMsg: "正在提交，请稍候...",
            success: function (form, action) {
                grid.getSelectionModel().clearSelections();
                store.reload();
                win.close();
                Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 2000));
            },
            failure: function (form, action) {
                Ext.MessageBox.show({ title: "提示", msg: "操作失败！" }, setTimeout(function () { Ext.Msg.hide(); }, 2000));
            }
        });
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
        Ext.Msg.confirm("提示", "您确定要执行删除操作吗？", function (btn) {
            if (btn == "yes") {
                grid.mask("正在处理中,请稍候.....");
                PostAjax({
                    url: 'api/Advert/DeleteAdvert?id=' + record.get('ID'),
                    complete: function (jqXHR, textStatus, errorThrown) {
                        grid.unmask();
                        if (textStatus == "success") {                            
                            grid.getSelectionModel().clearSelections();
                            store.reload();
                            Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 2000));
                        } else {
                            store.reload();
                            Ext.MessageBox.show({ title: "提示", msg: "操作失败！" }, setTimeout(function () { Ext.Msg.hide(); }, 2000));
                        }
                    }
                });
            }
        });
    },

    onClose: function (obj) {
        var win = obj.up('window');
        win.close();
    },

    onHide: function (button) {
        button.up('form').reset();
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();
        var filter = [{

        }];
        store.clearFilter(true);
        store.filter(filter);
        store.load();
        var win = button.up('window');        
        win.hide();
    }
});