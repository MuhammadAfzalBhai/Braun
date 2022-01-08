


var SearchType = [
            {
                "Descr": "Please Select",
                "Code": "Please Select"
            }, {
                "Descr": "Vehicle Details",
                "Code": "Vehicle Details"
            }, {
                "Descr": "By VIN",
                "Code": "By VIN"
            }];

var StateCodes = [
{ "Descr": "Alabama, US", "Code": "AL" },
{ "Descr": "Alaska, US", "Code": "AK" },
{ "Descr": "Arizona, US", "Code": "AZ" },
{ "Descr": "Arkansas, US", "Code": "AR" },
{ "Descr": "California, US", "Code": "CA" },
{ "Descr": "Colorado, US", "Code": "CO" },
{ "Descr": "Connecticut, US", "Code": "CT" },
{ "Descr": "Delaware, US", "Code": "DE" },
{ "Descr": "District of Columbia, US", "Code": "DC" },
{ "Descr": "Florida, US", "Code": "FL" },
{ "Descr": "Georgia, US", "Code": "GA" },
{ "Descr": "Hawaii, US", "Code": "HI" },
{ "Descr": "Idaho, US", "Code": "ID" },
{ "Descr": "Illinois, US", "Code": "IL" },
{ "Descr": "Indiana, US", "Code": "IN" },
{ "Descr": "Iowa, US", "Code": "IA" },
{ "Descr": "Kansas, US", "Code": "KS" },
{ "Descr": "Kentucky, US", "Code": "KY" },
{ "Descr": "Louisiana, US", "Code": "LA" },
{ "Descr": "Maine, US", "Code": "ME" },
{ "Descr": "Maryland, US", "Code": "MD" },
{ "Descr": "Massachusetts, US", "Code": "MA" },
{ "Descr": "Michigan, US", "Code": "MI" },
{ "Descr": "Minnesota, US", "Code": "MN" },
{ "Descr": "Mississippi, US", "Code": "MS" },
{ "Descr": "Missouri, US", "Code": "MO" },
{ "Descr": "Montana, US", "Code": "MT" },
{ "Descr": "Nebraska, US", "Code": "NE" },
{ "Descr": "Nevada, US", "Code": "NV" },
{ "Descr": "New Hampshire, US", "Code": "NH" },
{ "Descr": "New Jersey, US", "Code": "NJ" },
{ "Descr": "New Mexico, US", "Code": "NM" },
{ "Descr": "New York, US", "Code": "NY" },
{ "Descr": "North Carolina, US", "Code": "NC" },
{ "Descr": "North Dakota, US", "Code": "ND" },
{ "Descr": "Ohio, US", "Code": "OH" },
{ "Descr": "Oklahoma, US", "Code": "OK" },
{ "Descr": "Oregon, US", "Code": "OR" },
{ "Descr": "Pennsylvania, US", "Code": "PA" },
{ "Descr": "Rhode Island, US", "Code": "RI" },
{ "Descr": "South Carolina, US", "Code": "SC" },
{ "Descr": "South Dakota, US", "Code": "SD" },
{ "Descr": "Tennessee, US", "Code": "TN" },
{ "Descr": "Texas, US", "Code": "TX" },
{ "Descr": "Utah, US", "Code": "UT" },
{ "Descr": "Vermont, US", "Code": "VT" },
{ "Descr": "Virginia, US", "Code": "VA" },
{ "Descr": "Washington, US", "Code": "WA" },
{ "Descr": "West Virginia, US", "Code": "WV" },
{ "Descr": "Wisconsin, US", "Code": "WI" },
{ "Descr": "Wyoming, US", "Code": "WY" }
]

//Common Method for Binding DevExtreme DropDown
function BinddxDropdown(id, data, selectDefaultValue, selectedValue) {
    var selectValue = "";
    if (selectDefaultValue === true && data != undefined && data.length > 0) {
        selectValue = data[0].ID;
    }
    else if (selectDefaultValue === false && data != undefined && data.length > 0) {
        selectValue = selectedValue;
    }

    $("#" + id).dxSelectBox({
        items: data,
        value: selectValue,
        displayExpr: "Descr",
        valueExpr: "Code",
        disabled: false,
        onOpened: function () {
            $("body").css("overflow", "hidden");
            $("body").css("padding-right", "16px");
        },
        onClosed: function () {
            $("body").css("overflow", "auto");
            $("body").css("padding-right", "0");
        },
        onValueChanged: function (data) {

            var controlID = data.element[0].id;

            switch (controlID) {
                //case "vehicledetail":

                //    if (data.value != "Please Select") {

                //        if (data.value == "Vehicle Details") {
                //            //clicktab("searchvehicledetails");
                //            var year = new Date().getFullYear();
                //            getMakeListByYear(year);
                //            //$("#searchvehicledetails").trigger("click");
                //            //clicktab("searchvehicledetails");
                            
                //            BinddxDropdown('state', StateCodes, false, 0);
                //            collapseanchor("searchvehicledetails");
                //        }
                //        else {
                //            //debugger
                //            //clicktab("searchvin");
                //            //$("#searchvehicledetails").trigger("click");
                //            //$("#searchvin").trigger("click");
                //            //clicktab("searchvehicledetails");
                //            //clicktab("searchvin");
                //            collapseanchor("searchvin");
                //        }

                //        //var year = new Date().getFullYear();
                //        //getMakeListByYear(year);
                //    }
                //    else {
                //        $("#vehicledetails1").addClass("disabled_anchor");
                //        $("#vehicledetails2").addClass("disabled_anchor");

                //        $("#searchvehicledetails").addClass("collapsed");
                //        $("#searchvehicledetails").attr("aria-expanded", "false");
                //        $("#searchvehicledetailscontent").removeClass("show");

                //        $("#searchvin").addClass("collapsed");
                //        $("#searchvin").attr("aria-expanded", "false");
                //        $("#searchvincontent").removeClass("show");
                //    }
                //    break;

                case "make":
                    if (data.value != "") {
                        //getYears();
                        getSeries(getSelectBoxSelectedText("year"), getSelectBoxSelectedText("make"), 0);
                    }
                    break;
                case "year":
                    if (data.value != "") {
                        
                        getMakeListByYear(getSelectBoxSelectedText("year"),0);
                        //var makeCode = getSelectBoxSelectedValue("make");
                        //getSeries(data.value, makeCode);
                    }
                    break;
                case "model":
                    if (data.value != "") {
                        debugger;
                        var year = getSelectBoxSelectedText("year");
                        var makeCode = getSelectBoxSelectedText("make");
                        var model = getSelectBoxSelectedText("model")
                        getTrims(year, makeCode, model, 0);
                    }
            }
            //if (controlID == "vehicledetail") {
            //    if (data.value != "Please Select") {
            //        getMakeListByYear("braun_631", "braun_631@@")
            //        //var year = new Date().getFullYear();
            //        //getMakeListByYear(year);
            //    }
            //}
            //else if (controlID == "make") {
            //    if (data.value != "") {
            //        //getYears();
            //    }
            //}
            //else if (controlID == "year") {
            //    if (data.value != "") {
            //        //var makeCode = getSelectBoxSelectedValue("ddlMake");
            //        //getSeries(data.value, makeCode);
            //    }
            //}
            //else if (controlID == "model") {
            //    if (data.value != "") {
            //        //var year = getSelectBoxSelectedValue("ddlYear");
            //        //var makeCode = getSelectBoxSelectedValue("ddlMake");
            //        //getTrims(year, makeCode, data.value);
            //    }
            //}

            console.log(data.value);
        }
    });
}


//Common Method for Enable/Disable DevExtreme DropDown
function DisableDropDown(id, disableValue) {
    $("#" + id).dxSelectBox({
        disabled: disableValue
    });
}



//Selected Value of Select Box
//getSelectBoxSelectedValue("example")
function getSelectBoxSelectedValue(ControlID) {
    var value = $("#" + ControlID).dxSelectBox("instance").option("value");
    return value;
}

//Selected Value of Select Box
//getSelectBoxSelectedText("example")
function getSelectBoxSelectedText(ControlID) {
    var value = $("#" + ControlID).dxSelectBox("instance").option("text");
    return value;
}

function getStateCodes() {
    var token = $("#NadaToken").val();

    $.ajax({
        url: "/Employee/GetStateCode",
        type: "GET",
        data: { Token: token },
        success: function (data) {
            debugger
            //console.log(data);
            if (data.status) {
                BinddxDropdown('make', data.CarsMake, false, 0);
                $("#NadaToken").val(data.Token);
            }
            else {
                AlertToast("warning", data.message);
            }
        }
    });
}


function getMakeListByYear(year, period) {

    period = 0;
    $.ajax({
        type: "GET",
        url: "/QuoteNew/GetMakes?period="+period+"&modelyear="+ year,
        data: {},
        //processData: false,
        success: function (data) {
            data = JSON.parse(data);
            if (data.status) {
                BinddxDropdown('make', data.data, false, 0);
                $("#NadaToken").val(data.Token);
                if ($("#CleanTradeIn").val('') != undefined) {
                    $("#CleanTradeIn").val('');
                    $("#AverageTradeIn").val('');
                    $("#RoughTradeIn").val('');
                    $("#hidecleanavgroughtable").hide();
                }

            }
            else {
                AlertToast("warning", data.message);
            }
        }
    });
}


// OLD
//function getMakeListByYear(year) {
//    $.ajax({
//        url: "/Employee/GetMakeList",
//        type: "GET",
//        data: { Year: year },
//        success: function (data) {
//            debugger
//            //console.log(data);
//            if (data.status) {
//                BinddxDropdown('make', data.CarsMake, false, 0);
//                $("#NadaToken").val(data.Token);
//                if ($("#CleanTradeIn").val('') != undefined) {
//                    $("#CleanTradeIn").val('');
//                    $("#AverageTradeIn").val('');
//                    $("#RoughTradeIn").val('');
//                    $("#hidecleanavgroughtable").hide();
//                }

//            }
//            else {
//                AlertToast("warning", data.message);
//            }
//        }
//    });
//}


function getYears(period) {
    period = 0;
    $.ajax({
        type: "GET",
        url: "/QuoteNew/GetYear?period=" + period,
        data: {},
        //processData: false,
        success: function (data) {
            data = JSON.parse(data);
            if (data.status) {
                BinddxDropdown('year', data.data, false, 0);
                if ($("#CleanTradeIn").val('') != undefined) {
                    $("#CleanTradeIn").val('');
                    $("#AverageTradeIn").val('');
                    $("#RoughTradeIn").val('');
                    $("#hidecleanavgroughtable").hide();
                }

            }
            else {
                AlertToast("warning", data.message);
            }

        }
    });
}


function getSeries(year, makeCode, period) {
    period = 0;
    $.ajax({
        type: "GET",
        url: "/QuoteNew/GetModels?period=" + period + "&modelyear=" + year + "&make=" + makeCode,
        data: {},
        //processData: false,
        success: function (data) {
            data = JSON.parse(data);
            if (data.status) {
                BinddxDropdown('model', data.data, false, 0);
                if ($("#CleanTradeIn").val('') != undefined) {
                    $("#CleanTradeIn").val('');
                    $("#AverageTradeIn").val('');
                    $("#RoughTradeIn").val('');
                    $("#hidecleanavgroughtable").hide();
                }
            }
            else {
                AlertToast("warning", data.message);
            }
        }
    });
}

function getTrims(year, makeCode, seriesCode, period) {

    period = 0;
    $.ajax({
        type: "GET",
        url: "/QuoteNew/GetTrims?period=" + period + "&modelyear=" + year + "&make=" + makeCode + "&model=" + seriesCode,
        data: {},
        //processData: false,
        success: function (data) {
            data = JSON.parse(data);
            if (data.status) {
                BinddxDropdown('trim', data.data, false, 0);
                if ($("#CleanTradeIn").val('') != undefined) {
                    $("#CleanTradeIn").val('');
                    $("#AverageTradeIn").val('');
                    $("#RoughTradeIn").val('');
                    $("#hidecleanavgroughtable").hide();
                }
            }
            else {
                AlertToast("warning", data.message);
            }
        }
    });
}


//function getYears() {

//    var token = $("#NadaToken").val();

//    $.ajax({
//        url: "/Employee/GetYears",
//        type: "GET",
//        data: { Token: token },
//        success: function (data) {
//            debugger
//            //console.log(data);
//            if (data.status) {
//                BinddxDropdown('year', data.CarsYear, false, 0);
//                if ($("#CleanTradeIn").val('') != undefined) {
//                    $("#CleanTradeIn").val('');
//                    $("#AverageTradeIn").val('');
//                    $("#RoughTradeIn").val('');
//                    $("#hidecleanavgroughtable").hide();
//                }

//            }
//            else {
//                AlertToast("warning", data.message);
//            }

//        }
//    });
//}

//function getSeries(year, makeCode) {

//    var token = $("#NadaToken").val();

//    $.ajax({
//        url: "/Employee/GetSeries_Models",
//        type: "GET",
//        data: { Token: token, Year: year, MakeCode: makeCode },
//        success: function (data) {
//            debugger
//            //console.log(data);
//            if (data.status) {
//                BinddxDropdown('model', data.CarsSeries, false, 0);
//                if ($("#CleanTradeIn").val('') != undefined) {
//                    $("#CleanTradeIn").val('');
//                    $("#AverageTradeIn").val('');
//                    $("#RoughTradeIn").val('');
//                    $("#hidecleanavgroughtable").hide();
//                }
//            }
//            else {
//                AlertToast("warning", data.message);
//            }

//        }
//    });
//}


//function getTrims(year, makeCode, seriesCode) {

//    var token = $("#NadaToken").val();

//    $.ajax({
//        url: "/Employee/GetTrims",
//        type: "GET",
//        data: { Token: token, Year: year, MakeCode: makeCode, SeriesCode: seriesCode },
//        success: function (data) {
//            debugger
//            //console.log(data);
//            if (data.status) {
//                BinddxDropdown('trim', data.CarsTrim, false, 0);
//                if ($("#CleanTradeIn").val('') != undefined) {
//                    $("#CleanTradeIn").val('');
//                    $("#AverageTradeIn").val('');
//                    $("#RoughTradeIn").val('');
//                    $("#hidecleanavgroughtable").hide();
//                }
//            }
//            else {
//                AlertToast("warning", data.message);
//            }

//        }
//    });
//}

function getRegions() {
    debugger;
    var token = $("#NadaToken").val();

    $.ajax({
        url: "/Employee/GetRegions",
        type: "GET",
        data: { Token: token },
        success: function (data) {
            debugger
            data = JSON.parse(data);
            //console.log(data);
            if (data.status) {
                BinddxDropdown('region', data.data, false, 0);
            }
            else {
                AlertToast("warning", data.message);
            }

        }
    });
}




function collapseanchor(id) {
    $(".loader").css({ "display": "block" }); //Loader
    if (id == "searchvehicledetails") {

        $("#searchvehicledetails").removeClass("collapsed");
        $("#searchvehicledetails").attr("aria-expanded", "true");
        $("#searchvehicledetailscontent").addClass("show");
        $("#vehicledetails1").removeClass("disabled_anchor");

        $("#searchvin").addClass("collapsed");
        $("#searchvin").attr("aria-expanded", "false");
        $("#searchvincontent").removeClass("show");
        $("#vehicledetails2").addClass("disabled_anchor");
    }
    else {
        $("#searchvin").removeClass("collapsed");
        $("#searchvin").attr("aria-expanded", "true");
        $("#searchvincontent").addClass("show");
        $("#vehicledetails2").removeClass("disabled_anchor");

        $("#searchvehicledetails").addClass("collapsed");
        $("#searchvehicledetails").attr("aria-expanded", "false");
        $("#searchvehicledetailscontent").removeClass("show");
        $("#vehicledetails1").addClass("disabled_anchor");
    }
    $(".loader").css({ "display": "none" }); //Loader
}


