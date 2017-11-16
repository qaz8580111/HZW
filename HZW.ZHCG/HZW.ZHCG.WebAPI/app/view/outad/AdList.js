var centerstore = Ext.create('Ext.data.Store', {
    fields: ['Code', 'Name', 'Type', 'State', 'Action'],
    data: {
        'items': [
            { 'Code': '1', 'Name': '11', 'Type': '111', 'State':'1111', 'Action': '11111' },
            { 'Code': '2', 'Name': '22', 'Type': '222', 'State': '2222', 'Action': '22222' },
            { 'Code': '3', 'Name': '33', 'Type': '333', 'State': '3333', 'Action': '33333' },
            { 'Code': '4', 'Name': '44', 'Type': '444', 'State': '4444', 'Action': '44444' }
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

Ext.define('TianZun.view.OutAD.AdList', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.adlist',
    title: '<font style="color:black;font-weight:initial;">户外广告 > 广告列表</font>',
    header: {
        cls: 'x-panel-header-white'
    },
    layout: 'hbox',
    initComponent: function () {
        Ext.apply(this, {
            layout: 'border',
            items: [{
                xtype: 'grid',
                region: 'center',
                columns: [
                        { header: '编号', dataIndex: 'Code', width: '15%', align: 'center'},
                        { header: '名称', dataIndex: 'Name', width: '25%', align: 'center' },
                        { header: '类型', dataIndex: 'Type', width: '15%', align: 'center' },
                        { header: '状态', dataIndex: 'State', width: '15%', align: 'center' },
                        { header: '操作', dataIndex: 'Action', width: '30%', align: 'center', renderer: renderCall }
                ],
                store: centerstore,
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
                   }, '状态',
                   {
                       xtype: 'combo',
                       id: 'AdState',
                       width: 120
                   },
                   {
                       text: '查询',
                       handler: 'onQuery'
                   }
                ],
                bbar: {
                    xtype: 'pagingtoolbar',
                    store: centerstore,
                    displayInfo: true
                },
            }]
        });
        this.callParent();
    }
});

function renderCall(value, p, record) {
    var html = '<div style="text-align: center;">' +
               '<span class="BtnBase BtnLook" onclick=Look(' + record.get('Action') + ');>查看</span>' +
               '<span class="BtnBase BtnEdit" onclick=Edit(' + record.get('Action') + ');>修改</span>' +
               '<span class="BtnBase BtnDelete" onclick=Delete(' + record.get('Action') + ');>删除</span></div> ';
    return html;
}

function Look(id) {
    alert("查看id=" + id);
}
function Edit(id) {
    alert("修改id=" + id);
}
function Delete(id) {
    alert("删除id=" + id);
}