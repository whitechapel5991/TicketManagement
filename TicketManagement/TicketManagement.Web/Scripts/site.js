function reloadPage(response) {
    if (response.success) {
        $.ajax({
                url: '/TicketManagement.Web/',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: function(result) {
                    reloadContentWithMenu(response.updateContentUrl);
                }
            });
    }
}

function redirectToMainPage(response) {
    if (response.success) {
        window.location = '/TicketManagement.Web/';
    }
}

function loadEvents() {
    reloadContentWithMenu('/TicketManagement.Web/Event/Events');
}

function loadEventAreaDetails(eventAreaId) {
    reloadContentWithMenu('/TicketManagement.Web/Event/EventAreaDetail?eventAreaId=' + eventAreaId);
}

function loadEventManagerEvents() {
    reloadContentWithMenu('/TicketManagement.Web/EventManager/Event');
}

function reloadContentWithMenu(contentUrl) {
    if (contentUrl === '/TicketManagement.Web/StartApp/Index') {
        document.location.reload();
    } else {
        $('#menu').load('/TicketManagement.Web/StartApp/ReloadMenu');
        $('#content').load(contentUrl);
    }
}