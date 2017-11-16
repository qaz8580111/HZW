Ext.define('TianZun.store.RoleManageStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.RoleManage',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/BaseRoles/GetRoles',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    },
    autoLoad: true

});
