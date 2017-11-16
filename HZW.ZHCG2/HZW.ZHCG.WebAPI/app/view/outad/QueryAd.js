Ext.define('TianZun.view.outad.QueryAd', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.queryAd',

    title: '查询广告',
    layout: 'fit',
    initComponent: function () {
        this.items = [{
            xtype: 'form',
            border: false,
            bodyPadding: 10,
            layout: {
                type: 'table',
                columns: 2,
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
                //    name: 'IDType'
                //},
                {
                    fieldLabel: '名称',
                    name: 'AdName'
                },
                {
                    fieldLabel: '类型',
                    xtype: 'combo',
                    name: 'TypeID',
                    store: Ext.create('TianZun.store.AdvertTypeStore'),
                    valueField: 'ID',
                    displayField: 'Name',
                    editable: false
                },
                {
                    fieldLabel: '状态',
                    xtype: 'combo',
                    name: 'State',
                    store:Ext.create('Ext.data.Store', {
                        data : [
                            { ID: 1, Name: '未到期' },
                            { ID: 2, Name: '将到期' },
                            { ID: 3, Name: '已到期' }
                        ]
                    }),
                    valueField: 'ID',
                    displayField: 'Name',
                    editable: false
                }
            ],
            buttons: [{
                text: '确定',
                handler: 'onQueryOK'
            }, {
                text: '重置',
                handler: 'onHide'
            }]
        }];

        this.callParent();
    }
});