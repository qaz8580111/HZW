Ext.define('TianZun.view.test.Test', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.testTest',

    initComponent: function () {

        Ext.apply(this, {
            items: [
                {
                    xtype: 'panel',
                    height: 30,
                    border: false,
                    html: '<div style="line-height:28px;border-bottom:1px solid #808080;">&nbsp;沿街店家&nbsp;>&nbsp;店家列表</div>',
                },
                {
                    xtype: 'panel',
                    border: false,
                    height: 50,
                    items: [
                        {
                            xtype: 'form',
                            border: false,
                            bodyPadding: 10,
                            layout: {
                                type: 'table',
                                columns: 3,
                            },
                            fieldDefaults: {
                                labelAlign: 'right',
                                labelWidth: 75
                            },
                            defaults: {
                                xtype: 'textfield',
                                width: 180
                            },
                            items: [
                                {
                                    fieldLabel: '编号',
                                    name: 'Name'
                                },
                                {
                                    fieldLabel: '名称',
                                    name: 'Name'
                                },
                                {
                                    fieldLabel: '类型',
                                    name: 'Name'
                                }
                            ],
                        }
                    ]
                },
                {
                    xtype: 'grid',
                    layout: 'fit',
                    padding: 10,
                    sortableColumns: false,
                    viewConfig: {
                        enableTextSelection: true
                    },
                    columns: [
                        { header: '角色名称', dataIndex: 'Name', width: 150 },
                        { header: '说明', dataIndex: 'Comment', flex: 1 },
                        { header: '是否系统内置', dataIndex: 'IsSystemName', width: 105 },
                        { header: '排序', dataIndex: 'SeqNo', width: 60 },
                        {
                            xtype: 'datecolumn',
                            header: '更新时间',
                            dataIndex: 'UpdatedTime',
                            format: 'Y-m-d H:i',
                            width: 125
                        }
                    ],
                    bbar: {
                        xtype: 'pagingtoolbar',
                        displayInfo: true
                    }
                }
            ]
        });

        this.callParent();
    }
});