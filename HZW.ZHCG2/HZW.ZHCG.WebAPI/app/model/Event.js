Ext.define('TianZun.model.Event', {
    extend: 'Ext.data.Model',
    fields: [
        //基本信息
        { name: 'event_id', type: 'string' },//编号类型   案件编号  id_type+event_id（MH000001）
       
        { name: 'title', type: 'string' },
         {
             name: 'reporttime', type: 'string', convert: function (value) {
                 var enddate = Ext.Date.format(new Date(value), "Y-m-d H:i");
                 return enddate;
             }
         },
        { name: 'reportperson', type: 'string' },//上报人
        { name: 'source', type: 'string' },//来源
        { name: 'contact', type: 'string' },//联系方式
        { name: 'content', type: 'string' },//内容
        { name: 'grometry', type: 'string' },//地理位置

        //审核处理信息
        {
            name: 'isexamine', type: 'string',
            convert: function (value) {
                var isexamine;
                if (value==1) {
                    isexamine = '录入';
                } else if (value == 2) {
                    isexamine = '作废';
                } else {
                    isexamine = '未审核';
                }
                return isexamine;
            }
        },//是否审核(1是 2否)
        { name: 'inputperson', type: 'int' },//审核人ID
        { name: 'inputpersonname', type: 'string' },//审核人姓名
        //{ name: 'examinetime', type: 'date' },//审核时间
         {
             name: 'inputtime', type: 'string', convert: function (value) {
                 var enddate = Ext.Date.format(new Date(value), "Y-m-d H:i");
                 return enddate;
             }
         },
        { name: 'inputcontent', type: 'string' },//审核意见
       

       { name: 'invalisperson', type: 'int' },//作废人ID
        { name: 'invalispersonname', type: 'string' },//作废人姓名
        //{ name: 'examinetime', type: 'date' },//作废时间
         {
             name: 'invalistime', type: 'string', convert: function (value) {
                 var enddate = Ext.Date.format(new Date(value), "Y-m-d H:i");
                 return enddate;
             }
         },
        { name: 'invaliscontent', type: 'string' },//作废意见
       
        {
            name: 'ispush', type: 'string',
            convert: function (value) {
                var ispush;
                if (value == 1) {
                    ispush = '已推送';
                } else {
                    ispush = '未推送';
                }
                return ispush;
            }
        },//是否推送(1是 2否)

        //图片地址
        { name: 'photo1', type: 'string' },
        { name: 'photo2', type: 'string' },
        { name: 'photo3', type: 'string' },




    ],
});
