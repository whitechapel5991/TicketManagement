"use strict";

var selectorConstants = SelectorConstants;
var urlConstants = UrlConstants;
var requestConstants = RequestConstants;

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

var AddAntiForgeryToken = function() {
    var formData = new FormData($(selectorConstants.PostForm).get(0));
    let token = $(selectorConstants.RequestVerificationToken).val();
    formData.append(requestConstants.RequestVerificationToken, token);
    return formData;
};

var updateContentWithMenu = function(contentUrl) {
    updateContent(contentUrl);
    $(selectorConstants.Menu).load(urlConstants.MenuUrl);
}

var OnSuccessChangeLanguage = function(data) {
    //Globalize('en');
    //console.log(Globalize.culture().name);
    reloadPage(data);
    loadcldr();
}

var getCookie = function(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

var loadcldr = function() {
    var currentCultureCode = getCookie('lang');
    if (currentCultureCode === undefined || currentCultureCode === null) {
        currentCultureCode = 'en';
    } else {
        currentCultureCode = currentCultureCode.toLowerCase();
    }

    var publicCdnGlobalizeCompleteUrl = "/TicketManagement.Web/Scripts/cldr/";

    $.when(
        $.get(publicCdnGlobalizeCompleteUrl + "supplemental/likelySubtags.json"),
        $.get(publicCdnGlobalizeCompleteUrl + "main/" + currentCultureCode + "/numbers.json"),
        $.get(publicCdnGlobalizeCompleteUrl + "supplemental/numberingSystems.json"),
        $.get(publicCdnGlobalizeCompleteUrl + "main/" + currentCultureCode + "/ca-gregorian.json"),
        $.get(publicCdnGlobalizeCompleteUrl + "main/" + currentCultureCode + "/timeZoneNames.json"),
        $.get(publicCdnGlobalizeCompleteUrl + "supplemental/timeData.json"),
        $.get(publicCdnGlobalizeCompleteUrl + "supplemental/weekData.json"),
        $.get(publicCdnGlobalizeCompleteUrl + "main/" + currentCultureCode + "/currencies.json")
    ).then(function () {

        // Normalize $.get results, we only need the JSON, not the request statuses.
        return [].slice.apply(arguments, [0]).map(function (result) {
            return result[0];
        });

    }).then(Globalize.load).then(function () {
        //if (currentCultureCode === 'be') {
        //    currentCultureCode = 'ru-BY';
        //}
        Globalize.locale(currentCultureCode);
        //customNumberParser = Globalize.numberParser();

        $(document).trigger("globalizeHasBeenLoadedEvent");
    });
}

var loadTimePicker = function() {
    var currentCultureCode = getCookie('lang');
    if (currentCultureCode === undefined || currentCultureCode === null) {
        currentCultureCode = 'en';
    } else {
        currentCultureCode = currentCultureCode.toLowerCase();
    }

    $.datepicker.setDefaults($.datepicker.regional[currentCultureCode]);
    $("input[time='jQueryUI']").timepicker();
}

var loadDataTimePicker = function() {
    var currentCultureCode = getCookie('lang');
    if (currentCultureCode === undefined || currentCultureCode === null) {
        currentCultureCode = 'en';
    } else {
        currentCultureCode = currentCultureCode.toLowerCase();
    }

    $.datepicker.setDefaults($.datepicker.regional[currentCultureCode]);

    if (currentCultureCode === 'be') {

        $("input[calendar='jQueryUI']").datepicker({ dateFormat: 'dd.mm.y' });
        
        $.validator.addMethod('date',
            function (value, element) {
                var ok = true;
                try {
                    $.datepicker.parseDate('dd.mm.y', value);
                }
                catch (err) {
                    ok = false;
                }
                return ok;
            });
    } else {
        $("input[calendar='jQueryUI']").datepicker();
    }
}

var loadCurrencyInput = function() {
    var currentCultureCode = getCookie('lang');
    if (currentCultureCode === undefined || currentCultureCode === null) {
        currentCultureCode = 'en';
    } else {
        currentCultureCode = currentCultureCode.toLowerCase();
    }

    $.widget( "ui.sspinner", $.ui.spinner, {
        _parse: function( val ) {
            if ( typeof val === "string" && val !== "" ) {
                val = window.Globalize && this.options.numberFormat ?
                    Globalize.numberParser({ maximumFractionDigits : 10 })(val) : +val;
            }
            return val === "" || isNaN( val ) ? null : val;
        },

        _format: function( value ) {
            if ( value === "" ) {
                return "";
            }

            if( window.Globalize && this.options.numberFormat ){

                this.options.currency || ( this.options.currency = 'COP' );

                switch( this.options.numberFormat ) {
                case 'C': return Globalize(this.options.culture).formatCurrency( value, this.options.currency ); break;
                default: return Globalize(this.options.culture).formatNumber( value ); break;
                }
            }
        }
    });

    //$.validator.methods.number = function (value, element) {
    //    return this.optional(element) || !isNaN(Globalize.parseFloat(value));
    //} 

    //$.validator.methods.range = function (value, element, param) {
    //    return this.optional(element) || (Globalize.parseFloat(value) >= param[0] && Globalize.parseFloat(value) <= param[1]);
    //}

    var config = {
        'input[currency="jQueryUI"]': {
            min: 0,
            max: 9999999,
            step: 1,
            start: 1000,
            culture: currentCultureCode,
            numberFormat: "C2"
        }
    }

    for (var selector in config) {
        if (Object.prototype.hasOwnProperty.call(config, selector)) {
            $(selector).each(function(index, el) {
                var $el = $(el);
                var spinner = {};

                if (!$el.sspinner("instance")) {
                    spinner = $el.sspinner(config[selector]);

                    if ($el.val() === '')
                        spinner.sspinner("value", 0);
                }
            });
        }
    }

    //$("input[currency='jQueryUI']").spinner({
    //    min: 0,
    //    max: 9999999,
    //    step: 1,
    //    start: 1000,
    //    culture: currentCultureCode,
    //    numberFormat: "C2",
    //    currency: 'COP'
    //});
}

var updateContent = function(contentUrl) {
    $(selectorConstants.MainContent).load(contentUrl);
}

var reloadContentWithUpdatingMenu = function(contentUrl) {
    $('#menu').load('/TicketManagement.Web/StartApp/ReloadMenu');
    updateContent(contentUrl);
}

var failureRequestHandlerFunc = function(data) {
    let msg = undefined;  
    try  
    {
        msg = JSON.parse(data.responseText);  
    }  
    catch(ers)  
    {  
        document.open(); 
        document.write(data.responseText);
        document.close();
    }
    if (msg !== undefined) {
        $(selectorConstants.ValidationSummary).empty();
        $(selectorConstants.ValidationSummary).removeClass("validation-summary-valid");
        $(selectorConstants.ValidationSummary).addClass("validation-summary-errors");
        let validationSummary = $(selectorConstants.ValidationSummary + ' ul');
        if (validationSummary.length === 0) {
            $(selectorConstants.ValidationSummary).append('<ul></ul>');
            validationSummary = $(selectorConstants.ValidationSummary + ' ul');
        }
        validationSummary.append('<li>' + msg + '</li>');
    }
}

var postRequest = function() {
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

function reloadPage(response) {
    if (response.success) {
        if (response.updateContentUrl === urlConstants.StartAppUrl) {
            document.location.reload();
        } else {
            $.ajax({
                url: response.redirectUrl,
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: function() {
                    reloadContentWithMenu(response.updateContentUrl);
                }
            });
        }
    }
}

function reloadContentWithMenu(contentUrl) {
    if (contentUrl === '/TicketManagement.Web/StartApp/Index') {
        document.location.reload();
    } else {
        $('#menu').load('/TicketManagement.Web/StartApp/ReloadMenu');
        updateContent(contentUrl);
    }
}




// change cost popup
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
        $.validator.unobtrusive.parse(form);
        
        if (form.valid()) {
            $.ajax({
                url: form.attr("action"),
                method: form.attr("method"),
                data: form.serialize(),
                success: successFunc,
                error:function (error) {
                    $(modalDialogSelector).find("#errorMsg").html(error).show();
                }
            });
        }
        return false;
    });
}