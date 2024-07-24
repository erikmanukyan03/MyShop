document.addEventListener('DOMContentLoaded', () => {
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

