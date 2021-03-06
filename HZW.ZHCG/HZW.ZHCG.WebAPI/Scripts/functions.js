﻿function PostAjax(options) {
    if (options.data == undefined)
    {
        options.data = null;
    }

    $.ajax({
        method: "Post",
        url: configs.WebApi + options.url,
        data: options.data,
        contentType: 'application/x-www-form-urlencoded',
        complete: function (jqXHR, textStatus, errorThrown) {
            options.complete(jqXHR, textStatus, errorThrown);
        }
    });
}

function GetAjax(options) {
    $.ajax({
        method: "Get",
        url: configs.WebApi + options.url,
        complete: function (jqXHR, textStatus, errorThrown) {
            options.complete(jqXHR, textStatus, errorThrown);
        }
    });
}

function FilterStore(store, filters) {

    store.clearFilter();

    if (filters.length > 0)
        store.filter(filters);
    else
        store.load();
}

function Mask() {
    if (Ext.ComponentQuery.query('viewport').length > 0)
    {
        Ext.ComponentQuery.query('viewport')[0].mask();
    }
}

function UnMask() {
    if (Ext.ComponentQuery.query('viewport').length > 0) {
        Ext.ComponentQuery.query('viewport')[0].unmask();
    }
}