Ext.define('TianZun.controller.Categors', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.categors',

    requires: [
        'TianZun.view.mh.CategorsAdd',
        'TianZun.view.mh.CategorsEdit',
        'TianZun.view.mh.CategorsDeail',
        'TianZun.view.mh.CategorsQuery',
    ],

    onRender: function () {
        var isAdd = false;
        var isEdit = false;
        var isDelete = false;
        var isHandle = false;
        $.each(configs.Permissions, function (key, item) {
            if (item.Code == "MH_ADD")
                isAdd = true;
            if (item.Code == "MH_EDIT")
                isEdit = true;
            if (item.Code == "MH_DELETE")
                isDelete = true;
            if (item.Code == "MH_LM")
                isHandle = true;
        })
        if (!isAdd) {
            this.getView().down('[action=add]').hide();
        }
        if (!isEdit) {
            this.getView().down('[action=edit]').hide();
        }
        if (!isDelete) {
            this.getView().down('[action=delete]').hide();
        }
        if (!isHandle) {
            this.getView().down('[action=handle]').hide();
        }
    },

    onGridItemDbClick: function (obj, record) {
        var grid = this.getView().child('gridpanel');
        var sm = grid.getSelectionModel();

        var record = sm.getSelection()[0];

        var win = Ext.create('widget.categorsDeail', { record: record });
        this.getView().add(win);
        win.show();
    },

    onQuery: function (button, e) {

        var win = this.getView().child("categorsQuery");

        if (!win) {
            win = Ext.create('widget.categorsQuery');
            this.getView().add(win);
        }

        win.show();
    },

    onQueryOK: function (button, e) {
        var win = button.up('window');
        var form = win.down('form');

        var bigid = form.getForm().findField("BigID").getValue();
        var smallid = form.getForm().findField("ID").getValue();
        var state = form.getForm().findField("isonline").getValue();

        var filter = [];

        if ($.trim(bigid) != null && $.trim(bigid) != "") {
            filter.push({ property: "BigID", value: $.trim(bigid) });
        }

        if ($.trim(smallid) != null && $.trim(smallid) != "") {
            filter.push({ property: "ID", value: $.trim(smallid) });
        }

        if ($.trim(state) != null && $.trim(state) != "") {
            filter.push({ property: "isonline", value: state });
        }
        var gridStore = this.getView().child('gridpanel').getStore();
        gridStore.clearFilter(true);
        gridStore.filter(filter);
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


    onAdd: function (button, e) {
        var grid = this.getView();

        var win = Ext.create('widget.categorsAdd');
        this.getView().add(win);

        win.show();
    },

    onAddOK: function (obj, e) {
        var me = this;

        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();

        var win = obj.up('window');
        var form = win.down('form');

        if (!form.isValid()) {
            return;
        }
        form.mask("正在处理中,请稍等");
        // grid.mask();    //马赛克

        PostAjax({
            url: 'api/Categors/AddCategor',
            data: form.getValues(),
            complete: function (jqXHR, textStatus, errorThrown) {
                grid.unmask();
                if (textStatus == "success") {
                    //grid.getSelectionModel().clearSelections();
                    Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                    store.reload();
                    win.close();
                } else {
                    //store.reload();
                    Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
                }
            }
        });
    },

    onEdit: function (button, e) {
        var grid = this.getView().child('gridpanel');
        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }

        var record = sm.getSelection()[0];

        var win = Ext.create('widget.categorsEdit', { record: record });
        this.getView().add(win);
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

        form.mask("正在处理中,请稍等");
        // Mask();    //马赛克

        PostAjax({
            url: 'api/Categors/EditCategor',
            data: formData,
            async: false,
            complete: function (jqXHR, textStatus) {
                grid.unmask();
                if (textStatus == "success") {
                    //grid.getSelectionModel().clearSelections();
                    Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                    win.close();
                    store.reload();
                } else {
                    //store.reload();
                    Ext.MessageBox.show({ title: "提示", msg: "操作失败！" });
                }
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
        $.ajax({
            url: 'api/News/GetNewsListCount?categoryid=' + record.get('ID') + '&limit=10',
            type: "get",
            dataType: "json",
            success: function (result) {
                if (result == 0) {
                    Ext.Msg.confirm("提示", "您确定要执行删除操作吗？", function (btn) {
                        if (btn == "yes") {
                            grid.mask("正在处理中,请稍等");
                            // Mask();    //马赛克
                            PostAjax({
                                url: 'api/Categors/DeleteCategor?id=' + record.get('ID'),
                                complete: function (jqXHR, textStatus, errorThrown) {
                                    grid.unmask();

                                    if (textStatus == "success") {
                                        grid.getSelectionModel().clearSelections();
                                        store.reload();
                                        Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 1500));
                                    } else {
                                        store.reload();
                                        Ext.Msg.alert("提示", "操作失败！");
                                    }
                                }
                            });
                        }
                    });
                } else {
                    store.reload();
                    Ext.Msg.alert("提示", "该栏目下有数据,无法删除！");
                }
            }, error: function (xhr, textStatus) {
                store.reload();
                Ext.Msg.alert("提示", "操作失败！");
            }
        });
    },

    onHandle: function (view) {
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();

        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }

        var record = sm.getSelection()[0];

        Ext.Msg.confirm("提示", "您确定要执行上线/下线操作吗？", function (btn) {
            if (btn == "yes") {
                grid.mask("正在处理中,请稍等");
                // Mask();    //马赛克

                GetAjax({
                    url: 'api/Categors/EditCategorOnLine?id=' + record.get('ID'),
                    complete: function (jqXHR, textStatus, errorThrown) {
                        grid.unmask();
                        if (textStatus == "success") {
                            grid.getSelectionModel().clearSelections();
                            store.reload();
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