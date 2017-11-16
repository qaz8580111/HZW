Ext.define('TianZun.view.shop.ShopList', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.ShopList',

    requires: [
       'TianZun.controller.Shop'
    ],
    controller: 'shop',
    title: '店家列表',
    sortable: false,

    initComponent: function () {
        var store = Ext.create('TianZun.store.ShopStore');

        Ext.apply(this, {
            layout: 'border',
            items: [{
                xtype: 'grid',
                region: 'center',
                columns: [
                        { header: '编号', dataIndex: 'idtype', flex: 1 },
                        { header: '名称', dataIndex: 'storename', flex: 1 },
                        { header: '类型', dataIndex: 'typename', flex: 1 },
                        { header: '店家地址', dataIndex: 'address', flex: 1 },
                        { header: '负责人', dataIndex: 'person', flex: 1 },
                ],  listeners: {
                    render: 'onRender',
                    itemdblclick: 'onGridItemDbClick',
                },
                store: store,
                tbar: [
                   {
                       text: '查询',
                       handler: 'onQuery'
                   },
                   {
                       text: '修改',
                       action:'edit',
                       handler: 'onEdit'
                   },
                   {
                       text: '删除',
                       action:'delete',
                       handler: 'onDelete'
                   }
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