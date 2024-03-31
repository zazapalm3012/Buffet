$(document).ready(function () {
    var multipleCardCarousel = document.querySelector(
        "#recomend"
    );
    if (window.matchMedia("(min-width: 576px)").matches) {
        var carousel = new bootstrap.Carousel(multipleCardCarousel, {
            interval: false,
            wrap: false
        });
        var carouselWidth = $(".menu-inner")[0].scrollWidth;
        var cardWidth = $(".menu-item").width();
        var scrollPosition = 0;
        $("#recomend .next-icon").on("click", function () {
            if (scrollPosition < carouselWidth - cardWidth * 3) {
                scrollPosition += cardWidth;
                $("#recomend .menu-inner").animate(
                    { scrollLeft: scrollPosition },
                    600
                );
            }
        });
        $("#recomend .prev-icon").on("click", function () {
            if (scrollPosition > 1) {
                scrollPosition -= cardWidth;
                $("#recomend .menu-inner").animate(
                    { scrollLeft: scrollPosition },
                    600
                );
            }
        });
    } else {
        $(multipleCardCarousel).addClass("slide");
    }
});
$(document).ready(function () {
    var multipleCardCarousel = document.querySelector(
        "#foodtype"
    );
    if (window.matchMedia("(min-width: 576px)").matches) {
        var carousel = new bootstrap.Carousel(multipleCardCarousel, {
            interval: false,
            wrap: false
        });
        var carouselWidth = $(".menu-inner")[0].scrollWidth;
        var cardWidth = $(".menu-item").width();
        var scrollPosition = 0;
        $("#foodtype .next-icon").on("click", function () {
            if (scrollPosition < carouselWidth - cardWidth * 3) {
                scrollPosition += cardWidth;
                $("#foodtype .menu-inner").animate(
                    { scrollLeft: scrollPosition },
                    600
                );
            }
        });
        $("#foodtype .prev-icon").on("click", function () {
            if (scrollPosition > 1) {
                scrollPosition -= cardWidth;
                $("#foodtype .menu-inner").animate(
                    { scrollLeft: scrollPosition },
                    600
                );
            }
        });
    } else {
        $(multipleCardCarousel).addClass("slide");
    }
});