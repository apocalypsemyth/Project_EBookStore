$(document).ready(function () {
    function BuildShoppingCartBadge() {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx",
            method: "GET",
            success: function (orderBookAmount) {
                if (orderBookAmount === "0")
                    return;
                else {
                    var shoppingCartBadgeHtml =
                        `
                            購物車
                            <span id="spnshoppingcartbadge" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
                                ${orderBookAmount}
                                <span class="visually-hidden">unread messages</span>
                            </span>
                        `;

                    $("#btnShoppingCart").empty();
                    $("#btnShoppingCart").append(shoppingCartBadgeHtml);
                }
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    BuildShoppingCartBadge();

    $("#btnShoppingCart").click(function (e) {
        e.preventDefault();

        window.location = "OrderDetail.aspx";
    })

    function getUrlParameter(strParam) {
        var strPageUrl = window.location.search.substring(1),
            strUrlVariables = strPageUrl.split('&'),
            strParameterName

        for (var i = 0; i < strUrlVariables.length; i++) {
            strParameterName = strUrlVariables[i].split('=');

            if (strParameterName[0] === strParam) {
                return strParameterName[1] === undefined
                    ? true
                    : decodeURIComponent(strParameterName[1]);
            }
        }

        return false;
    }
    function AddShoppingCart(strBookID) {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=CREATE",
            method: "POST",
            data: { "bookID": strBookID },
            success: function (orderBookAmount) {
                if (orderBookAmount === "0" || orderBookAmount === "NULL")
                    return;
                else {
                    var shoppingCartBadgeHtml =
                        `
                            購物車
                            <span id="spnshoppingcartbadge" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
                                ${orderBookAmount}
                                <span class="visually-hidden">unread messages</span>
                            </span>
                        `;

                    $("#btnShoppingCart").empty();
                    $("#btnShoppingCart").append(shoppingCartBadgeHtml);
                }
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    $("#btnAddShoppingCart").click(function (e) {
        e.preventDefault();

        var strOrBolBookID = getUrlParameter("ID");
        if (typeof strOrBolBookID === "boolean")
            return;
        else 
            AddShoppingCart(strOrBolBookID);
    });

    //以下為使用者究其勾選哪一CheckBox的部分實作區
    function getCheckedCheckBox() {
        var selected = [];
        $("#divOrderBookList label.list-group-item input:checked").each(function () {
            selected.push($(this).attr('name'));
        });
    }
    function DeleteShoppingCart(strBookID) {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=DELETE",
            method: "POST",
            data: { "bookID": strBookID },
            success: function (orderBookAmount) {
                if (orderBookAmount === "0" || orderBookAmount === "NULL")
                    return;
                else {
                    var shoppingCartBadgeHtml =
                        `
                            購物車
                            <span id="spnshoppingcartbadge" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
                                ${orderBookAmount}
                                <span class="visually-hidden">unread messages</span>
                            </span>
                        `;

                    $("#btnShoppingCart").empty();
                    $("#btnShoppingCart").append(shoppingCartBadgeHtml);
                }
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    $("input[id*=ckbOrderBook]").click(function (e) {
        e.preventDefault();

        DeleteShoppingCart();
    })
})