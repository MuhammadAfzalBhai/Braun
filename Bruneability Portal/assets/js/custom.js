$('#sidebarCollapse').on('click', function () {
			 $('#sidebar').toggleClass('active');
			 
		 });
		 

		$('#sidebar ul li').on('click', function () {
			var button = $(this).find('i.fa.indicator-mn');
			button.toggleClass('fa-angle-left').toggleClass('fa-angle-right');
			
		});
		
		$('#sidebarCollapse').on('click', function () {
			$("body").toggleClass("mini-navbar");
		});

	$(window).scroll(function() {
		var scroll = $(window).scrollTop();

		if (scroll >= 100) {
			$(".navbar").addClass("stuck");
		} else {
			$(".navbar").removeClass("stuck");
		}
	});
	
	//line kpi chart
	
	$(function () {
    var chart = $("#kpichart").dxChart({
        dataSource: population,
        series: [{
            argumentField: "country"
        }],
        argumentAxis: {
            label: {
                overlappingBehavior: overlappingModes[0]
            }
        },
        legend: {
            visible: false
        },
        title: "",
		valueAxis: { label: { visible: false }}
		 
    }).dxChart("instance");

    $("#overlapping-modes").dxSelectBox({
        dataSource: overlappingModes,
        value: overlappingModes[0],
        onValueChanged: function (e) {
            chart.option("argumentAxis.label.overlappingBehavior", e.value);
        }
    });
});


var overlappingModes = ["stagger", "rotate", "hide", "none"],
    population = [
        { country: "China", val: 1382500000 },
        { country: "India", val: 1314200000 },
        { country: "United States", val: 3247890004 },
        { country: "Indonesia", val: 2616000400 },
        { country: "Brazil", val: 2073328000 },
        { country: "Pakistan", val: 1968675000 },
        { country: "Nigeria", val: 1885000070 },
        { country: "Bangladesh", val: 1682240001 },
        { country: "Russia", val: 1464800000 },
        { country: "Japan", val: 1267607000 },
        { country: "Mexico", val: 1222753000 },
        { country: "Ethiopia", val: 1043450700 },
        { country: "Philippines", val: 1039065000 },
        { country: "Egypt", val: 928476800 }
    ];
	
	
	
// conversation grid	
$(function(){
    $("#gridConversion").dxDataGrid({
        dataSource: customers,
        showBorders: true,
        searchPanel: {
            visible: true
        },
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        columns: ["Conversion Manufacturer", "Conversion Type", "Retail Price", "Dealer Price", "Conversion Year", "Additional Details", "Actions"]
    });
    
});

var customers = [{
    "ID" : 1,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 2,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 3,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 4,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 5,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 6,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 7,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 8,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 9,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 10,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 11,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 12,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 13,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 14,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 15,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 16,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 17,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 18,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 19,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}, {
    "ID" : 20,
    "Conversion Manufacturer" : "XYZ Company",
    "Conversion Type" : "Cup Holder",
    "Retail Price" : "$45.00",
    "Dealer Price" : "$70.00",
    "Conversion Year" : 2018,
    "Additional Details" : "Lorem Ipsum is simply dummy text.",
    "Actions" : ""
}];

// griddeduction grid	
$(function(){
    $("#griddeduction").dxDataGrid({
        dataSource: griddeduction,
        showBorders: true,
		rowAlternationEnabled: true,
        searchPanel: {
            visible: true
        },
		
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        columns: ["Deduction amount", "Description", "Actions"]
    });
    
});

var griddeduction = [{
    "ID" : 1,
    "Deduction amount" : "$100",
    "Description" : "Lorem Ipsum is simply dummy text of the printing",
    "Actions" : ""
}, {
    "ID" : 2,
    "Deduction amount" : "$200",
    "Description" : "Lorem Ipsum is simply text of",
    "Actions" : ""
}, {
    "ID" : 3,
    "Deduction amount" : "$300",
    "Description" : "Lorem Ipsum is simply text of",
    "Actions" : ""
}, {
    "ID" : 4,
    "Deduction amount" : "$400",
    "Description" : "Lorem Ipsum is simply dummy text of",
    "Actions" : ""
}];

//

// depreciationgrid	
$(function(){
    $("#depreciationgrid").dxDataGrid({
        dataSource: depreciationgrid,
        showBorders: true,
		rowAlternationEnabled: true,
        searchPanel: {
            visible: true
        },
		
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        columns: ["Deduction Conversion % of depreciation", "Age in month"]
    });
    
});

var depreciationgrid = [{
    "ID" : 1,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "New"
}, {
    "ID" : 2,
    "Deduction Conversion % of depreciation" : "70%",
    "Age in month" : "0-6 months"
}, {
    "ID" : 3,
    "Deduction Conversion % of depreciation" : "98.7%",
    "Age in month" : "7"
}, {
    "ID" : 4,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "8"
}, {
    "ID" : 5,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "9"
}, {
    "ID" : 6,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "10"
}, {
    "ID" : 7,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "11"
}, {
    "ID" : 8,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "12"
}, {
    "ID" : 9,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "13"
}, {
    "ID" : 10,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "14"
}, {
    "ID" : 11,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "15"
}, {
    "ID" : 12,
    "Deduction Conversion % of depreciation" : "New",
    "Age in month" : "16"
}];

//

// usermanagement	
$(function(){
    $("#usermanagement").dxDataGrid({
        dataSource: usermanagement,
        showBorders: true,
		rowAlternationEnabled: true,
        searchPanel: {
            visible: true
        },
		
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        columns: ["User Name", "User Email", "Joining Date", "Contact", "Status", "Is App User", "Actions"]
    });
    
});

var usermanagement = [{
    "ID" : 1,
    "User Name" : "Harv Mudd",
    "User Email" : "xyz@test.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "Active",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 2,
    "User Name" : "Jim Packard",
    "User Email" : "jim_packard@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "InActive",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 3,
    "User Name" : "Harv Mudd",
    "User Email" : "harvmudd@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "Active",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 4,
    "User Name" : "Todd Hoffman",
    "User Email" : "todd_hoffman@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "InActive",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 5,
    "User Name" : "Clark Morgan",
    "User Email" : "clarkmorgan@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "Active",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 6,
    "User Name" : "Todd Hoffman",
    "User Email" : "todd_hoffman@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "InActive",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 7,
    "User Name" : "Clark Morgan",
    "User Email" : "clarkmorgan@gmailc.om",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "Active",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 8,
    "User Name" : "Clark Morgan",
    "User Email" : "clarkmorgan@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "InActive",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 9,
    "User Name" : "Todd Hoffman",
    "User Email" : "todd_hoffman@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "Active",
    "Is App User" : "",
    "Actions" : ""
}, {
    "ID" : 10,
    "User Name" : "Clark Morgan",
    "User Email" : "clarkmorgan@gmailc.om",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : "InActive",
    "Is App User" : "",
    "Actions" : ""
}];

////

// usermanagementmdl	
$(function(){
    $("#usermanagementmdl").dxDataGrid({
        dataSource: usermanagementmdl,
        showBorders: true,
		rowAlternationEnabled: true,
        searchPanel: {
            visible: true
        },
		
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        columns: ["User Name", "User Email", "Joining Date", "Contact", "Status"]
    });
    
});

var usermanagementmdl = [{
    "ID" : 1,
    "User Name" : "Harv Mudd",
    "User Email" : "xyz@test.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 2,
    "User Name" : "Jim Packard",
    "User Email" : "jim_packard@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 3,
    "User Name" : "Harv Mudd",
    "User Email" : "harvmudd@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 4,
    "User Name" : "Todd Hoffman",
    "User Email" : "todd_hoffman@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 5,
    "User Name" : "Clark Morgan",
    "User Email" : "clarkmorgan@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 6,
    "User Name" : "Todd Hoffman",
    "User Email" : "todd_hoffman@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 7,
    "User Name" : "Clark Morgan",
    "User Email" : "clarkmorgan@gmailc.om",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 8,
    "User Name" : "Clark Morgan",
    "User Email" : "clarkmorgan@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 9,
    "User Name" : "Todd Hoffman",
    "User Email" : "todd_hoffman@gmail.com",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}, {
    "ID" : 10,
    "User Name" : "Clark Morgan",
    "User Email" : "clarkmorgan@gmailc.om",
    "Joining Date" : "11/6/2018",
    "Contact" : "111-111-1111",
    "Status" : ""
}];

////

//datepicker

	
	$(function () {
		$('.datetimepicker1').datetimepicker();
	});

    // $(".drp-buttons button").addClass("btn-cstm");

	// $(".drp-buttons .btn-primary").addClass("btn-blue");

	// $(".drp-buttons button").removeClass("btn-sm");

