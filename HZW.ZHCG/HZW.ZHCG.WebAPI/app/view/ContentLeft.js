Ext.define('TianZun.view.ContentLeft', {
    extend: 'Ext.Panel',
    alias: 'widget.contentLeft',

    requires: [
        'TianZun.controller.ContentLeft'
    ],

    controller: 'contentLeft',
    header: false,
    border: false,
    bodyStyle: 'background:#94BDD4;',
    width: 220,
    items: [
        {
            xtype: 'menu',
            plain: true,
            floating: false,
            bodyStyle: 'background:#94BDD4;',
            style: 'border:0px;',
            items: [{
                text: '店家列表',
                height: 30,
                style: 'padding-top:2px;padding-left: 12px;',
                icon: 'Images/店家列表.png',
                listeners: {
                    render: 'onMenuItemClick',
                    click: 'onMenuItemClick'
                }
            }, {
                text: '新增店家',
                height: 30,
                style: 'padding-top:2px;padding-left: 12px;',
                icon: 'Images/新增店家.png',
                listeners: {
                    click: 'onAddShopMenu',
                }
            }]
        }
    ],
    listeners: {
        render: 'onShow'
    }
});