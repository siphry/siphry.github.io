var pageURL = window.location.href;
var id = pageURL.substr(pageURL.lastIndexOf('/') + 1);
var source = "/Items/Update/" + id

var ajax_call = function () {
    //your jQuery ajax code
    $.ajax({
        method: "GET",
        dataType: "json",
        url: source,
        success: successUpdate,
        error: errorAjax
    })
};

function successUpdate(latestBid) {
    //add new bids to table
    var latestPrice = $("#price").html();
    if (latestPrice < latestBid.price) {
        $(".tableBids").prepend("<tr>< td >" + latestBid.buyer + "</td ><td id='price'>" + latestBid.price + "</td></tr >")
    }
}

function errorAjax() {
    console.log("error in ajax");
}

function refresh() {
    var interval = 1000 * 5; // where X is your timer interval in X seconds
    window.setInterval(ajax_call, interval);
}
