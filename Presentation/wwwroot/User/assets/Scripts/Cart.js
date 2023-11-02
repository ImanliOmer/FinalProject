

$(document).on('click', '.increaseCount', function (e) {
 

    var id = $(this).data('id');

    $.ajax({
        method: "GET",
        url: "/basket/increase/",
        data: {
            id: id
        },
        success: function (res) {
            window.location.reload() 
        },
        error: function (err) {
            alert(err.responseText)
        }
    })
})

$(document).on('click', '.decreaseCount', function (e) {
    var decreaseCount = $(this);

    var id = $(this).data('id');

    $.ajax({
        method: "GET",
        url: "/basket/decrease",
        data: {
            id: id
        },
        success: function (res) {
            var countElement = $(decreaseCount).parent().siblings().eq(3);
            var count = parseInt(countElement.html());

            if (count > 0) {
                count--;
                countElement.html(count);
            }
            window.location.reload()
        },
        error: function (err) {
            alert(err.responseText)
        }
    })
})