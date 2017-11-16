Ext.define('TianZun.view.mh.CategorsQuery', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.categorsQuery',

    title: '栏目查询',
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
                {
                    fieldLabel: '栏目大类',
                    xtype: 'combo',
                    name: 'BigID',
                    emptyText: '请选择',
                    store: Ext.create('TianZun.store.NewsBigClassStore'),
                    valueField: 'ID',
                    displayField: 'Name',
                    allowBlank: true,
                    editable: false,
                    listeners: {
                        'change': function () {
                            var cyCombo = Ext.getCmp('ACSmallTypeID');
                            cyCombo.clearValue();
                            cyStore = Ext.create('TianZun.store.NewsSmallClassStore');
                            cyStore.getProxy().url = 'api/Categors/GetSmallCategors?typeID=' + this.getValue();
                            cyCombo.bindStore(cyStore, false);
                            cyStore.load();
                        }
                    }
                },
                    {
                        id: 'ACSmallTypeID',
                        xtype: 'combo',
                        fieldLabel: '栏目小类',
                        emptyText: '请选择',
                        name: 'ID',
                        editable: false,
                        valueField: 'ID',
                        displayField: 'Name',
                        allowBlank: true
                    },
                   {
                       fieldLabel: '状态',
                       emptyText: '请选择',
                       xtype: 'combo',
                       name: 'isonline',
                       store: Ext.create('Ext.data.Store', {
                           data: [
                               { ID: 1, Name: '上线' },
                               { ID: 2, Name: '下线' }
                           ]
                       }),
                       valueField: 'ID',
                       displayField: 'Name',
                       editable: false,
                       allowBlank: false
                   },
            ],
            buttons: [{
                text: '查询',
                handler: 'onQueryOK'
            }, {
                text: '重置',
                handler: 'onClear'
            }]
        }];

        this.callParent();
    }
});