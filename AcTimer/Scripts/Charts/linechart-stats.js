google.charts.load('current', { packages: ['corechart', 'line'] });
google.charts.setOnLoadCallback(drawBasic);

function drawBasic() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Typical action to be performed when the document is ready:
            var data = xhttp.responseText;
            drawFirst(data);
        }
    };
    xhttp.open("GET", '/api/stats', true);
    xhttp.send();
}


function drawFirst(data) {
    var arr = JSON.parse(data);
    for (let i = 0; i < arr.length; i++) {
        var body = document.getElementById("body");
        var row = body.insertRow(i);
        var parsedArr = arr[i];
        var graphData = parsedArr.activitiesDto;
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        cell1.innerHTML = parsedArr.catgoryName;
        cell2.innerHTML = parsedArr.averageTime;
        drawGraph(graphData, cell3);
    }
}
//drawFirst();
function drawGraph(data, cell3) {
    console.log(data);
    var table = new google.visualization.DataTable();
    table.addColumn('string', 'Date');
    table.addColumn('number', 'Hours Spent');

    for (var i = 0; i < data.length; i++) {
        table.addRow([data[i].date.split("T00")[0], data[i].totalSpent]);
    }
    console.log(table);

    var options = {
        chartArea: { width: '70%' },
        hAxis: {
            title: 'Date',
            minValue: 0
        },
        vAxis: {
            title: 'Hours Spent'
        },
        seriesType: 'line',
        pointSize: 5,
        lineWidth: 5,
        series: { 0: { type: 'line', lineWidth: 3 } },
        backgroundColor: { fill: 'transparent' },
        'width': 850,
        'height': 200
    };

    var chart = new google.visualization.LineChart(cell3);

    chart.draw(table, options);
}