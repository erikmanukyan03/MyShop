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
            searchForm?.classList.add("active");
            searchInput?.focus();
        }
        else if (searchForm?.classList.contains('active') && !e.target.closest(".header__search")) {
            searchForm?.classList.remove('active');
        }
    });

    searchForm?.addEventListener("click", (e) => {
        e.stopPropagation();
    });



    const callbackForm = document.getElementById("callback");
    const loader = document.getElementById("loader");

    callbackForm.addEventListener("submit", (e) => {
        e.preventDefault();
        loader.style.display = "flex";
        const sendCallback = document.getElementById("send-callback")
        const captchaError = document.getElementById("captchaerror");
        const formData = new FormData(callbackForm);
        const customerPhone = document.getElementById("customerPhone");


        fetch("/Customer/Add", {
            method: "POST",
            body: formData
        }).then(response => response.json()).then(data => {
            if (data.success) {
                loader.style.display = "none";
                sendCallback.style.display = "block";

                const closeBtn = sendMessage.querySelector(".bottomHalf a");
                closeBtn.addEventListener("click", () => {
                    window.location.href = "/";
                })

            } else if (data.error === "captcha") {
                loader.style.display = "none";
                captchaError.classList.add("active");
                captchaError.textContent = "Captcha error"
            } else if (data.errors.PhoneNumber.length > 0) {
                loader.style.display = "none";
                customerPhone.textContent = data.errors.PhoneNumber[0];
            }
        })
    })


    function validateForm(fields) {
        const {
            phoneInput,
            nameInput,
            messageInput,
            captchaInput,
            emailInput,
            sendFormButton,
            errorMessage,
            phoneRegex,
            captchaLength
        } = fields;


        const isPhoneValid = phoneInput ? phoneRegex.test(phoneInput.value.trim()) : true;
        const isEmailValid = emailInput ? emailInput.value.trim() !== '' : true;
        const isNameFilled = nameInput ? nameInput.value.trim() !== '' : true;
        const isMessageFilled = messageInput ? messageInput.value.trim() !== '' : true;
        const isCaptchaLengthValid = captchaInput ? captchaInput.value.trim().length === captchaLength : true;

        // Управление отображением ошибки
        if (phoneInput?.value.length === 0 || isPhoneValid) {
            errorMessage?.classList.remove("active");
        } else {
            errorMessage?.classList.add("active");
        }

        // Активация/деактивация кнопки
        if (sendFormButton) {
            sendFormButton.disabled = !(isPhoneValid && isNameFilled && isEmailValid && isMessageFilled && isCaptchaLengthValid);
        }
    }

    // Обработка формы обратной связи
    const sendFormButton = document.querySelector(".callback .btn-green");
    const phoneInput = document.getElementById('callbackPhoneNumber');
    const nameInput = document.getElementById('callbackName');
    const messageInput = document.getElementById('callbackMessage');
    const captchaInput = document.getElementById('callbackCaptcha');
    const errorMessage = document.getElementById('callbackPhoneError');
    const armenianPhoneRegex = /^(99|91|93|96|98|77|43|41|94|95|55)[0-9]{6}$/;



    function onCallbackFieldInput() {
        validateForm({
            phoneInput,
            nameInput,
            messageInput,
            captchaInput,
            sendFormButton,
            errorMessage,
            phoneRegex: armenianPhoneRegex,
            captchaLength: 4
        });
    }

    phoneInput?.addEventListener('input', onCallbackFieldInput);
    nameInput?.addEventListener('input', onCallbackFieldInput);
    messageInput?.addEventListener('input', onCallbackFieldInput);
    captchaInput?.addEventListener('input', onCallbackFieldInput);



    // Начальная валидация для формы обратной связи
    onCallbackFieldInput();

    // Обработка формы заказа
    const orderForm = document.querySelector(".cart-order__form");
    const orderPhoneInput = document.getElementById('orderPhoneNumber');
    const orderNameInput = document.getElementById('orderName');
    const orderEmailInput = document.getElementById('orderEmail');
    const orderErrorMessage = document.getElementById('orderPhoneError');
    const sendOrderButton = orderForm?.querySelector(".btn-green");
    const armenianOrderPhoneRegex = /^(99|91|93|96|98|77|43|41|94|95|55)[0-9]{6}$/;

    function onOrderFieldInput() {
        validateForm({
            phoneInput: orderPhoneInput,
            nameInput: orderNameInput,
            emailInput: orderEmailInput,
            messageInput: null, // Не используется в форме заказа
            captchaInput: null, // Не используется в форме заказа
            sendFormButton: sendOrderButton,
            errorMessage: orderErrorMessage,
            phoneRegex: armenianOrderPhoneRegex,
            captchaLength: 0 // Не используется в форме заказа
        });
    }

    orderPhoneInput?.addEventListener('input', onOrderFieldInput);
    orderNameInput?.addEventListener('input', onOrderFieldInput);
    orderEmailInput?.addEventListener('input', onOrderFieldInput);

    // Начальная валидация для формы заказа
    onOrderFieldInput();
})

