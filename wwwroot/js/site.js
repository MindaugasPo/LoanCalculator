function isFormValid() {
    var isValid = true;
    var fixedFee = $("#administration-fixed").val();
    if (!fixedFee || isNaN(fixedFee) || fixedFee < 0) {
        $("#administration-fixed-error").removeClass("d-none");
        isValid = false;
    }
    var amountFee = $("#administration-amount").val();
    if (!amountFee || isNaN(amountFee)) {
        $("#administration-amount-error").removeClass("d-none");
        isValid = false;
    }
    var interestRate = $("#interest-rate").val();
    if (!interestRate || isNaN(interestRate) || interestRate < 0) {
        $("#interest-rate-error").removeClass("d-none");
        isValid = false;
    }
    var loanDuration = $("#loan-duration").val();
    if (!loanDuration || isNaN(loanDuration) || loanDuration < 0) {
        $("#loan-duration-error").removeClass("d-none");
        isValid = false;
    }
    var loanAmount = $("#loan-amount").val();
    if (!loanAmount || isNaN(loanAmount || loanAmount < 0)) {
        $("#loan-amount-error").removeClass("d-none");
        isValid = false;
    }
    return isValid;
}
function clearOutput() {
    $("#output-monthly-payment").html("");
    $("#output-administration-fee").html("");
    $("#output-total-interest-paid").html("");
    $("#output-apr").html("");
}
function preprocessInput(v) {
    v = v.replace(",", ".");
    v = v.replace(/\s/g, '');
    return v;
}
function preprocessForm() {
    $("#administration-fixed").val(preprocessInput($("#administration-fixed").val()));
    $("#administration-amount").val(preprocessInput($("#administration-amount").val()));
    $("#interest-rate").val(preprocessInput($("#interest-rate").val()));
    $("#loan-duration").val(preprocessInput($("#loan-duration").val()));
    $("#loan-amount").val(preprocessInput($("#loan-amount").val()));
}
function calculateLoan() {
    clearOutput();
    preprocessForm();
    $(".form-error").addClass("d-none");
    if (!isFormValid()) {
        return;
    }

    $.ajax({
        type: "GET",
        url: "/Home/CalculateLoan",
        data: $("#loan-details").serialize(),
        success: function(response) {
            if (response) {
                $("#output-monthly-payment").html(parseFloat(response.monthlyPayment).toFixed(2));
                $("#output-administration-fee").html(parseFloat(response.administrationFee).toFixed(2));
                $("#output-total-interest-paid").html(parseFloat(response.totalInterestPaid).toFixed(2));
                $("#output-apr").html(parseFloat(response.apr).toFixed(2));
            } else {
                $("#output-container-content").html("Something went wrong. Please check your input");
            }
        },
        async: false,
        cache: false
    });
}
$("#execute").on("click", function (e) {
    e.preventDefault();
    calculateLoan();
});
$("#command").on("keypress", function (e) {
    e.preventDefault();
    if (e.which === 13) {
        calculateLoan();
    }
});