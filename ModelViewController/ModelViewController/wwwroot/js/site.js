// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onscroll = function () {
    scrollFunction()
};
function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("myBtn").style.display = "block";
    } else {
        document.getElementById("myBtn").style.display = "none";
    }
}

function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}


function changeRange() {
    pricesLabel.innerText = priceRange.value;
}

function getCheckedCheckBoxes() {
    var checkboxes = document.getElementsByClassName('checkbox');
    var checkboxesChecked = [];

    for (var index = 0; index < checkboxes.length; index++) {
        checkboxesChecked.push(checkboxes[index].checked); // положим в массив выбранный
        
    }
    return checkboxesChecked; // для использования в нужном месте
}

function getSortedProduct() {
    var _price = $("#priceRange").val();
    var _checks = getCheckedCheckBoxes();
    var _selectedIndex = sort.selectedIndex;
    var _name = input.value;
    $.ajax({
        type: 'POST',
        url: '/Catalog/SortedProduct',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: { price: _price, cheks: _checks, selectedIndex: _selectedIndex,name: _name},
        success: function (result) {
            ctalogDiv.innerHTML = result;
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed');
        }
    })
}

function saveFile() {
    var f = document.getElementById("fileinput").files[0];

    var data = new FormData();
    data.append("file" + 1, f);

    $.ajax({
        type: 'POST',
        url: '/Product/DownloadFile',
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {

            document.getElementById('fileCol').innerHTML = result;
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
}

$("#regionSelect").change(function () {
    var select = document.getElementById("regionSelect");
    var data = select.value;
     
    $.ajax({
        type: 'POST',
        url: '/Home/CitiesList',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: { Id: data },
        success: function (result) {
            document.getElementById('citySelect').innerHTML = result;
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed');
        }
    })
});

function characteristicsTab() {
    var id = document.getElementById('characteristics-tab').value;

    $.ajax({
        type: 'POST',
        url: '/Catalog/Characteristics',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: { Id :id},
        success: function (result) {
            document.getElementById('characteristics').innerHTML = result;
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed');
        }
    });

}

function aboutTab() {
    var id = document.getElementById('adout-tab').value;

    $.ajax({
        type: 'POST',
        url: '/Catalog/AboutProduct',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: { Id: id },
        success: function (result) {
            document.getElementById('adout').innerHTML = result;
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed');
        }
    });

}

function serviceProduct() {

    $.ajax({
        type: 'POST',
        url: '/Catalog/ServicesProduct',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        
        success: function (result) {
            document.getElementById('services').innerHTML = result;
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed');
        }
    });

}
