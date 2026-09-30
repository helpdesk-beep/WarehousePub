// fonts size option

$(document).ready(function () {
    var originalSize = $('div').css('font-size');
    // reset
    $(".resetMe").click(function () {
        $('div').css('font-size', originalSize);

    });

    // Increase Font Size
    $(".increase").click(function () {
        var currentSize = $('div').css('font-size');
        var currentSize = parseFloat(currentSize) * 1.2;
        $('div').css('font-size', currentSize);

        return false;
    });

    // Decrease Font Size
    $(".decrease").click(function () {
        var currentFontSize = $('div').css('font-size');
        var currentSize = $('div').css('font-size');
        var currentSize = parseFloat(currentSize) * 0.8;
        $('div').css('font-size', currentSize);

        return false;
    });
});


//-------------------Tabs

$(document).ready(function () {
    $('#tabscon').easyResponsiveTabs({
        type: 'default',
        width: 'auto',
        fit: true,
        closed: 'accordion',
        activate: function (event) {
            var $tab = $(this);
            var $info = $('#tabInfo');
            var $name = $('span', $info);
            $name.text($tab.text());
            $info.show();
        }
    });

    $('#socialtbs').easyResponsiveTabs({
        type: 'default',
        width: 'auto',
        fit: true,
        closed: 'accordion',
        activate: function (event) {
            var $tab = $(this);
            var $info = $('#tabInfo');
            var $name = $('span', $info);
            $name.text($tab.text());
            $info.show();
        }
    });
});


//-----------------Slider

$(window).load(function () {
    $('.flexslider').flexslider({
        animation: "slide",
        controlNav: false,
        directionNav: true,
        start: function (slider) {
            $('body').removeClass('loading');
        }
    });
});