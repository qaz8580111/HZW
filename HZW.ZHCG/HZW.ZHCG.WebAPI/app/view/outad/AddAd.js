Ext.define('TianZun.view.OutAD.AddAd', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.addad',
    title: '<font style="color:black;font-weight:initial;">户外广告 > 新增广告</font>',
    header: {
        cls: 'x-panel-header-white'
    },
    layout: 'hbox',
    initComponent: function () {
        Ext.apply(this, {
            layout: 'border',
            items: [
                {
                    xtype: 'form',
                    region: 'center',
                    border: false,
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
                        width: 340
                    },
                    items: [
                    {
                        fieldLabel: '编号<span style="color:red">*</span>',
                        name: 'Name',
                        xtype: 'textfield',
                        allowBlank: false,
                        margin: '20 50 10 50',
                    },
                    {
                        fieldLabel: '名称 <span style="color:red">*</span>',
                        name: 'UnitName',
                        xtype: 'combo',
                        displayField: 'Name',
                        valueField: 'ID',
                        editable: false,
                        allowBlank: false,
                        margin: '10 50 10 50',
                    },
                    {
                        fieldLabel: '类型 <span style="color:red">*</span>',
                        name: 'Roadid',
                        xtype: 'combo',
                        displayField: 'Name',
                        valueField: 'ID',
                        editable: false,
                        allowBlank: false,
                        margin: '10 50 10 50',
                    },
                    {
                        fieldLabel: '所属企业 <span style="color:red">*</span>',
                        xtype: 'textfield',
                        name: 'Address',
                        margin: '10 50 10 50',
                    },
                    {
                        fieldLabel: '制作商 <span style="color:red">*</span>',
                        name: 'MaintenanceTypeId',
                        xtype: 'textfield',
                        displayField: 'MtypeName',
                        valueField: 'MtypeID',
                        editable: false,
                        allowBlank: false,
                        margin: '10 50 10 50',
                        colspan: 2,
                        width: 790,
                    },
                    {
                        fieldLabel: '审批单位 <span style="color:red">*</span>',
                        name: 'SourceId',
                        xtype: 'combo',
                        displayField: 'SourceName',
                        valueField: 'SourceID',
                        editable: false,
                        allowBlank: false,
                        margin: '10 50 10 50',
                    },
                    {
                        fieldLabel: '审批日期 <span style="color:red">*</span>',
                        xtype: 'datefield',
                        name: 'ProcessingTime',
                        allowBlank: false,
                        margin: '10 50 10 50',
                    },
                    {
                        fieldLabel: '开始日期 <span style="color:red">*</span>',
                        xtype: 'datefield',
                        id: 'ProcessoutTime',
                        name: 'ProcessoutTime',
                        margin: '10 50 10 50',
                    },
                    {
                        fieldLabel: '到期日期 <span style="color:red">*</span>',
                        xtype: 'datefield',
                        name: 'Description',
                        margin: '10 50 10 50',
                    },
                    {
                        fieldLabel: '地址 <span style="color:red">*</span>',
                        name: 'MaintenanceTypeId',
                        xtype: 'combo',
                        displayField: 'MtypeName',
                        valueField: 'MtypeID',
                        editable: false,
                        allowBlank: false,
                        margin: '10 50 10 50',
                        colspan: 2,
                        width: 790,
                    },
                    {
                        xtype: 'panel',
                        name: 'ImageInfo',
                        border: false,
                        bodyBorder: false,
                        colspan: 2,
                        margin: '10 0 10 0',
                        width: 760,
                        layout: {
                            type: 'hbox',
                            align: 'left'
                        },
                        items: [
                            {
                                xtype: 'label',
                                text: '照片',
                                margin: '10 50 10 50'
                            },
                            {
                                xtype: 'filefield',
                                buttonOnly: true,
                                width: 85,
                                height: 85,
                                buttonText: '<span style="background-image:url(Images/buttonAdd.png);position:absolute; width:89px; height:89px; top:-5px; left:-5px"></span>',
                                margin: '2',
                                buttonConfig: {
                                    width: 85,
                                    height: 85,
                                    alain: 'center',
                                    border: 0,
                                    shadow: false,
                                },
                                //handler: function (obj,action) {
                                //    obj.up().add(Ext.create('Ext.button.Button'));
                                //},
                                listeners: {
                                    change: 'agingClassAddButton'
                                }
                            },
                            {
                                xtype: 'filefield',
                                buttonOnly: true,
                                width: 85,
                                height: 85,
                                margin: '10 50 10 50',
                                buttonText: '<span style="background-image:url(Images/buttonAdd.png);position:absolute; width:89px; height:89px; top:-5px; left:-5px"></span>',
                                margin: '2',
                                buttonConfig: {
                                    width: 85,
                                    height: 85,
                                    alain: 'center',
                                    border: 0,
                                    shadow: false,
                                },
                                //handler: function (obj,action) {
                                //    obj.up().add(Ext.create('Ext.button.Button'));
                                //},
                                listeners: {
                                    change: 'agingClassAddButton'
                                }
                            },
                            {
                                xtype: 'filefield',
                                buttonOnly: true,
                                width: 85,
                                height: 85,
                                buttonText: '<span style="background-image:url(Images/buttonAdd.png);position:absolute; width:89px; height:89px; top:-5px; left:-5px"></span>',
                                margin: '2',
                                buttonConfig: {
                                    width: 85,
                                    height: 85,
                                    alain: 'center',
                                    border: 0,
                                    shadow: false,
                                },
                                //handler: function (obj,action) {
                                //    obj.up().add(Ext.create('Ext.button.Button'));
                                //},
                                listeners: {
                                    change: 'agingClassAddButton'
                                }
                            }
                        ],                        
                    },
                    {
                        xtype: 'panel',
                        name: 'FileInfo',
                        border: false,
                        bodyBorder: false,
                        colspan: 2,
                        width: 760,
                        margin:'10 0 10 0',
                        layout: {
                            type: 'hbox',
                            align: 'left'
                        },
                        items: [
                            {
                                xtype: 'label',
                                text: '文件',
                                margin: '10 50 10 50'
                            },
                            {
                                xtype: 'filefield',
                                buttonOnly: true,
                                width: 85,
                                height: 85,
                                buttonText: '<span style="background-image:url(Images/buttonAdd.png);position:absolute; width:89px; height:89px; top:-5px; left:-5px"></span>',
                                margin: '2',
                                buttonConfig: {
                                    width: 85,
                                    height: 85,
                                    alain: 'center',
                                    border: 0,
                                    shadow: false,
                                },
                                //handler: function (obj,action) {
                                //    obj.up().add(Ext.create('Ext.button.Button'));
                                //},
                                listeners: {
                                    change: 'agingClassAddButton'
                                }
                            },
                            {
                                xtype: 'filefield',
                                buttonOnly: true,
                                width: 85,
                                height: 85,
                                margin: '10 50 10 50',
                                buttonText: '<span style="background-image:url(Images/buttonAdd.png);position:absolute; width:89px; height:89px; top:-5px; left:-5px"></span>',
                                margin: '2',
                                buttonConfig: {
                                    width: 85,
                                    height: 85,
                                    alain: 'center',
                                    border: 0,
                                    shadow: false,
                                },
                                //handler: function (obj,action) {
                                //    obj.up().add(Ext.create('Ext.button.Button'));
                                //},
                                listeners: {
                                    change: 'agingClassAddButton'
                                }
                            },
                            {
                                xtype: 'filefield',
                                buttonOnly: true,
                                width: 85,
                                height: 85,
                                buttonText: '<span style="background-image:url(Images/buttonAdd.png);position:absolute; width:89px; height:89px; top:-5px; left:-5px"></span>',
                                margin: '2',
                                buttonConfig: {
                                    width: 85,
                                    height: 85,
                                    alain: 'center',
                                    border: 0,
                                    shadow: false,
                                },
                                //handler: function (obj,action) {
                                //    obj.up().add(Ext.create('Ext.button.Button'));
                                //},
                                listeners: {
                                    change: 'agingClassAddButton'
                                }
                            }
                        ],
                    },
                    {
                        xtype: 'panel',
                        name: 'Geogery',
                        border: false,
                        bodyBorder: false,
                        colspan: 2,
                        margin: '10 0 10 0',
                        width: 760,
                        layout: {
                            type: 'hbox',
                            align: 'left'
                        },
                        items: [{
                            xtype: 'label',
                            text: '地理位置',
                            margin: '10 25 10 50'
                        },
                        {
                            id: 'EVENT_COORDINATE_ID',
                            name: 'Name',
                            xtype: 'textfield',
                            colspan: 2,
                            width: 760,
                            allowBlank: false,
                            listeners: {
                                focus: function () {
                                    CreateAarcgisMap('EVENT_COORDINATE_ID', '事件坐标', 1, 1, '');
                                }
                            }
                        }]
                    }
                    ],
                    buttons: [{
                        text: '确认提交',
                        handler: 'onAddOK'
                    }, {
                        text: '取消',
                        handler: 'onClose'
                    }]
                }
            ]
        });
        this.callParent();
    }
});

//创建地图
function CreateAarcgisMap(input_id, title, model, show_model, map_data) {
    arcgisMapLoaded = function () {
        if (show_model == 1) {
            addMapPoint(map_data);
        } else if (show_model == 2) {
            addMapPolyline(map_data);
        } else if (show_model == 3) {
            addMapPolygon(map_data);
        }
    }
    var mapWindow = new Ext.Window({
        title: title,
        layout: 'fit',
        autoShow: true,
        modal: true,
        html: "<div style='width:600px;height:400px'><object id='arcgisMapApp' data='data:application/x-silverlight-2,' type='application/x-silverlight-2' width='100%' height='100%'><param name='source' value='ClientBin/ArcGISMapApp.xap' /><param name='onError' value='onSilverlightError' /><param name='background' value='white' /><param name='minRuntimeVersion' value='5.0.61118.0' /><param name='autoUpgrade' value='true' /><param name='initParams' value='mode=" + model + ",url=http://172.18.13.180/gisProxy/Tile/ArcGISFlex/HZTDTVECTORBLEND.gis,minx=120.082421100261,miny=30.1979580315871,maxx=120.280283961832,maxy=30.2858486565871' /></object></div>",
        buttons: [
            {
                text: '确定',
                hidden: function () {
                    if (model == 0) {
                        return true;
                    }
                }(),
                handler: function () {
                    var mapData = getMapData();
                    Ext.getCmp(input_id).setValue(mapData);
                    mapWindow.close();
                }
            }, {
                text: '关闭',
                handler: function () {
                    mapWindow.close();
                }
            }
        ]
    });
    mapWindow.show();
}
