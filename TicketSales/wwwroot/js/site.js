toastr.options.positionClass = 'toast-bottom-right';
function updateCartCount() {
    $.ajax({
        url: '/Cart/GetCartCount',
        type: 'GET',
        success: function (data) {
            $('#cartCount').text(data);
        }
    });
}

function refreshEvenList() {
    var selectedSortOrder = document.getElementById('sortOrder').value;
    var eventFilter = document.getElementById('eventFilter').value;
    // refresh partial view only
    $.get("/Home/GetEvents?eventFilter=" + eventFilter + "&sortOrder=" + selectedSortOrder, function (data) {
        $("#eventListContainer").html(data);
    });
}

function sortEvents() {   
    refreshEvenList();   
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


function addBookmark(eventId) {
    $.ajax({
         url: "/Bookmarks/Add",  // Make sure this resolves to the correct URL
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        type: 'POST',
        data: {
            eventId: eventId
        },
        success: function (result) {
            // Handle the successful bookmark addition, e.g., change the button appearance
           
            toastr.info('Event bookmarked!')     
            //window.location.href = "/home/index";
            refreshEvenList();
        },
        error: function (xhr, status, error) {
            // Handle errors
            alert("Error bookmarking event: " + error);
        }
    });
}

function removeBookmark(eventId) {
    $.ajax({
        url: "/Bookmarks/Remove",  // Make sure this resolves to the correct URL
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        type: 'POST',
        data: {
            eventId: eventId
        },
        success: function (result) {
            // Handle the successful bookmark removal,
           
            toastr.info('Bookmark removed!');
            refreshEvenList();            
        },
        error: function (xhr, status, error) {
            // Handle errors
            alert("Error bookmarking event: " + error);
        }
    });
}