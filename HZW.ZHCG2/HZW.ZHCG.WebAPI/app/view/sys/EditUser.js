Ext.define('TianZun.view.sys.EditUser', {
    extend: 'TianZun.ux.Window',
    alias: 'widget.userEdit',

    title: '修改用户',
    layout: 'fit',

    initComponent: function () {
        var me = this;
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
                    name: 'ID',
                    value: this.record.get('ID')
                },
                {
                    xtype: 'hidden',
                    name: 'UnitID',
                    value: this.record.get('UnitID')
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
                    allowBlank: false,
                    listeners: {
                        render: function (combo) {
                            this.getStore().load();
                        }
                    }
                },
                {
                    fieldLabel: '所属单位',
                    disabled: true,
                    value: this.record.get('UnitName')
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
                    regexText: "密码必须用英文和字母6-15位字符组成！"
                },
                {
                    fieldLabel: '确认密码',
                    name: 'newPassword',
                    inputType: 'password'
                },
                {
                    xtype: 'label',
                },
                {
                    xtype: 'tagfield',
                    fieldLabel: '所属角色',
                    store: Ext.create('TianZun.store.RoleStore'),
                    displayField: 'Name',
                    valueField: 'ID',
                    name: "RoleIDArr",
                    allowBlank: false,
                    colspan: 2,
                    width: 510,
                    height: 52,
                    listeners: {
                        render: function (combo) {
                            combo.getStore().on("load", function (store) {
                                combo.setValue(me.roleIDArr)
                            });
                            this.getStore().load();
                        }
                    }
                },
                {
                    fieldLabel: '创建时间',
                    value: Ext.Date.format(this.record.get('CreatedTime'), 'Y-m-d H:i'),
                    disabled: true
                },
                {
                    fieldLabel: '更新时间',
                    value: Ext.Date.format(this.record.get('UpdatedTime'), 'Y-m-d H:i'),
                    disabled: true
                }
            ],
            buttons: [{
                text: '确定',
                handler: 'onEditOK'
            }, {
                text: '关闭',
                handler: 'onClose'
            }]
        }];

        this.callParent();
        this.child('form').loadRecord(this.record);
    }
});