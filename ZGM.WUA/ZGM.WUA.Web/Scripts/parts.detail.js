$(function () {
    detail.init(parent.parts.Part, parent.parts.Type);
    // parent.mapSW.deepView();
});

var detail = {
    apiconfig: null,
    Type: null,
    Part: null,
    init: function (part, type) {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.Type = type;
        this.Part = part;
        switch (this.Type) {
            case "Road":
                this.initRoad(this.Part);
                break;
            case "CoverLoad":
                this.initCoverLoad(this.Part);
                break;
            case "Bridge":
                this.initBridge(this.Part);
                break;
            case "StreetLamp":
                this.initStreetLamp(this.Part);
                break;
            case "LandscapeLamp":
                this.initLandscapeLamp(this.Part);
                break;
            case "Pump":
                this.initPump(this.Part);
                break;
            case "ParkGreen":
                this.initParkGreen(this.Part); break;
            case "LoadGreen":
                this.initLoadGreen(this.Part); break;
            case "ProtectionGreen":
                this.initProtectionGreen(this.Part); break;
            case "Toilt":
                this.initToilt(this.Part); break;
            case "River":
                this.initRiver(this.Part); break;
        }
        //
        $(".minbtn").toggle(function () {
            detail.expand();
        }, function () {
            detail.collapse();
        });
    },
    initCoverLoad: function (CoverLoad) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetCoverLoadByCoverLoadId",
            data: { coverLoadId: CoverLoad.CoverLoadId },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span  >井盖名:</span><span id="roadName"  style="color:red" title=' + data.CoverLoadName + '>' + data.CoverLoadName
                    + '</span></div><div><span  >井盖性质:</span><span title=' + data.JGXZ_TYPE + '  style="color:#fff">' + (data.JGXZ_TYPE == null ? "" : data.JGXZ_TYPE)
                    + '</span></div><div><span  >起讫点:</span><span  style="color:#fff" title=' + data.QQD + '>' + (data.QQD == null ? "" : data.QQD)
                    + '</span></div><div><span  >竣工日期：</span><span style="color:#fff" title=' + data.JGRQ + '>' + (data.JGRQ == null ? "" : data.JGRQ.substr(0, 10))


                    + '</span></div></div><div class="partsbaseinfo_right"><div><span  >参考价格:</span><span style="color:#fff" title=' + data.CKJG + '>' + (data.CKJG == null ? "" : data.CKJG)
                      + '</span></div><div><span  >备注:</span><span style="color:#fff" title=' + data.CKJG + '>' + (data.CKJG == null ? "" : data.CKJG)
                    + '</span></div><div><span >移交日期:</span><span style="color:#fff" title=' + data.YJRQ + '>' + (data.YJRQ == null ? "" : data.YJRQ.substr(0, 10))
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initRiver: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetRiver",
            data: { riverId: road.id },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span  >河流名:</span><span id="roadName"  style="color:#fff" title=' + data.RiverName + '>' + data.RiverName
                    + '</span></div><div><span  >河道长度（米）:</span><span title=' + data.HDCD + '  style="color:#fff">' + (data.HDCD == null ? "" : data.HDCD)
                    + '</span></div><div><span  >河道宽度（米）:</span><span  style="color:#fff" title=' + data.HDKD + '>' + (data.HDKD == null ? "" : data.HDKD)
                    + '</span></div><div><span  >河道面积（平米）：</span><span style="color:#fff" title=' + data.HDMJ + '>' + (data.HDMJ == null ? "" : data.HDMJ)
                     + '</span></div><div><span  >河道类型:</span><span style="color:#fff" title=' + data.HDLX_TYPE + '>' + (data.HDLX_TYPE == null ? "" : data.HDLX_TYPE)
                      + '</span></div><div><span  >水质等级:</span><span style="color:#fff" title=' + data.SZDJ_TYPE + '>' + (data.SZDJ_TYPE == null ? "" : data.SZDJ_TYPE)

                    + '</span></div></div><div class="partsbaseinfo_right"><div><span  >保洁等级:</span><span style="color:#fff" title=' + data.BJDJ_TYPE + '>' + (data.BJDJ_TYPE == null ? "" : data.BJDJ_TYPE)
                      + '</span></div><div><span  >水质养护说明:</span><span style="color:#fff" title=' + data.SZYHSM + '>' + (data.SZYHSM == null ? "" : data.SZYHSM)
                    + '</span></div><div><span  >河道终点:</span><span style="color:#fff" title=' + data.HDZD + '>' + (data.HDZD == null ? "" : data.HDZD)
                    + '</span></div><div><span  >河道起点:</span><span style="color:#fff" title=' + data.HDQD + '>' + (data.HDQD == null ? "" : data.HDQD)
                    + '</span></div><div><span >包含的支河:</span><span style="color:#fff" title=' + data.BHDZH + '>' + (data.BHDZH == null ? "" : data.BHDZH)
                     //+ '</span></div><div><span >移交日期:</span><span>' + (data.YJRQ == null ? "" : data.YJRQ)
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initToilt: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetToilt",
            data: { toiltId: road.id },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span   >厕所别名:</span><span id="roadName" style="color:red" title=' + data.ToiltName + '>' + data.ToiltName
                    + '</span></div><div><span   >公厕地址:</span><span style="color:red" title=' + data.GCDZ + '>' + (data.GCDZ == null ? "" : data.GCDZ)
                    + '</span></div><div><span   >公厕面积（平米）:</span><span style="color:#fff" title=' + data.GCMJ + '>' + (data.GCMJ == null ? "" : data.GCMJ)

                     + '</span></div><div><span   >男坑位数:</span><span style="color:#fff" title=' + data.MKWS + '>' + (data.MKWS == null ? "" : data.MKWS)
                      + '</span></div><div><span   >女坑位数:</span><span style="color:#fff" title=' + data.WKWS + '>' + (data.WKWS == null ? "" : data.WKWS)
                       + '</span></div><div><span   >备注:</span><span style="color:#fff" title=' + data.BZ + '>' + (data.BZ == null ? "" : data.BZ)

                    + '</span></div></div><div class="partsbaseinfo_right"><div><span   >是否有母婴室:</span><span  style="color:#fff" title=' + data.SFYMYS + '>' + (data.SFYMYS == null ? "" : data.SFYMYS)
                      + '</span></div><div><span   >是否有残疾人专用:</span><span style="color:#fff" title=' + data.SFYCJRZY + '>' + (data.SFYCJRZY == null ? "" : data.SFYCJRZY)
                      + '</span></div><div><span   >星级：</span><span style="color:#fff" title=' + data.XJ_TYPE + '>' + (data.XJ_TYPE == null ? "" : data.XJ_TYPE)
                    + '</span></div><div><span   >参考价格:</span><span style="color:#fff" title=' + data.CKJG + '>' + (data.CKJG == null ? "" : data.CKJG)

                    + '</span></div><div><span  >竣工日期:</span><span  style="color:#fff" title=' + data.JGRQ + '>' + (data.JGRQ == null ? "" : data.JGRQ.substr(0, 10))
                     + '</span></div><div><span  >移交日期:</span><span style="color:#fff" title=' + data.YJRQ + '>' + (data.YJRQ == null ? "" : data.YJRQ.substr(0, 10))
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initProtectionGreen: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetProtectionGreen",
            data: { protectionGreenId: road.id },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span   >绿地别名:</span><span id="roadName" style="color:red" title=' + data.ProtectGreenName + '>' + data.ProtectGreenName
                    + '</span></div><div><span   >防护绿地等级:</span><span  style="color:#fff">' + (data.FHLDDJ_TYPE == null ? "" : data.FHLDDJ_TYPE)
                    + '</span></div><div><span   >防护绿地类型:</span><span  style="color:#fff">' + (data.FHLDLX_TYPE == null ? "" : data.FHLDLX_TYPE)
                    + '</span></div><div><span   >区域描述：</span><span style="color:#fff" title=' + data.QYMS + '>' + (data.QYMS == null ? "" : data.QYMS)
                     + '</span></div><div><span   >面积（平米）:</span><span  style="color:green">' + (data.MJ == null ? "" : data.MJ)
                      //+ '</span></div><div><span   >出水口管径:</span><span>' + (data.CSKGJ == null ? "" : data.CSKGJ)
                      // + '</span></div><div><span   >出水口位置:</span><span>' + (data.CSKWZ == null ? "" : data.CSKWZ)

                    + '</span></div></div><div class="partsbaseinfo_right"><div><span   >参考价格:</span><span  style="color:#fff">' + (data.CKJG == null ? "" : data.CKJG)
                    + '</span></div><div><span   >备注:</span><span  style="color:#fff" title=' + data.BZ + '>' + (data.BZ == null ? "" : data.BZ)
                    + '</span></div><div><span  >竣工日期:</span><span style="color:#fff">' + (data.JGRQ == null ? "" : data.JGRQ.substr(0, 10))
                     + '</span></div><div><span  >移交日期:</span><span style="color:#fff">' + (data.YJRQ == null ? "" : data.YJRQ.substr(0, 10))
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initLoadGreen: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetLoadGreenByLoadGreenId",
            data: { loadGreenId: road.id },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span   >绿地别名:</span><span id="roadName"  style="color:#fff" title=' + data.LoadGreenName + '>' + data.LoadGreenName
                    + '</span></div><div><span   >道路绿地等级:</span><span style="color:#fff">' + (data.DLLDDJ_TYPE == null ? "" : data.DLLDDJ_TYPE)
                    + '</span></div><div><span   >区域描述:</span><span  style="color:#fff" title=' + data.QYMS + '>' + (data.QYMS == null ? "" : data.QYMS)
                    + '</span></div><div><span   >面积（平米）：</span><span style="color:#fff">' + (data.MJ == null ? "" : data.MJ)
                     //+ '</span></div><div><span   >参考价格:</span><span>' + (data.CKJG == null ? "" : data.CKJG)
                      //+ '</span></div><div><span   >出水口管径:</span><span>' + (data.CSKGJ == null ? "" : data.CSKGJ)
                      // + '</span></div><div><span   >出水口位置:</span><span>' + (data.CSKWZ == null ? "" : data.CSKWZ)

                    + '</span></div></div><div class="partsbaseinfo_right"><div><span   >参考价格:</span><span style="color:#fff">' + (data.CKJG == null ? "" : data.CKJG)
                    //  + '</span></div><div><span   >单位时间流量:</span><span>' + (data.DWSJLL == null ? "" : data.DWSJLL)
                    //+ '</span></div><div><span   >参考价格:</span><span>' + (data.CKJG == null ? "" : data.CKJG)
                    + '</span></div><div><span   >备注:</span><span style="color:#fff">' + (data.BZ == null ? "" : data.BZ)
                    + '</span></div><div><span  >竣工日期:</span><span style="color:#fff">' + (data.JGRQ == null ? "" : data.JGRQ.substr(0, 10))
                     + '</span></div><div><span  >移交日期:</span><span style="color:#fff">' + (data.YJRQ == null ? "" : data.YJRQ.substr(0, 10))
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initParkGreen: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetParkGreenByParkGreenId",
            data: { parkGreenId: road.id },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span   >绿地别名:</span><span id="roadName" style="color:red"  title=' + data.PartGreenName + '>' + data.PartGreenName
                    + '</span></div><div><span   >公园绿地等级:</span><span style="color:#fff">' + (data.DLLDDJ_TYPE == null ? "" : data.GYLDDJ_TYPE)
                    + '</span></div><div><span   >区域描述:</span><span  style="color:green" title=' + data.QYMS + '>' + (data.QYMS == null ? "" : data.QYMS)
                    + '</span></div><div><span   >面积（平米）：</span><span style="color:#fff">' + (data.MJ == null ? "" : data.MJ)
                     //+ '</span></div><div><span   >水泵数量:</span><span>' + (data.SBSL == null ? "" : data.SBSL)
                     // + '</span></div><div><span   >出水口管径:</span><span>' + (data.CSKGJ == null ? "" : data.CSKGJ)
                     //  + '</span></div><div><span   >出水口位置:</span><span>' + (data.CSKWZ == null ? "" : data.CSKWZ)

                    + '</span></div></div><div class="partsbaseinfo_right"><div><span   >参考价格:</span><span style="color:#fff">' + (data.CKJG == null ? "" : data.CKJG)
                    //  + '</span></div><div><span   >单位时间流量:</span><span>' + (data.DWSJLL == null ? "" : data.DWSJLL)
                    //+ '</span></div><div><span   >参考价格:</span><span>' + (data.CKJG == null ? "" : data.CKJG)
                    + '</span></div><div><span   >备注:</span><span style="color:#fff">' + (data.BZ == null ? "" : data.BZ)
                    + '</span></div><div><span  >竣工日期:</span><span style="color:#fff">' + (data.JGRQ == null ? "" : data.JGRQ.substr(0, 10))
                     + '</span></div><div><span  >移交日期:</span><span style="color:#fff">' + (data.YJRQ == null ? "" : data.YJRQ.substr(0, 10))
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initPump: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetPumpByPumpId",
            data: { pumpId: road.PumpId },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span  >泵站名:</span><span id="roadName" style="color:#fff" title=' + data.PumpName + '>' + data.PumpName
                    + '</span></div><div><span  >泵站类型:</span><span style="color:#fff">' + (data.BZLX_TYPE == null ? "" : data.BZLX_TYPE)
                    + '</span></div><div><span  >水泵功率:</span><span style="color:#fff">' + (data.SBGL == null ? "" : data.SBGL)
                    + '</span></div><div><span  >发电机功率：</span><span style="color:#fff">' + (data.FDJGL == null ? "" : data.FDJGL)
                     + '</span></div><div><span  >水泵数量:</span><span style="color:#fff">' + (data.SBSL == null ? "" : data.SBSL)
                      + '</span></div><div><span  >出水口管径:</span><span style="color:#fff">' + (data.CSKGJ == null ? "" : data.CSKGJ)
                       + '</span></div><div><span  >出水口位置:</span><span style="color:#fff">' + (data.CSKWZ == null ? "" : data.CSKWZ)

                    + '</span></div></div><div class="partsbaseinfo_right"><div><span  >地址详细信息:</span><span style="color:#fff" title=' + data.DZXXXX + '>' + (data.DZXXXX == null ? "" : data.DZXXXX)
                      + '</span></div><div><span  >单位时间流量:</span><span style="color:#fff" title=' + data.DWSJLL + '>' + (data.DWSJLL == null ? "" : data.DWSJLL)
                    + '</span></div><div><span  >参考价格:</span><span style="color:#fff">' + (data.CKJG == null ? "" : data.CKJG)
                    + '</span></div><div><span  >备注:</span><span style="color:#fff" title=' + data.BZ + '>' + (data.BZ == null ? "" : data.BZ)
                    + '</span></div><div><span >竣工日期:</span><span style="color:#fff">' + (data.JGRQ == null ? "" : data.JGRQ.substr(0, 10))
                     + '</span></div><div><span >移交日期:</span><span style="color:#fff">' + (data.YJRQ == null ? "" : data.YJRQ.substr(0, 10))
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initLandscapeLamp: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetLandscapeLampTJs",
            data: { LLId: road.LLId },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var JsonData = data;

                var str = '<div class="partsbaseinfo_left"><div><span style="color: #3cd93c;">灯泡类型</span><span style="color: #3cd93c;" id="roadName">' + "功率";
                for (var i = 0; i < JsonData.length; i++) {
                    str += '</span></div><div  style="color:#fff"><span>' + (JsonData[i].DPLX_TYPE == null ? "" : JsonData[i].DPLX_TYPE) + '</span><span>' + (JsonData[i].GL_TYPE == null ? "" : JsonData[i].GL_TYPE);
                }
                str += '</span></div></div><div class="partsbaseinfo_right"><div><span style="color: #3cd93c;">' + "盏数";
                for (var i = 0; i < JsonData.length; i++) {
                    str += '</span></div><div  style="color:#fff"><span style="color:#fff">' + (JsonData[i].ZS == null ? "" : JsonData[i].ZS);
                }
                str += '</span></div></div>';
                str += '</div>';
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initStreetLamp: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetStreetLampTJs",
            data: { SLLId: road.SLLId },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var JsonData = data;

                var str = '<div class="partsbaseinfo_left"><div><span style="color: #3cd93c;">灯杆类型</span><span style="color: #3cd93c;" id="roadName">' + "灯泡类型";
                for (var i = 0; i < JsonData.length; i++) {
                    str += '</span></div><div><span style="color:#fff">' + JsonData[i].DGLX_TYPE + '</span><span style="color:#fff">' + JsonData[i].DPLX_TYPE;
                }
                str += '</span></div></div><div class="partsbaseinfo_right"><div><span style="color: #3cd93c;">功率</span><span style="color: #3cd93c;">' + "杆数";
                for (var i = 0; i < JsonData.length; i++) {
                    str += '</span></div><div><span style="color:#fff">' + JsonData[i].GL_TYPE + '</span><span style="color:#fff">' + JsonData[i].GS;
                }
                str += '</span></div></div>';
                str += '</div>';
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initRoad: function (road) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetRoadDetailByRoadId",
            data: { roadId: road.RoadId },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span>路段名称:</span><span style="color: #3cd93c;" id="roadName">' + data.RoadName
                    + '</span></div><div><span>总宽度(m):</span><span style="color:#fff">' + (parseFloat(data.CXDKD) + parseFloat(data.RXDK))
                    + '</span></div><div><span>车行道宽度(m):</span><span style="color:#fff">' + data.CXDKD
                    + '</span></div><div><span>车行道面积(㎡):</span><span style="color: #ff9211;">' + data.CXDMJ
                    + '</span></div></div><div class="partsbaseinfo_right"><div><span>总长度(m):</span><span style="color:#fff">' + data.CXDCD
                    + '</span></div><div><span>总面积(㎡):</span><span style="color: #f6ef37;">' + (parseFloat(data.CXDMJ) + parseFloat(data.RXDMJ))
                    + '</span></div><div><span>人行道宽度(m):</span><span style="color:#fff">' + data.RXDK
                    + '</span></div><div><span>人行道面积(㎡):</span><span style="color: #ff3030;">' + data.RXDMJ
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initBridge: function (Part) {
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Parts/GetBridgeDetail",
            data: { bridgeId: Part.BridgeId },
            dataType: "json",
            success: function (data) {
                $(".partsbaseinfo").html("");
                var str = '<div class="partsbaseinfo_left"><div><span>桥梁长度(m):</span><span style="color: #3cd93c;" id="roadName">' + data.QLCD
                    + '</span></div><div><span>荷载等级:</span><span style="color:#fff">' + (data.HZDJ_TYPE == null ? "" : data.HZDJ_TYPE)
                    + '</span></div><div><span>车行道宽度(m):</span><span style="color:#fff">' + (data.CXDK == null ? "" : data.CXDK)
                    + '</span></div><div><span>车行道面积(㎡):</span><span style="color: #ff9211;">' + (data.CXDMJ == null ? "" : data.CXDMJ)
                     + '</span></div><div><span>养护类型:</span><span style="color:#fff">' + (data.YHLX_TYPE == null ? "" : data.YHLX_TYPE)
                      + '</span></div><div><span>养护等级:</span><span style="color:#fff">' + (data.YHDJ_TYPE == null ? "" : data.YHDJ_TYPE)
                       + '</span></div><div><span>桥面标高（m）:</span><span style="color:#fff">' + (data.QMBG == null ? "" : data.QMBG)
                       + '</span></div><div><span>梁底标高（m）:</span><span style="color:#fff">' + (data.LDBG == null ? "" : data.LDBG)
                    + '</span></div></div><div class="partsbaseinfo_right"><div><span>桥面铺装材料:</span><span style="color:#fff">' + (data.QMPZCL_TYPE == null ? "" : data.QMPZCL_TYPE)
                    + '</span></div><div><span>总面积(㎡):</span><span style="color: #f6ef37;">' + (parseFloat(data.CXDMJ) + parseFloat(data.RXDMJ))
                    + '</span></div><div><span>人行道宽度(m):</span><span style="color:#fff">' + (data.RXDK == null ? "" : data.HZDJ_TYPE)
                    + '</span></div><div><span>人行道面积(㎡):</span><span style="color: #ff3030;">' + (data.RXDMJ == null ? "" : data.RXDMJ)
                     + '</span></div><div><span>竣工日期:</span><span style="color:#fff">' + (data.JGRQ == null ? "" : data.JGRQ.substr(0, 10))
                      + '</span></div><div><span>改造日期:</span><span style="color:#fff">' + (data.GZRQ == null ? "" : data.GZRQ.substr(0, 10))
                       + '</span></div><div><span>移交日期:</span><span style="color:#fff">' + (data.YJRQ == null ? "" : data.YJRQ.substr(0, 10))
                    + '</span></div></div>'
                   + '</div>'
                $(".partsbaseinfo").append(str);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
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
    },
}