Ext.define('TianZun.view.mh.NewsDeail', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.newsDeail',

    title: '新闻详情',
    layout: 'fit',

    initComponent: function () {
        var me = this;
        Ext.apply(this, {
            items: [
                {
                    xtype: 'form',
                    region: 'center',
                    border: false,
                    width: 880,
                    height: 550,
                    overflowY: 'auto',
                    layout: {
                        type: 'table',
                        columns: 2,
                    },
                    fieldDefaults: {
                        labelAlign: 'left',
                        labelWidth: 110
                    },
                    defaults: {
                        xtype: 'textfield',
                        width: 300
                    },
                    items: [
                        {
                            fieldLabel: '文章标题 <span style="color:red">*</span>',
                            name: 'title',
                            xtype: 'textfield',
                            valueField: 'ID',
                            allowBlank: false,
                            editable: false,
                            colspan: 2,
                            margin: '10 0 10 50',
                            width: 765,
                            value: this.record.get('title')
                        },
                       {
                           fieldLabel: '栏目大类',
                           name: 'BigName',
                           allowBlank: true,
                           editable: false,
                           width: 355,
                           margin: '10 0 10 50',
                           value: this.record.get('categoryBName')
                       },
                   {
                       fieldLabel: '栏目小类',
                       name: 'BigName',
                       allowBlank: true,
                       editable: false,
                       width: 355,
                       margin: '10 0 10 50',
                       value: this.record.get('categorySName')
                   },
                   {
                       fieldLabel: '新闻内容',
                       xtype: 'textareafield',
                       region: 'center',
                       name: 'content',
                       id: 'content',
                       anchor: '90%',
                       colspan: 2,
                       width: 765,
                       height: 420,
                       editable: false,
                       margin: '10 0 10 50',
                       listeners: {
                           'render': function (f) {
                               setTimeout(function () {
                                   if (KindEditor) {
                                       Nceditor = KindEditor.create('#content-inputEl', {
                                           cssPath: 'Scripts/kindeditor/plugins/code/prettify.css',
                                           resizeType: 1,
                                           resizeMode: 0,
                                           allowFileManager: true
                                       });
                                   }
                               }, 500);
                           }
                       },
                       value: this.record.get('content')
                   },

                    ]
                }
            ],
            buttons: [{
                text: '关闭',
                handler: 'onClose'
            }]
        });


        this.callParent();
    }
});