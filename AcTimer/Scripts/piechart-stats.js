



google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawBasic);

function drawBasic() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Typical action to be performed when the document is ready:
            var data = xhttp.responseText;
            console.log('darwBasic : ' + data);
            drawChart(JSON.parse(data));
        }
    };
    xhttp.open("GET", '/api/piestats', true);
    xhttp.send();

}

/*******************************************/

function drawChart(data) {

    var matrix = []
    matrix.push(["Activity", "Percentage"]);
    for (var i = 0; i < data.length; i++) {
        matrix.push([data[i].categoryName, data[i].timeSpent]);
    }

    var table = google.visualization.arrayToDataTable(matrix);

    var options = {
        title: 'My Daily Activities',
        chartArea: { width: 600, height: 300 },
        backgroundColor: { fill: 'transparent' },
        width: 500,
        height: 400
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
    chart.draw(table, options);
}

