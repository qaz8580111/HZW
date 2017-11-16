var Shopstore = Ext.create('Ext.data.Store', {
    fields: ['id_type', 'store_name', 'type_name', 'Action'],
    data: {
        'items': [
            { 'id_type': 'CS001', 'store_name': '世纪联华', 'type_name': '超市' },
            { 'id_type': 'CS002', 'store_name': '全家超市', 'type_name': '超市' },
            { 'id_type': 'CS003', 'store_name': '可迪超市', 'type_name': '超市' },
            { 'id_type': 'CS004', 'store_name': '物美超市', 'type_name': '超市' }
        ]
    },
    proxy: {
        type: 'memory',
        reader: {
            type: 'json',
            root: 'items'
        }
    }
});

Ext.define('TianZun.view.Shop.ShopList', {
    extend: 'Ext.panel.Panel',
    alias: 'Shop.ShopList',
    title: '<font style="color:black;font-weight:initial;">事件审核 > 全部事件列表</font>',
    header: {
        cls: 'x-panel-header-white'
    },
    layout: 'hbox',
    initComponent: function () {

        Ext.apply(this, {
            layout: 'border',
            items: [
                {
                    xtype: 'grid',
                    region: 'center',
                    sortableColumns: false,
                    viewConfig: {
                        enableTextSelection: true
                    },
                    columns: [
                        { header: '编号', dataIndex: 'id_type', width: '20%' },
                        { header: '名称', dataIndex: 'store_name', width: '30%' },
                        { header: '类型', dataIndex: 'type_name', width: '30%' },
                         { header: '操作', dataIndex: 'Action', width: '20%', align: 'center', renderer: AllShopCall }
                    ],
                    store: Shopstore,
                    tbar: [
                  '编号',
                  {
                      xtype: 'textfield',
                      id: 'AdID',
                      width: 120
                  }, '名称',
                  {
                      xtype: 'textfield',
                      id: 'AdName',
                      width: 120
                  }, '类型',
                  {
                      xtype: 'combo',
                      id: 'AdType',
                      width: 120
                  },
                  {
                      text: '查询',
                      handler: 'onQuery'
                  }
                    ],
                    bbar: {
                        xtype: 'pagingtoolbar',
                        store: Shopstore,
                        displayInfo: true
                    }
                }
            ]
        });

        this.callParent();
    }
});

function AllShopCall(value, p, record) {
    var html = '<div style="text-align: center;">' +
             '<span class="BtnBase BtnLook" onclick=ShopLook(' + record.get('Action') + ');>查看</span>' +
             '<span class="BtnBase BtnEdit" onclick=ShopLook(' + record.get('Action') + ');>修改</span>' +
             '<span class="BtnBase BtnDelete" onclick=del(' + record.get('Action') + ');>删除</span></div> ';
    return html;
}
function ShopLook(id) {
    var content = Ext.create('TianZun.view.Shop.ShopLook');
    var view = Ext.ComponentQuery.query('viewport')[0];
    var panel = view.items.getAt(3)
    view.remove(panel)
    content.region = 'center';
    view.add(content);
}
function del(val) {
    return confirm("确认删除吗?");
}