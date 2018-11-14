var gmap = {};

$(function () {
    //ShowPartial("Home/TnodPartial", "ulTnod");
    //ShowPartial("Home/RssPartial", "ulRss");
    ShowPartial("Home/YoutubePartial", "dvYoutube");
    //ShowPartial("Home/ForumPartial", "ulForum");
    //ShowPartial("Home/MayorPartial", "ulMayor"); //此段會洗畫面 導致資料不正常
    //ShowPartial("Home/SuggestPartial", "dvSuggest");
    mapInit();

});

function MayorDetail(id) {
    if (!gmap.data) {
        return;
    }
    var filter = gmap.data.filter(function (d) {
        return d._id === id;
    });
    if (filter) {
        var data = filter[0];
        MarkerCenter(data.坐標Y, data.坐標X);
        $('#mayorTitle').html(data.標題);
        $('#mayorEvent').html(data.行程事由);
        $('#mayorTime').html(moment(data.開始時間).format("YYYY年MMMDo a h:mm") + " ~ " + moment(data.結束時間).format("a h:mm"));
        $('#mayorHolder').html(data.主辦單位);
        $('#mayorLocation').html(data.行程地點);
    }
}

function MarkerCenter(lat, lng) {
    console.log('進到MarkerCenter');
    var p = new google.maps.LatLng(lat, lng);
    gmap.marker.setPosition(p);
    setTimeout(function () {
        google.maps.event.trigger(gmap.map, "resize");
        gmap.map.setCenter(p);
    }, 250);

}

function mapInit() {
    var position = new google.maps.LatLng(22.991608, 120.184955);
    var myOptions = {
        zoom: 16,
        center: position,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    gmap.map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

    gmap.marker = new google.maps.Marker({
        position: position,
        map: gmap.map
    });
}