Ext.define('TianZun.view.mh.CategorsDeail', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.categorsDeail',

    title: '栏目详情',
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
                    fieldLabel: '栏目大类',
                    name: 'BigName',
                    allowBlank: true,
                    editable: false,
                    value: this.record.get('BigName')
                },
                {
                    fieldLabel: '栏目小类',
                    name: 'Name',
                    allowBlank: true,
                    editable: false,
                    value: this.record.get('Name')
                },
               {
                   fieldLabel: '状态',
                   name: 'isonline',
                   allowBlank: true,
                   editable: false,
                   value: this.record.get('isonlinestring')
               },
                 {
                     fieldLabel: '排序',
                     name: 'SeqNo',
                     allowBlank: true,
                     editable: false,
                     value: this.record.get('SeqNo')
                 }
            ],
            buttons: [{
                text: '关闭',
                handler: 'onClose'
            }]
        }];

        this.callParent();
    }
});