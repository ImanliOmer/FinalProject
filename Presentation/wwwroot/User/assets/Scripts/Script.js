﻿jQuery(function ($) {
    $(document).on('click', '#addToCart', function () {

        var id = $(this).data('id');
        $.ajax({
            method: "GET",
            url: "/basket/add",
            data: {
                id: id
            },
            success: function (res) {
                console.log(res)
                alert(res)
            },
            error: function (err) {
                alert(err.responseText)
            }
        })
    })
})