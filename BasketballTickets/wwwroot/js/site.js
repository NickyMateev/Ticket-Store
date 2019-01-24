// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.dropdown-menu').click(function (e) {
    e.stopPropagation();
});

function bookSeatBtn(event, ticket) {
    addToCartRequest(ticket);
    event.remove();
    createBookedBtn(ticket);
}

function unbookSeatBtn(event, ticket) {
    removeFromCartRequest(ticket.id);
    event.remove();
    removeBookedBtn(ticket);
}

function createBookedBtn(ticket) {
    var bookedSeats = document.getElementById("booked-seats");

    var seatDiv = document.createElement("div");
    seatDiv.classList.add("row", "center");

    var seatBtn = document.createElement("button");
    seatBtn.classList.add("btn", "btn-warning", "ticket-btn");
    seatBtn.textContent = "Seat #" + ticket.seatNo + " ($" + ticket.price + ")";
    seatBtn.onclick = function (e) {
        unbookSeatBtn(e.toElement, ticket);
    };

    seatDiv.appendChild(seatBtn);
    bookedSeats.appendChild(seatDiv);
}

function removeBookedBtn(ticket) {
    var availableSeats = document.getElementById("available-seats");

    var seatBtn = document.createElement("button");
    seatBtn.classList.add("btn", "btn-info", "ticket-btn");
    seatBtn.textContent = "Seat #" + ticket.seatNo + " ($" + ticket.price + ")";
    seatBtn.onclick = function (e) {
        bookSeatBtn(e.toElement, ticket);
    };

    availableSeats.appendChild(seatBtn);
}

function addToCartRequest(ticket) {
    $.ajax({
        type: "POST",
        url: "/ShoppingCart/Add",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(ticket.id),
        success: function () {
            addTicketToNavMenu(ticket);
        }
    });
}

function removeFromCartRequest(ticketId) {
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

function addTicketToNavMenu(ticket) {
    var navbarTickets = document.getElementById("navbar-tickets");

    var ticketDiv = document.createElement("div");
    ticketDiv.id = "nav-ticket-" + ticket.id;
    ticketDiv.classList.add("row", "shopping-cart-row");

    var link = document.createElement("a");
    link.classList.add("dropdown-item");
    link.setAttribute("asp-controller", "Tickets");
    link.setAttribute("asp-action", "Book");
    link.setAttribute("asp-route-gameId", ticket.id);

    var homeTeamImg = document.createElement("img");
    homeTeamImg.setAttribute("src", ticket.homeTeamImg.substr(1));
    homeTeamImg.setAttribute("width", "20px");
    homeTeamImg.setAttribute("height", "16px");
    homeTeamImg.setAttribute("asp-append-version", true);

    var textTag = document.createElement("text");
    textTag.textContent = "\xa0vs\xa0";

    var awayTeamImg = document.createElement("img");
    awayTeamImg.setAttribute("src", ticket.awayTeamImg.substr(1));
    awayTeamImg.setAttribute("width", "20px");
    awayTeamImg.setAttribute("height", "16px");
    awayTeamImg.setAttribute("asp-append-version", true);

    var dateTextTag = document.createElement("text");
    dateTextTag.textContent = "\xa0Nov 28\xa0";

    var seatTextTag = document.createElement("text");
    seatTextTag.textContent = "\xa0(Seat #" + ticket.seatNo + ")\xa0\xa0\xa0\xa0";

    var removeLink = document.createElement("a");
    removeLink.classList.add("btn", "btn-danger", "btn-xs")
    removeLink.onclick = function () {
        removeFromCartRequest(ticket.id);
    }

    var removeGlyphicon = document.createElement("i");
    removeGlyphicon.classList.add("glyphicon", "glyphicon-remove")

    removeLink.appendChild(removeGlyphicon);

    link.appendChild(homeTeamImg);
    link.appendChild(textTag);
    link.appendChild(awayTeamImg);
    link.appendChild(dateTextTag);
    link.appendChild(seatTextTag);
    link.appendChild(removeLink);

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