Ext.define('TianZun.store.UnitTypePageStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.UnitType',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/UnitType/GetUnitTypes',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    }
});