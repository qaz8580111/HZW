Ext.define('TianZun.view.outad.AddAdvert', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.addAdvert',
    requires: [
        'TianZun.controller.Advert',
    ],
    controller: 'advert',
    title: '新增广告',
    layout: 'fit',
    initComponent: function () {
        var me = this;
        Ext.apply(this, {            
            items: [
                {
                    xtype: 'form',
                    region: 'center',
                    border: false,
                    width: 800,
                    height: 550,
                    overflowY:'auto',
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
                            xtype: 'hidden',
                            name: 'Createuserid',
                            value: $.cookie("USER_ID")
                        },
                        {
                            fieldLabel: '名称 <span style="color:red">*</span>',
                            name: 'AdName',
                            xtype: 'textfield',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '类型 <span style="color:red">*</span>',
                            name: 'TypeID',
                            xtype: 'combo',
                            store: Ext.create('TianZun.store.AdvertTypeStore'),
                            displayField: 'Name',
                            valueField: 'ID',
                            editable: false,
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '所属单位 <span style="color:red">*</span>',
                            xtype: 'textfield',
                            name: 'UnitName',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '所属单位联系人',
                            xtype: 'textfield',
                            name: 'UnitPerson',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '所属单位联系电话',
                            xtype: 'textfield',
                            name: 'UnitPhone',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '制作商 <span style="color:red">*</span>',
                            name: 'Producers',
                            xtype: 'textfield',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '制作商联系电话',
                            name: 'Prophone',
                            xtype: 'textfield',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '审批单位 <span style="color:red">*</span>',
                            name: 'ExamUnit',
                            xtype: 'textfield',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '审批日期 <span style="color:red">*</span>',
                            xtype: 'datefield',
                            name: 'ExamDate',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '开始日期 <span style="color:red">*</span>',
                            xtype: 'datefield',
                            name: 'StartDate',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '到期日期 <span style="color:red">*</span>',
                            xtype: 'datefield',
                            name: 'EndDate',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '地址 <span style="color:red">*</span>',
                            name: 'Address',
                            xtype: 'textfield',
                            allowBlank: false,
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '长',
                            xtype: 'numberfield',
                            name: 'VLong',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '宽',
                            xtype: 'numberfield',
                            name: 'VWide',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '高',
                            xtype: 'numberfield',
                            name: 'VHigh',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '材质',
                            xtype: 'textfield',
                            name: 'Materials',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '养护单位',
                            xtype: 'textfield',
                            name: 'Curingunit',
                            margin: '10 0 10 50',
                        },
                        {
                            fieldLabel: '监管单位',
                            xtype: 'textfield',
                            name: 'Superviseunit',
                            margin: '10 0 10 50',
                        },                    
                        {
                            fieldLabel: '现场照片',
                            xtype: 'filefield',
                            id: 'Photo1',
                            name: 'Photo1',
                            margin: '10 0 10 50',
                            buttonText: '选择图片',
                            colspan: 2,
                            width: 660,                       
                        },
                        {
                            xtype: 'filefield',
                            id: 'Photo2',
                            name: 'Photo2',
                            margin: '10 0 10 165',
                            buttonText: '选择图片',
                            colspan: 2,
                            width: 545,
                        },
                        {
                            xtype: 'filefield',
                            id: 'Photo3',
                            name: 'Photo3',
                            margin: '10 0 10 165',
                            buttonText: '选择图片',
                            colspan: 2,
                            width: 545,
                        },
                        {
                            fieldLabel: '预期效果',
                            xtype: 'filefield',
                            id: 'Photo4',
                            name: 'Photo4',
                            margin: '10 0 10 50',
                            buttonText: '选择图片',
                            colspan: 2,
                            width: 660,
                        },
                        {
                            fieldLabel: '审批文件',
                            xtype: 'filefield',
                            id: 'File1',
                            name: 'File1',
                            margin: '10 0 10 50',
                            buttonText: '选择文件',
                            colspan: 2,
                            width: 660,
                        },
                        {
                            xtype: 'panel',                            
                            border: false,
                            bodyBorder: false,
                            colspan: 2,
                            margin: '10 0 10 0',
                            width: '96%',
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
                                name: 'Grometry',
                                xtype: 'textfield',
                                allowBlank: false,
                                colspan: 2,
                                width: 570,
                                listeners: {
                                    render: function (p) {
                                        p.getEl().on('click', function (p) {
                                            CreateAarcgisMap('EVENT_COORDINATE_ID', '广告坐标', 1, 1, this.component.getValue());
                                        });
                                    },
                                }
                            }]
                        },
                         {
                             fieldLabel: '广告备注',
                             name: 'Remark',
                             xtype: 'textarea',
                             colspan: 2,
                             margin: '0 0 10 50',
                             width: '88%',
                         },
                    ]
                }
            ]
        });
        this.buttons = [
            {
                xtype: 'box',
                style:'margin-left:20px',
                html: '<image style="float: left;margin-right: 5px;" src="../../Images/必填提示.png" /><span style="float:left;">带</span><b style="color:red;font-size:18px;display: block;float: left;line-height: 21px;">*</b>为必填项'
            },
            '->',{
                text: '确定',
                handler: 'onAddOK'
            }, {
                text: '关闭',
                handler: function () {
                    var content = Ext.create("TianZun.view.outad.AdList");
                    var view = Ext.getCmp("IndexLeft").up();
                    var panel = view.items.getAt(3)
                    var gridArr = Ext.getCmp("IndexLeft").query('gridcolumn')[1].up('treepanel').getEl().query('.x-grid-item');
                    gridArr[0].className = "x-grid-item x-grid-item-selected";
                    gridArr[1].className = "x-grid-item";
                    view.remove(panel)
                    content.region = 'center';
                    view.add(content);

                    me.close();
                }
            },            
        ]

        this.callParent();
    },
    listeners: {
        close: function (win) {
            var content = Ext.create("TianZun.view.outad.AdList");
            var view = Ext.getCmp("IndexLeft").up();
            var panel = view.items.getAt(3)
            var gridArr = Ext.getCmp("IndexLeft").query('gridcolumn')[1].up('treepanel').getEl().query('.x-grid-item');
            gridArr[0].className = "x-grid-item x-grid-item-selected";
            gridArr[1].className = "x-grid-item";
            view.remove(panel)
            content.region = 'center';
            view.add(content);
            UnMask();
        },
    }
});
