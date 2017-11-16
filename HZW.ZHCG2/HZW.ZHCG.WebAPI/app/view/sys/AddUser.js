Ext.define('TianZun.view.sys.AddUser', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.userAdd',

    title: '添加用户',
    layout: 'fit',

    initComponent: function () {
        this.items = [{
            xtype: 'form',
            border: false,
            bodyPadding: 10,
            layout: {
                type: 'table',
                columns: 2,
            },
            fieldDefaults: {
                labelAlign: 'right',
                labelWidth: 75
            },
            defaults: {
                xtype: 'textfield',
                width: 255
            },
            items: [
                {
                    xtype: 'hidden',
                    name: 'UnitID',
                    value: this.record.get('ID')
                },
                {
                    fieldLabel: '用户编号',
                    name: 'Code',
                    allowBlank: false
                },
                {
                    fieldLabel: '用户名称',
                    name: 'DisplayName',
                    allowBlank: false
                },
                {
                    fieldLabel: '用户类型',
                    xtype: 'combo',
                    name: 'UserTypeID',
                    store: Ext.create('TianZun.store.UserTypeStore'),
                    valueField: 'ID',
                    displayField: 'Name',
                    editable: false,
                    allowBlank: false
                },
                {
                    fieldLabel: '所属单位',
                    disabled: true,
                    value: this.record.get('Name')
                },
                {
                    fieldLabel: '登陆帐号',
                    name: 'LoginName',
                    allowBlank: false
                },
                {
                    fieldLabel: '登陆密码',
                    name: 'Password',
                    inputType: 'password',
                    regex: /^(?![^a-zA-Z]+$)(?!\D+$).{6,15}$/,
                    regexText: "密码必须用英文和字母6-15位字符组成！",
                    allowBlank: false
                },
                {
                    fieldLabel: '确认密码',
                    name: 'newPassword',
                    inputType: 'password',
                    allowBlank: false
                },
                {
                    xtype: 'label',
                },
                {
                    xtype: 'tagfield',
                    fieldLabel: '所属角色',
                    store: Ext.create('TianZun.store.RoleManageStore'),
                    displayField: 'Name',
                    valueField: 'ID',
                    name: "RoleIDArr",
                    allowBlank: false,
                    colspan: 2,
                    width: 510,
                    height: 52
                }
            ],
            buttons: [{
                text: '确定',
                handler: 'onAddOK'
            }, {
                text: '关闭',
                handler: 'onClose'
            }]
        }];

        this.callParent();
    }
});