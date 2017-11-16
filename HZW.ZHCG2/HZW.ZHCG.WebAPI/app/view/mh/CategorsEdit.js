Ext.define('TianZun.view.mh.CategorsEdit', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.categorsEdit',

    title: '编辑栏目',
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
                    xtype: 'hidden',
                    name: 'ID',
                    value: this.record.get('ID')
                },
                {
                    fieldLabel: '栏目大类<span style="color:red">*</span>',
                    emptyText: '请选择',
                    xtype: 'combo',
                    name: 'BigID',
                    store: Ext.create('TianZun.store.NewsBigClassStore'),
                    valueField: 'ID',
                    displayField: 'Name',
                    editable: false,
                    allowBlank: false,
                    listeners: {
                        render: function (combo) {
                            this.getStore().load();
                        }
                    },
                    value: this.record.get('BigID')
                },
                {
                    fieldLabel: '栏目小类<span style="color:red">*</span>',
                    name: 'Name',
                    allowBlank: false,
                    value: this.record.get('Name')
                },
               {
                   fieldLabel: '状态<span style="color:red">*</span>',
                   emptyText: '请选择',
                   xtype: 'combo',
                   name: 'isonline',
                   store: Ext.create('Ext.data.Store', {
                       data: [
                           { ID: 1, Name: '上线' },
                           { ID: 2, Name: '下线' }
                       ]
                   }),
                   valueField: 'ID',
                   displayField: 'Name',
                   editable: false,
                   allowBlank: false,
                   value: this.record.get('isonline')
               },
                 {
                     fieldLabel: '排序<span style="color:red">*</span>',
                     xtype: 'numberfield',
                     name: 'SeqNo',
                     allowBlank: false,
                     minValue: 1,
                     value: this.record.get('SeqNo')
                 },
            ],
            buttons: [{
                xtype: 'box',
                style: 'margin-left:20px',
                html: '<image style="float: left;margin-right: 5px;" src="../../Images/必填提示.png" /><span style="float:left;">带</span><b style="color:red;font-size:18px;display: block;float: left;line-height: 21px;">*</b>为必填项'
            },
            '->', {
                text: '确定',
                handler: 'onEditOK'
            }, {
                text: '关闭',
                handler: 'onClose'
            }]
        }];

        this.callParent();
    }
});