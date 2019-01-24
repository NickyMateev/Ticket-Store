// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function bookSeat(event, seatNo) {
    event.classList.remove("btn-info");
    event.classList.add("btn-warning");

    var selectedSeats = document.getElementById("selected-seats");

    var seatDiv = document.createElement("div");
    seatDiv.classList.add("row", "center");
    var seatBtn = document.createElement("button");
    seatBtn.classList.add("btn", "btn-warning", "ticket-btn");
    seatBtn.textContent = "Seat #" + seatNo;
    seatBtn.onclick = function () {
        event.classList.remove("btn-warning");
        event.classList.add("btn-info");
        seatBtn.remove()
    };
    seatDiv.appendChild(seatBtn);
    selectedSeats.appendChild(seatDiv);
}