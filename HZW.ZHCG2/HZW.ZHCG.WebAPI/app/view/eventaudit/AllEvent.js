Ext.define('TianZun.view.eventaudit.AllEvent', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.allEvent',

    requires: [
    'TianZun.controller.AllEvent'
    ],

    controller: 'allEvent',
    title: '全部事件',
    sortable: false,


    initComponent: function () {
        var store = Ext.create('TianZun.store.AllEventStore');
        store.load();
        Ext.apply(this, {
            layout: 'border',
            items: [{
                xtype: 'grid',
                region: 'center',
                columns: [
                         { header: '编号', dataIndex: 'event_id', width: 150, align: 'center' },
                         { header: '标题', dataIndex: 'title', flex: 1, align: 'center' },
                         { header: '上报时间', dataIndex: 'reporttime', width: 150, align: 'center' },
                         { header: '来源', dataIndex: 'source', width: 150, align: 'center' },
                         { header: '状态', dataIndex: 'isexamine', width: 150, align: 'center' },
                         { header: '是否推送', dataIndex: 'ispush', width: 150, align: 'center' },
                ], listeners: {
                    render: 'onRender',
                    itemdblclick: 'onLook',
                },
                store: store,
                tbar: [
                   {
                       text: '查询',
                       handler: 'onQuery'
                   }, {
                       text: '查看',
                       action: 'look',
                       handler: 'onLook'
                   },
                   //{
                   //    text: '作废',
                   //    action: 'delete',
                   //    handler: 'onDelete'
                   //}, {
                   //    text: '录入',
                   //    action: 'input',
                   //    handler: 'onInput'
                   //}
                ],
                bbar: {
                    xtype: 'pagingtoolbar',
                    store: store,
                    displayInfo: true
                },
            }]
        });

        this.callParent();
    },
   
});