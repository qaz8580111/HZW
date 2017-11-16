Ext.define('TianZun.view.eventaudit.EventQuery', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.eventQuery',

    title: '查询事件',
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
                {
                    fieldLabel: '编号',
                    name: 'Code'
                },
                {
                    fieldLabel: '事件标题',
                    name: 'title'
                }, {
                    fieldLabel: '开始时间',
                    xtype: 'datefield',
                    width: 255,
                    format: 'Y-m-d',
                    id: 'STime',
                    value: new Date()  // limited to the current date or prior
                }, {
                    fieldLabel: '结束时间',
                    xtype: 'datefield',
                    width: 255,
                    format: 'Y-m-d',
                    id: 'ETime',
                    value: new Date()  // defaults to today
                },
              
            ],
            buttons: [{
                text: '确定',
                handler: 'onQueryOK'
            }, {
                text: '重置',
                handler: 'onClear'
            }]
        }];

        this.callParent();
    }
});