Ext.define('TianZun.view.mh.CategorsManage', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.categorsManage',

    requires: [
        'TianZun.controller.Categors'
    ],

    controller: 'categors',
    title: '栏目管理',
    layout: 'border',
    initComponent: function () {
        var store = Ext.create('TianZun.store.CategorsStore');
        store.load();
        this.items = [
                {
                    xtype: 'gridpanel',
                    border: false,
                    region: 'center',
                    viewConfig: {
                        enableTextSelection: true
                    },
                    columns: [
		                { header: '序号', xtype: 'rownumberer', width: 60, align: 'center', sortable: false },
		                { header: '栏目大类', dataIndex: 'BigName', flex: 1, align: 'center' },
                        { header: '栏目小类', dataIndex: 'Name', flex: 1, align: 'center' },
		                { header: '更新时间', dataIndex: 'createdTime', flex: 1, align: 'center' },
                        { header: '状态', dataIndex: 'isonlinestring', flex: 1, align: 'center' },
                    ],
                    store: store,
                    bbar: {
                        xtype: 'pagingtoolbar',
                        store: store,
                        displayInfo: true
                    },
                    listeners: {
                        render: 'onRender',
                        itemdblclick: 'onGridItemDbClick',
                    }
                },
        ];

        this.tbar = [
                {
                    text: '查询',
                    handler: 'onQuery'
                },
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
                },
                {
                    text: '上线/下线',
                    action:'handle',
                    handler: 'onHandle'
                },
        ]

        this.callParent();
    }
});