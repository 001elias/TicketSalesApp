function updateCartCount() {
    $.ajax({
        url: '/Cart/GetCartCount',
        type: 'GET',
        success: function (data) {
            $('#cartCount').text(data);
        }
    });
}

function sortEvents() {
    var selectedSortOrder = document.getElementById('sortOrder').value;
    var eventFilter = document.getElementById('eventFilter').value;
    window.location.href = `/Home/Index?sortOrder=${selectedSortOrder}&eventFilter=${eventFilter}`;    
}

$('.add-to-cart').click(function () {
    
    let cartItem = {
        ticketID: $(this).data('ticket-id'),
        eventID: $(this).data('event-id'),
        venue: $(this).data('venue'),
        eventDate: $(this).data('event-date'),
        eventDescription: $(this).data('description'),
        ticketType: $(this).data('ticket-type'),
        quantity: 1,
        price: $(this).data('price')
    };

    $.ajax({
        url: "/Cart/AddToCart", 
        type: 'POST',
        data: cartItem,
        success: function (response) {
            // Handle success (you can redirect, show a message, etc.)
            toastr.options.positionClass = 'toast-bottom-right';
            toastr.info('Ticket added to Cart !')
            updateCartCount();
        },
        error: function (error) {
            // Handle error
            console.log(error);
        }
    });
});// Update count when the page loads


$(document).ready(function () {
    updateCartCount();
})