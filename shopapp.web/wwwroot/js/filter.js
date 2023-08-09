$('#categoryBtn').click(function (ev) { btnClick(ev, "category") })
$('#priceBtn').click(function (ev) { btnClick(ev, "price") })
$('.sortValue').click(function () {
    $('.sortName').text($(this).text())
})

$('#categorySearch').on("input", function () {
    var value = $(this).val()
    var inputs = $('.categoryInputs').find(".categoryInput")
    for (let i = 0; i < inputs.length; i++) {
        const element = inputs[i];
        if (!$(element).text().toLowerCase().includes(value.toLowerCase())) {
            $(element).css("display", "none")
        } else {
            $(element).css("display", "block")
        }

    }
})

function btnClick(ev, btn) {
    if (btn == "category") {
        $('#categoryDiv').toggle(() => {
            $('#categoryArrow').toggleClass('fa-caret-up fa-caret-down')
        })
    }
    if (btn == "price") {
        $('#priceDiv').toggle(() => {
            $('#priceArrow').toggleClass('fa-caret-up fa-caret-down')
        })
    }
}


$('#leastInput').on('change', inputTextChange)
$('#mostInput').on('change', inputTextChange)

function inputTextChange(ev) {
    if (ev.target.id == "leastInput") {
        if ($('#leastInput').val() == "" && $('#mostInput').val() != "") {
            $('#leastInput').val("0")
        }
        if ($('#leastInput').val() == ""){
            $('#leastInput').val("0")
        }
    }
    if (ev.target.id == "mostInput") {
        if ($('#leastInput').val() == "") {
            $('#leastInput').val("0")
        }
    }
    if ($('#mostInput').val() != "" && parseInt($('#leastInput').val()) > parseInt($('#mostInput').val())) {
        $('#mostInput').val($('#leastInput').val())
    }
    $(".prcRadio input").prop("checked", false);
    $('#prcInput').val(`${$('#leastInput').val()}-${$('#mostInput').val()}`)
    $('#prcInput').attr("name", "prc")
}

$('.prcRadio input').on('change', function () {
    var input = $(this)
    $.each($('.prcRadio input'), function () {
        if ($(this)[0] != input[0]) {
            $(this).prop("checked", false)
        }
    })
    var prc = $(this).parent().find('label').text()
    $('#prcInput').val(prc)
    $('#leastInput').val(prc.split("-")[0])
    $('#mostInput').val(prc.split("-")[1])
    $('#prcInput').attr("name", "prc")
})