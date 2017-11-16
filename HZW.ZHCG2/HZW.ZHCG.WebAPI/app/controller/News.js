/// <reference path="../../Scripts/extjs/ext-all-debug.js" />

Ext.define('TianZun.controller.News', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.news',
    requires: [
       'TianZun.view.mh.NewsEdit',
       'TianZun.view.mh.NewsDeail',
       'TianZun.view.mh.NewsQuery',
    ],

    onRender: function () {
        var isEdit = false;
        var isDelete = false;
        var isHandle = false;
        var isUp = false;
        $.each(configs.Permissions, function (key, item) {
            if (item.Code == "MH_EDIT")
                isEdit = true;
            if (item.Code == "MH_DELETE")
                isDelete = true;
            //if (item.Code == "MH_LM")
            //    isHandle = true;
            if (item.Code == "MH_HANDLE")
                isUp = true;
        })
        if (!isEdit) {
            this.getView().down('[action=edit]').hide();
        }
        if (!isDelete) {
            this.getView().down('[action=delete]').hide();
        }
        //if (!isHandle) {
        //    this.getView().down('[action=handle]').hide();
        //}
        if (!isUp) {
            this.getView().down('[action=uphandle]').hide();
        }
    },

    onGridItemDbClick: function (grid, record) {
        var grid = this.getView().child('gridpanel');
        var sm = grid.getSelectionModel();

        var record = sm.getSelection()[0];

        var win = Ext.create('widget.newsDeail', { record: record });
        this.getView().add(win);
        win.show();
    },

    onQuery: function (button, e) {

        var win = this.getView().child("newsQuery");

        if (!win) {
            win = Ext.create('widget.newsQuery');
            this.getView().add(win);
        }

        win.show();
    },

    onQueryOK: function (button, e) {
        var win = button.up('window');
        var form = win.down('form');

        var title = form.getForm().findField("title").getValue();
        var categoryid_bid = form.getForm().findField("categoryid_bid").getValue();
        var categoryID = form.getForm().findField("categoryID").getValue();
        var isonline = form.getForm().findField("isonline").getValue();

        var filter = [];

        if ($.trim(title) != null && $.trim(title) != "") {
            filter.push({ property: "title", value: $.trim(title) });
        }

        if (typeof categoryid_bid == "number") {
            filter.push({ property: "categoryid_bid", value: $.trim(categoryid_bid) });
        }

        if (typeof categoryID == "number") {
            filter.push({ property: "categoryID", value: categoryID });
        }

        if (typeof isonline == "number") {
            filter.push({ property: "isonline", value: isonline });
        }
      

        var gridStore = this.getView().child('gridpanel').getStore();
        gridStore.clearFilter(true);
        gridStore.filter(filter);

        win.hide();
    },

    onAddOK: function (button, e) {
        var win = button.up('window');
        var form = win.down('form');
        form.getValues().hidcontent = Nceditor.html();
        Ext.getCmp("hidcontent").setValue(Nceditor.html());

        if (!form.isValid()) {
            return;
        }

        form.submit({
            url: "api/News/AddNew",
            method: "POST",
            waitTitle: "正在提交",
            waitMsg: "正在提交，请稍候...",
            success: function (form, action) {
                Ext.Array.each(Ext.getCmp("IndexLeft").query('gridcolumn'), function (value, key) {
                    if (value.up('panel').up('panel').title == '门户网站') {
                        var content = Ext.create("TianZun.view.mh.NewsList");
                        var view = Ext.getCmp("IndexLeft").up();
                        var panel = view.items.getAt(3)
                        var gridArr = value.up('treepanel').getEl().query('.x-grid-item');
                        gridArr[0].className = "x-grid-item x-grid-item-selected";
                        gridArr[1].className = "x-grid-item";
                        view.remove(panel)
                        content.region = 'center';
                        view.add(content);
                        //win.unmask();
                        Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 2000));
                        win.close();
                    }
                })



               
            },
            failure: function (form, action) {
                // win.unmask();
                Ext.Msg.show(
                {
                    title: "错误提示",
                    icon: Ext.Msg.ERROR,
                    msg: "非常抱歉！" + "保存数据时发生错误，请您与管理员联系！<br/>错误信息：" + action.response.status
                });
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
        var win = Ext.create('widget.newsEdit', { record: record });
        me.getView().add(win);

        win.show();
    },

    onEditOK: function (button, e) {
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();

        var win = button.up('window');
        var form = win.down('form'); 

        form.getValues().hidcontent = Nceditor.html();
        Ext.getCmp("hidcontent").setValue(Nceditor.html());
       

        if (!form.isValid()) {
            return;
        }

        form.submit({
            url: "api/News/EditNew",
            method: "POST",
            waitTitle: "正在提交",
            waitMsg: "正在提交，请稍候...",
            success: function (form, action) {
                //win.unmask();
                Ext.MessageBox.show({ title: "提示", msg: "操作成功！" }, setTimeout(function () { Ext.Msg.hide(); }, 2000));
                store.load();
                win.close();
            },
            failure: function (form, action) {
                // win.unmask();
                Ext.Msg.show(
                {
                    title: "错误提示",
                    icon: Ext.Msg.ERROR,
                    msg: "非常抱歉！" + "保存数据时发生错误，请您与管理员联系！<br/>错误信息：" + action.response.status
                });
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
                    url: 'api/News/DeleteNew?articleID=' + record.get('articleID'),
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

    onHandle: function (view) {
        var grid = this.getView().child('gridpanel');
        var store = grid.getStore();

        var sm = grid.getSelectionModel();
        if (sm.getSelection().length == 0) {
            Ext.Msg.alert("提示", "请选择一条记录");
            return;
        }

        var record = sm.getSelection()[0];

        Ext.Msg.confirm("提示", "您确定要执行发布/取消发布操作吗？", function (btn) {
            if (btn == "yes") {
                grid.mask("正在处理中,请稍等");
                // Mask();    //马赛克

                PostAjax({
                    url: 'api/News/EditNewsOnLine?articleID=' + record.get('articleID'),
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