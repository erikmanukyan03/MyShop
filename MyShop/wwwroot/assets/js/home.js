document.addEventListener('DOMContentLoaded', () => {

    const swiper = new Swiper(".home-banner", {
        //autoplay: {
        //    delay: 2500,
        //    disableOnInteraction: false,
        //    loop: true
        //},
        loop: true,
        slidesPerView: 1
    });

    const topSalesSwiper = new Swiper(".top-sales-slider", {
        slidesPerView: 5,
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
                slidesPerView: 1.5,
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


    $(function () {
        $('.slider').slick({
            autoplay: true,
            autoplaySpeed: 0,
            speed: 5000,
            arrows: false,
            swipe: false,
            slidesToShow: 6,
            cssEase: 'linear',
            pauseOnFocus: false,
            pauseOnHover: false,
            rtl: true
        });
    });

})



