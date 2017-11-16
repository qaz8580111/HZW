Ext.define('TianZun.store.UnitTypeStore', {
    extend: 'Ext.data.Store',
    pageSize: 0,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/Unit/GetUnitTypes'
    },
    fields: ['ID', 'Name']
});