Ext.define('TianZun.model.Categors', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'ID', type: 'int' },
        { name: 'Name', type: 'string' },
        {
            name: 'createdTime', type: 'string', convert: function (value) {
                var enddate = Ext.Date.format(new Date(value), "Y-m-d");
                return enddate;
            }
        },
        { name: 'isonline', type: 'int'},
        {
            name: 'isonlinestring', type: 'string', convert: function (value,record) {
                if (record.get('isonline') == 1) {
                    return '上线';
                }
                else {
                    return '下线';
                }
            }
        },
        { name: 'BigID', type: 'int' },
        { name: 'BigName', type: 'string' },
        { name: 'Createuserid', type: 'int' },
        { name: 'SeqNo', type: 'int' }
    ]
});
