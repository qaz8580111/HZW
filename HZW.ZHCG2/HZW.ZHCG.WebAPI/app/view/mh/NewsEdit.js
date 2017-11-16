Ext.define('TianZun.view.mh.NewsEdit', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.newsEdit',

    title: '编辑新闻',
    layout: 'fit',

    initComponent: function () {
        var me = this;
        Ext.apply(this, {
            items: [
                {
                    xtype: 'form',
                    region: 'center',
                    border: false,
                    width: 880,
                    height: 550,
                    overflowY: 'auto',
                    layout: {
                        type: 'table',
                        columns: 2,
                    },
                    fieldDefaults: {
                        labelAlign: 'left',
                        labelWidth: 110
                    },
                    defaults: {
                        xtype: 'textfield',
                        width: 300
                    },
                    items: [
                        {
                            xtype: 'hidden',
                            name: 'createUserID',
                            value: $.cookie("USER_ID")
                        },
                        {
                            xtype: 'hidden',
                            id: 'hidcontent',
                            name: 'hidcontent',
                            value: this.record.get('content')
                        },
                        {
                            xtype: 'hidden',
                            id: 'articleID',
                            name: 'articleID',
                            value: this.record.get('articleID')
                        },
                        {
                            fieldLabel: '文章标题 <span style="color:red">*</span>',
                            name: 'title',
                            xtype: 'textfield',
                            valueField: 'ID',
                            allowBlank: false,
                            colspan: 2,
                            margin: '10 0 10 50',
                            width: 765,
                            value: this.record.get('title')
                        },
                        {
                            fieldLabel: '栏目大类<span style="color:red">*</span>',
                            xtype: 'combo',
                            name: 'categoryid_bid',
                            store: Ext.create('TianZun.store.NewsBigClassStore'),
                            valueField: 'ID',
                            margin: '10 0 10 50',
                            displayField: 'Name',
                            width: 355,
                            allowBlank: false,
                            listeners: {
                                render: function (combo) {
                                    this.getStore().load();
                                },
                                select: function () {
                                    var cyCombo = Ext.getCmp('ACSmallTypeID');
                                    cyCombo.clearValue();
                                    var cyStore = Ext.create('TianZun.store.NewsSmallClassStore');
                                    cyStore.getProxy().url = 'api/Categors/GetSmallCategors?typeID=' + this.getValue();
                                    cyCombo.bindStore(cyStore, false);
                                    cyStore.load();
                                },
                            },
                            value: this.record.get('categoryid_bid')
                        },
                    {
                        id: 'ACSmallTypeID',
                        xtype: 'combo',
                        fieldLabel: '栏目小类<span style="color:red">*</span>',
                        name: 'categoryID',
                        valueField: 'ID',
                        margin: '10 0 10 50',
                        width: 355,
                        displayField: 'Name',
                        allowBlank: false,
                        listeners: {
                            render: function (combo) {
                                var dlValue = me.record.get("categoryid_bid");

                                var xlStore = Ext.create('TianZun.store.NewsSmallClassStore');
                                xlStore.getProxy().url = 'api/Categors/GetSmallCategors?typeID=' + dlValue;

                                combo.bindStore(xlStore, false);
                                xlStore.load();
                            }
                        },
                        value: this.record.get('categoryID')
                    },

                        {
                            fieldLabel: '标题配图',
                            xtype: 'filefield',
                            id: 'LoadImage',
                            name: 'fileName',
                            margin: '10 0 10 50',
                            colspan: 2,
                            width: 765,
                            buttonText: '选择图片',
                        },
                        {
                            fieldLabel: "<span style='color:red;'>重选</span>附件",
                            xtype: 'filefield',
                            id: 'LoadFile',
                            name: 'fileNewName',
                            margin: '10 0 10 50',
                            colspan: 2,
                            width: 765,
                            buttonText: '选择附件',
                        },
                        {
                            fieldLabel: '新闻内容',
                            xtype: 'textareafield',
                            region: 'center',
                            name: 'content',
                            id: 'content',
                            anchor: '90%',
                            colspan: 2,
                            width: 765,
                            height: 380,
                            margin: '10 0 10 50',
                            listeners: {
                                'render': function (f) {
                                    setTimeout(function () {
                                        if (KindEditor) {
                                            Nceditor = KindEditor.create('#content-inputEl', {
                                                cssPath: 'Scripts/kindeditor/plugins/code/prettify.css',
                                                resizeType: 1,
                                                resizeMode: 0,
                                                allowFileManager: true
                                            });
                                        }
                                    }, 500);
                                }
                            },
                            value: this.record.get('content')
                        },
                    ]
                }
            ]
        });
        this.buttons = [{
            xtype: 'box',
            style: 'margin-left:20px',
            html: '<image style="float: left;margin-right: 5px;" src="../../Images/必填提示.png" /><span style="float:left;">带</span><b style="color:red;font-size:18px;display: block;float: left;line-height: 21px;">*</b>为必填项'
        },
            '->',
            {
                text: '确定',
                handler: 'onEditOK'
            }, {
                text: '关闭',
                handler: function () {
                    me.close();
                }
            }
        ]

        this.callParent();
        //this.child('form').loadRecord(this.record);
    }
});