Ext.define('TianZun.view.eventaudit.DetailEvent', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.detailEvent',

    title: '查看详情',
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
                            margin: '0 0 0 50',
                            editable: false,
                            id: 'inputpersonname',
                            fieldLabel: '录入人',
                            name: 'inputpersonname',
                            xtype: 'textfield',
                            value: this.record.get('inputpersonname'),
                            listeners: {
                                render: {
                                    fn: function (p) {
                                        if (me.record.get('inputpersonname') == "")
                                            this.hide();
                                    }
                                },
                            }
                        },
                         {
                             id: 'inputtime',
                             fieldLabel: '录入时间',
                             name: 'inputtime',
                             xtype: 'textfield',
                             editable: false,
                             margin: '0 0 0 50',
                             value: this.record.get('inputtime'),
                             listeners: {
                                 render: {
                                     fn: function (p) {
                                         if (me.record.get('inputtime') == "1970-01-01 08:00")
                                             this.hide();
                                     }
                                 },
                             }
                         },
                    {
                        id: 'inputcontent',
                        fieldLabel: '录入意见',
                        xtype: 'textarea',
                        margin: '5 0 10 50',
                        name: 'inputcontent',
                        editable: false,
                        colspan: 2,
                        width: 680,
                        value: this.record.get('inputcontent'),
                        listeners: {
                            render: {
                                fn: function (p) {
                                    if (me.record.get('inputcontent') == "")
                                        this.hide();
                                }
                            },
                        }
                    },

                    {
                        margin: '0 0 0 50',
                        editable: false,
                        id: 'invalispersonname',
                        fieldLabel: '作废人',
                        name: 'invalispersonname',
                        xtype: 'textfield',
                        value: this.record.get('invalispersonname'),
                        listeners: {
                            render: {
                                fn: function (p) {
                                    if (me.record.get('invalispersonname') == "")
                                        this.hide();
                                }
                            },
                        }
                    },
                        
                          {
                              id: 'invalistime',
                              fieldLabel: '作废时间',
                              name: 'invalistime',
                              xtype: 'textfield',
                              editable: false,
                              margin: '0 0 0 50',
                              value: this.record.get('invalistime'),
                              listeners: {
                                  render: {
                                      fn: function (p) {
                                          if (me.record.get('invalistime') == "1970-01-01 08:00")
                                              this.hide();
                                      }
                                  },
                              }
                          },
                    {
                        id: 'invaliscontent',
                        fieldLabel: '作废意见',
                        xtype: 'textarea',
                        margin: '5 0 10 50',
                        name: 'invaliscontent',
                        editable: false,
                        colspan: 2,
                        width: 680,
                        value: this.record.get('invaliscontent'),
                        listeners: {
                            render: {
                                fn: function (p) {
                                    if (me.record.get('invaliscontent') == "")
                                        this.hide();
                                }
                            },
                        }
                    }

                    ]
                }
            ]
        });
        this.buttons = [
            {
                text: '关闭',
                handler: function () {
                    me.close();
                }
            }
        ]

        this.callParent();
    }
});