// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function bookSeat(event, ticketId, seatNo) {
    addToCart(ticketId);
    event.remove();

    var bookedSeats = document.getElementById("booked-seats");

    var seatDiv = document.createElement("div");
    seatDiv.classList.add("row", "center");

    var seatBtn = document.createElement("button");
    seatBtn.classList.add("btn", "btn-warning", "ticket-btn");
    seatBtn.textContent = "Seat #" + seatNo;
    seatBtn.onclick = function (e) {
        unbookSeat(e.toElement, ticketId, seatNo);
    };

    seatDiv.appendChild(seatBtn);
    bookedSeats.appendChild(seatDiv);
}

function unbookSeat(event, ticketId, seatNo) {
    removeFromCart(ticketId);
    event.remove();

    var availableSeats = document.getElementById("available-seats");

    var seatBtn = document.createElement("button");
    seatBtn.classList.add("btn", "btn-info", "ticket-btn");
    seatBtn.textContent = "Seat #" + seatNo;
    seatBtn.onclick = function (e) {
        bookSeat(e.toElement, ticketId, seatNo);
    };

    availableSeats.appendChild(seatBtn);
}

function addToCart(ticketId) {
    $.ajax({
        type: "POST",
        url: "/ShoppingCart/Add",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(ticketId),
        success: function () {
            cartQuantity = document.getElementById("cartQuantity");
            cartQuantity.textContent = parseInt(cartQuantity.textContent, 10) + 1
        }
    });
}

function removeFromCart(ticketId) {
    $.ajax({
        type: "DELETE",
        url: "/ShoppingCart/Remove",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(ticketId),
        success: function () {
            cartQuantity = document.getElementById("cartQuantity");
            cartQuantity.textContent = parseInt(cartQuantity.textContent, 10) - 1
        }
    });
}