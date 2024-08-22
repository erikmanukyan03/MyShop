document.addEventListener('DOMContentLoaded', () => {
    const basketCountElement = document.querySelector(".basket__count");
    const sendMessage = document.querySelector(".send-message__container");
    const orderForm = document.querySelector(".cart-order__form");

    // Функция для обновления количества товаров
    function updateCount(product, delta) {
        const countInput = product.querySelector('.product-count');
        const productPriceElement = product.querySelector(".product__price");
        const productPrice = productPriceElement.dataset.price;
        const cartId = product.dataset.cartid;
        let timeoutId;

        clearTimeout(timeoutId);
        let currentValue = parseInt(countInput.value, 10);
        let newValue = currentValue + delta;

        newValue = Math.max(1, Math.min(newValue, 10));
        countInput.value = newValue;

        timeoutId = setTimeout(() => {
            fetch(`/Cart/Update`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ cartId, count: newValue })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        let totalBasketCount = 0;
                        document.querySelectorAll('.product-count').forEach(input => {
                            totalBasketCount += parseInt(input.value, 10);
                        });
                        basketCountElement.textContent = totalBasketCount;
                        productPriceElement.innerHTML = `${productPrice * newValue} AMD`;
                    } else {
                        console.error("Update failed:", data.message);
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        }, 500);
    }

    document.addEventListener('click', (e) => {
        if (e.target.matches('.count-dec')) {
            const product = e.target.closest('.cart__item');
            updateCount(product, -1);
        }

        if (e.target.matches('.count-inc')) {
            const product = e.target.closest('.cart__item');
            updateCount(product, 1);
        }

        if (e.target.matches('.item__remove')) {
            e.preventDefault();
            const product = e.target.closest('.cart__item');
            const countInput = product.querySelector('.product-count');
            const removeItemCount = parseInt(countInput.value, 10);
            const cartId = product.dataset.cartid;

            fetch(`/Cart/Delete/${cartId}`, { method: "DELETE" })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        let totalBasketCount = parseInt(basketCountElement.textContent, 10) - removeItemCount;
                        basketCountElement.textContent = totalBasketCount;

                        if (totalBasketCount <= 0) {
                            window.location.href = "/";
                        } else {
                            product.remove();
                        }
                    } else {
                        console.error("Delete failed:", data.message);
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        }
    });

    if (orderForm) {
        orderForm.addEventListener("submit", (e) => {
            e.preventDefault();

            const formData = new FormData(orderForm);
            const data = Object.fromEntries(formData.entries());

            fetch("/Order/Create", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    Customer: {
                        Name: data.Name,
                        PhoneNumber: data.PhoneNumber,
                        Email: data.Email
                    }
                })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        sendMessage.style.display = "block";
                        sendMessage.querySelector(".bottomHalf a").addEventListener("click", () => {
                            window.location.href = "/";
                        });
                    } else {
                        console.error("Order submission failed:", data.message);
                    }
                })
                .catch(error => console.error('Error:', error));
        });
    }
});
