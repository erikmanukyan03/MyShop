document.addEventListener('DOMContentLoaded', () => {
    const products = document.querySelectorAll('.cart__item');
    const orderForm = document.querySelector(".cart-order__form");
    products.forEach(product => {
        const decrementButton = product.querySelector('.count-dec');
        const incrementButton = product.querySelector('.count-inc');
        const countInput = product.querySelector('.product-count');
        const productDelete = product.querySelector(".item__remove");
        const productPriceElement = product.querySelector(".product__price");
        const productPrice = productPriceElement.dataset.price;
        const cartId = product.dataset.cartid;
        let timeoutId;

        function updateCount(delta) {
            clearTimeout(timeoutId);
 
            let currentValue = parseInt(countInput.value, 10);
            let newValue = currentValue + delta;

            if (newValue < 1) {
                newValue = 1;
            } else if (newValue > 10) {
                newValue = 10;
            }

            countInput.value = newValue;


            timeoutId = setTimeout(() => {
                fetch(`/Cart/Update`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({ cartId: cartId, count: newValue })
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            document.querySelector(".basket__count").textContent = newValue;
                            productPriceElement.innerHTML = productPrice * newValue + " AMD"
                        } else {
                            console.error("Update failed:", data.message);
                        }
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });
            }, 500)
        }

        if (decrementButton) {
            decrementButton.addEventListener('click', () => {
                updateCount(-1);
            });
        }

        if (incrementButton) {
            incrementButton.addEventListener('click', () => {
                updateCount(1);
            });
        }

        if (productDelete) {
            productDelete.addEventListener("click", (e) => {
                e.preventDefault();

                fetch(`/Cart/Delete/${cartId}`, { method: "DELETE" }).then(response => response.json()).then(data => {
                    if (data.success) {
                        if (products.length > 1) {
                            product.remove();

                        } else {
                            window.location.href = "/"
                        }
                    }
                })
            })
        }
    });


    orderForm.addEventListener("submit", (e) => {
        e.preventDefault();

        const formData = new FormData(orderForm);
        const data = {};
        formData.forEach((value, key) => {
            data[key] = value;
        });


        fetch("/Order/Create", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        }).then(response => response.json())
            .then(data => {
                if (data.success) {
                    /*window.location.href = "/Home/Index";*/
                    console.log("asd")
                } else {
                    console.error("Order submission failed:", data.message);
                }
            })
            .catch(error => console.error('Error:', error));
    })
});