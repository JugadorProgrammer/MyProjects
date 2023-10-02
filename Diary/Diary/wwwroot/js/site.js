//VANTA.CLOUDS({
//    el: "#vandaBg",
//    mouseControls: true,
//    touchControls: true,
//    gyroControls: false,
//    minHeight: 200.00,
//    minWidth: 200.00,
//    skyColor: 0xc54091,
//    cloudColor: 0xd9adc0,
//    sunColor: 0xff1818
//});
function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}
function getDays(date) {
    
    $.ajax({
        type: 'GET',
        url: '/Home/Data',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: { date: date },
        success: function (result) {
            table = document.getElementById("dataTable");
            table.innerHTML = result;
            scrollToElement(date);
        },
        error: function (error) {
            alert('Произошла непредвиеднная ошибка!');
            console.log(error);
        }
    });

}

function calendarOnChange() {
    let element = document.getElementById("calendar");
    getDays(element.value);
}

function saveInformation() {
    const Id = (this.id.toString()).replace("_V", "");
    const date = document.getElementById(`${Id}_date`);
    const element = document.getElementById(`${Id}_value`);
    console.log(`saveInformation:${date.innerText}`);
    sendRequest(Id, element.value,date.innerText);
}

function deleteInformation() {
    const Id = (this.id.toString()).replace("_X", "");
    sendRequest(Id);

    this.value = "";
}

function sendRequest(Id, value ,date) {
    console.log(`sendRequest:${date}`);
    $.ajax({
        type: 'PUT',
        url: '/Home/UpdateData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: { Id: Id, dateTime: date, value: value },
        success: function (result) {
            alert("Успешно!");
            window.location.assign("/Home/");
        },
        error: function (error) {
            alert('Произошла непредвиеднная ошибка!');
            console.log(error);
        }
    });
}

function scrollToElement(id) {
    const element = document.getElementById(id);
    element.scrollIntoView({
        behavior: 'smooth',
        block: 'start',
    });
}

let cookie = getCookie("SelectionDate").replace(/\./g, "-");

if (cookie) {
    getDays(cookie);
}

