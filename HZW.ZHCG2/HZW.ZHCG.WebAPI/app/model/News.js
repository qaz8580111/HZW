Ext.define('TianZun.model.News', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'articleID', type: 'int' },
        { name: 'categoryid_bid', type: 'int' },
        { name: 'categoryID', type: 'int' },
        { name: 'title', type: 'string' },
        { name: 'content', type: 'string' },
        { name: 'author', type: 'string' },
        { name: 'createUserName', type: 'string' },
        { name: 'createUserID', type: 'int' },
       {
           name: 'createdTime', type: 'string', convert: function (value) {
               var enddate = Ext.Date.format(new Date(value), "Y-m-d H:i:s");
               return enddate;
           }
       },
       {
           name: 'isonline', type: 'string', convert: function (value) {
               if (value == 1) {
                   return '发布';
               }
               else {
                   return '未发布';
               }
           }
       },

        { name: 'fileName', type: 'string' },

         { name: 'categoryBName', type: 'string' },
        { name: 'categorySName', type: 'string' },
    ]
});
