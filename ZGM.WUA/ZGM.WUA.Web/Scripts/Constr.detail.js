$(function () {
    detail.init(parent.Constr.ConstrInfo);
    parent.mapSW.deepView();
});
var detail = {
    ConstrInfo: null,
    Type: 0,
    searchType: 0,
    init: function (ConstrInfo) {
        $(".minbtn").toggle(function () {
            detail.expand();
        }, function () {
            detail.collapse();
        });
        this.optionClick();
        this.optionSGClick();
        this.ConstrInfo = ConstrInfo;
        this.getDetails();
    },
    checkIsNull: function (str) {
        return str == null ? "" : str;
    },
    getDetails: function () {
        if ($("div:contains('工程名称')+div").length > 0) {
            $("div:contains('工程名称')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.ConstrName);
        }
        if ($("div:contains('立项批文号')+div").length > 0) {
            $("div:contains('立项批文号')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.LXYJ);
        }
        if ($("div:contains('立项批准机关')+div").length > 0) {
            $("div:contains('立项批准机关')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.LXPZJG);
        }
        if ($("div:contains('工程类型')+div").length > 0) {
            $("div:contains('工程类型')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.GCLX_NAME);
        }
        if ($("div:contains('工程性质')+div").length > 0) {
            $("div:contains('工程性质')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.GCXZ_NAME);
        }
        if ($("div:contains('预算资金(元)')+div").length > 0) {
            $("div:contains('预算资金(元)')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.YSZJ);
        }
        if ($("div:contains('计划开工日期')+div").length > 0) {
            $("div:contains('计划开工日期')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.JHKGRQ.substr(0,10));
        }
        if ($("div:contains('计划竣工日期')+div").length > 0) {
            $("div:contains('计划竣工日期')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.JHJGRQ.substr(0, 10));
        }
        if ($("div:contains('工程阶段')+div").length > 0) {
            $("div:contains('工程阶段')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.GCGCZT_NAME);
        }
        if ($("div:contains('建设内容')+div").length > 0) {
            $("div:contains('建设内容')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.JSNR);
        }
        if ($("div:contains('工程地址')+div").length > 0) {
            $("div:contains('工程地址')+div")[0].textContent = detail.checkIsNull(this.ConstrInfo.Address);
        }
        //获取附件
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Constr/GetConstrFiles",
            data: { constrId: detail.ConstrInfo.ConstrId },
            dataType: "json",
            success: function (data) {
                var selfData = [{ FileId: 1, FileName: "测试图片", FilePath: "aaa.jpg" }]
                //测试
                //data = selfData;
                var str = "";
                for (var i = 0; i < data.length; i++) {
                    //str += "<tr onclick='parent.zoom(this, \"" + parent.globalConfig.constrImportantPath + data[i].FilePath + "\")'><td class='tbHide'>" + data[i].FileName + "</td> </tr>";
                    str += "<tr><td class='tbHide'><a href='" + parent.globalConfig.constrImportantPath + data[i].FilePath + "'  target=\"view_window\">" + data[i].FileName + "</a></td> </tr>";
                }
                $("#tableFile").html(str);
            }
        });
    },
    getDetails_first: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Constr/GetConstrZB",
            data: { constrId: detail.ConstrInfo.ConstrId },
            dataType: "json",
            success: function (data) {
                if ($("div:contains('招标日期')+div").length > 0) {
                    $("div:contains('招标日期')+div")[0].textContent = detail.checkIsNull(data.ZBRQ).substr(0, 10);
                }
                if ($("div:contains('招标方式')+div").length > 0) {
                    $("div:contains('招标方式')+div")[0].textContent = detail.checkIsNull(data.ZBFS_TYPE);
                }
                if ($("div:contains('招标负责人')+div").length > 0) {
                    var zbfzr = detail.checkIsNull(data.ZBFZR);
                    $("div:contains('招标负责人')+div")[0].textContent = zbfzr;
                    $("div:contains('招标负责人')+div")[0].title = zbfzr;
                }
                if ($("div:contains('中标金额')+div").length > 0) {
                    var zbje = detail.checkIsNull(data.ZBJE);
                    $("div:contains('中标金额')+div")[0].textContent = zbje;
                    $("div:contains('中标金额')+div")[0].title = zbje;
                }
                if ($("div:contains('中标公司')+div").length > 0) {
                    var zbgs = detail.checkIsNull(data.ZBGS);
                    $("div:contains('中标公司')+div")[0].textContent = zbgs;
                    $("div:contains('中标公司')+div")[0].title = zbgs;
                }
                if ($("div:contains('中标负责人')+div").length > 0) {
                    var zbgsfzr = detail.checkIsNull(data.ZBGSFZR) == "" ? "" : detail.checkIsNull(data.ZBGSFZR) + ' ' + detail.checkIsNull(data.ZBGSLXDH) + ' ';
                    $("div:contains('中标负责人')+div")[0].textContent = zbgsfzr;
                    $("div:contains('中标负责人')+div")[0].title = zbgsfzr;
                }
                if ($("div:contains('监理公司')+div").length > 0) {
                    var jlgs = detail.checkIsNull(data.JLGS);
                    $("div:contains('监理公司')+div")[0].textContent = jlgs;
                    $("div:contains('监理公司')+div")[0].title = jlgs;
                }
                if ($("div:contains('监理公司负责人')+div").length > 0) {
                    var jlgsfzr = detail.checkIsNull(data.JLGSFZR) == "" ? "" : detail.checkIsNull(data.JLGSFZR) + ' ' + detail.checkIsNull(data.JLGSLXDH) + ' ';
                    $("div:contains('监理公司负责人')+div")[0].textContent = jlgsfzr;
                    $("div:contains('监理公司负责人')+div")[0].title = jlgsfzr;
                }
                if ($("div:contains('设计公司')+div").length > 0) {
                    var sjgs = detail.checkIsNull(data.SJGS);
                    $("div:contains('设计公司')+div")[0].textContent = sjgs;
                    $("div:contains('设计公司')+div")[0].title = sjgs;
                }
                if ($("div:contains('设计公司负责人')+div").length > 0) {
                    var sjgsfzr = detail.checkIsNull(data.SJGSFZR) == "" ? "" : detail.checkIsNull(data.SJGSFZR) + ' ' + detail.checkIsNull(data.SJGSLXDH) + ' ';
                    $("div:contains('设计公司负责人')+div")[0].textContent = sjgsfzr;
                    $("div:contains('设计公司负责人')+div")[0].title = sjgsfzr;
                }
                if ($("div:contains('招标代理人')+div").length > 0) {
                    var zbdlr = detail.checkIsNull(data.ZBDLR);
                    $("div:contains('招标代理人')+div")[0].textContent = zbdlr;
                    $("div:contains('招标代理人')+div")[0].title = zbdlr;
                }
                if ($("div:contains('招标联系人')+div").length > 0) {
                    var zblxr = detail.checkIsNull(data.ZBLXR) == "" ? "" : detail.checkIsNull(data.ZBLXR) + ' ' + detail.checkIsNull(data.ZBLXDH) + ' ';
                    $("div:contains('招标联系人')+div")[0].textContent = zblxr;
                    $("div:contains('招标联系人')+div")[0].title = zblxr;
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
        
    },
    getDetails_second: function (obj) {
        detail.getSGDetails_first();
    },
    getSGDetails_first: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Constr/GetConstrSGWTs",
            data: { constrId: detail.ConstrInfo.ConstrId },
            dataType: "json",
            success: function (data) {
                var str = "";
                str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;color:#619ae2">' +
                                '<span class="columm4 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">发现日期</span>' +
                                '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">是否扣款</span>' +
                                '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">扣款金额</span>' +
                                '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">问题说明</span>' +
                                '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">填报时间</span>' +
                            '</div>';
                for (var i = 0; i < data.length; i++) {
                    str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;color:#fff">' +
                                 '<span class="columm4 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + detail.checkIsNull(data[i].FXRQ).substr(0, 10) + '</span>' +
                                  '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + (detail.checkIsNull(data[i].SFKK)=="1"?"是":"否") + '</span>' +
                                   '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + detail.checkIsNull(data[i].KKJE) + '</span>' +
                                 '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + detail.checkIsNull(data[i].WTSM) + '</span>' +
                                 '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + detail.checkIsNull(data[i].TBSJ).substr(0, 10) + '</span>' +
                             '</div>';

                }
                $("#sgList").html(str);
            }
        });
    },
    getSGDetails_second: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Constr/GetConstrSGJDs",
            data: { constrId: detail.ConstrInfo.ConstrId },
            dataType: "json",
            success: function (data) {
                var str = "";
                //str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;color:#619ae2">' +
                //                '<span class="columm1 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">汇报日期</span>' +
                //                '<span class="columm3 textAlign" style=" width:135px;margin-left:0px;overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">工程进度</span>' +
                //                '<span class="columm3 textAlign" style="width:135px;margin-left:0px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">工程进度说明</span>' +
                //                '<span class="columm3 textAlign" style="width:135px;margin-left:0px;   overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">填报时间</span>' +
                //            '</div>';
                //for (var i = 0; i < data.length; i++) {
                //    str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;color:#fff">' +
                //                 '<span class="columm1 textAlign" style=" list-style: outside none none;display: list-item; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + (data[i].HBRQ == null ? "" : data[i].HBRQ.substr(0, 10)) + '</span>' +
                //                  '<span class="columm2 textAlign" style="list-style: outside none none;display: list-item; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + (data[i].GCJD == null ? "" : data[i].GCJD) + '</span>' +
                //                   '<span class="columm3 textAlign" style="list-style: outside none none;display: list-item;width:135px;margin-left:0px;  overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + (data[i].GCJDSM == null ? "" : data[i].GCJDSM) + '</span>' +
                //                 '<span class="columm3 textAlign" style="list-style: outside none none;display: list-item;width:135px;margin-left:0px;  overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + (data[i].TBSJ == null ? "" : data[i].TBSJ.substr(0, 10)) + '</span>' +
                //             '</div>';

                //}
                str += "<table style='margin-left: 80px;'><tr class='' style='color:#619ae2'> <th class='columm1 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>汇报日期</th><th class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>工程进度（%）</th><th class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>工程进度说明</th><th class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>填报时间</th></tr>";
                for (var i = 0; i < data.length; i++) {
                    str += "<tr>"
                    str += "<td class='columm1 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].HBRQ == null ? "" : data[i].HBRQ.substr(0, 10)) + "</td>"
                    str += "<td class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].GCJD == null ? "" : data[i].GCJD) + "</td>"
                    str += "<td class=' columm3 textAlign' style='overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].GCJDSM == null ? "" : data[i].GCJDSM) + "</td>"
                    str += "<td class='columm3 textAlign' style='overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].TBSJ == null ? "" : data[i].TBSJ.substr(0, 10)) + "</td>"
                    str += "</tr>";
                }
                str += "</table>";
                $("#sgList").html(str);
            }
        });
    },
    getSGDetails_third: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Constr/GetConstrSGZJBFs",
            data: { constrId: detail.ConstrInfo.ConstrId },
            dataType: "json",
            success: function (data) {
                var str = "";
                //str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;color:#619ae2">' +
                //                '<span class="columm4 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">拨付日期</span>' +
                //                '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">拨付金额</span>' +
                //                '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">扣款金额</span>' +
                //                '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">统计时间</span>' +
                //                '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">拨付说明</span>' +
                //            '</div>';
                //for (var i = 0; i < data.length; i++) {
                //    str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;color:#fff">' +
                //                 '<span class="columm4 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-left:40px">' + (data[i].BFRQ == null ? "" : data[i].BFRQ.substr(0, 10)) + '</span>' +
                //                  '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].BFZE + '</span>' +
                //                   '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].KKZE + '</span>' +
                //                 '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + (data[i].TJSJ == null ? "" : data[i].TJSJ.substr(0, 10)) + '</span>' +
                //                 '<span class="columm5 textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].BFSM + '</span>' +
                //             '</div>';

                str += "<table style='margin-left: 30px;'><tr class='' style='color:#619ae2'> <th class='columm1 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>拨付日期</th><th class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>拨付金额</th><th class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>统计时间</th><th class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>拨付说明</th></tr>";

                //<th class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>扣款金额</th>
                for (var i = 0; i < data.length; i++) {
                    str += "<tr>"
                    str += "<td class='columm1 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].BFRQ == null ? "" : data[i].BFRQ.substr(0, 10)) + "</td>"
                    str += "<td class='columm3 textAlign' style=' overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].BFZE == null ? "" : data[i].BFZE) + "</td>"
                    //str += "<td class=' columm3 textAlign' style='overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].KKZE == null ? "" : data[i].KKZE) + "</td>"
                    str += "<td class='columm3 textAlign' style='overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].TJSJ == null ? "" : data[i].TJSJ.substr(0, 10)) + "</td>"
                    str += "<td class='columm3 textAlign' style='overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin:0px'>" + (data[i].BFSM == null ? "" : data[i].BFSM) + "</td>"
                    str += "</tr>";
                }
                str += "</table>";
                $("#sgList").html(str);
            }
        });
    },
    getDetails_third: function (obj) {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Constr/GetConstrJG",
            data: { constrId: detail.ConstrInfo.ConstrId },
            dataType: "json",
            success: function (data) {
                //if ($("div:contains('竣工日期')+div").length > 0) {
                //    $("div:contains('竣工日期')+div")[0].textContent = detail.checkIsNull(data.JGRQ).substr(0, 10);
                //}
                if ($("div:contains('是否按期竣工')+div").length > 0) {
                    $("div:contains('是否按期竣工')+div")[0].textContent = detail.checkIsNull(data.SFAQJG);
                }
                if ($("div:contains('超期天数')+div").length > 0) {
                    $("div:contains('超期天数')+div")[0].textContent = detail.checkIsNull(data.CQTS);
                }
                if ($("div:contains('质量结果')+div").length > 0) {
                    $("div:contains('质量结果')+div")[0].textContent = detail.checkIsNull(data.ZLJG);
                }
                if ($("div:contains('竣工说明')+div").length > 0) {
                    $("div:contains('竣工说明')+div")[0].textContent = detail.checkIsNull(data.JGSM);
                }
                if ($("div:contains('填报时间:')+div").length > 0) {
                    $("div:contains('填报时间:')+div")[0].textContent = detail.checkIsNull(data.TBSJ).substr(0, 10);
                }
                //if ($("div:contains('计划工期:')+div").length > 0) {
                //    $("div:contains('计划工期:')+div")[0].textContent = detail.checkIsNull(data.JHGQ);
                //}
                //if ($("div:contains('实际工期:')+div").length > 0) {
                //    $("div:contains('实际工期:')+div")[0].textContent = detail.checkIsNull(data.SJGQ);
                //}
                $('#Div29').html(detail.checkIsNull(data.JHGQ));
                $('#Div30').html(detail.checkIsNull(data.SJGQ));
                $('#Div31').html(detail.checkIsNull(data.KGRQ).substr(0, 10));

                //if ($("div:contains('开工日期:')+div").length > 0) {
                //    $("div:contains('开工日期:')+div")[0].textContent = detail.checkIsNull(data.KGRQ).substr(0, 10);
                //}
                //if ($("div:contains('竣工日期:')+div").length > 0) {
                //    $("div:contains('竣工日期:')+div")[0].textContent = detail.checkIsNull(data.JGRQ).substr(0, 10);
                //}
                $('#Div23').html(detail.checkIsNull(data.JGRQ).substr(0, 10));
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getDetails_fourth: function (obj) {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Constr/GetConstrSJ",
            data: { constrId: detail.ConstrInfo.ConstrId },
            dataType: "json",
            success: function (data) {
                if ($("div:contains('审计开始时间')+div").length > 0) {
                    $("div:contains('审计开始时间')+div")[0].textContent = detail.checkIsNull(data.SJKSRQ).substr(0, 10);
                }
                if ($("div:contains('审计结束时间')+div").length > 0) {
                    $("div:contains('审计结束时间')+div")[0].textContent = detail.checkIsNull(data.SJJSRQ).substr(0, 10);
                }
                if ($("div:contains('审计单位')+div").length > 0) {
                    $("div:contains('审计单位')+div")[0].textContent = detail.checkIsNull(data.SJDW);
                }
                if ($("div:contains('审计工程金额')+div").length > 0) {
                    $("div:contains('审计工程金额')+div")[0].textContent = detail.checkIsNull(data.SJGCJE);
                }
                if ($("div:contains('审计扣款金额')+div").length > 0) {
                    $("div:contains('审计扣款金额')+div")[0].textContent = detail.checkIsNull(data.SJKKJE);
                }
                if ($("div:contains('审计说明')+div").length > 0) {
                    $("div:contains('审计说明')+div")[0].textContent = detail.checkIsNull(data.SJSM);
                }
                if ($("div:contains('填报日期')+div").length > 0) {
                    $("div:contains('填报日期')+div")[0].textContent = detail.checkIsNull(data.TBSJ).substr(0, 10);
                }
                if ($("div:contains('送审日期')+div").length > 0) {
                    $("div:contains('送审日期')+div")[0].textContent = detail.checkIsNull(data.SSSJ).substr(0, 10);
                }

            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getDetails_fifth: function (obj) {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingHouseCheckout",
            data: { houseId: detail.RemoveBuildingInfo.HouseId },
            dataType: "json",
            success: function (data) {
                var html = "";
                if (data != null) {
                    html += '<div class="personnum_BMD" style="height:45px">'
                        + '<div class="columm1" style="margin-left: 20px;color:#3cd93c">合算负责人:</div>'
                        + '<div class="columm2" id="xm">' + (data.AccountUserName == null ? "" : data.AccountUserName) + '</div>'
                        + '<div class="columm3"  style="color:#3cd93c">合算金额:</div>'
                        + '<div class="columm2" id="name2">' + (data.Money == null ? "" : data.Money) + '</div>'
                    + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
                     + '<div class="columm1" style="margin-left: 20px;color:#3cd93c">结算人:</div>'
                     + '<div class="columm2" id="xm">' + (data.CheckoutUserName == null ? "" : data.CheckoutUserName) + '</div>'
                     + '<div class="columm3"  style="color:#3cd93c">合算金额:</div>'
                     + '<div class="columm2" id="name2">' + (data.CheckoutTime == null ? "" : data.CheckoutTime.substr(0, 10)) + '</div>'
                 + '</div>';
                } else {
                    html += '<div class="personnum_BMD" style="height:45px">'
                        + '<div class="columm1" style="margin-left: 20px;color:#3cd93c">合算负责人:</div>'
                        + '<div class="columm2" id="xm"></div>'
                        + '<div class="columm3"  style="color:#3cd93c">合算金额:</div>'
                        + '<div class="columm2" id="name2"></div>'
                    + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
                     + '<div class="columm1" style="margin-left: 20px;color:#3cd93c">结算人:</div>'
                     + '<div class="columm2" id="xm"></div>'
                     + '<div class="columm3"  style="color:#3cd93c">合算金额:</div>'
                     + '<div class="columm2" id="name2"></div>'
                 + '</div>"';
                }
                obj.html(html);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });

    },
    optionClick: function () {
        $('.option').each(function (i) {
            $(this).click(function () {
                $(this).addClass("current").siblings().removeClass("current");
                detail.searchType = i;
                detail.personbasedetail();

            })
        })
    },
    optionSGClick: function () {
        $('.noCss .option').each(function (i) {
            $(this).click(function () {
                $(this).addClass("current").siblings().removeClass("current");
                detail.personbasedetailSG(i);

            })
        })
    },
    personbasedetail: function () {
        $('.personbasedetail').each(function (j) {
            if (detail.searchType == j) {
                $(this).css("display", "block").siblings().css("display", "none");
                switch (detail.searchType) {
                    case 1: detail.getDetails_first($(this)); break;
                    case 2: detail.getDetails_second($(this)); break;
                    case 3: detail.getDetails_third($(this)); break;
                    case 4: detail.getDetails_fourth($(this)); break;
                }
            }
        });
    },
    personbasedetailSG: function (i) {
        switch (i) {
            case 0: detail.getSGDetails_first($(this)); break;
            case 1: detail.getSGDetails_second($(this)); break;
            case 2: detail.getSGDetails_third($(this)); break;
        }
        $("#sgList").html("");
    },
    collapse: function () {
        parent.detail.collapse();
        setTimeout(function () {
            $("body").css("background-image", "url('/images/ppolice/ppolicebg1.png')");
        }, 300);
    },
    expand: function () {
        parent.detail.expand();
        $("body").css("background-image", "url('/images/ppolice/ppolicebg.png')");
    },
    close: function () {
        parent.detail.close();
    }
}