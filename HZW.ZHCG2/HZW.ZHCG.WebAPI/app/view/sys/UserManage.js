Ext.define('TianZun.view.sys.UserManage', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.userManage',

    requires: [
        'TianZun.controller.User'
    ],

    controller: 'user',
    title: '用户管理',
    sortable: false,

    initComponent: function () {
        var store = Ext.create('TianZun.store.UserPageStore');

        Ext.apply(this, {
            layout: 'border',
            items: [
                {
                    xtype: 'treepanel',
                    border: false,
                    width: 200,
                    region: 'west',
                    padding: '0 2 0 0',
                    style: {
                        background: '#cccccc',
                    },
                    listeners: {
                        render: 'onTreeRender',
                        itemclick: 'onTreeItemClick'
                    }
                },
                {
                    xtype: 'gridpanel',
                    border: false,
                    region: 'center',
                    viewConfig: {
                        enableTextSelection: true
                    },
                    columns: [
                        { header: '用户名称', dataIndex: 'DisplayName', flex: 1 },
                        { header: '用户编号', dataIndex: 'Code', flex: 1 },
                        { header: '用户类型', dataIndex: 'UserTypeName', flex: 1 },
                        {
                            xtype: 'datecolumn',
                            header: '更新时间',
                            dataIndex: 'UpdatedTime',
                            format: 'Y-m-d H:i',
                            width: 125,
                            flex:1
                        }
                    ],
                    store: store,

                    bbar: {
                        xtype: 'pagingtoolbar',
                        store: store,
                        displayInfo: true
                    },
                    listeners: {
                        itemdblclick: 'onGridItemDbClick',
                    }
                },
            ],
            tbar: [
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
                }
            ]
        });

        this.callParent();
    }
});