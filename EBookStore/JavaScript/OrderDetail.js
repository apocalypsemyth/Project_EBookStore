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

    function getCheckedBookID() {
        var checkedBookID = [];

        $("#divOrderBookList label.list-group-item input:checked").each(function () {
            var aHref = $(this).siblings("a.btn").attr("href");
            var strKeyValue = aHref.substring(aHref.indexOf("?") + 1);
            var strValue = strKeyValue.split("=")[1];

            checkedBookID.push(strValue);
        });

        return checkedBookID.join();
    }
    function DeleteOrderBookInShoppingCart(strCheckedBookID) {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=DELETE",
            method: "POST",
            data: { "checkedBookID": strCheckedBookID },
            success: function (bookList) {
                var orderBookInShoppingCartHtml = "";
                for (var book of bookList) {
                    orderBookInShoppingCartHtml +=
                        `
                            <label class="list-group-item">
                                <input class="form-check-input me-1" type="checkbox" />
                                <a class="btn" href="BookDetail.aspx?ID=${book.BookID}" title="前往查看：${book.BookName}">
                                    <img class="card-img-top" src="${book.Image}" />
                                    <div class="card-body">
                                        <h5 class="card-title">書名:${book.BookName}
                                        </h5>
                                        <p class="card-text">${book.Price}</p>
                                    </div>
                                </a>
                            </label>
                        `;
                }

                $("#divOrderBookList").html(orderBookInShoppingCartHtml);
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    $("#btnDeleteOrderBook").click(function (e) {
        e.preventDefault();

        var strCheckedBookID = getCheckedBookID();
        DeleteOrderBookInShoppingCart(strCheckedBookID);
        BuildShoppingCartBadge();
    });

    function getSelectedPaymentID() {
        var strSelectedPaymentID = "";

        $("select[id*=ddlPaymentList]").select(function (e) {
            strSelectedPaymentID = e.target.val();
        });

        return strSelectedPaymentID;
    }
    function CompleteOrder(numOrderStatus) {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=UPDATE",
            method: "POST",
            data: { "orderStatus": numOrderStatus },
            success: function (bookList) {
                //var orderBookInShoppingCartHtml = "";
                //for (var book of bookList) {
                //    orderBookInShoppingCartHtml +=
                //        `
                //            <label class="list-group-item">
                //                <input class="form-check-input me-1" type="checkbox" />
                //                <a class="btn" href="BookDetail.aspx?ID=${book.BookID}" title="前往查看：${book.BookName}">
                //                    <img class="card-img-top" src="${book.Image}" />
                //                    <div class="card-body">
                //                        <h5 class="card-title">書名:${book.BookName}
                //                        </h5>
                //                        <p class="card-text">${book.Price}</p>
                //                    </div>
                //                </a>
                //            </label>
                //        `;
                //}

                //$("#divOrderBookList").html(orderBookInShoppingCartHtml);
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    $("#btnCompleteOrder").click(function (e) {
        e.preventDefault();

        var strSelectedPaymentID = getSelectedPaymentID();
        console.log(strSelectedPaymentID);
        //CompleteOrder(1);
    })
})