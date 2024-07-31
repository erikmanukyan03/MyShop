document.addEventListener('DOMContentLoaded', (event) => {
    document.querySelectorAll(".addToCartForm").forEach(form => {
        form.addEventListener("submit", function (e) {
            e.preventDefault();

            let formData = new FormData(e.target);
            let actionUrl = e.target.action;

            fetch(actionUrl, {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        const decrementButton = document.querySelector('.count-dec');
                        const basketCountElement = document.querySelector(".basket__count");
                        if (decrementButton) {
                            const countInput = document.querySelector('.product-count');
                            let countValue = parseInt(countInput.value, 10);
                            let basketCount = parseInt(basketCountElement.textContent, 10);
                            const basketCountResult = basketCount + countValue;
                            if (basketCountResult > 99) {
                                basketCountElement.textContent = "99+";
                                basketCountElement.classList.add("more")
                            } else {
                                basketCountElement.textContent = basketCountResult
                            }

                        } else {
                            let basketCount = parseInt(basketCountElement.textContent, 10);
                            if (!isNaN(basketCount)) {
                                const basketCountResult = basketCount + 1;
                                basketCountElement.textContent = basketCountResult > 99 ? "99+" : basketCountResult
                                basketCountResult > 99 ? basketCountElement.classList.add("more") : "";

                            } else {
                                console.error('Basket count is not a valid number.');
                            }
                        }
                    } else {
                        alert(data.message);
                    }
                })
                .catch(error => {
                    console.error('There was a problem with the fetch operation:', error);
                    alert('There was an error adding the product to the cart.');
                });
        });
    });



    const buttons = document.getElementsByClassName("add-toast");

    for (let i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener("click", function () {
            Toastify({
                text: "Ավելացված է զամբյուղում",
                duration: 3000,
                style: {
                    background: "linear-gradient(to left, #00b09b, #96c93d)",
                },
                position: "right",
                gravity: "bottom"
            }).showToast();
        });
    }
});