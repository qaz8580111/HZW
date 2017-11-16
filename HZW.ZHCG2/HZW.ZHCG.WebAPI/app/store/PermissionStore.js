Ext.define('TianZun.store.PermissionStore', {
    extend: 'Ext.data.Store',

    pageSize: 0,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/Permission/GetPermissions'
    },
    fields: ['Code', 'Name']
});