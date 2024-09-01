"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/Hub", {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
}).build();

connection.on("ReceiveNewTeam", function (id, Name) {
    console.log(id, Name)
    var table = document.getElementById("TeamsTable");
    var row = table.insertRow(-1);

    row.setAttribute("id", `${id}`);

    var cell1 = row.insertCell(0);
    cell1.textContent = `${Name}`;

    var cell2 = row.insertCell(1);
    cell2.innerHTML
        = `<td>
                <form method="post" action="Team/Delete?id=${id}">
                    <input class="btn" type="submit" value="Удалить" />
                </form>
            </td>`;
});

connection.on("ReceiveDeleteTeam", function (id) {
    document.getElementById(`${id}`).remove();
});

connection.start();