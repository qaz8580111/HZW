Ext.define('TianZun.store.UserTypePageStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.UserType',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/UserType/GetUserTypes',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    }
});