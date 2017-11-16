var Shopstore = Ext.create('Ext.data.Store', {
    fields: ['store_id', 'id_type', 'store_name', 'type_name', ],
    data: {
        'items': [
            { 'store_id': '1', 'id_type': 'CS001', 'store_name': '世纪联华', 'type_name': '超市' },
            { 'store_id': '2', 'id_type': 'CS002', 'store_name': '全家超市', 'type_name': '超市' },
            { 'store_id': '3', 'id_type': 'CS003', 'store_name': '可迪超市', 'type_name': '超市' },
            { 'store_id': '4', 'id_type': 'CS004', 'store_name': '物美超市', 'type_name': '超市' }
        ]
    },
    proxy: {
        type: 'memory',
        reader: {
            type: 'json',
            root: 'items'
        }
    }
});