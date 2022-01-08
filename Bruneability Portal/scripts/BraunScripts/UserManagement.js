

function GetEmployeeData() {
    $.ajax({
        url: '@Url.Action("GetEmployeeData", "Admin")',
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        dataType: 'json',
        data: {},
        success: function (result) {
            debugger

            var JSONStatus = JSON.parse(result.jsondata);

            $("#employees-grid").dxDataGrid({
                dataSource: JSONStatus.EmployeeList,

                columns: ["UserName", "Email", "Phone", "IsActive", {
                    dataField: "ID",
                    caption: "Action",

                    cellTemplate: function (container, options) {
                        $("<div>")
                            .append($("<a class='EmployeeEdit' _id=" + options.value + " title='View'><i class='fa fa-edit'></i></a>"))
                            .appendTo(container);
                    }
                }
                ]
            });

        }

    });
}