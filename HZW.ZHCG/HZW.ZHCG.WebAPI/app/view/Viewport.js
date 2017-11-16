Ext.define('TianZun.view.Viewport', {
    extend: 'Ext.container.Viewport',
    layout: 'border',
    padding: 0,

    requires: [
        'TianZun.view.ContentLeft',
        'TianZun.view.ContentCenter'
    ],

    initComponent: function () {
        this.items = [{
            region: 'north',
            border: false,
            height: 100,
            contentEl: "title",
        }, {
            region: 'west',
            width:150,
            xtype: 'contentLeft'
        },
        {
            region: 'center',
            xtype: 'contentCenter'
        }
        ];

        this.callParent();
    },
    listeners: {
        render: function () {
            Mask();
        }
    }
});