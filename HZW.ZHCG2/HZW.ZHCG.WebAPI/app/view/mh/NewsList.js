Ext.define('TianZun.view.mh.NewsList', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.newsList',

    requires: [
        'TianZun.controller.News',
    ],

    controller: 'news',
    title: '新闻列表',
    layout: 'border',
    initComponent: function () {
        var store = Ext.create('TianZun.store.NewsStore');
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
		                { header: '标题', dataIndex: 'title', flex: 1 },
		                { header: '栏目大类', dataIndex: 'categoryBName', flex: 1 },
                        { header: '栏目小类', dataIndex: 'categorySName', flex: 1 },
                        { header: '更新时间', dataIndex: 'createdTime', flex: 1 },
                        { header: '状态', dataIndex: 'isonline', flex: 1 }, 
                   
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
                     text: '发布/取消发布',
                     action:'uphandle',
                     handler: 'onHandle'
                 },
        ]

        this.callParent();
    }
});