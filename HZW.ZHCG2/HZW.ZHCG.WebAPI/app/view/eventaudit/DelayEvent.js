Ext.define('TianZun.view.eventaudit.DelayEvent', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.delayEvent',

    requires: [
    'TianZun.controller.DelayEvent'
    ],

    controller: 'delayEvent',
    title: '待审核事件',
    sortable: false,


    initComponent: function () {
        var store = Ext.create('TianZun.store.DelayEventStore');
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
                   {
                       text: '审核',
                       action: 'audit',
                       handler: 'onAudit'
                   },
                   //{
                   //    text: '作废',
                   //    action: 'delete',
                   //    handler: 'onDelete'
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