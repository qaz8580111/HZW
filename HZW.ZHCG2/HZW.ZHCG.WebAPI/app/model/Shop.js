Ext.define('TianZun.model.Shop', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'storeid', type: 'int' },
        { name: 'storename', type: 'string' },
        {
            name: 'idtype', type: 'string', convert: function (value, record) {
                var idtype = (Array(6).join(0) + record.get('storeid')).slice(-6);
                return value + idtype;
            }
        },
        { name: 'typeid', type: 'int' },
        { name: 'address', type: 'string' },
        { name: 'person', type: 'string' },
        { name: 'businessperson', type: 'string' },
        { name: 'businesscontact', type: 'string' },
        { name: 'registnum', type: 'int' },
        { name: 'registname', type: 'string' },
        { name: 'registcontact', type: 'string' },
        { name: 'businessscope', type: 'string' },
        {
            name: 'registdate', type: 'string', convert: function (value) {
                var registdate = Ext.Date.format(new Date(value), "Y-m-d");
                return registdate;
            }
        },
        {
            name: 'businessenddate', type: 'string', convert: function (value) {
                var businessenddate = Ext.Date.format(new Date(value), "Y-m-d");
                return businessenddate;
            }
        },
        { name: 'businesslicense', type: 'int' },
        { name: 'healthcard', type: 'int' },
        { name: 'mqsbperson', type: 'string' },
        { name: 'gridnum', type: 'string' },
        { name: 'gridperson', type: 'string' },
        { name: 'gridcontact', type: 'string' },
        { name: 'createuserid', type: 'string' },
        { name: 'grometry', type: 'string' },
        { name: "remark2", type: "string" },
        { name: "remark", type: "string" },
    ]
});
