"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44318/MeasurementHub")
    .build();

connection.start();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

connection.on("SendMeasurement", function () {

    const url = 'https://localhost:44318/api/Measurements/Recent';

    var li = document.createElement("li");
    document.getElementById("messagesList").innerHTML = '';
    li.setAttribute('style', 'white-space: pre;');
    fetch(url)
        .then(
            function (response) {
                response.json().then(function (data) {
                    if (data[0] != null) {
                        for (var i = 0; i < data.length; i++) {

                            li.textContent += "ID: " + data[i].measurementId + " - ";
                            li.textContent += "Date: " + data[i].date + " - ";
                            li.textContent += "Temperature: " + data[i].temp + " - ";
                            li.textContent += "Humidity: " + data[i].humidity + " - ";
                            li.textContent += "Barometric pressure: " + data[i].pressure;
                            li.textContent += "Location: " + data[i].locationId + " - ";
                            li.textContent += "\r\n";
                        }

                        document.getElementById("messagesList").appendChild(li);

                    } else {
                        li.textContent += "No reports found";
                        document.getElementById("messagesList").appendChild(li);
                    }
                });
            }
        ) // SEMIKOLON SKAL MÅSKE FJERNES /////////////////////////////////////////////////////

});


document.getElementById("getSpecific").addEventListener("click", function (event) {
    var date = document.getElementById("specific").value;
    date = date.substring(2);

    const url = 'https://localhost:44318/api/Measurements/DateSpecific/' + date;
    var li = document.createElement("li");
    li.setAttribute('style', 'white-space: pre;');

    fetch(url)
        .then(
            function (response) {
                response.json().then(function (data) {
                    if (data[0] != null) {
                        for (var i = 0; i < data.length; i++) {

                            li.textContent += "ID: " + data[i].measurementId + " - ";
                            li.textContent += "Date: " + data[i].date + " - ";
                            li.textContent += "Temperature: " + data[i].temp + " - ";
                            li.textContent += "Humidity: " + data[i].humidity + " - ";
                            li.textContent += "Barometric pressure: " + data[i].pressure;
                            li.textContent += "Location: " + data[i].locationId + " - ";
                            li.textContent += "\r\n";
                        }

                        document.getElementById("Measurementlist").appendChild(li);

                    }
                    else {
                        li.textContent += "No reports found for the selected date";
                        document.getElementById("Measurementlist").appendChild(li);
                    }
                });
            }
        )
});

document.getElementById("getMeasurements").addEventListener("click", function (event) {
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    startDate = startDate.substring(2);
    endDate = endDate.substring(2);



    const url = 'https://localhost:44318/api/Measurements/daterange/' + startDate + "/" + endDate; 
    var li = document.createElement("li");
    li.setAttribute('style', 'white-space: pre;');

    fetch(url)
        .then(
            function (data) {
                if (data[0] != null) {
                        for (var i = 0; i < data.length; i++) {

                            li.textContent += "ID: " + data[i].measurementId + " - ";
                            li.textContent += "Date: " + data[i].date + " - ";
                            li.textContent += "Temperature: " + data[i].temp + " - ";
                            li.textContent += "Humidity: " + data[i].humidity + " - ";
                            li.textContent += "Barometric pressure: " + data[i].pressure;
                            li.textContent += "Location: " + data[i].locationId + " - ";
                            li.textContent += "\r\n";
                        }

                        document.getElementById("Measurementlist").appendChild(li);
                    }
                    else {
                        li.textContent += "No reports found for the selected dates";
                        document.getElementById("Measurementlist").appendChild(li);
                    }
                
            }
        )
});

