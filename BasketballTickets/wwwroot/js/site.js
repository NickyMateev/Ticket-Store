// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function bookSeatBtn(event, ticketId, seatNo) {
    addToCart(ticketId);
    event.remove();
    createBookedBtn(ticketId, seatNo);
}

function unbookSeatBtn(event, ticketId, seatNo) {
    removeFromCart(ticketId);
    event.remove();
    removeBookedBtn(ticketId, seatNo);
}

function createBookedBtn(ticketId, seatNo) {
    var bookedSeats = document.getElementById("booked-seats");

    var seatDiv = document.createElement("div");
    seatDiv.classList.add("row", "center");

    var seatBtn = document.createElement("button");
    seatBtn.classList.add("btn", "btn-warning", "ticket-btn");
    seatBtn.textContent = "Seat #" + seatNo;
    seatBtn.onclick = function (e) {
        unbookSeatBtn(e.toElement, ticketId, seatNo);
    };

    seatDiv.appendChild(seatBtn);
    bookedSeats.appendChild(seatDiv);
}

function removeBookedBtn(ticketId, seatNo) {
    var availableSeats = document.getElementById("available-seats");

    var seatBtn = document.createElement("button");
    seatBtn.classList.add("btn", "btn-info", "ticket-btn");
    seatBtn.textContent = "Seat #" + seatNo;
    seatBtn.onclick = function (e) {
        bookSeatBtn(e.toElement, ticketId, seatNo);
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
            addTicketToNavMenu(ticketId);
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
            removeTicketFromNavMenu(ticketId);
        }
    });
}

function addTicketToNavMenu(ticketId) {
    var navbarTickets = document.getElementById("navbar-tickets");

    var ticketDiv = document.createElement("div");
    ticketDiv.id = "nav-ticket-" + ticketId;
    ticketDiv.classList.add("row", "shopping-cart-row");

    var link = document.createElement("a");
    link.classList.add("dropdown-item");
    link.setAttribute("asp-controller", "Tickets");
    link.setAttribute("asp-action", "Book");

    ticketDiv.appendChild(link);
    navbarTickets.appendChild(ticketDiv);


    cartQuantity = document.getElementById("cartQuantity");
    cartQuantity.textContent = parseInt(cartQuantity.textContent, 10) + 1
}

function removeTicketFromNavMenu(ticketId) {
    navbarItem = document.getElementById("nav-ticket-" + ticketId);
    navbarItem.remove();

    cartQuantity = document.getElementById("cartQuantity");
    cartQuantity.textContent = parseInt(cartQuantity.textContent, 10) - 1
}