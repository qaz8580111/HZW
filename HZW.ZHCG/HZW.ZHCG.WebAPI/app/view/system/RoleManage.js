var Rolestore = Ext.create('Ext.data.Store', {
    fields: ['ROLEID', 'ROLENAME', 'DESCRIPTION', 'STATUSID', 'Action'],
    data: {
        'items': [
            { 'ROLEID': '1', 'ROLENAME': '超级管理员', 'DESCRIPTION': '拥有本系统最高权限', 'STATUSID': '已启用' },
            { 'ROLEID': '1', 'ROLENAME': '超级管理员', 'DESCRIPTION': '拥有本系统最高权限', 'STATUSID': '已启用' },
            { 'ROLEID': '1', 'ROLENAME': '超级管理员', 'DESCRIPTION': '拥有本系统最高权限', 'STATUSID': '已启用' },
            { 'ROLEID': '1', 'ROLENAME': '超级管理员', 'DESCRIPTION': '拥有本系统最高权限', 'STATUSID': '已启用' }
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

Ext.define('TianZun.view.System.RoleManage', {
    extend: 'Ext.panel.Panel',
    alias: 'System.RoleManage',
    title: '<font style="color:black;font-weight:initial;">角色管理 > 角色管理列表</font>',
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
                        { header: '角色ID', dataIndex: 'ROLEID', width: '20%' },
                        { header: '角色名称', dataIndex: 'ROLENAME', width: '30%' },
                        { header: '角色说明', dataIndex: 'DESCRIPTION', width: '30%' },
                         { header: '角色状态', dataIndex: 'STATUSID', width: '20%', align: 'center', renderer: RoleManageCall }
                    ],
                    store: Rolestore,
                    tbar: [
                  '角色名称',
                  {
                      xtype: 'textfield',
                      id: 'ROLENAME',
                      width: 120
                  }, '状态',
                  {
                      xtype: 'combo',
                      id: 'STATUSID',
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

function RoleManageCall(value, p, record) {
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