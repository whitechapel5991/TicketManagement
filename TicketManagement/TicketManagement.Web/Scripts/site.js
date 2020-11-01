function postRequest(postUrl, successRedirect) {
    $('#postForm .submit-btn').on('click',
        function() {
            var $form = $('#postForm');
            $.validator.unobtrusive.parse($form);
            $form.submit(function () {
                if ($form.valid()) {
                    $.ajax({
                        url: postUrl,
                        async: true,
                        type: 'POST',
                        data: $(this).serialize(),
                        beforeSend: function (xhr, opts) {
                            //alert(this.url);
                        },
                        contentType: 'application/json; charset=utf-8',
                        complete: function () {
                        },
                        success: function (data) {
                            //alert("success");
                            console.log(data);
                            window.location.href = successRedirect;
                        },
                        error: function(qXHR, status, thrownError) {
                           
                            var msg = "";  
                            try  
                            {  
                                var responseText = jqXHR.responseJson;
                                msg =JSON.parse(qXHR.responseText);  
                            }  
                            catch(ers)  
                            {  
                                msg =qXHR.responseText;  
                            }
                            console.error('Error submitting form', msg);
                            alert(msg);
                        },
                    });
                }
                return false;
            });
        });
}


$(function () {
    //setup ajax error handling
    $.ajaxSetup({
        error: function (x, status, error) {
            var msg = "";  
            try  
            {  
                msg =JSON.parse(error);  
            }  
            catch(ers)  
            {  
                msg =error;  
            }
            console.log(msg);
        }
    });
});


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