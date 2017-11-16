Ext.define('TianZun.view.sys.UnitTypeList', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.unitTypeList',

    requires: [
        'TianZun.controller.UnitType'
    ],

    controller: 'unitType',
    title: '部门类型管理',
    sortable: false,

    initComponent: function () {
        var store = Ext.create('TianZun.store.UnitTypePageStore');

        Ext.apply(this, {
            viewConfig: {
                enableTextSelection: true
            },
            columns: [
                { header: '类型名称', dataIndex: 'Name', flex: 1 },
                { header: '排序', dataIndex: 'SeqNo', width: 100 }
            ],
            store: store,
            tbar: [
                {
                    text: '添加',
                    action: 'add',
                    handler: 'onAdd'
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
            }
        });

        this.callParent();
    },
    listeners: {
        render: 'onRender',
        itemdblclick: 'onItemDbClick',
    }
});