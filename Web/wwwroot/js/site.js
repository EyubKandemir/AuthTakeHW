﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showModal(html, id) {
    if (!id || id == "") {
        id = "gdmodal";
    }

    $("#" + id).html(html);
    $("#" + id).css("display", "block");
}