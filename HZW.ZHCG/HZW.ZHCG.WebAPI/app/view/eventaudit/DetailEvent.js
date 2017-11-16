Ext.define('TianZun.view.EventAudit.DetailEvent', {
    extend: 'Ext.panel.Panel',
    alias: 'EventAudit.DetailEvent',
    title: '<font style="color:black;font-weight:initial;">事件审核 > 事件详情</font>',
    header: {
        cls: 'x-panel-header-white'
    },
    layout: 'hbox',
    initComponent: function () {
        Ext.apply(this, {
            region: 'center',
            layout: {
                type: 'hbox',
                align: 'middle ',
                pack: 'center'
            },
            items: [
                {
                    xtype: 'form',
                    region: 'center',
                    border: false,
                    style: {
                        textAlign: 'center',
                    },
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
                        width: 500
                    },
                    items: [
                    {
                        fieldLabel: '标题',
                        name: 'Name',
                        xtype: 'textfield',
                        allowBlank: false,
                        margin: '20 50 10 50',
                        value: '事件标题'
                    },
                    {
                        fieldLabel: '上报时间',
                        name: 'Name',
                        xtype: 'textfield',
                        allowBlank: false,
                        margin: '20 50 10 50',
                        value: '2016-11-11'
                    }, {
                        fieldLabel: '上报人',
                        name: 'Name',
                        xtype: 'textfield',
                        allowBlank: false,
                        margin: '20 50 10 50',
                        value: '柳金元'
                    }, {
                        fieldLabel: '来源',
                        name: 'Name',
                        xtype: 'textfield',
                        allowBlank: false,
                        margin: '20 50 10 50',
                        value: '金元手机上报'
                    }, {
                        fieldLabel: '联系方式',
                        name: 'Name',
                        xtype: 'textfield',
                        allowBlank: false,
                        colspan: 2,
                        margin: '20 50 10 50',
                        value: 'NB74110'
                    }, {
                        fieldLabel: '内容',
                        name: 'MaintenanceTypeId',
                        xtype: 'textfield',
                        displayField: 'MtypeName',
                        valueField: 'MtypeID',
                        editable: false,
                        allowBlank: false,
                        margin: '10 50 10 50',
                        colspan: 2,
                        width: 790,
                        value: 'neirong 1111111111111111'
                    },
                    {
                        xtype: 'panel',
                        name: 'ImageInfo',
                        border: false,
                        bodyBorder: false,
                        colspan: 2,
                        width: 760,
                        layout: {
                            type: 'hbox',
                            padding: '5',
                            align: 'left'
                        },
                        items: [
                            {
                                xtype: 'label',
                                text: '照片',
                                margin: '10 20 10 80'
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
                                //listeners: {
                                //    change: 'agingClassAddButton'
                                //}
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
                                //listeners: {
                                //    change: 'agingClassAddButton'
                                //}
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
                                //listeners: {
                                //    change: 'agingClassAddButton'
                                //}
                            }
                        ],
                    },
                    {
                        xtype: 'panel',
                        name: 'Geogery',
                        border: false,
                        bodyBorder: false,
                        colspan: 2,
                        width: 1000,
                        layout: {
                            type: 'hbox',
                            padding: '5',
                            align: 'left'
                        },
                        items: [{
                            xtype: 'label',
                            text: '地理位置',
                            margin: '10 20 10 80'
                        },
                        {
                            id: 'EVENT_COORDINATE_ID',
                            name: 'Name',
                            xtype: 'textfield',
                            colspan: 2,
                            width: 790,
                            allowBlank: false,
                            listeners: {
                                focus: function () {
                                    CreateAarcgisMap('EVENT_COORDINATE_ID', '事件坐标', 1, 1, '');
                                }
                            }
                        }]
                    }, {
                        xtype: 'panel',
                        border: false,
                        colspan: 2,
                        padding: '0 10 0 0',
                        layout: {
                            type: 'hbox',
                            pack: 'start',
                            //align: 'stretch'
                        },
                        items: [
                            {
                                xtype: 'panel',
                                border: false,
                                html: '<div style=" text-align:right;float:left; "></div>'
                            },
                            {
                                xtype: 'panel',
                                border: false,
                                flex: 1,
                                //html: '<div">  <input type="button" value="提交" style="background:#56A6E3; border:0px solid; width:80px; height:30px; color:white" />&nbsp;&nbsp;<input type="button" value="取消" style="background:#7E807F; border:0px solid; width:80px; height:30px; color:white"/></div>'
                                html: '<div"><input type="button" value="取消" style="background:#7E807F; border:0px solid; width:80px; height:30px; color:white"/></div>'
                            }
                        ]
                    }


                    ],

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