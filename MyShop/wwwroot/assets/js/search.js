function LiveSearch() {
    let value = document.getElementById('search').value;




    if (!value) {
        document.getElementById('result').innerHTML = '';
        document.getElementById('result').classList.add('hidden');
       hideLoadingSpinner();
        return;
    }

    fetch(`/Home/Search/?keyword=${encodeURIComponent(value)}`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('result').innerHTML = data;
            document.getElementById('result').classList.remove('hidden');
            hideLoadingSpinner();
        })
        .catch(error => {
            console.error('Error:', error);
           hideLoadingSpinner();
        });
}

let timeout = null;
document.getElementById('search').addEventListener('input', function (e) {
    if (e.target.value.trim() === '') {
        document.getElementById('result').classList.add('hidden');
        hideLoadingSpinner();
        return;
    }

    clearTimeout(timeout);
    showLoadingSpinner();
    timeout = setTimeout(function () {
        LiveSearch();
    }, 700);
});

function showLoadingSpinner() {
    document.getElementById('search-loader').classList.remove('hidden');
    document.getElementById('search-icon').style.display = 'none';
}

function hideLoadingSpinner() {
    document.getElementById('search-loader').classList.add('hidden');
    document.getElementById('search-icon').style.display = 'inline';
}


document.addEventListener('click', function (event) {
    if (!event.target.closest('#result, #search')) {
        document.getElementById('result').classList.add('hidden');
    }
});










//function LiveSearch() {
//    let value = document.getElementById('search').value

//    if (!value) {
//        $('#result').html('').addClass('hidden');
//        hideLoadingSpinner();
//    }

//    $.ajax({
//        type: "POST",
//        url: "/Home/Search/?keyword=" + encodeURIComponent(value),
//        datatype: "html",
//        success: function (data) {
//            $('#result').html(data).removeClass('hidden');
//            hideLoadingSpinner();
//        },
//    });
//}
//let timeout = null;
//document.getElementById('search').addEventListener('keydown', function (e) {
//    if (e.key === "Enter") {
//        e.preventDefault();
//        return;
//    }
//    clearTimeout(timeout);
//    showLoadingSpinner();
//    timeout = setTimeout(function () {
//        LiveSearch()
//        hideLoadingSpinner
//    }, 700);
//});

//function showLoadingSpinner() {
//    $('#loading').removeClass('hidden');
//    $(".search-icon").hide();
//}

//function hideLoadingSpinner() {
//    $('#loading').addClass('hidden');
//    $(".search-icon").show();
//}

//$(document).click(function (event) {
//    if (!$(event.target).closest('#result, #search').length) {
//        $('#result').addClass('hidden');
//    }
//});



