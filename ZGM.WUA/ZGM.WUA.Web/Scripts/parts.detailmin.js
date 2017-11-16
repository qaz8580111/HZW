$(function () {
    detailMin.init(parent.parts.Part, parent.parts.Type);
});

var detailMin = {
    Type: null,
    Part: null,
    init: function (part, type) {
        this.Type = type;
        this.Part = part;
        switch (this.Type) {
            case "Road":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/road.png')");
                this.initRoad(this.Part);
                break;
            case "Bridge":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/ql.png')");
                this.initBridge(this.Part);
                break;
            case "StreetLamp":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/ld.png')");
                this.initStreetLamp(this.Part);
                break;
            case "LandscapeLamp":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/jgd.png')");
                this.initLandscapeLamp(this.Part);
                break;
            case "Pump":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/bz.png')");
                this.initPump(this.Part);
                break;
            case "CoverLoad":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/jg.png')");
                this.initCoverLoad(this.Part);
                break;
            case "ParkGreen":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/gyld.png')");
                this.initPart(this.Part);
                break;
            case "LoadGreen":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/dlld.png')");
                this.initPart(this.Part);
               break;
            case "ProtectionGreen":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/fhld.png')");
                this.initPart(this.Part);
                break;
            case "Toilt":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/gc.png')");
                this.initPart(this.Part);
               break;
            case "River":
                $(".partsminicoin").css("background-image", "url('/images/partsmin/river.png')");
                this.initPart(this.Part);
                break;
        }
    },
    initPart: function (road) {
        $(".partsminposition").html(this.Part.name);
    },
    initCoverLoad: function (road) {
        $(".partsminposition").html(this.Part.CoverLoadName);
    },
    initPump: function (road) {
        $(".partsminposition").html(this.Part.PumpName);
    },
    initLandscapeLamp: function (road) {
        $(".partsminposition").html(this.Part.LLName);
    },
    initRoad: function (road) {       
        $(".partsminposition").html(this.Part.RoadName);
    },
    initBridge: function (Bridge) {
        $(".partsminposition").html(this.Part.BridgeName);
    },
    initStreetLamp: function (StreetLamp) {
        $(".partsminposition").html(this.Part.SLLName);
    },
    initDetail: function () {
        parent.parts.initDetail(this.Part);
    },
    close: function () {
        parent.detailMin.close();
    }
}