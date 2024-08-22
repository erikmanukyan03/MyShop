document.addEventListener('DOMContentLoaded', (event) => {

    document.querySelectorAll(".addToCompare").forEach(compare => {
        compare.addEventListener("click", function (e) {
            e.preventDefault();
            let compareCount = document.querySelector(".compare__count").innerHTML;
            let prodId = compare.dataset.prodid
            compareCount = parseInt(compareCount);
            if (compareCount < 3) {

                fetch(`/Compare/Add?prodId=${prodId}`, {
                    method: 'POST',
                    body: prodId
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            const compareCountElement = document.querySelector(".compare__count");
                            let compareCount = parseInt(compareCountElement.textContent, 10);
                            const compareCountResult = compareCount + 1;
                            compareCountElement.textContent = compareCountResult

                            const buttons = document.getElementsByClassName("add-toast-compare");

                            for (let i = 0; i < buttons.length; i++) {
                                buttons[i].addEventListener("click", function () {
                                    Toastify({
                                        text: "Ավելացված է համեմատել բաժնում",
                                        duration: 3000,
                                        style: {
                                            background: "linear-gradient(to left, #00b09b, #96c93d)",
                                        },
                                        position: "right",
                                        gravity: "bottom"
                                    }).showToast();
                                });
                            }

                        } else {
                            alert(data.message);
                        }
                    })
                    .catch(error => {
                        console.error('There was a problem with the fetch operation:', error);
                        alert('There was an error adding the product to the cart.');
                    });
            } else {
                alert("Համեմատել կարող եք ընդամենը 3 ապրանք:")
            }
        });
    });



  
});