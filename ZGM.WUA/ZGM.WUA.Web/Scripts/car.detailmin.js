$(function () {
	detailMin.init(parent.car.Car);
	parent.mapSW.deepView();
});

var detailMin = {
	Car: null,
	init: function (car) {
		this.Car = car;
		$(".cnumber").html(this.Car.CarNumber);
	},
	initDetail: function () {
		parent.car.initDetail(this.Car);
	},
	getSearchArea: function () {
	    parent.car.getSearchArea(this.Car);
	},
	foundCircum: function () {
	    parent.car.foundCircum(this.Car);
	},
	traceReplay: function () {
	    parent.car.traceReplay(this.Car);
	},
	close: function () {
		parent.detailMin.close();
	}
}