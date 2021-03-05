function calculate(amount) {
    if (!amount) {
        $("#output-container-content").html("Please enter a number");
        return;
    }
    amount = amount.replace(/\s/g, '');
    amount = amount.replace(",", ".");
    if (isNaN(amount)) {
        $("#output-container-content").html("Only numbers are accepted");
        return;
    }
    $("#output-container-content").html(amount);
    $("#history-container").html($("#history-container").html() + "\n" + amount);
}
$("#execute").on("click", function () {
    calculate($("#command").val());
});
$("#command").on("keypress", function (e) {
    if (e.which === 13) {
        calculate($("#command").val());
    }
});