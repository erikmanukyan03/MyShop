document.addEventListener('DOMContentLoaded', () => {

    const swiper = new Swiper(".home-banner", {
        autoplay: {
            delay: 2500,
            disableOnInteraction: false,
            loop: true
        },
    });

    const topSalesSwiper = new Swiper(".top-sales-slider", {
        slidesPerView: 5,
        spaceBetween: 30,
        loop: true,
        //autoplay: {
        //    delay: 2500,
        //    disableOnInteraction: false,
        //},
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        breakpoints: {
            320: {
                slidesPerView: 1.2,
                spaceBetween: 10,
            },
            768: {
                slidesPerView: 2.5,
                spaceBetween: 10,
            },
            992: {
                slidesPerView: 3,
                spaceBetween: 20,
            },
            1440: {
                slidesPerView: 5,
                spaceBetween: 30,
            },
        },
    });

})



