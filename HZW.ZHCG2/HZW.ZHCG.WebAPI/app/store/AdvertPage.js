Ext.define('TianZun.store.AdvertPage', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.Advert',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/Advert/GetAdverts',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    }
});