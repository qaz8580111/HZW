Ext.define('TianZun.store.AllEventStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.Event',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/Event/GetAllEventList',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    }
});