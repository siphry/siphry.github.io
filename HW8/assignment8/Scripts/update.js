//finds the item id from the url
var pageURL = window.location.href;
var id = pageURL.substr(pageURL.lastIndexOf('/') + 1);
var source = "/Items/Update/" + id
//gets the latest price from the item's bids for comparrison
var latestPrice = $("#price").html();

var ajax_call = function () {
    //jQuery ajax code
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
    if (latestBid.price > latestPrice) {
        $("#inner").prepend("<tr><td>" + latestBid.buyer + "</td><td>" + latestBid.price + "</td></tr>");
        latestPrice = latestBid.price;
    }
}

function errorAjax() {
    console.log("error in ajax");
}

function refresh() {
    var interval = 1000 * 5; // where X is your timer interval in X seconds
    window.setInterval(ajax_call, interval);
}
