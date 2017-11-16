Ext.define('TianZun.store.PermissionPageStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.Permission',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/Permission/GetPermissions',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    }
});