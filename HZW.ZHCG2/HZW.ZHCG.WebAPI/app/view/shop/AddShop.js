Ext.define('TianZun.view.shop.AddShop', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.addShop',

    requires: [
        'TianZun.controller.Shop',
    ],
    controller: 'shop',

    title: '新增店家',
    layout: 'fit',
    initComponent: function () {
        var me = this;
        Ext.apply(this, {
            items: [
                {
                    xtype: 'form',
                    region: 'center',
                    border: false,
                    width: 720,
                    height: 480,
                    overflowY: 'auto',
                    layout: {
                        type: 'table',
                        columns: 2,
                    },
                    fieldDefaults: {
                        labelAlign: 'left',
                        labelWidth:150
                    },
                    defaults: {
                        xtype: 'textfield',
                        width:300
                    },
                    items: [
                        {
                            xtype: 'hidden',
                            name: 'createuserid',
                            value: $.cookie("USER_ID")
                        },
                        {
                            fieldLabel: '店家名称 <span style="color:red">*</span>',
                            name: 'storename',
                            xtype: 'textfield',
                            valueField: 'ID',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '店家类型 <span style="color:red">*</span>',
                            xtype: 'combo',
                            name: 'typeid',
                            store: Ext.create('TianZun.store.StoreclassesStore'),
                            valueField: 'type_id',
                            displayField: 'type_name',
                            editable: false,
                            allowBlank: false,
                            listeners: {
                                render: function (combo) {
                                    combo.getStore().on("load", function (store) {
                                        combo.setValue(combo.value)
                                    });
                                    this.getStore().load();
                                }
                            },
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '店家地址 <span style="color:red">*</span>',
                            xtype: 'textfield',
                            name: 'address',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '负责人<span style="color:red">*</span>',
                            xtype: 'textfield',
                            name: 'person',
                            margin: '10 0 10 50',
                            allowBlank: false,
                        },
                        {
                            fieldLabel: '实际经营者姓名',
                            name: 'businessperson',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '实际经营者联系方式',
                            name: 'businesscontact',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '注册号',
                            name: 'registnum',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '注册人姓名',
                            name: 'registname',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '注册人联系方式',
                            name: 'registcontact',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '经营截止日期',
                            name: 'businessenddate',
                            xtype: 'datefield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '注册日期',
                            name: 'registdate',
                            xtype: 'datefield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '营业执照(有无)',
                            xtype: 'combo',
                            name: 'businesslicense',
                            store: Ext.create('Ext.data.Store', {
                                data: [
                                    { ID: 1, Name: '有' },
                                    { ID: 2, Name: '无' }
                                ]
                            }),
                            valueField: 'ID',
                            displayField: 'Name',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '卫生证(有无)',
                            xtype: 'combo',
                            name: 'healthcard',
                            store: Ext.create('Ext.data.Store', {
                                data: [
                                    { ID: 1, Name: '有' },
                                    { ID: 2, Name: '无' }
                                ]
                            }),
                            valueField: 'ID',
                            displayField: 'Name',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '门前三包责任人 <span style="color:red">*</span>',
                            name: 'mqsbperson',
                            xtype: 'textfield',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            //fieldLabel: '网格号 <span style="color:red">*</span>',
                            fieldLabel: '网格号',
                            name: 'gridnum',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '网格责任人',
                            name: 'gridperson',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '网格责任人联系方式',
                            name: 'gridcontact',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                         {
                             fieldLabel: '经营范围',
                             name: 'businessscope',
                             xtype: 'textfield',
                             margin: '10 0 10 50',
                         },
                         {
                             xtype: 'panel',
                             border: false,
                             bodyBorder: false,
                             colspan: 2,
                             margin: '10 0 10 0',
                             width: '100%',
                             layout: {
                                 type: 'hbox',
                                 align: 'left'
                             },
                             items: [{
                                 xtype: 'label',
                                 html: '地理位置<span style="color:red">*</span>',
                                 margin: '10 60 10 50'
                             },
                             {
                                 id: 'EVENT_COORDINATE_ID',
                                 name: 'grometry',
                                 xtype: 'textfield',
                                 allowBlank: false,
                                 colspan: 2,
                                 width: '89%',
                                 margin: '0 0 0 38',
                                 listeners: {
                                     render: function (p) {
                                         p.getEl().on('click', function (p) {
                                             CreateAarcgisMap('EVENT_COORDINATE_ID', '店家坐标', 1, 1, this.component.getValue());
                                         });
                                     },
                                 }
                             }]
                         },
                         {
                             xtype: 'panel',
                             border: false,
                             bodyBorder: false,
                             colspan: 2,
                             margin: '0 0 10 0',
                             width: '100%',
                             layout: {
                                 type: 'hbox',
                                 align: 'left'
                             },
                             items: [
                                 {
                                     fieldLabel: '店家备注',
                                     name: 'remark2',
                                     xtype: 'textarea',
                                     colspan: 2,
                                     width: "100%",
                                     margin: '0 0 10 50',
                                 },
                             ]
                         },                       
                    ]
                }
            ]
        });
        this.buttons = [{
            xtype: 'box',
            style: 'margin-left:20px',
            html: '<image style="float: left;margin-right: 5px;" src="../../Images/必填提示.png" /><span style="float:left;">带</span><b style="color:red;font-size:18px;display: block;float: left;line-height: 21px;">*</b>为必填项'
        },
            '->',
            {
                text: '确定',
                handler: 'onAddOK'
            }, {
                text: '关闭',
                handler: function () {
                    var content = Ext.create("TianZun.view.shop.ShopList");
                    var view = Ext.getCmp("IndexLeft").up();
                    var panel = view.items.getAt(3)
                    var gridArr = Ext.getCmp("IndexLeft").query('gridcolumn')[0].up('treepanel').getEl().query('.x-grid-item');
                    gridArr[0].className = "x-grid-item x-grid-item-selected";
                    gridArr[1].className = "x-grid-item";
                    view.remove(panel)
                    content.region = 'center';
                    view.add(content);

                    me.close();
                }
            }
        ]

        this.callParent();
    },
    listeners: {
        close: function (win) {
            var content = Ext.create("TianZun.view.shop.ShopList");
            var view = Ext.getCmp("IndexLeft").up();
            var panel = view.items.getAt(3)
            var gridArr = Ext.getCmp("IndexLeft").query('gridcolumn')[0].up('treepanel').getEl().query('.x-grid-item');
            gridArr[0].className = "x-grid-item x-grid-item-selected";
            gridArr[1].className = "x-grid-item";
            view.remove(panel)
            content.region = 'center';
            view.add(content);
            UnMask();
        },
    }
});
