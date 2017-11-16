Ext.define('TianZun.store.DelayEventStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.Event',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/Event/GetDelayEventList',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    }
});