"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/Hub", {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
    }).build();


connection.on("ReceiveNewFootballer", function (id, firstName, secondName, team, teamId, country, gender, bday) {
    console.log(id, firstName)
    var table = document.getElementById("PlayersTable");
    var row = table.insertRow(-1);
    
    row.setAttribute("id", `${id}`);
    row.setAttribute("name", `teamid ${teamId}`);

    var cell1 = row.insertCell(0);
    cell1.setAttribute("id", `${id} Fn`);
    cell1.textContent = `${firstName}`;
    
    var cell2 = row.insertCell(1);
    cell2.setAttribute("id", `${id} Sn`);
    cell2.textContent = `${secondName}`;

    var cell3 = row.insertCell(2);
    cell3.setAttribute("id", `${id} Bd`);
    cell3.textContent = `${bday}`;

    var cell4 = row.insertCell(3);
    cell4.setAttribute("id", `${id} G`);
    cell4.textContent = `${gender}`;

    var cell5 = row.insertCell(4);
    cell5.innerHTML = `<td id="${id} T"> ${team}</td>`;
    cell5.setAttribute("id", `${id} T`);
    cell5.textContent = `${team}`;

    var cell6 = row.insertCell(5);
    cell6.setAttribute("id", `${id} C`);
    cell6.textContent = `${country}`;

    var cell7 = row.insertCell(6);
    cell7.innerHTML
        = `<td>
                <a class="btn" href="/Player/Edit?id=${id}">Изменить</a>
            </td>`;

    var cell8 = row.insertCell(7);
    cell8.innerHTML
        = `<td>
                <form action="/Player/Delete?id=${id} method="post"">
                        <input  class="btn" type="submit" value="Удалить" />
                </form>
            </td>`;
});

connection.on("ReceiveEditFootballer", function (id, firstName, secondName, team, country, gender, bday) {
    document.getElementById(`${id} Fn`).textContent = firstName;
    document.getElementById(`${id} Sn`).textContent = secondName;
    document.getElementById(`${id} Bd`).textContent = bday;
    document.getElementById(`${id} G`).textContent = gender;
    document.getElementById(`${id} T`).textContent = team;
    document.getElementById(`${id} C`).textContent = country;
});

connection.on("ReceiveDeleteFootballer", function (id) {
    document.getElementById(`${id}`).remove();
});

connection.on("ReceiveDeleteTeam", function (id) {
    console.log(id)
    var elements = document.querySelectorAll(`[name="teamid ${id}"]`);

    elements.forEach(element => {
        element.remove();
    });
});

connection.start();