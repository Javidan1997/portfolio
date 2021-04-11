$(document).ready(function () {
    $(document).on("click", "#myTab li", function () {

        var slider;
        if ($(this).data("type") == "0") {
            slider = $("#men .sb-slick-slider");
        }
        else if ($(this).data("type") == "1") {
            slider = $("#woman .sb-slick-slider");
        }
        else {
            slider = $("#all .sb-slick-slider");
        }

        $.ajax({
            url: "Home/BookSliderCardPartial?type=" + $(this).data("type"),
            type: "GET",
            success: function (result) {
                $(slider).slick('removeSlide', null, null, true);
                $(slider).slick('slickAdd', result);
            }
        })
    })
})