Ext.define('TianZun.store.UserStore', {
    extend: 'Ext.data.Store',
    pageSize: 0,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/User/GetUser'
    },
    autoLoad: false,
    fields: ['ID', 'UserName']
});