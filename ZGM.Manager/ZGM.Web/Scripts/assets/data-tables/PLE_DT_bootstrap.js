/* 设置为默认参数值,初始化datatable */
$.extend(true, $.fn.dataTable.defaults, {
    //"sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
    "sPaginationType": "full_numbers",
    "bProcessing": true,
    "bSort": false,
    "bFilter": false,
    "bPaginate": true,
    "bAutoWidth": false,
    "bServerSide": true,
    "bProcessing": false,
    "bLengthChange": true,
    "oLanguage": {
        "sLengthMenu": "每页显示 _MENU_ 条",
        "sZeroRecords": "没有匹配结果",
        "sEmptyTable": "没有数据",
        "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
        "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
        "sInfoFiltered": "(一共有 _MAX_  条记录)",
        "oPaginate": {
            "sFirst": "首页",
            "sPrevious": " 上一页 ",
            "sNext": " 下一页 ",
            "sLast": " 末页 "
        }
    },
});

/* API方法得到分页信息 */
$.fn.dataTableExt.oApi.fnPagingInfo = function (oSettings) {
    return {
        "iStart": oSettings._iDisplayStart,
        "iEnd": oSettings.fnDisplayEnd(),
        "iLength": oSettings._iDisplayLength,
        "iTotal": oSettings.fnRecordsTotal(),
        "iFilteredTotal": oSettings.fnRecordsDisplay(),
        "iPage": Math.ceil(oSettings._iDisplayStart / oSettings._iDisplayLength),
        "iTotalPages": Math.ceil(oSettings.fnRecordsDisplay() / oSettings._iDisplayLength)
    };
};