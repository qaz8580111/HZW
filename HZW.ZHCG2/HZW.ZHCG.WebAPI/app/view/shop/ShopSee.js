Ext.define('TianZun.view.shop.ShopSee', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.shopSee',

    title: '查看店家',
    layout: 'fit',

    initComponent: function () {
        var me = this;
        Ext.apply(this, {
            items: [
                {
                    xtype: 'form',
                    region: 'center',
                    border: false,
                    width: 730,
                    height: 490,
                    overflowY: 'auto',
                    layout: {
                        type: 'table',
                        columns: 2,
                    },
                    fieldDefaults: {
                        labelAlign: 'left',
                        labelWidth: 150
                    },
                    defaults: {
                        xtype: 'textfield',
                        width: 300
                    },
                    items: [
                        {
                            xtype: 'hidden',
                            name: 'storeid',
                            value: this.record.get('storeid')
                        },
                        {
                            fieldLabel: '店家名称 <span style="color:red">*</span>',
                            name: 'storename',
                            valueField: 'ID',
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
                            name: 'address',
                            value: this.record.get('address'),
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '负责人<span style="color:red">*</span>',
                            name: 'person',
                            value: this.record.get('person'),
                            margin: '10 0 10 50',
                            editable: false,
                        },
                        {
                            fieldLabel: '实际经营者姓名',
                            name: 'businessperson',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '实际经营者联系方式',
                            name: 'businesscontact',
                            margin: '10 0 10 50',
                            editable: false,
                        },
                        {
                            fieldLabel: '注册号',
                            name: 'registnum',
                            margin: '10 0 10 50',
                            editable: false,
                        },
                        {
                            fieldLabel: '注册人姓名',
                            name: 'registname',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '注册人联系方式',
                            name: 'registcontact',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '经营截止日期',
                            name: 'businessenddate',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '注册日期',
                            name: 'registdate',
                            editable: false,
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
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '网格号 <span style="color:red">*</span>',
                            name: 'gridnum',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '网格责任人 <span style="color:red">*</span>',
                            name: 'gridperson',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '网格责任人联系方式 <span style="color:red">*</span>',
                            name: 'gridcontact',
                            editable: false,
                            margin: '10 0 10 50',
                        },
                         {
                             fieldLabel: '经营范围',
                             name: 'businessscope',
                             editable: false,
                             margin: '10 0 10 50',
                         },
                         {
                             xtype: 'panel',
                             border: false,
                             bodyBorder: false,
                             colspan: 2,
                             margin: '10 0 10 0',
                             width:'100%',
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
                                 editable: false,
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
                    ],
                    buttons: [{
                        text: '关闭',
                        handler: 'onClose'
                    }]
                }]
        });
        this.callParent();
        this.child('form').loadRecord(this.record);
    }
});