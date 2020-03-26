(function () {
    var dataTable = $('#empTable').DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        searching: true,
        "ajax": {
            "url": "/api/Values/Employee",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "UserName", "name": "UserName", "autoWidth": true },
            { "data": "groupname", "name": "groupname", "autoWidth": true },
        ]
    });

    $('#searchByName').keyup(function () {
        dataTable.draw();
    });

    $('#searchByGender').change(function () {
        dataTable.draw();
    });
})();
