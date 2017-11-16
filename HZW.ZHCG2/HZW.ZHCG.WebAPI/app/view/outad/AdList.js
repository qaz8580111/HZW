Ext.define('TianZun.view.outad.AdList', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.adlist',
    title: '广告列表',
    requires: [
        'TianZun.controller.Advert'
    ],
    controller: 'advert',
    sortable: false,
    layout: 'hbox',
    initComponent: function () {
        var store = Ext.create('TianZun.store.AdvertPage');
        store.load();

        Ext.apply(this, {
            layout: 'border',
            items: [{
                xtype: 'grid',
                region: 'center',
                columns: [
                        { header: '编号', dataIndex: 'IDType', flex: 1, align: 'center' },
                        { header: '名称', dataIndex: 'AdName', flex: 1, align: 'center' },
                        { header: '类型', dataIndex: 'TypeName', flex: 1, align: 'center' },
                        { header: '状态', dataIndex: 'State', flex: 1, align: 'center',}
                ],
                store: store,
                tbar: [
                   {
                       text: '查询',
                       handler: 'onQuery'
                   },
                   {
                       text: '修改',
                       action: 'edit',
                       handler: 'onEdit'
                   },
                   {
                       text: '删除',
                       action: 'delete',
                       handler: 'onDelete'
                   }
                ],
                bbar: {
                    xtype: 'pagingtoolbar',
                    store: store,
                    displayInfo: true
                },
                listeners: {
                    render: 'onRender',
                    itemdblclick: 'onGridItemDbClick',
                }
            }]
        });
        this.callParent();
    }
});