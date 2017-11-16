Ext.define('TianZun.view.Shop.AddShop', {
    extend: 'Ext.panel.Panel',
    alias: 'Shop.AddShop',

    initComponent: function () {

        Ext.apply(this, {
            items: [
                {
                    xtype: 'panel',
                    height: 30,
                    border: false,
                    html: '<div style="line-height:28px;border-bottom:1px solid #808080;">&nbsp;沿街店家&nbsp;>&nbsp;新增店家</div>',
                },
                {
                    xtype: 'panel',
                    border: false,
                    height: 500,
                    items: [
                        {
                            xtype: 'form',
                            border: false,
                            bodyPadding: 10,
                            layout: {
                                type: 'table',
                                columns: 2,
                            },
                            fieldDefaults: {
                                labelAlign: 'right',
                                labelWidth: 125
                            },
                            items: [
                                {
                                    xtype: 'textfield',
                                    fieldLabel: '编号',
                                    name: 'Name',
                                    style: "width:100%; padding-right:10px",
                                },
                                {
                                    xtype: 'textfield',
                                    fieldLabel: '名称',
                                    name: 'Name',
                                    style: "width:100%; padding-right:10px",
                                },
                                {
                                    xtype: 'textfield',
                                    fieldLabel: '类型',
                                    style: "width:100%; padding-right:10px",
                                },
                                 {
                                     xtype: 'textfield',
                                     fieldLabel: '负责人',
                                     style: "width:100%; padding-right:10px",
                                 },
                                 {
                                     xtype: 'textfield',
                                     fieldLabel: '实际经营者姓名',
                                     style: "width:100%; padding-right:10px",
                                 },
                                  {
                                      xtype: 'textfield',
                                      fieldLabel: '联系方式',
                                      style: "width:100%; padding-right:10px",
                                  },
                                  {
                                      xtype: 'textfield',
                                      fieldLabel: '注册号',
                                      style: "width:100%; padding-right:10px",
                                  },
                                  {
                                      xtype: 'textfield',
                                      fieldLabel: '注册人姓名',
                                      style: "width:100%; padding-right:10px",
                                  },
                                  {
                                      xtype: 'textfield',
                                      fieldLabel: '地址',
                                      colspan: 2,
                                      style: "width:100%; padding-right:10px",
                                  },
                                  {
                                      xtype: 'panel',
                                      border: false,
                                      height: 200,
                                      colspan: 2,
                                      padding: '0 10 0 0',
                                      layout: {
                                          type: 'hbox',
                                          pack: 'start',
                                          align: 'stretch'
                                      },
                                      items: [
                                          {
                                              xtype: 'panel',
                                              border: false,
                                              width: 125,
                                              html: '<div style="width:100%; height:200px;text-align:right;float:left; ">地图位置:</div>'
                                          },
                                          {
                                              xtype: 'panel',
                                              border: false,
                                              flex: 1,
                                              height: 100,
                                              html: '<div style="height:200px;margin-left:4px; background-color:#3892D4"></div>'
                                          }
                                      ]

                                  },
                                  {
                                      xtype: 'panel',
                                      border: false,
                                      height: 200,
                                      colspan: 2,
                                      padding: '0 10 0 0',
                                      layout: {
                                          type: 'hbox',
                                          pack: 'start',
                                          align: 'stretch'
                                      },
                                      items: [
                                          {
                                              xtype: 'panel',
                                              border: false,
                                              width: 125,
                                              html: '<div style="width:100%; height:200px;text-align:right;float:left; "></div>'
                                          },
                                          {
                                              xtype: 'panel',
                                              border: false,
                                              flex: 1,
                                              height: 100,
                                              html: '<div style="height:200px; margin-left:4px;">  <input type="button" value="提交" style="background:#56A6E3; border:0px solid; width:80px; height:30px; color:white" />&nbsp;&nbsp;<input type="button" value="取消" style="background:#7E807F; border:0px solid; width:80px; height:30px; color:white"/></div>'
                                          }
                                      ]
                                  }
                            ],
                        }
                    ]
                }
            ]
        });

        this.callParent();
    }
});
