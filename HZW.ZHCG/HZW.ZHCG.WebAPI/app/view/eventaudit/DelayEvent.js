var DelayEventstore = Ext.create('Ext.data.Store', {
    fields: ['Code', 'Name', 'Type', 'State', 'Action'],
    data: {
        'items': [
            { 'Code': '1', 'Name': '待审核标题1', 'Type': '待审核类型1', 'State': '待审核时间1' },
            { 'Code': '2', 'Name': '待审核标题2', 'Type': '待审核类型2', 'State': '待审核时间2' },
            { 'Code': '3', 'Name': '待审核标题3', 'Type': '待审核类型3', 'State': '待审核时间3' },
            { 'Code': '4', 'Name': '待审核标题4', 'Type': '待审核类型4', 'State': '待审核时间4' }
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




Ext.define('TianZun.view.EventAudit.DelayEvent', {
    extend: 'Ext.panel.Panel',
    alias: 'EventAudit.DelayEvent',
    title: '<font style="color:black;font-weight:initial;">事件审核 > 待审核列表</font>',
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
                       { header: '编号', dataIndex: 'Code', width: '10%', align: 'center' },
                     { header: '名称', dataIndex: 'Name', width: '30%', align: 'center' },
                     { header: '类型', dataIndex: 'Type', width: '20%', align: 'center' },
                     { header: '状态', dataIndex: 'State', width: '20%', align: 'center' },
                     { header: '操作', dataIndex: 'Action', width: '20%', align: 'center', renderer: DelayEventCall }
                ],
                store: DelayEventstore,
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
                   }, '开始时间', {
                       xtype: 'datefield',
                       width: 180,
                       id: 'from_date',
                       value: new Date()  // limited to the current date or prior
                   }, '结束时间', {
                       xtype: 'datefield',
                       width: 180,
                       id: 'to_date',
                       value: new Date()  // defaults to today
                   },
                   {
                       text: '查询',
                       handler: 'onQuery'
                   }
                ],
                bbar: {
                    xtype: 'pagingtoolbar',
                    store: DelayEventstore,
                    displayInfo: true
                },
            }]
        });
        this.callParent();
    }
});

function DelayEventCall(value, p, record) {
    var html = '<div style="text-align: center;">' +
               '<span class="BtnBase BtnLook" onclick=DelayEventLook(' + record.get('Action') + ');>查看</span>' +
               '<span class="BtnBase BtnEdit" onclick=DelayEventEdit(' + record.get('Action') + ');>审核</span>' +
               '<span class="BtnBase BtnDelete" onclick=DelayEventDelete(' + record.get('Action') + ');>作废</span></div> ';
    return html;
}

function DelayEventLook(id) {
    var content = Ext.create('TianZun.view.EventAudit.DetailEvent');
    var view = Ext.ComponentQuery.query('viewport')[0];
    var panel = view.items.getAt(3)
    view.remove(panel)
    content.region = 'center';
    view.add(content);
}
function DelayEventEdit(id) {
    alert("审核id=" + id);
}
function DelayEventDelete(id) {
    alert("作废id=" + id);
}