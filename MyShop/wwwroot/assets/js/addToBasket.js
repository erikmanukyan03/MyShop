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
                        console.log("Product added to cart successfully.");
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
});