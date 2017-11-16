Ext.define('TianZun.view.shop.ShopQuery', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.shopQuery',

    title: '店家筛选',
    layout: 'fit',

    initComponent: function () {
        this.items = [{
            xtype: 'form',
            border: false,
            bodyPadding: 10,
            layout: {
                type: 'table',
                columns: 1,
            },
            fieldDefaults: {
                labelAlign: 'right',
                labelWidth: 75
            },
            defaults: {
                xtype: 'textfield',
                width: 255
            },
            items: [
                //{
                //    fieldLabel: '编号',
                //    name: 'idtype'
                //},
                {
                    fieldLabel: '名称',
                    name: 'storename'
                },
                {
                    fieldLabel: '类型',
                    xtype: 'combo',
                    name: 'typeid',
                    store: Ext.create('TianZun.store.StoreclassesStore'),
                    valueField: 'type_id',
                    displayField: 'type_name',
                    editable: false,
                    allowBlank: false,
                    listeners: {
                        render: function (combo) {
                            combo.getStore().on("load", function (store) {
                                combo.setValue(combo.value)
                            });
                            this.getStore().load();
                        }
                    }
                }
            ],
            buttons: [{
                text: '确定',
                handler: 'onQueryOK'
            }, {
                text: '重置',
                handler: 'onClear'
            }]
        }];

        this.callParent();
    }
});