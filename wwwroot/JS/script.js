$(document).ready(function () {
    var theForm = $("#theForm");
    theForm.hide();

    var buyButton = $("#buyButton");
    buyButton.on("click", function () {
        console.log("Confirm to Buy!")
    })

    var productProps = $(".product-props li")
    productProps.on("click", function () {
        console.log("you clicked on " + $(this).text())
    })

    var $loginToggle = $("#loginToggle")
    var $popupform = $(".popup-form")

    $loginToggle.on("click", function() {
        $popupform.toggle(1000)
    })

});