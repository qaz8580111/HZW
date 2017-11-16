var config =
    {
        WebApi: 'http://localhost:2826/api/',
        PathAshx: 'http://localhost:2826/',
        WebManagerUrl: 'http://localhost:2826/',
        //页大小
        pageSize: 10,
        //广告图片
        AdverFile: 'C://HZW//HZWImage//AdvertPath\\',
        //2维地图地址
        Mapurl:'http://183.136.153.51:8787/arcgis/rest/services/NBCG20140707/MapServer',
    };
var configajaxurl =
    {
        //人员列表
        userurl: '/User/getalluser',
        //人员分页总数
        userurlPage: '/User/getalluser',
        //人员详情
        getallUserdetalis: '/User/getUserdetalis',
        //人员类型
        userTypetable: '/User/getall',
        //沿街店家类型
        shopsalongtheStreettype: '/StoreClasses/GetAllStoreClasses',
        //沿街店家列表
        shopsalongtheStreetlist: '/StoreBases/SelectAllStore',
        //沿街店家分页总数
        shopsalongtheStreetPage: '/StoreBases/GetStoreListCount',
        //沿街店家详情
        shopsalongtheStreetdetail: '/StoreBases/GetStoreModelByID',
        //沿街店家饼图
        shopsalongthepiedata: '/StoreBases/GetStoreTypeNum',
        //沿街店家总数
        shopsalongtheStreetcount: '/StoreBases/getCount',
        //户外广告类型
        huwaiganggaotype: '/Advert/GetAdvertTypes',
        //户外广告列表
        huwaiguanggaourllist: '/Advert/AdvertAllStore',
        //户外广告分页总数
        huwaiguanggaoPage: '/Advert/GetAdvertListCount',
        //户外广告详情
        huwaiguanggaodetail: '/Advert/GetAdvertModelByID',
        //户外最新广告4条
        huwaiguanggaobestnew: '/Advert/GetNewAdvert',
        //户外广告到期4条
        huwaiguanggaopastdue: '/Advert/GetEndDateAdvert',
        //户外广告总数
        huwaiguanggaocount: '/Advert/GetAdvertTypeNumCount',
        //户外广告类型统计饼图
        huwaiguanggaotypepie: '/Advert/GetAdvertTypeNum',
        //监控列表
        cameralist: '/Camera/getcameraList',
        //监控分页:
        cameraPage: '/Camera/getcameraListPageIndex',
        //监控树
        cameraTreeData: '/Camera/getListTree',
        //监控详情
        cameradetails: '/Camera/getdetails',
        //监控区域名称
        cameraregionName: '/Camera/getaregionlist',
        //监控区域值
        cameraregionValue: '/Camera/getNumberByRegion',
        //监控网格数据
        cameraGridData: '/Camera/getCameraGridData',
        //监控类型统计
        cameraPieData: '/Camera/getCameraTypeNumber',
        //监控类型count统计
        cameraTypeCount:'/Camera/getCameraTypeCount',
        //部件底部内容数据
        assemblyunitbottom: '/PartsFacilities/gettypecout',
        //部件类型数据
        PartsType: '/PartsFacilities/getpartsType',
        //部件数据列表和筛选
        PartsList: '/PartsFacilities/getpartsbyType',
        //部件页码和筛选
        PartsPageIndex: '/PartsFacilities/getpartsbyType',
        //部件概要详情
        PartsDelits: '/PartsFacilities/getPartsById',
        //案件类型
        caseType: '/Case/getType',
        //案件列表
        caseList: '/Case/getall',
        //案件页码
        casePageIndex: '/Case/getcasePageIndex',
        //案件详情
        caseDetils: '/Case/getSingle',
        //案件最新4条
        casefour: '/Case/getHotcase',
        //案件Line图Legend
        caseLineLegend: '/Case/getLinelegend',
        //案件Line图data
        caseLineData: '/Case/getWeekNowCaseNumber',
        //案件柱状图
        casebarLegend: '/Case/getcasetype',
        //案件柱状图data
        casebardata: '/Case/getcasetypeNumber',
        //沿街店家案件
        caseforstreet: '/Case/getcaseforStreet',
        //六边形数据
        hexagoncount: '/statistics/getCount',
        //无人机列表
        wrjlist: '/Camera/getwrj',
        //无人机列表页码
        wrjlistPage:'/Camera/getwrjPage',
        //周边监控
        zbAreaCamera: '/Camera/getcamera',
        //部件框选
        partclick: '/PartsFacilities/getPartsByswmxid',
        //人员轨迹接口
        peopleTrack: '/Coordinate/GetUserCoordinate',
        //感知设备
        DeviceList: '/ProjectDev/GetListProject',
        //感知设备页码
        DeviceListPageIndex: '/ProjectDev/GetCountProject',
        //感知设备设备类型数据
        DevicetypeList: '/ProjectDev/GetProjectCountMid',
        //感知设备报警数据
        DeviceTableData: '/ProjectDev/GetProjectRight',
        //感知设备数量
        DeviceNumber: '/ProjectDev/GetSumCount',
        //今日在岗人数
        TodatePeopleCount: '/User/GetCountUserNowDate',
 };
var configtype =
    {
        yanjiedianjiaIcon: "Panel_Icon yjdj",
        yanjiedianjia: "yjdj",
        anjianIcon: "Panel_Icon aj",
        anjian: "aj",
        jiankongIcon: "Panel_Icon jk",
        jiankong: "jk",
        huwaiguanggaoIcon: "Panel_Icon hwgg",
        huwaiguanggao: "hwgg",
        wurenjiIcon: "Panel_Icon wrj",
        wurenji: "wrj",
        ganzhishebeiIcon: "Panel_Icon gzsb",
        ganzhishebei: "gzsb",
        renyuancheliangIcon: "Panel_Icon rycl",
        renyuancheliang: "rycl",
        renyuanIcon: "Panel_Icon ry",
        renyuan: "ry",
        cheliangIcon: "Panel_Icon cl",
        cheliang: "cl",
        bujianIcon: "Panel_Icon bj",
        bujian: "bj",
        zbjkcameraId:"zbjk",
    };

function subStringvalue(value, sublength) {
    if (value != null) {
        if (value.length > sublength) {
            return value.substring(0, sublength) + "...";
        } else {
            return value;
        }
    }
}