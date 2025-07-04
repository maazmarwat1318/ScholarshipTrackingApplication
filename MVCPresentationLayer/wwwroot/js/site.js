﻿$(document).ready(function () {
    let form = $('#form');
    let validator = form.data("validator");
    if (validator) {
        console.log("Here I am")
        validator.settings.highlight = function (element, errorClass, validClass) {
            $(element).addClass('is-invalid').removeClass('is-valid');
        };

        validator.settings.unhighlight = function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid').addClass('is-valid');
        };
    }

    //let toastElement = $("#toast")
    //let toast = new bootstrap.Toast(toastElement.get(0), { delay: 10000 })

    let btn = $("#form-btn");
    let btnLoader = btn.find("span");

    form.on("submit", function () {
        if (form.valid()) {
            btn.attr("disabled", true);
            btnLoader.removeClass("visually-hidden");
            try {
                window.on("beforeunload", function () {
                    btnLoader.addClass("visually-hidden")
                    btn.attr("disabled", false)
                })
            } catch { }
        }
    });

    let logoutBtn = $("#logout-btn");
    logoutBtn.on("click", () => {
        window.location.href = "/Account/Logout";
    })

    let navbar = $(".navbar");

    //window.addEventListener("scroll", (e) => {
    //    if (window.scrollY > 10) {
    //        if (navbar) {
    //            navbar.addClass(["box-shadow", "border-bottom"]);
    //        }
    //    } else {
    //        navbar.removeClass(["box-shadow", "border-bottom"]);
    //    }
    //})


    //$('#login-form').on("submit", (e) => {
    //    e.preventDefault()
    //    form.validate();
    //    if (form.valid()) {

    //        btn.attr("disabled", true)
    //        btnLoader.removeClass("visually-hidden")
    //        $.ajax({
    //            url: 'https://localhost:7025/Account/Login',
    //            method: 'POST',
    //            contentType: 'application/json',
    //            data: JSON.stringify({
    //                Email: $("#Email").val(),
    //                Password: $("#Password").val()
    //            }),
    //            dataType: "json",
    //            success: function (response) {
    //                btnLoader.addClass("visually-hidden")
    //                btn.removeAttr("disabled")
    //                localStorage.setItem("token", response.token)
    //                localStorage.setItem("firstName", response.firstName)
    //                localStorage.setItem("lastName", response.lastName)
    //                localStorage.setItem("email", response.email)
    //            },
    //            error: function (xhr, status, error) {
    //                btnLoader.addClass("visually-hidden")
    //                btn.removeAttr("disabled")
    //                toastElement.find(".toast-body").text(xhr.responseJSON.message);
    //                toast.show()
    //            }
    //        })
    //    }
    //})


})