$(function () {
    //setup ajax error handling
    $.ajaxSetup({
        error: function(qXHR, status, thrownError, data) {
                           
            var msg = undefined;  
            try  
            {
                msg =JSON.parse(qXHR.responseText);  
            }  
            catch(ers)  
            {  
                document.open(); 
                document.write(qXHR.responseText);
                document.close();
            }
            if (msg !== undefined) {
                $('#validationSummary').empty();
                $('#validationSummary').removeClass("validation-summary-valid");
                $('#validationSummary').addClass("validation-summary-errors");
                var validationSummary = $('#validationSummary ul');
                if (validationSummary.length === 0) {
                    $('#validationSummary').append('<ul></ul>');
                    validationSummary = $('#validationSummary ul');
                }
                validationSummary.append('<li>' + msg + '</li>');
            }
        },
    });

    // set verification token in the header
    $.ajaxPrefilter(function (options, originalOptions, jqXHR) {
        if (originalOptions.type === "POST")
            jqXHR.setRequestHeader('__RequestVerificationToken', $('body input[name=__RequestVerificationToken]').val());
    });
});

let AddAntiForgeryToken = function(form) {
    let data = $(form).serialize();
    let token = $('body input[name=__RequestVerificationToken]').val();
    $.extend(data, { '__RequestVerificationToken': token });
    return data;
};

function postRequest() {
    let $form = $('#postForm');
    let postUrl = $form.attr( 'action' );

    function postAjax() {
        $('#postForm .submit-btn').one('click',
            function() {
                $.validator.unobtrusive.parse($form);
                $form.submit(function (e) {
                    e.preventDefault();
                    if ($form.valid()) {
                        $.ajax({
                            url: postUrl,
                            type: 'POST',
                            async: true,
                            data: AddAntiForgeryToken(this),
                            success: function (data) {
                                $('#validationSummary').removeClass("validation-summary-errors");
                                $('#validationSummary').addClass("validation-summary-valid");
                                reloadContentWithUpdatingMenu(data.returnContentUrl);
                            },
                        });
                    }
                    return false;
                });
            });
    }

    postAjax();
};

function loginSuccess(successRedirect) {
    window.location.href = successRedirect;
}

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

function loadCart() {
    reloadContentWithMenu('/TicketManagement.Web/Cart/Index');
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

function loadUserProfile() {
    reloadContentWithMenu('/TicketManagement.Web/UserProfile/Index');
}

function reloadContentWithMenu(contentUrl) {
    if (contentUrl === '/TicketManagement.Web/StartApp/Index') {
        document.location.reload();
    } else {
        $('#menu').load('/TicketManagement.Web/StartApp/ReloadMenu');
        reloadContent(contentUrl);
    }
}

function reloadContentWithUpdatingMenu(contentUrl) {
    $('#menu').load('/TicketManagement.Web/StartApp/ReloadMenu');
    reloadContent(contentUrl);
}

function reloadContent(contentUrl) {
    $('#content').load(contentUrl);
}

function showPopupInit(modalDialogSelector, dialogContentSelector, invokerSelector) {
    $.ajaxSetup({ cache: false });
    $(invokerSelector).click(function (e) {
 
        e.preventDefault();
        $.get(this.href, function (data) {
            $(dialogContentSelector).html(data);
            $(modalDialogSelector).modal('show');
        });
    });
}

function changeCostPopupPost(modalDialogSelector, invokerSelector) {
    let successFunc = function (response) {
        $(modalDialogSelector).modal('toggle');
        $(modalDialogSelector).find("#errorMsg").hide();
        $("#areaPrice").html(response.newPrice);
    }

    postPopupInit(modalDialogSelector, successFunc, invokerSelector);
}

function postPopupInit(modalDialogSelector, successFunc, invokerSelector) {
    $(modalDialogSelector).on("submit", invokerSelector, function (e) {
        e.preventDefault();

        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),
            data: form.serialize(),
            success: successFunc,
            error:function (error) {
                $(modalDialogSelector).find("#errorMsg").html(error).show();
            }
        });
    });
}