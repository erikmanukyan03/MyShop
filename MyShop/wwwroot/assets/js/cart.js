document.addEventListener('DOMContentLoaded', () => {
    const products = document.querySelectorAll('.cart__item');
    const orderForm = document.querySelector(".cart-order__form");
    products.forEach(product => {
        const decrementButton = product.querySelector('.count-dec');
        const incrementButton = product.querySelector('.count-inc');
        const countInput = product.querySelector('.product-count');
        const productDelete = product.querySelector(".item__remove");

        const cartId = product.dataset.cartid;

        function updateCount(delta) {
            let currentValue = parseInt(countInput.value, 10);
            let newValue = currentValue + delta;
            const minValue = parseInt(countInput.min, 10);
            const maxValue = parseInt(countInput.max, 10);

            newValue = Math.max(newValue, minValue);

            if (newValue <= maxValue) {
                countInput.value = newValue;

                // Fetch to update the server
                fetch(`/Cart/Update?cartId=${cartId}`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({ count: newValue })
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            console.log("Update successful");
                        } else {
                            console.error("Update failed:", data.message);
                        }
                    })
                    .catch(error => console.error('Error:', error));
            }
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
                        product.remove();
                    }
                })
            })
        }
    });


    //orderForm.addEventListener("submit", (e) => {
    //    e.preventDefault();

    //    const formData = new FormData(orderForm);
    //    const data = {};
    //    formData.forEach((value, key) => {
    //        data[key] = value;
    //    });


    //    fetch("/Order/Create", {
    //        method: "POST",
    //        headers: {
    //            "Content-Type": "application/json"
    //        },
    //        body: JSON.stringify(data)
    //    }).then(response => response.json())
    //        .then(data => {
    //            if (data.success) {
    //                window.location.href = "/Home/Index";
    //            } else {
    //                console.error("Order submission failed:", data.message);
    //            }
    //        })
    //        .catch(error => console.error('Error:', error));
    //})
});