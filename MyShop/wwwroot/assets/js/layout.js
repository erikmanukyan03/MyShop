document.addEventListener('DOMContentLoaded', () => {
    const headerBurger = document.querySelector(".header__burger");
    const headerPages = document.querySelector(".header__pages");
    const body = document.body;

    function toggleMenu() {
        headerBurger.classList.toggle("active");
        headerPages.classList.toggle("active");
        body.classList.toggle("lock");
    }

    function closeMenu(e) {
        const isBurger = e.target.closest(".header__burger");
        const isNavbar = e.target.closest(".header__pages");
        if (!isBurger && !isNavbar && headerBurger.classList.contains("active")) {
            toggleMenu();
        }
    }

    headerBurger.addEventListener("click", toggleMenu);
    document.addEventListener("click", closeMenu);




    const searchForm = document.querySelector(".header__search");
    const searchInput = searchForm.querySelector("input");
   
    document.addEventListener("click", (e) => {
        if (e.target.closest(".search-icon-mobile")) {
            searchForm.classList.add("active");
            searchInput.focus();
        }
        else if (searchForm.classList.contains('active') && !e.target.closest(".header__search")) {
            searchForm.classList.remove('active');
        }
    });

    searchForm.addEventListener("click", (e) => {
        e.stopPropagation();
    });



    const callbackForm = document.getElementById("callback");
    const loader = document.getElementById("loader");

    callbackForm.addEventListener("submit", (e) => {
        e.preventDefault();
        loader.style.display = "block";
        const sendCallback = document.getElementById("send-callback")
        const captchaError = document.getElementById("captchaerror");

        const formData = new FormData(callbackForm);
        const data = {}
        formData.forEach((value, key) => {
            data[key] = value;
        })


        let timeoutId;

        fetch("/Customer/Add", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        }).then(response => response.json()).then(data => {
            if (data.success) {
                loader.style.display = "none";
                sendCallback.style.display = "block";

                clearTimeout(timeoutId); 

                timeoutId = setTimeout(function () {
                    sendCallback.style.display = "none";
                }, 2000);

                
            } else {
                loader.style.display = "none";
                captchaError.classList.add("active");
                captchaError.textContent = "Captcha error"
            }
        })

        
    })
    






})

