document.addEventListener('DOMContentLoaded', () => {

    const swiper = new Swiper(".home-banner", {
        //autoplay: {
        //    delay: 2500,
        //    disableOnInteraction: false,
        //},
    });

})



document.addEventListener("DOMContentLoaded", function () {
    new Splide("#js-splide", {
        perPage: 4,
        perMove: 1,
        arrows: true,
        focus: "center",
        padding: 0,
        height: "430px",
        cover: true,
        type: "loop",
        gap: "0em",
        classes: {
            arrows: 'splide__arrows your-class-arrows',
            arrow: 'splide__arrow your-class-arrow',
            prev: 'splide__arrow--prev your-class-prev',
            next: 'splide__arrow--next your-class-next',
        },
        breakpoints: {
            800: {
                perPage: 2,
                gap: "0.5em",
                focus: 0
            },
            600: {
                perPage: 1
            }
        }
    }).mount();
});