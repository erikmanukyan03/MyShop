document.addEventListener('DOMContentLoaded', () => {
    const body = document.body;
    const filterIcon = document.querySelector(".icon-filter");
    const wrapper = document.querySelector(".category__wrapper");
    const filterClose = document.querySelector(".filter__close");

    filterIcon.addEventListener("click", function () {
        wrapper.classList.add("active");
        body.classList.add("lock");
    });

    filterClose.addEventListener("click", function () {
        wrapper.classList.remove("active");
        body.classList.remove("lock");
    });


    const horizontalSvg = document.getElementById('horizontal');
    const verticalSvg = document.getElementById('vertical');
    const storageKey = 'displayMode';

    function updateActiveState() {
        const savedMode = localStorage.getItem(storageKey);
        if (savedMode === 'horizontal') {
            horizontalSvg.classList.add('active');
            wrapper.classList.add("horizontal");
            verticalSvg.classList.remove('active');
        } else {
            verticalSvg.classList.add('active');
            horizontalSvg.classList.remove('active');
            wrapper.classList.add("vertical");
        }
    }

    updateActiveState();

    function handleClick(event) {
        if (event.target.id === 'horizontal') {
            horizontalSvg.classList.add('active');
            verticalSvg.classList.remove('active');
            wrapper.classList.remove("vertical");
            wrapper.classList.add("horizontal");
            localStorage.setItem(storageKey, 'horizontal');
        } else if (event.target.id === 'vertical') {
            verticalSvg.classList.add('active');
            horizontalSvg.classList.remove('active');
            wrapper.classList.remove("horizontal");
            wrapper.classList.add("vertical");
            localStorage.setItem(storageKey, 'vertical');
        }
    }

    horizontalSvg.addEventListener('click', handleClick);
    verticalSvg.addEventListener('click', handleClick);


})