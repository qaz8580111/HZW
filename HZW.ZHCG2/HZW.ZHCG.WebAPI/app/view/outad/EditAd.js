Ext.define('TianZun.view.outad.EditAd', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.editAd',

    title: '修改广告',
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
                    listeners: {
                        render: function (combo) {
                            this.getStore().load();
                        }
                    }
                },
                {
                    fieldLabel: '所属单位 <span style="color:red">*</span>',
                    value: this.record.get('UnitName'),
                    name: 'UnitName',
                    allowBlank: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '所属单位联系人',
                    value: this.record.get('UnitPerson'),
                    name: 'UnitPerson',
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '所属单位联系电话',
                    value: this.record.get('UnitPhone'),
                    name: 'UnitPhone',
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '制作商 <span style="color:red">*</span>',
                    value: this.record.get('Producers'),
                    name: 'Producers',
                    allowBlank: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '制作商联系电话',
                    value: this.record.get('Prophone'),
                    name: 'Prophone',
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '审批单位 <span style="color:red">*</span>',
                    value: this.record.get('ExamUnit'),
                    name: 'ExamUnit',
                    allowBlank: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '审批日期 <span style="color:red">*</span>',
                    xtype: 'datefield',
                    value: Ext.Date.format(this.record.get('ExamDate'), 'Y-m-d'),
                    name: 'ExamDate',
                    allowBlank: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '开始日期 <span style="color:red">*</span>',
                    xtype: 'datefield',
                    value: Ext.Date.format(this.record.get('StartDate'), 'Y-m-d'),
                    name: 'StartDate',
                    allowBlank: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '到期日期 <span style="color:red">*</span>',
                    xtype: 'datefield',
                    value: Ext.Date.format(this.record.get('EndDate'), 'Y-m-d'),
                    name: 'EndDate',
                    allowBlank: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '地址 <span style="color:red">*</span>',
                    value: this.record.get('Address'),
                    name: 'Address',
                    allowBlank: false,
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '长',
                    xtype:'numberfield',
                    value: this.record.get('VLong'),
                    name: 'VLong',
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '宽',
                    xtype: 'numberfield',
                    value: this.record.get('VWide'),
                    name: 'VWide',
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '高',
                    xtype: 'numberfield',
                    value: this.record.get('VHigh'),
                    name: 'VHigh',
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '材质',
                    value: this.record.get('Materials'),
                    name: 'Materials',
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '养护单位',
                    value: this.record.get('Curingunit'),
                    name: 'Curingunit',
                    margin: '10 0 10 50',
                },
                {
                    fieldLabel: '监管单位',
                    value: this.record.get('Superviseunit'),
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
                    value: this.record.get('Photo2'),
                    colspan: 2,
                    width: 545,
                },
                {
                    xtype: 'filefield',
                    id: 'Photo3',
                    name: 'Photo3',
                    margin: '10 0 10 165',
                    buttonText: '选择图片',
                    value: this.record.get('Photo3'),
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
        this.child('form').loadRecord(this.record);
    }
});