Ext.define('TianZun.controller.Shop', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.shop',
    model:'Shop',
    requires: [
        'TianZun.view.shop.ShopEdit',
        'TianZun.view.shop.ShopQuery',
        'TianZun.view.shop.ShopSee'
    ],

    onRender: function () {
        var isEdit = false;
        var isDelete = false;

        $.each(configs.Permissions, function (key, item) {
            if (item.Code == "STORE_EDIT")
                isEdit = true;
            if (item.Code == "STORE_DELETE")
                isDelete = true;
        })
        if (!isEdit) {
            this.getView().down('[action=edit]').hide();
        }
        if (!isDelete) {
            this.getView().down('[action=delete]').hide();
        }
    },

    onQuery: function (obj, e) {
        var win = this.getView().child("shopQuery");

        if (!win) {
            win = Ext.create('widget.shopQuery');
            this.getView().add(win);
        }

        win.show();
    },

    onQueryOK: function (obj, e) {
        var win = obj.up('window');
        var form = win.down('form');

        //var idtype = form.getForm().findField("idtype").getValue();
        var storename = form.getForm().findField("storename").getValue();
        var typeid = form.getForm().findField("typeid").getValue();

        var filter = [];

        //if ($.trim(idtype) != null && $.trim(idtype) != "") {
        //    filter.push({ property: "idtype", value: $.trim(idtype) });
        //}

        if ($.trim(storename) != null && $.trim(storename) != "") {
            filter.push({ property: "storename", value: $.trim(storename) });
        }

        if (typeof typeid == "number") {
            filter.push({ property: "typeid", value: typeid });
        }

        var store = this.getView().child('gridpanel').getStore();
        store.clearFilter(true);
        store.filter(filter);

        win.hide();
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

    onAdd: function (obj, e) {
        var grid = this.getView();

        var win = Ext.create('widget.roleAdd');
        this.getView().add(win);

        win.show();
    },

    onAddOK: function (button, e) {

        var win = button.up('window');
        var form = win.down('form');

        if (!form.isValid()) {
            return;
        }

        var formData = form.getValues();
        win.mask();
        form.mask();
        PostAjax({
            url: 'api/StoreBases/AddStore',
            data: formData,
            complete: function (jqXHR, textStatus, errorThrown) {
                form.unmask();
                if (textStatus == "success") {
                    var content = Ext.create("TianZun.view.shop.ShopList");
                    var view = Ext.getCmp("IndexLeft").up();
                    var panel = view.items.getAt(3)
                    var gridArr = Ext.getCmp("IndexLeft").query('gridcolumn')[0].up('treepanel').getEl().query('.x-grid-item');
                    gridArr[0].className = "x-grid-item x-grid-item-selected";
                    gridArr[1].className = "x-grid-item";
                    view.remove(panel)
                    content.region = 'center';
                    view.add(content);

                    win.close();
                    Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                } else {
                    Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
                }
            }
        });
    },

    onGridItemDbClick: function (grid, record) {
        var grid = this.getView().child('gridpanel');
        var sm = grid.getSelectionModel();

        var record = sm.getSelection()[0];

        var win = Ext.create('widget.shopSee', { record: record });
        this.getView().add(win);
        win.show();
    },

    onEdit: function (obj, e) {
        var me = this;
        var grid = this.getView().child('gridpanel');

        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }

        var record = sm.getSelection()[0];
        var win = Ext.create('widget.shopEdit', { record: record });
        me.getView().add(win);

        win.show();

    },

    onEditOK: function (obj, e) {

        //alert('操你妈!');
        var grid = this.getView().child('gridpanel');;

        var store = grid.getStore();

        var win = obj.up('window');
        var form = win.down('form');

        //if (!form.isvalid()) {
        //    return;
        //}
        var formData = form.getValues();
        console.log(formData);
        PostAjax({
            url: 'api/StoreBases/EditStore',
            data: formData,
            complete: function (jqXHR, textStatus) {
                UnMask();

                if (textStatus == "success") {
                    var result = jqXHR.responseText;

                    grid.getSelectionModel().clearSelections();
                    store.reload();
                    win.close();
                    Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                } else
                    Ext.Msg.alert("提示", "操作失败！");
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
                grid.mask("正在处理中,请稍等.....");
                PostAjax({
                    url: 'api/StoreBases/Delete?id=' + record.get('storeid'),
                    complete: function (jqXHR, textStatus, errorThrown) {
                        grid.unmask();

                        if (textStatus == "success") {
                            grid.getSelectionModel().clearSelections();
                            store.reload();
                            grid.unmask();
                            Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                        } else {
                            store.reload();
                            Ext.Msg.alert("提示", "操作失败！");
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
        var win = button.up('window');
        win.hide();
    }
});