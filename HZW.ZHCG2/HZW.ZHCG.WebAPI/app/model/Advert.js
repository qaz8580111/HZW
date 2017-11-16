Ext.define('TianZun.model.Advert', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'ID', type: 'int' },
        {
            name: 'IDType', type: 'string', convert: function (value, record) {
                var idtype = (Array(6).join(0) + record.get('ID')).slice(-6);
                return value + idtype;
            }
        },
        { name: 'TypeID', type: 'int' },
        { name: 'TypeName', type: 'string' },
        { name: 'AdName', type: 'string' },
        { name: 'UnitName', type: 'string' },
        { name: 'UnitPerson', type: 'string' },
        { name: 'UnitPhone', type: 'string' },
        { name: 'Producers', type: 'string' },
        { name: 'Prophone', type: 'string' },
        {
            name: 'State', type: 'string', convert: function (value, record) {
                var state;
                if (new Date(record.get('EndDate')) < new Date()) {
                    state = '<img src="../../Images/已到期.png" />';
                } else if (new Date(record.get('EndDate')) <= new Date((new Date() / 1000 + 86400 * 7) * 1000)) {
                    state = '<img src="../../Images/将到期.png" />';
                } else {
                    state = '<img src="../../Images/未到期.png" />';
                }
                return state;
            }
        },
        { name: 'ExamUnit', type: 'string' },
        {
            name: 'ExamDate', type: 'string', convert: function (value) {
                var examdate = Ext.Date.format(new Date(value), "Y-m-d");
                return examdate;
            }
        },
        {
            name: 'StartDate', type: 'string', convert: function (value) {
                var startdate = Ext.Date.format(new Date(value), "Y-m-d");
                return startdate;
            }
        },
        {
            name: 'EndDate', type: 'string', convert: function (value) {
                var enddate = Ext.Date.format(new Date(value), "Y-m-d");
                return enddate;
            }
        },
        { name: 'Address', type: 'string' },
        { name: 'Photo1', type: 'string' },
        { name: 'Photo2', type: 'string' },
        { name: 'Photo3', type: 'string' },
        { name: 'Photo4', type: 'string' },
        { name: 'FileName1', type: 'string' },
        { name: 'FileName2', type: 'string' },
        { name: 'FileName3', type: 'string' },
        { name: 'FilePath1', type: 'string' },
        { name: 'FilePath2', type: 'string' },
        { name: 'FilePath3', type: 'string' },
        { name: 'Grometry', type: 'string' },
        { name: 'Volume', type: 'number' },
        { name: 'VLong', type: 'number' },
        { name: 'VWide', type: 'number' },
        { name: 'VHigh', type: 'number' },
        { name: 'Materials', type: 'string' },
        { name: 'Curingunit', type: 'string' },
        { name: 'Superviseunit', type: 'string' },
        { name: 'Createtime', type: 'string' },
        { name: 'Createuserid', type: 'int' },
        { name: 'Status', type: 'int' },
        { name: 'Remark', type: 'string' },
    ]
});
