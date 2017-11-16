Ext.define('TianZun.view.mh.NewsQuery', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.newsQuery',

    title: '新闻查询',
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
                    fieldLabel: '文章标题',
                    name: 'title',
                    xtype: 'textfield',
                    valueField: 'ID',
                },

                {
                    fieldLabel: '栏目大类',
                    xtype: 'combo',
                    name: 'categoryid_bid',
                    emptyText: '请选择',
                    store: Ext.create('TianZun.store.NewsBigClassStore'),
                    valueField: 'ID',
                    displayField: 'Name',
                    editable: false,
                    listeners: {
                        'change': function () {
                            var cyCombo = Ext.getCmp('ACSmallTypeIDQ');
                            cyCombo.clearValue();
                            cyStore = Ext.create('TianZun.store.NewsSmallClassStore');
                            cyStore.getProxy().url = 'api/Categors/GetSmallCategors?typeID=' + this.getValue();
                            cyCombo.bindStore(cyStore, false);
                            cyStore.load();
                        }
                    }
                },
                    {
                        id: 'ACSmallTypeIDQ',
                        xtype: 'combo',
                        fieldLabel: '栏目小类',
                        emptyText: '请选择',
                        name: 'categoryID',
                        valueField: 'ID',
                        editable: false,
                        displayField: 'Name',
                    },
                   {
                       fieldLabel: '状态',
                       emptyText: '请选择',
                       xtype: 'combo',
                       name: 'isonline',
                       editable: false,
                       store: Ext.create('Ext.data.Store', {
                           data: [
                               { ID: 1, Name: '上线' },
                               { ID: 2, Name: '下线' }
                           ]
                       }),
                       valueField: 'ID',
                       displayField: 'Name',
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