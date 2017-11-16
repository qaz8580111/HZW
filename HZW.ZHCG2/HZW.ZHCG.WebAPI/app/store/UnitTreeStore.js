Ext.define('TianZun.store.UnitTreeStore', {
    extend: 'Ext.data.TreeStore',
    model: 'TianZun.model.Unit',
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/Unit/GetTreeUnits'
    }
});