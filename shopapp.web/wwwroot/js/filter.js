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