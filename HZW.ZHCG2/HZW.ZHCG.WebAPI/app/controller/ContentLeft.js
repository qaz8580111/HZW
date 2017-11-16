Ext.define('TianZun.controller.ContentLeft', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.contentLeft',

    onShow: function (obj, eOpts) {
        GetPermissions(function () {
            GetAjax({
                url: 'api/Menu/GetTreeMenus?userID=' + $.cookie("USER_ID"),
                complete: function (jqXHR, textStatus, errorThrown) {
                    if (textStatus == "success") {
                        var treeMenus = jQuery.parseJSON(jqXHR.responseText);

                        for (var i = 0; i < treeMenus.length; i++) {
                            var mainMenu = treeMenus[i];

                            var treePanel = Ext.create('Ext.tree.Panel', {
                                border: false,
                                rootVisible: false,
                                root: {
                                    children: mainMenu.children
                                },
                                listeners: {
                                    itemclick: function (itemObj, record, item, index) {
                                        var gridArr = Ext.getCmp("IndexLeft").getEl().query('.x-grid-item');
                                        $.each(gridArr, function (a, b) {
                                            b.className = "x-grid-item";
                                        })
                                        item.className = "x-grid-item x-grid-item-selected";

                                        var url = record.get("Url");

                                        if (url != null && url != "") {
                                            var content = Ext.create(url);
                                            var view = itemObj.up().up().up().up();
                                            var panel = view.items.getAt(3)
                                            view.remove(panel)
                                            content.region = 'center';
                                            view.add(content);
                                        }
                                    },                                    
                                },
                            });

                            if (i == 0) {
                                treePanel.on("render", function () {

                                    var rootNode = treePanel.getStore().getRootNode();
                                    var child = rootNode.getChildAt(0);
                                    treePanel.getSelectionModel().select(child);

                                    var content = Ext.create(child.get('Url'));
                                    var view = Ext.ComponentQuery.query('viewport')[0];
                                    var panel = view.items.getAt(3)
                                    view.remove(panel)
                                    content.region = 'center';
                                    view.add(content);
                                });
                            }

                            obj.add({
                                title: mainMenu.text,
                                items: treePanel,
                                //listeners: {
                                //    expand: function (panel, eOpts) {
                                //        var url = panel.query('treeview')[0].getStore().data.items[0].data.Url;
                                //        var gridArr = Ext.getCmp("IndexLeft").query('gridcolumn')[0].up('treepanel').getEl().query('.x-grid-item');
                                //        gridArr[0].className = "x-grid-item x-grid-item-selected";
                                //        if (url != null && url != "") {
                                //            var content = Ext.create(url);
                                //            var view = obj.up();
                                //            var panel = view.items.getAt(3)
                                //            view.remove(panel)
                                //            content.region = 'center';
                                //            view.add(content);
                                //        }
                                //    }
                                //}
                            });
                        }
                    }
                }
            });
        })

    },
});
