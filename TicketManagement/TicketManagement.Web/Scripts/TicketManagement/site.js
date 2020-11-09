"use strict";

let selectorConstants = SelectorConstants;
let urlConstants = UrlConstants;
let requestConstants = RequestConstants;

$(function () {
    //setup ajax error handling
    $.ajaxSetup({
        error: function(qXhr) {
            $(selectorConstants.Loader).hide();        
            var msg = undefined;  
            try  
            {
                msg =JSON.parse(qXhr.responseText);  
            }  
            catch(ers)  
            {  
                document.open(); 
                document.write(qXhr.responseText);
                document.close();
            }
            if (msg !== undefined) {
                $(selectorConstants.ValidationSummary).empty();
                $(selectorConstants.ValidationSummary).removeClass("validation-summary-valid");
                $(selectorConstants.ValidationSummary).addClass("validation-summary-errors");
                var validationSummary = $(selectorConstants.ValidationSummary + ' ul');
                if (validationSummary.length === 0) {
                    $(selectorConstants.ValidationSummary).append('<ul></ul>');
                    validationSummary = $(selectorConstants.ValidationSummary + ' ul');
                }
                validationSummary.append('<li>' + msg + '</li>');
            }
        }
    });

    // set verification token in the header
    $.ajaxPrefilter(function (options, originalOptions, jqXhr) {
        if (originalOptions.type === "POST")
            jqXhr.setRequestHeader(requestConstants.RequestVerificationToken, $(selectorConstants.RequestVerificationToken).val());
    });


});

let AddAntiForgeryToken = function() {
    var formData = new FormData($(selectorConstants.PostForm).get(0));
    let token = $(selectorConstants.RequestVerificationToken).val();
    formData.append(requestConstants.RequestVerificationToken, token);
    return formData;
};

let updateContentWithMenu = function(contentUrl) {
    updateContent(contentUrl);
    $(selectorConstants.Menu).load(urlConstants.MenuUrl);
}

let updateContent =function(contentUrl) {
    $(selectorConstants.MainContent).load(contentUrl);
}

let reloadContentWithUpdatingMenu = function(contentUrl) {
    $('#menu').load('/TicketManagement.Web/StartApp/ReloadMenu');
    updateContent(contentUrl);
}

let postRequest = function() {
    let $form = $(selectorConstants.PostForm);
    let postUrl = $form.attr( 'action' );

    function postAjax() {
        $(selectorConstants.PostForm + ' button[type=submit]').one('click',
            function() {
                $.validator.unobtrusive.parse($form);
                $form.submit(function (e) {
                    e.preventDefault();
                    $(selectorConstants.ValidationSummary).empty();
                    $(selectorConstants.ValidationSummary).removeClass("validation-summary-valid");
                    $(selectorConstants.ValidationSummary).addClass("validation-summary-errors");
                    $(selectorConstants.PostForm + ' ' + selectorConstants.Loader).show(); 
                    if ($form.valid()) {
                        $.ajax({
                            url: postUrl,
                            type: 'POST',
                            contentType: false, // Not to set any content header  
                            processData: false, // Not to process data  
                            async: true,
                            data: AddAntiForgeryToken(this),
                            success: function(data) {
                                $(selectorConstants.PostForm + ' ' + selectorConstants.Loader).hide();
                                $(selectorConstants.PostForm + ' ' + selectorConstants.ValidationSummary).removeClass("validation-summary-errors");
                                $(selectorConstants.PostForm + ' ' + selectorConstants.ValidationSummary).addClass("validation-summary-valid");
                                reloadContentWithUpdatingMenu(data.returnContentUrl);
                            }
                        });
                    } else {
                        $(selectorConstants.Loader).hide();
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
                success: function() {
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
        updateContent(contentUrl);
    }
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