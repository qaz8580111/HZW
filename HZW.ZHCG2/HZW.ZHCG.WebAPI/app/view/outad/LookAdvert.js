Ext.define('TianZun.view.outad.LookAdvert', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.lookAd',

    title: '查看广告',
    layout: 'fit',

    initComponent: function () {
        var me = this;
        this.items = [{
            xtype: 'form',
            border: false,
            width: 800,
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
                    xtype: 'hidden',
                    name: 'ID',
                    value: this.record.get('ID')
                },
                {
                    fieldLabel: '名称 <span style="color:red">*</span>',
                    name: 'AdName',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '类型 <span style="color:red">*</span>',
                    name: 'TypeName',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '所属单位 <span style="color:red">*</span>',
                    value: this.record.get('UnitName'),
                    name: 'UnitName',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '所属单位联系人',
                    value: this.record.get('UnitPerson'),
                    name: 'UnitPerson',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '所属单位联系电话',
                    value: this.record.get('UnitPhone'),
                    name: 'UnitPhone',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '制作商 <span style="color:red">*</span>',
                    value: this.record.get('Producers'),
                    name: 'Producers',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '制作商联系电话',
                    value: this.record.get('Prophone'),
                    name: 'Prophone',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '审批单位 <span style="color:red">*</span>',
                    value: this.record.get('ExamUnit'),
                    name: 'ExamUnit',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '审批日期 <span style="color:red">*</span>',
                    value: Ext.Date.format(this.record.get('ExamDate'), 'Y-m-d'),
                    name: 'ExamDate',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '开始日期 <span style="color:red">*</span>',
                    value: Ext.Date.format(this.record.get('StartDate'), 'Y-m-d'),
                    name: 'StartDate',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '到期日期 <span style="color:red">*</span>',
                    value: Ext.Date.format(this.record.get('EndDate'), 'Y-m-d'),
                    name: 'EndDate',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '地址 <span style="color:red">*</span>',
                    value: this.record.get('Address'),
                    name: 'Address',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '长',
                    value: this.record.get('VLong'),
                    name: 'VLong',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '宽',
                    value: this.record.get('VWide'),
                    name: 'VWide',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '高',
                    value: this.record.get('VHigh'),
                    name: 'VHigh',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '材质',
                    value: this.record.get('Materials'),
                    name: 'Materials',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '养护单位',
                    value: this.record.get('Curingunit'),
                    name: 'Curingunit',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '监管单位',
                    value: this.record.get('Superviseunit'),
                    name: 'Superviseunit',
                    editable: false,
                    margin: '10 0 10 50',
                },
                {
                    xtype: 'panel',
                    border: false,
                    bodyBorder: false,
                    colspan: 2,
                    width: 760,
                    layout: {
                        type: 'hbox',
                        align: 'left'
                    },
                    items: [
                        {
                            xtype: 'label',
                            text: '现场照片',
                            id: 'picture',
                            name: 'picture',
                            margin: '10 65 10 50'
                        },
                        {
                            xtype: 'box',
                            width: 100,
                            height: 100,
                            id: 'Photo1',
                            name: 'Photo1',
                            margin: '0 30 0 0',
                            html: '<img src="/GetPictureFile.ashx?PicPath=' + configs.OutAdvertFile + this.record.get('Photo1') + '" style="width:100px;height:100px;" />',
                            listeners: {
                                render: {
                                    fn: function (p) {
                                        if (me.record.get('Photo1') == "")
                                            this.hide();
                                        p.getEl().on('click', function (p) {
                                            var objE = document.createElement("div");
                                            objE.innerHTML = this.dom.innerHTML;
                                            var fileViewer = Ext.create('TianZun.ux.LargeImageWindow', objE.childNodes[0].src);
                                        });
                                    }
                                },
                            }
                        },
                         {
                             xtype: 'box',
                             width: 100,
                             height: 100,
                             id: 'Photo2',
                             name: 'Photo2',
                             margin: '0 30 0 0',
                             html: '<img src="/GetPictureFile.ashx?PicPath=' + configs.OutAdvertFile + this.record.get('Photo2') + '" style="width:100px;height:100px;" />',
                             listeners: {
                                 render: {
                                     fn: function (p) {
                                         if (me.record.get('Photo2') == "")
                                             this.hide();
                                         p.getEl().on('click', function (p) {
                                             var objE = document.createElement("div");
                                             objE.innerHTML = this.dom.innerHTML;
                                             var fileViewer = Ext.create('TianZun.ux.LargeImageWindow', objE.childNodes[0].src);
                                         });
                                     }
                                 },
                             }

                         },
                        {
                            xtype: 'box', 
                            width: 100,
                            height: 100,
                            id: 'Photo3',
                            name: 'Photo3',
                            html: '<img src="/GetPictureFile.ashx?PicPath=' + configs.OutAdvertFile + this.record.get('Photo3') + '" style="width:100px;height:100px;" />',
                            listeners: {
                                render: {
                                    fn: function (p) {
                                        if (me.record.get('Photo3') == "")
                                            this.hide();
                                        p.getEl().on('click', function (p) {                                            
                                            var objE = document.createElement("div");
                                            objE.innerHTML = this.dom.innerHTML;
                                            var fileViewer = Ext.create('TianZun.ux.LargeImageWindow', objE.childNodes[0].src);
                                        });
                                    }
                                },
                            }
                        }
                    ],
                },
                {
                    xtype: 'panel',
                    border: false,
                    bodyBorder: false,
                    colspan: 2,
                    width: 760,
                    layout: {
                        type: 'hbox',
                        align: 'left'
                    },
                    items: [
                        {
                            xtype: 'label',
                            text: '预期效果',
                            id: 'yphoto',
                            name: 'yphoto',
                            margin: '10 65 10 50'
                        },
                        {
                            xtype: 'box',
                            width: 100,
                            height: 100,
                            id: 'photo4',
                            name: 'photo4',
                            margin: '10 0 0 0',
                            html: '<img src="/GetPictureFile.ashx?PicPath=' + configs.OutAdvertFile + this.record.get('Photo4') + '" style="width:100px;height:100px;" />',
                            listeners: {
                                render: {
                                    fn: function (p) {
                                        if (me.record.get('Photo4') == "")
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
                    ],
                },
                {
                    xtype: 'panel',
                    border: false,
                    bodyBorder: false,
                    colspan: 2,
                    width: 660,
                    margin:'10 0 10 0',
                    layout: {
                        type: 'hbox',
                        align: 'left'
                    },
                    items: [
                        {
                            xtype: 'label',
                            text: '审批文件',
                            id: 'file',
                            name: 'file',
                            margin: '10 40 10 50'
                        },
                        {
                            xtype: 'box',
                            id: 'File1',
                            name: 'File1',
                            margin: '10 0 10 50',
                            html: '<a href="/FileDownLoad.ashx?filePath=' + configs.OutAdvertFile + this.record.get('FilePath1') + '&&fileName=' + this.record.get('FileName1') + '">' + this.record.get('FileName1') + '</a>'
                        },
                    ],
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
                        editable: false,
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
                             fieldLabel: '备注',
                             xtype: "textarea",
                             name: 'Remark',
                             xtype: 'textarea',
                             colspan: 2,
                             margin: '0 0 10 50',
                             width: '88%',
                             editable:false,
                         },
            ],
            buttons: [{
                text: '关闭',
                handler: 'onClose'
            }]
        }];

        this.callParent();
        this.child('form').loadRecord(this.record);
    }
});