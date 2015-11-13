
//alert("site");

//$(".formatPrice").each(function () {
//    $(this).text(formatPrice($(this).text()));
//})

//function removeFromCart(pId, row) {
//    $.ajax({
//        url: '@Url.Action("RemoveFromCart","Home")',
//        data: {
//            ProductId: pId
//        },
//        method: "POST",
//        success: function (result) {
//            if (result == -1) {
//                alert("feil ved sletting av vare: " + result);
//            }
//            else {
//                row.remove();
//                $(".cartCounter").text(JSON.parse(result));
//                updateSumTotal();

//            }
//        }, error: function (result) {
//            alert("remove: error: " + JSON.stringify(result));
//        }
//    })
//}

//function updateSumTotal() {
//    $.ajax({
//        url: '@Url.Action("GetSumTotalCart","Home")',
//        success: function (result) {
//            $(".shoppingcart_summary #sumTotal").text(result);

//        }, error: function (result) {
//            alert("error in " +@Request.RawUrl +" : " + result);
//        }
//    })

//    $(".shoppingcart_summary #sumTotal").text(sumTotal);
//}

//function formatPrice(price) {
//    var formatted = "";
//    console.log(price.length)
//    var counter = 0;
//    for (var i = (price.length - 1) ; i >= 0; i--) {
//        if (counter % 3 == 0)
//            formatted = " " + formatted;
//        formatted = price.charAt(i) + formatted;
//        counter++;
//        console.log(formatted);
//    }

//    return formatted;
//}