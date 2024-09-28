﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var searchInp = document.getElementById("searchInp");

searchInp.addEventListener("keyup", function () {

    var searchValue = searchInp.value;


    var xhr = new XMLHttpRequest();

    xhr.open("GET", `http://localhost:5180/Employee?search=${searchValue}`);

    xhr.send();

    xhr.onreadystatechange = function () {
        if (xhr.readyState == XMLHttpRequest.DONE) {

            if (xhr.status == 200)
                document.body.innerHTML = xhr.responseText

            else
                alert('something else other than 200 was returned');

        }

    };
})