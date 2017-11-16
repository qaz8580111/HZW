var AllEventstore = Ext.create('Ext.data.Store', {
    fields: ['Code', 'Name', 'Type', 'State', 'Action'],
    data: {
        'items': [
            { 'Code': '1', 'Name': '全部事件标题1', 'Type': '全部事件类型1', 'State': '全部事件时间1' },
            { 'Code': '2', 'Name': '全部事件标题2', 'Type': '全部事件类型2', 'State': '全部事件时间2' },
            { 'Code': '3', 'Name': '全部事件标题3', 'Type': '全部事件类型3', 'State': '全部事件时间3' },
            { 'Code': '4', 'Name': '全部事件标题4', 'Type': '全部事件类型4', 'State': '全部事件时间4' }
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




Ext.define('TianZun.view.EventAudit.AllEvent', {
    extend: 'Ext.panel.Panel',
    alias: 'EventAudit.AllEvent',
    title: '<font style="color:black;font-weight:initial;">事件审核 > 全部事件列表</font>',
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
                     { header: '操作', dataIndex: 'Action', width: '20%', align: 'center', renderer: AllEventCall }
                ],
                store: AllEventstore,
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
                    store: AllEventstore,
                    displayInfo: true
                },
            }]
        });
        this.callParent();
    }
});

function AllEventCall(value, p, record) {
    var html = '<div style="text-align: center;">' +
               '<span class="BtnBase BtnLook" onclick=AllEventLook(' + record.get('Action') + ');>查看</span>' +
               '<span class="BtnBase BtnEdit" onclick=AllEventEdit(' + record.get('Action') + ');>录入</span>' +
               '<span class="BtnBase BtnDelete" onclick=AllEventDelete(' + record.get('Action') + ');>作废</span></div> ';
    return html;
}

function AllEventLook(id) {
    var content = Ext.create('TianZun.view.EventAudit.DetailEvent');
    var view = Ext.ComponentQuery.query('viewport')[0];
    var panel = view.items.getAt(3)
    view.remove(panel)
    content.region = 'center';
    view.add(content);
}
function AllEventEdit(id) {
    alert("录入id=" + id);
}
function AllEventDelete(id) {
    alert("作废id=" + id);
}