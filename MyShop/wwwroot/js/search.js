function LiveSearch() {
    let value = document.getElementById('search').value

    if (!value) {
        $('#result').html('').addClass('hidden');
        hideLoadingSpinner();
    }

    $.ajax({
        type: "POST",
        url: "/Home/Search/?keyword=" + encodeURIComponent(value),
        datatype: "html",
        success: function (data) {
            $('#result').html(data).removeClass('hidden');
            hideLoadingSpinner();
        },
    });
}
let timeout = null;
document.getElementById('search').addEventListener('keydown', function (e) {
    if (e.key === "Enter") {
        e.preventDefault();
        return;
    }
    clearTimeout(timeout);
    showLoadingSpinner();
    timeout = setTimeout(function () {
        LiveSearch()
        hideLoadingSpinner
    }, 700);
});

function showLoadingSpinner() {
    $('#loading').removeClass('hidden');
    $(".search-icon").hide();
}

function hideLoadingSpinner() {
    $('#loading').addClass('hidden');
    $(".search-icon").show();
}

$(document).click(function (event) {
    if (!$(event.target).closest('#result, #search').length) {
        $('#result').addClass('hidden');
    }
});