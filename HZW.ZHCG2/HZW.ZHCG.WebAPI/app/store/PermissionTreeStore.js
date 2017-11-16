Ext.define('TianZun.store.PermissionTreeStore', {
    extend: 'Ext.data.TreeStore',
    model: 'TianZun.model.Permission',
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/Permission/GetTreePermissions'
    }
});