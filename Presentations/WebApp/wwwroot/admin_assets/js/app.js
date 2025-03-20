// loading process
if ($(".moneyformat") != undefined && $(".moneyformat").length > 0) {
    $(".moneyformat").maskMoney({ thousands: '.', decimal: ',', allowZero: true, suffix: ' €' });
    $('.moneyformat').trigger('mask.maskMoney');
}

$(document).on('select2:open', () => {
    document.querySelector('.select2-container--open .select2-search__field').focus();
});

var $loading = $('.loading-app').hide();
$(document)
    .ajaxStart(function () {
        $loading.show();
    })
    .ajaxStop(function () {
        $loading.hide();
    });

//form Post

$("form").submit(function () {
    var form = $(this);
    var formId = form.attr('id');
    var formAction = form.attr('action');
    var formData = new FormData();

    $('#' + formId + ' input, ' + '#' + formId + ' select,' + '#' + formId + ' textarea').each(
        function (index) {
            var input = $(this);
            var inputName = input.attr('name');
            var inputVal = input.val();
            if (input.hasClass('moneyformat')) {
                inputVal = $(input).maskMoney('unmasked')[0].toString().replace('.', ',');
            }
            if (input.attr('type') == "text" || input.attr('type') == "password" || input.attr('type') == "number" || input.attr('type') == "email" || input.attr('type') == "hidden" || $(input).is("textarea")) {
                formData.append(inputName, inputVal);
            }
            else if (input.attr('type') == "file") {
                if (input[0].files.length > 0) {
                    for (var i = 0; i < input[0].files.length; i++) {
                        formData.append(input.attr('name'), input[0].files[i]);
                    }
                }
                else {
                    formData.append(input.attr('name'), "");
                }
            }
            else if ($(input).is("select")) {
                formData.append(inputName, inputVal);
            }
            else if (input.attr('type') == "checkbox") {
                if (input.prop('checked')) {
                    formData.append(inputName, inputVal);
                }

            }
        }
    );

    $.ajax({
        url: '?handler=' + formAction,
        type: "POST",
        data: formData,
        //contentType: attr("enctype", "multipart/form-data"),
        contentType: false,
        headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
        async: false,
        beforeSend: function () {
            /*$("#loading").show();*/
        },
        success: function (response) {
            if (response.isSuccess) {
                ShowSuccessModal(response);
            }
            else {
                if (response.errorData != null && response.errorData.isPriceControl) {
                    ShowConfirmPriceModel(response.redirectUrl, response.errorData.productId, response.errorData.marketplace, response.errors[0])
                }
                else {
                    ShowErrorModal(response);
                }

            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText);
        },
        cache: false,
        processData: false
    });

    event.preventDefault();
});

function sendAjaxPost(formData, url) {
    $.ajax({
        url: url,
        type: "POST",
        data: formData,
        headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
        async: false,
        beforeSend: function () {
            /*$("#loading").show();*/
        },
        success: function (response) {
            if (response.isSuccess) {
                ShowSuccessModal(response);
            }
            else {
                if (response.errorData != null && response.errorData.isPriceControl) {
                    ShowConfirmPriceModel(response.redirectUrl, response.errorData.productId, response.errorData.marketplace, response.errors[0])
                }
                else {
                    ShowErrorModal(response);
                }

            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText);
        },
        cache: false,
        contentType: false,
        processData: false
    });
}

function SubmitWizard(id) {
    $('#' + id).submit();
    return false;
}

function ShowErrorModal(modal) {
    var title = $("#modalErrorTitle");
    var text = $("#modalErrorText");
    var okButton = $("#modalErrorOkButton");
    var modalName = "ModalError";

    if (modal.errors.length > 0) {
        modalName = "ModalLargeError";
        title = $("#modalLargeErrorTitle");
        text = $("#modalLargeErrorText");
        okButton = $("#modalLargeErrorOkButton");
    }


    title.text(modal.title);
    var errorText = '';
    for (var i = 0; i < modal.errors.length; i++) {
        errorText += (i === 0 ? "<hr><span>" + modal.errors[i] + "</span>" : '<br /><hr><span>' + modal.errors[i] + "</span>");
    }
    text.html(errorText);

    if (modal.okButton !== null && modal.okButton !== '') {
        okButton.text(modal.okButton);
    }

    $("#" + modalName).trigger("click");
}

function ShowSuccessModal(modal) {
    var title = $("#modalSuccessTitle");
    var text = $("#modalSuccessText");
    var okButton = $("#modalSuccessOkButton");
    var okRedirectButton = $("#modalSuccessRedirectButton");
    if (modal.redirectUrl !== null && modal.redirectUrl !== '' && modal.successMessage == '' && (modal.errors == '' || modal.errors == null)) {
        window.location.href = modal.redirectUrl;
    }
    else {
        if (modal.redirectUrl !== null && modal.redirectUrl !== '') {
            //okRedirectButton.attr('href', model.redirectUrl).css('display','block');
            okRedirectButton.attr('href', modal.redirectUrl);
            okButton.css('display', 'none');

            setTimeout(function () {
                window.location.href = modal.redirectUrl;
            }, 2000);
        }
        if (modal.title !== null && modal.title !== '') {
            title.text(modal.title);
        }
        if (modal.successMessage !== null && modal.successMessage !== '') {
            text.text(modal.successMessage);
        }

        if (modal.errors !== null && modal.errors.length > 0) {
            var errorText = '';
            for (var i = 0; i < modal.errors.length; i++) {
                errorText += (i === 0 ? modal.errors[i] : '<br />' + modal.errors[i]);
            }
            text.html(errorText);
        }

        if (modal.okButton !== null && modal.okButton !== '') {
            okButton.text(modal.okButton);
        }

        $("#ModalSuccess").trigger("click");
    }
}


function ShowConfirmDeleteModel(redirectUrl, id) {
    var okRedirectButton = $("#modalConfirmRedirectButton");

    if (redirectUrl !== null && redirectUrl !== '') {
        okRedirectButton.attr('data-href', redirectUrl);
        okRedirectButton.attr('data-id', id);
    }

    $("#ModalConfirmDelete").trigger("click");
}
function ConfirmDeleteClose() {
    $("#confirmDeleteClose").trigger('click');
}

function ConfirmDeleteOk() {
    var okRedirectButton = $("#modalConfirmRedirectButton");
    var formData = new FormData();
    formData.append('id', parseInt(okRedirectButton.attr('data-id')));

    $.ajax({
        url: '?handler=' + okRedirectButton.attr('data-href'),
        type: "POST",
        data: formData,
        headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
        async: false,
        success: function (response) {
            if (response.isSuccess) {
                ShowSuccessModal(response);
            }
            else {
                ShowErrorModal(response);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText);
        },
        cache: false,
        contentType: false,
        processData: false
    });
}

function ShowConfirmPriceModel(redirectUrl, productId, marketplace, message) {
    var okRedirectButton = $("#modalConfirmPriceRedirectButton");

    if (redirectUrl !== null && redirectUrl !== '') {
        okRedirectButton.attr('data-href', redirectUrl);
        okRedirectButton.attr('data-productId', productId);
        okRedirectButton.attr('data-marketplace', marketplace);
    }

    var text = $("#modalConfirmMessage");
    text.html(message)

    $("#ModalConfirmPrice").trigger("click");
}

function ConfirmPriceOk() {
    var okRedirectButton = $("#modalConfirmPriceRedirectButton");

    MarketPlaceSync(okRedirectButton.attr('data-productId'), okRedirectButton.attr('data-marketplace'), false);
}

function ConfirmPriceClose() {
    $("#ConfirmPriceClose").trigger('click');
}

function CustomExtend(obj, ext) {
    Object.keys(ext).forEach(function (key) {
        obj[key] = ext[key];
    });
    return obj;
}

GenerateCustomTable('.datatable-init-custom', {
    responsive: {
        details: true
    }
});

function MarketPlaceSync(productId, marketPlace, isPriceControl = true) {
    $.ajax({
        url: "?handler=MarketPlaceSync&product_id=" + productId + "&market_place=" + marketPlace + "&is_price_control=" + isPriceControl,
        type: "GET",
        success: function (response) {
            if (response.isSuccess) {
                ShowSuccessModal(response);
            }
            else {
                if (response.errorData != null && response.errorData.isPriceControl) {
                    ShowConfirmPriceModel(response.redirectUrl, response.errorData.productId, response.errorData.marketplace, response.errors[0])
                }
                else {
                    ShowErrorModal(response);
                }

            }
        }
    });
}

function GenerateCustomTable(elm, opt) {
    if ($(elm).exists()) {
        $(elm).each(function () {
            var auto_responsive = $(this).data('auto-responsive'),
                has_export = typeof opt.buttons !== 'undefined' && opt.buttons ? true : false;
            var export_title = $(this).data('export-title') ? $(this).data('export-title') : 'Export';
            var btn = has_export ? '<"dt-export-buttons d-flex align-center"<"dt-export-title d-none d-md-inline-block">B>' : '',
                btn_cls = has_export ? ' with-export' : '';
            var dom_normal = '<"row justify-between g-2' + btn_cls + '"<"col-7 col-sm-4 text-start"f><"col-5 col-sm-8 text-end"<"datatable-filter"<"d-flex justify-content-end g-2"' + btn + 'l>>>><"datatable-wrap my-3"t><"row align-items-center"<"col-7 col-sm-12 col-md-9"p><"col-5 col-sm-12 col-md-3 text-start text-md-end"i>>';
            var dom_separate = '<"row justify-between g-2' + btn_cls + '"<"col-7 col-sm-4 text-start"f><"col-5 col-sm-8 text-end"<"datatable-filter"<"d-flex justify-content-end g-2"' + btn + 'l>>>><"my-3"t><"row align-items-center"<"col-7 col-sm-12 col-md-9"p><"col-5 col-sm-12 col-md-3 text-start text-md-end"i>>';
            var dom = $(this).hasClass('is-separate') ? dom_separate : dom_normal;
            var def = {
                responsive: true,
                autoWidth: false,
                dom: dom,
                language: {
                    search: "",
                    searchPlaceholder: "Search",
                    lengthMenu: "<div class='form-control-select'> _MENU_ </div>",
                    info: "_START_ -_END_ Total _TOTAL_",
                    infoEmpty: "0",
                    infoFiltered: "( Total _MAX_  )",
                    paginate: {
                        "first": "First",
                        "last": "Last",
                        "next": "Next",
                        "previous": "Previous"
                    }
                }
            },
                attr = opt ? CustomExtend(def, opt) : def;
            attr = auto_responsive === false ? CustomExtend(attr, {
                responsive: false
            }) : attr;
            $(this).DataTable(attr);
            $('.dt-export-title').text(export_title);
        });
    }
}

GenerateCustomAjaxTable('.datatable-init-ajax', {
    responsive: {
        details: true
    },
    sort: false
    //dom: '<"toolbar">frtip',
    //    fnInitComplete: function () {
    //    $('div.toolbar').html('Custom tool bar!');
    //}
});

function GenerateCustomAjaxTable(elm, opt) {
    if ($(elm).exists()) {

        var parentId = $(elm).attr('parentId');

        var excelActive = $(elm).attr('exportButtons');
        if (excelActive != undefined) {
            opt.buttons = [{
                extend: 'excel',
                exportOptions: {
                    columns: ['.export-column'],
                    rows: ['.selected']
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ['.export-column'],
                    rows: ['.selected']
                }
            }];
        }

        $(elm).each(function () {
            var auto_responsive = $(this).data('auto-responsive'),
                has_export = typeof opt.buttons !== 'undefined' && opt.buttons ? true : false;
            var export_title = $(this).data('export-title') ? $(this).data('export-title') : '';
            var btn = has_export ? '<"dt-export-buttons d-flex align-center"<"dt-export-title d-none d-md-inline-block">B>' : '',
                btn_cls = has_export ? ' with-export' : '';
            var dom_normal = '<"row justify-between g-2' + btn_cls + '"<"col-7 col-sm-4 text-start"f><"col-5 col-sm-8 text-end"<"datatable-filter"<"d-flex justify-content-end g-2"' + btn + 'l>>>><"datatable-wrap my-3"t><"row align-items-center"<"col-7 col-sm-12 col-md-9"p><"col-5 col-sm-12 col-md-3 text-start text-md-end"i>>';
            var dom_separate = '<"row justify-between g-2' + btn_cls + '"<"col-7 col-sm-4 text-start"f><"col-5 col-sm-8 text-end"<"datatable-filter"<"d-flex justify-content-end g-2"' + btn + 'l>>>><"my-3"t><"row align-items-center"<"col-7 col-sm-12 col-md-9"p><"col-5 col-sm-12 col-md-3 text-start text-md-end"i>>';
            var dom = $(this).hasClass('is-separate') ? dom_separate : dom_normal;
            var def = {
                responsive: true,
                autoWidth: false,
                dom: dom,
                language: {
                    search: "",
                    searchPlaceholder: "Search",
                    lengthMenu: "<div class='form-control-select'> _MENU_ </div>",
                    info: "_START_ -_END_ Total _TOTAL_",
                    infoEmpty: "0",
                    infoFiltered: "( Total _MAX_  )",
                    paginate: {
                        "first": "First",
                        "last": "Last",
                        "next": "Next",
                        "previous": "Previous"
                    }
                },

                processing: true,
                serverSide: true,
                filter: true,
                ajax: {
                    type: "POST",
                    url: window.location.href.indexOf("?") == -1 ? '?handler=data&parentId=' + parentId : window.location.pathname + window.location.search + '&handler=data&parentId=' + parentId,
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                    datatype: "json",
                    data: function (data) {
                        if ($("#idAll").is(':checked'))
                            $("#idAll").prop('checked', false);

                        return data = data;
                    }
                },
                columns: datatableColumn,
                createdRow: (row, data, dataIndex, cells) => {
                    $(row).addClass('nk-tb-item');
                },
            },
                attr = opt ? CustomExtend(def, opt) : def;
            attr = auto_responsive === false ? CustomExtend(attr, {
                responsive: false
            }) : attr;
            $(this).DataTable(attr);
            $('.dt-export-title').text(export_title);

            $('.justify-content-end').prepend($(".productTableHeader"));
            $(".productTableHeader").show();
        });
    }
}


var tableSelectedData = [];

function rowSelectInput(id) {
    var index = $.inArray(id, tableSelectedData);

    if (index === -1) {
        tableSelectedData.push(id);
    } else {
        tableSelectedData.splice(index, 1);
    }
    $('#item-' + id).parent().parent().parent().toggleClass('selected');
}


function allRowSelectInput() {
    if ($("#idAll").is(':checked'))
        allRowSelectInputSet(true);
    else
        allRowSelectInputSet(false);
}

function allRowSelectInputSet(isSelect) {
    if (isSelect) {
        $(".nk-tb-item").each(function (e, el) {
            if (e > 0 && !$(el).hasClass('selected')) {
                $(el).addClass('selected');
                $($($(el).children()[0]).children().children('input')).prop('checked', true);
            }
        });
    }
    else {
        tableSelectedData = [];
        $(".nk-tb-item").each(function (e, el) {
            if (e > 0 && $(el).hasClass('selected')) {
                $(el).removeClass('selected');
                var element = $($($(el).children()[0]).children().children('input'));
                if (element.is(':checked')) {
                    element.prop('checked', false);
                }
            }
        });
    }
}




function GenerateDataTableRowDropdownButton(buttons) {
    return '<ul class="nk-tb-actions gx-1"><li> <div class="drodown"> <a href="#" class="dropdown-toggle btn btn-icon btn-trigger" data-bs-toggle="dropdown"><em class="icon ni ni-more-h"></em></a> <div class="dropdown-menu dropdown-menu-end"><ul class="link-list-opt no-bdr"> ' + buttons + ' </ul> </div>  </div></li></ul>';
}

$(".select2-ajax-init").select2({
    ajax: {
        url: function (params) {
            if (params.term != undefined && (params.term.length > 2 || params.term.length == 0)) {
                return '?handler=' + $(this).attr("handler") + "&search=" + params.term;
            }
        }
    }
});

for (var i = 0; i < $(".select2-ajax-referer-init").length; i++) {
    var selectElement = $(".select2-ajax-referer-init")[i];
    $('#' + selectElement.id).select2();
    if ($('#' + selectElement.id).attr('data-sub-select') !== undefined) {
        $('#' + selectElement.id).change(function (el) {
            var subElement = $('#' + $('#' + el.target.id).attr('data-sub-select'));
            subElement.val(null).empty().select2('destroy')
            $.get('?handler=' + subElement.attr('handler') + '&cityId=' + $('#' + selectElement.id).val(), function (data) {
                $('#' + subElement.attr('id')).select2({
                    data: data.results
                });
            });
        });

        //ilk gelişte de doldur
        if ($('#' + selectElement.id).val() !== "0" && $('#' + selectElement.id).val() !== "") {
            var subElement = $('#' + $('#' + selectElement.id).attr('data-sub-select'));
            $.get('?handler=' + subElement.attr('handler') + '&cityId=' + $('#' + selectElement.id).val(), function (data) {
                $('#' + subElement.attr('id')).select2({
                    data: data.results
                });
            });
        }
    }
}


$(".select2-custom-init").select2();

function GetSubstring(value, length) {
    if (value == undefined || value.length <= length) {
        return value;
    }
    else {
        return value.slice(0, length) + "...";
    }
}
function GetStartSubstring(value, length) {
    if (value == undefined || value.length <= length) {
        return value;
    }
    else {
        return "..." + value.slice(value.length - length, value.length);
    }
}

const formatter = new Intl.NumberFormat('tr-TR', {
    style: 'currency',
    currency: 'TRY',

    // These options are needed to round to whole numbers if that's what you want.
    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
});


function MoneyFormat(value) {
    return formatter.format(value);
}
function MoneyFormatNotInPriceFormat(value) {
    return formatter.format(value).replace("€ ", "");
}

function priceToNumber(v) {
    if (!v) { return 0; }
    v = v.split('.').join('');
    v = v.split(',').join('.');
    return Number(v.replace(/[^0-9.]/g, ""));
}



function ProductState(value, image) {


    var title = 'Not transferred';
    var cssClass = 'm-dot-warning';
    if (value == 1) {
        title = "Published";
        cssClass = 'm-dot-success';
    }
    else if (value == 2) {
        title = "Awaiting Approval";
        cssClass = 'm-dot-warning';
    }
    else if (value == 3) {
        title = "Not approved";
        cssClass = 'm-dot-danger';
    }

    var htmlResult = '<a href="javascript:void(0);" title="' + title + '" class="border rounded-circle me-2 p-2 marketplace-dot  ' + cssClass + '" data-bs-toggle="tooltip" data-bs-placement="left">';
    htmlResult += '<img src="/images/marketplaces/' + image + '">';
    htmlResult += '</a>';

    return htmlResult;
}


function UrlSetParameter(key, value, paramsString, path) {
    const searchParams = new URLSearchParams(paramsString);


    if (value) {
        searchParams.set(key, value);
    }
    else {
        searchParams.delete(key);
    }

    window.location.href = path + searchParams;
}