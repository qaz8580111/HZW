Ext.define('TianZun.view.eventaudit.AuditEvent', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.auditEvent',



    title: '事件审核',
    layout: 'fit',



    initComponent: function () {
        var me = this;
        Ext.apply(this, {
            items: [
                {
                    xtype: 'form',
                    region: 'center',
                    border: false,
                    width: 750,
                    overflowY: 'auto',
                    layout: {
                        type: 'table',
                        columns: 2,
                    },
                    fieldDefaults: {
                        labelAlign: 'left',
                        labelWidth: 75
                    },
                    defaults: {
                        xtype: 'textfield',
                        width: 300
                    },
                    items: [
                        {
                            xtype: 'hidden',
                            name: 'event_id',
                            value: this.record.get('event_id')

                        },
                         {
                             xtype: 'hidden',
                             name: 'inputperson',
                             value: $.cookie("USER_ID")
                         },
                         {
                             xtype: 'hidden',
                             name: 'invalisperson',
                             value: $.cookie("USER_ID")
                         },
                        {
                            fieldLabel: '标题',
                            name: 'title',
                            xtype: 'textfield',
                            colspan: 2,
                            width: 680,
                            editable: false,
                            margin: '5 0 0 50',
                            value: this.record.get('title')
                        },
                        {
                            fieldLabel: '上报人',
                            name: 'reportperson',
                            xtype: 'textfield',
                            editable: false,
                            margin: '5 0 0 50',

                            value: this.record.get('reportperson')
                        },
                        {
                            fieldLabel: '联系方式',
                            name: 'contact',
                            xtype: 'textfield',
                            editable: false,
                            margin: '5 0 0 50',
                            value: this.record.get('contact')
                        },
                        {
                            fieldLabel: '上报时间',
                            name: 'reporttime',
                            xtype: 'textfield',
                            editable: false,
                            margin: '5 0 0 50',
                            value: this.record.get('reporttime')
                        },
                         {
                             fieldLabel: '来源',
                             name: 'source',
                             xtype: 'textfield',
                             editable: false,
                             margin: '5 0 0 50',
                             value: this.record.get('source')
                         },

                        {
                            fieldLabel: '内容',
                            xtype: 'textarea',
                            name: 'content',
                            colspan: 2,
                            width: 680,
                            margin: '5 0 0 50',
                            editable: false,
                            value: this.record.get('content')

                        },
                        {
                            xtype: 'panel',
                            border: false,
                            bodyBorder: false,
                            colspan: 2,
                            margin: '5 0 10 0',
                            width: 760,
                            layout: {
                                type: 'hbox',
                                align: 'left'
                            },
                            items: [
                                {
                                    xtype: 'label',
                                    text: '照片:',
                                    id: 'ZP',
                                    name: 'ZP',
                                    margin: '5 50 0 52'
                                },
                                 {
                                     xtype: 'box', //或者xtype: 'component',  
                                     width: 100, //图片宽度  
                                     height: 100, //图片高度  
                                     id: 'photo1',
                                     name: 'photo1',
                                     html: '<img src="/GetPictureFile.ashx?PicPath=' + configs.EventFile + this.record.get('photo1') + '" style="width:100px;height:100px;" />',
                                     listeners: {
                                         render: {
                                             fn: function (p) {
                                                 if (me.record.get('photo1') == "")
                                                     this.hide();
                                                 p.getEl().on('click', function (p) {
                                                     var objE = document.createElement("div");
                                                     objE.innerHTML = this.dom.innerHTML;
                                                     var fileViewer = Ext.create('TianZun.ux.LargeImageWindow', objE.childNodes[0].src);
                                                 });
                                             }
                                         }
                                     }
                                 },
                                 {
                                     xtype: 'box', //或者xtype: 'component',  
                                     width: 100, //图片宽度  
                                     height: 100, //图片高度  
                                     id: 'photo2',
                                     name: 'photo2',
                                     html: '<img src="/GetPictureFile.ashx?PicPath=' + configs.EventFile + this.record.get('photo2') + '" style="width:100px;height:100px;" />',
                                     listeners: {
                                         render: {
                                             fn: function (p) {
                                                 if (me.record.get('photo2') == "")
                                                     this.hide();
                                                 p.getEl().on('click', function (p) {
                                                     var objE = document.createElement("div");
                                                     objE.innerHTML = this.dom.innerHTML;
                                                     var fileViewer = Ext.create('TianZun.ux.LargeImageWindow', objE.childNodes[0].src);
                                                 });
                                             }
                                         }
                                     }
                                 },
                                {
                                    xtype: 'box', //或者xtype: 'component',  
                                    width: 100, //图片宽度  
                                    height: 100, //图片高度  
                                    id: 'photo3',
                                    name: 'photo3',
                                    html: '<img src="/GetPictureFile.ashx?PicPath=' + configs.EventFile + this.record.get('photo3') + '" style="width:100px;height:100px;" />',
                                    listeners: {
                                        render: {
                                            fn: function (p) {
                                                if (me.record.get('photo3') == "")
                                                    this.hide();
                                                p.getEl().on('click', function (p) {
                                                    var objE = document.createElement("div");
                                                    objE.innerHTML = this.dom.innerHTML;
                                                    var fileViewer = Ext.create('TianZun.ux.LargeImageWindow', objE.childNodes[0].src);
                                                });
                                            }
                                        }
                                    }
                                }
                            ],
                        },

                        {
                            xtype: 'panel',
                            border: false,
                            id: 'radiogroup ',
                            bodyBorder: false,
                            colspan: 2,
                            margin: '5 0 10 0',
                            width: 760,
                            layout: {
                                type: 'hbox',
                                align: 'left'
                            },
                            items: [
                                {
                                    xtype: 'label',
                                    text: '状态:',
                                    margin: '5 50 0 52'
                                },
                               {
                                   xtype: 'radio',
                                   name: 'isinvalis',
                                   id:'inputisinvalis',
                                   inputValue: '0',
                                   boxLabel: '录入',
                                   checked: true,
                                   margin: '5 20 0 0',
                                   listeners: {
                                       render: function (p) {
                                           p.getEl().on('click', function (p) {
                                               Ext.getCmp('inputcontent').show();
                                               Ext.getCmp('inputcontent').allowBlank = false;
                                               Ext.getCmp('invaliscontent').hide();
                                               Ext.getCmp('invaliscontent').allowBlank = true;
                                           });
                                       },
                                   }
                               }, {
                                   xtype: 'radio',
                                   id: 'invalisisinvalis',
                                   name: 'isinvalis',
                                   inputValue: '1',
                                   boxLabel: '作废',
                                   margin: '5 20 0 0',
                                   listeners: {
                                       render: function (p) {
                                           p.getEl().on('click', function (p) {
                                               Ext.getCmp('invaliscontent').show();
                                               Ext.getCmp('invaliscontent').allowBlank = false;
                                               Ext.getCmp('inputcontent').hide();
                                               Ext.getCmp('inputcontent').allowBlank = true;
                                           });
                                       },
                                   }
                               }, 
                            ],
                        },

                    {
                        id: 'inputcontent',
                        fieldLabel: '录入意见',
                        xtype: 'textarea',
                        margin: '5 0 10 50',
                        name: 'inputcontent',
                        colspan: 2,
                        allowBlank: false,
                        width: 680,
                        //value:'同意'
                    },
                     {
                         id: 'invaliscontent',
                         fieldLabel: '作废意见',
                         xtype: 'textarea',
                         margin: '5 0 10 50',
                         name: 'invaliscontent',
                         colspan: 2,
                         allowBlank: false,
                         width: 680,
                         //value:'同意'
                     }]
                }
            ],
        });
        this.buttons = [
            {
                text: '确定',
                handler: 'onAuditOK'
            }, {
                text: '关闭',
                handler: 'onClose'
            }
        ]

        this.callParent();
    }
});




