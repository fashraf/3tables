var _examinations = {};
var _investigations = {};
var _diagonosis = {};
var _treatments = {};
var _medications = {};


function funDeleteALLInvestigations() {
    $('#hdnConfirmDialogAction').val("DELETE_ALL_INVESTIGATIONS");
    $('#hdnConfirmDialogVal').val(0);
    $('#ConfirmModalMessage').html("Do you want to delete all investigations?");
    $("#dlgConfirmDialog").modal("show");
}

function funDeleteInvestigation(investigationId, name) {
    $('#hdnConfirmDialogAction').val("DELETE_INVESTIGATION");
    $('#hdnConfirmDialogVal').val(investigationId);
    $('#ConfirmModalMessage').html("Do you want to delete '" + name + "' from investigation?");
    $("#dlgConfirmDialog").modal("show");
}


function HidePopup() {
    $('#dlgPopupDialog').modal('hide');
}

function ConfirmModalDelete() {
    $("input[id$='hdnToastrContent']").val("");
    var action = $('#hdnConfirmDialogAction').val();
    var val = $('#hdnConfirmDialogVal').val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();

    if (action == "DELETE_ALL_INVESTIGATIONS") {
        DeleteAppointmentInvestigation(appointmentId, 0);
        $("#dlgConfirmDialog").modal("hide");
        ShowInvestigationPopup();
        LoadPaging();
    }
    else if (action == "DELETE_INVESTIGATION") {
        DeleteAppointmentInvestigation(appointmentId, val);
        $("#dlgConfirmDialog").modal("hide");
        ShowInvestigationPopup();
        LoadPaging();
    }
}

function ConfirmModalCancel() {
    return false;
}

function ShowExaminationPopup() {
    alert(1);
    $('#txtSearchExamination').val('');
    AppendSearchDivHeader('divSearchExaminationResults', 'tbSearchExaminations', 'Examination');
    AppendSearchDivHeader('divExaminationFavourites', 'tbFavExaminations', 'Examination');
    AppendSearchDivHeader('divExaminationHistory', 'tbHistoryExaminations', 'Examination');

    $("#dlgExamination").modal("show");
    SearchExamination('SearchExamination');
    LoadPaging();
}

function LoadPaging() {
    $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif" /><br/>Just a moment...</h1>' });
    $('#tbSearchInvestigations').dataTable({
        "scrollY": "350px",
        "bInfo": false
        , "bLengthChange": false
        , "bFilter": false
        , "bPaginate": false
        , "bSort": false
        , "oLanguage": {
            "sZeroRecords": "",
            "sEmptyTable": ""
        }
    });
    $.unblockUI();
}

function AppendSearchDivHeader(divSearchResult, tbName, searchFor) {
    $('#' + divSearchResult).empty();
    var table = '<table class="table mb-0" cellspacing="0" id="' + tbName + '" style="width: 100 %; border - collapse: collapse;" style="width:40%;" >';
    table += '<tbody>';
    table += '<thead class="thead-light"><tr>';
    table += '<th scope = "col" style="width:10%;">SR.NO.</th > <th scope="col">' + searchFor + '</th>';
    if (searchFor != 'Diagnosis')
        table += '<th scope = "col" style="width:10%;"> Cost</th>';
    //table += '<th scope = "col" >Urgent ?</th > <th scope="col">Add</th>';
    table += '<th scope="col" style="width:10%;">View</th>';
    table += '<th scope="col" style="width:10%;">Add</th>';
    table += '</tr></thead>'
    table += "</tbody>";
    table += "</table>";
    $('#' + divSearchResult).append(table);
}



function SearchExamination(type) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchExamination').val();
    $.ajax({
        url: '../../handler/SearchExaminationHandler.ashx',
        data: {
            'Action': type,
            'SearchText': searchText,
            'DoctorID': doctorId,
            'AppointmentId': appointmentId
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",
        responseType: "json",
        error: function () {
            alert('An error has occurred. Please try again.');
        },
        success: function (data) {
            PopulateExaminationTable(data.examinations, data.appointmentExaminations, 'tbSearchExaminations');
            PopulateFavouriteExaminationTable(data.favourites, data.appointmentExaminations, 'tbFavExaminations');
            PopulateExaminationHistoryTable(data.history, data.appointmentExaminations, 'tbHistoryExaminations');
        },
    });
}


function PopulateExaminationTable(data, alreadyAddedData, tableId) {
    $("[id^='trSearchExaminations_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trSearchExaminations_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:60%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Cost;
        table += '</td>';

        table += '<td style="width:10%;">';

        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentExamination_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentExamination_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentExamination_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteExamination_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteExamination_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddExamination(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateFavouriteExaminationTable(data, alreadyAddedData, tableId) {
    $("[id^='trFavouriteExaminations_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trFavouriteExaminations_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:60%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Cost;
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentExaminationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentExaminationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        }
        else {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentExaminationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentExaminationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteExamination_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteExamination_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddFavouriteExamination(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateExaminationHistoryTable(data, alreadyAddedData, tableId) {
    $("[id^='trHistoryExaminations_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trHistoryExaminations_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:60%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Cost;
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentExaminationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentExaminationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input"  disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentExaminationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteExamination_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteExamination_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddHistoryExamination(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function AddExamination(examinationId, examinationCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchExamination').val();
    var isUrgent = $('#chkIsUrgentExamination_' + examinationId).is(':checked');
    $.ajax({
        url: '../../handler/SearchExaminationHandler.ashx',
        data: {
            'Action': 'AddExamination',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'ExaminationID': examinationId,
            'ExaminationCost': examinationCost,
            'AppointmentId': appointmentId,
            'IsUrgent': isUrgent,
            'IsFav': false,
            'IsHistory': false
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",

        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
            PopulateExaminationTable(data.examinations, data.appointmentExaminations, 'tbSearchExaminations');
            PopulateFavouriteExaminationTable(data.favourites, data.appointmentExaminations, 'tbFavExaminations');
            PopulateExaminationHistoryTable(data.history, data.appointmentExaminations, 'tbHistoryExaminations');
            toastr.success('Examination is added succesfully');
        }
    });
}

function AddFavouriteExamination(examinationId, examinationCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchExamination').val();
    var isUrgent = $('#chkIsUrgentExaminationFavourite_' + examinationId).is(':checked');
    $.ajax({
        url: '../../handler/SearchExaminationHandler.ashx',
        data: {
            'Action': 'AddExamination',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'ExaminationID': examinationId,
            'ExaminationCost': examinationCost,
            'AppointmentId': appointmentId,
            'IsUrgent': isUrgent,
            'IsFav': true,
            'IsHistory': false
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",

        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
            PopulateExaminationTable(data.examinations, data.appointmentExaminations, 'tbSearchExaminations');
            PopulateFavouriteExaminationTable(data.favourites, data.appointmentExaminations, 'tbFavExaminations');
            PopulateExaminationHistoryTable(data.history, data.appointmentExaminations, 'tbHistoryExaminations');
            toastr.success('Examination from favourites is added succesfully');
        }
    });
}

function AddHistoryExamination(examinationId, examinationCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchExamination').val();
    var isUrgent = $('#chkIsUrgentExaminationHistory_' + examinationId).is(':checked');
    $.ajax({
        url: '../../handler/SearchExaminationHandler.ashx',
        data: {
            'Action': 'AddExamination',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'ExaminationID': examinationId,
            'ExaminationCost': examinationCost,
            'AppointmentId': appointmentId,
            'IsUrgent': isUrgent,
            'IsFav': false,
            'IsHistory': true
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",

        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
            PopulateExaminationTable(data.examinations, data.appointmentExaminations, 'tbSearchExaminations');
            PopulateFavouriteExaminationTable(data.favourites, data.appointmentExaminations, 'tbFavExaminations');
            PopulateExaminationHistoryTable(data.history, data.appointmentExaminations, 'tbHistoryExaminations');
            toastr.success('Examination from history is added succesfully');
        }
    });
}

function DeleteAppointmentExamination(appointmentId, examinationId) {
    $.ajax({
        url: '../../handler/ExaminationHandler.ashx',
        data: {
            'Action': 'Delete',
            'ExaminationID': examinationId,
            'AppointmentId': appointmentId,
        },
        type: 'POST',
        async: false,
        contentType: "application/x-www-form-urlencoded",
        responseType: "json",
        error: function () {
            toastr.error('Some error has occured. Please try again');
        },
        success: function (data) {
            _examinations = data.examinations;
            ShowAppointmentExaminationMasterDetails(data.examinations, false);
            if (examinationId == 0)
                toastr.success('All examinations are deleted successfully');
            else
                toastr.success('Examination is deleted successfully');
        }
    });
}

function GetAppointmentExaminations() {
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    $.ajax({
        url: '../../handler/ExaminationHandler.ashx',
        data: {
            'AppointmentId': appointmentId
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",
        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
            _examinations = data.examinations;
            ShowAppointmentExaminationMasterDetails(data.examinations, false);
        },
    });
}

function ShowAppointmentExaminationMasterDetails(data, isReview) {
    var table = '';
    if (data != undefined && data.length > 0) {
        table = '<table class="table table-hover" cellspacing="0" id="gvExaminations" style="width:100%;border-collapse:collapse;">';
        table += '<tbody>';
        table += '<tr style="color:White;background-color:Black;">';
        table += '<th scope="col">Ser.#</th><th scope="col">Examination Name</th><th scope="col">Examination Cost</th><th scope="col"> Is Urgent ?</th><th scope="col">Status</th>';
        if (!isReview) {
            table += '<th scope="col">Delete</th>';
        }
        table += '</tr >';
        for (var i = 0; i < data.length; i++) {
            table += '<tr>';

            table += '<td style="width:10%;">';
            table += (i + 1);
            table += '</td>';

            table += '<td style="width:50%;">';
            table += data[i].Name;
            table += '</td>';

            table += '<td style="width:5%;">';
            table += data[i].Cost;
            table += '</td>';

            table += '<td style="width:10%;">';
            table += data[i].IsUrgent;
            table += '</td>';

            table += '<td style="width:15%;">';
            table += data[i].Status;
            table += '</td>';

            if (!isReview) {
                table += '<td style="width:10%;">';
                table += '<input type="button" id="btnDelete" value="Delete" class="btn btn-danger" onclick="return funDeleteExamination(' + data[i].SNo + ',\'' + data[i].Name + '\');">';
                table += '</td>';
            }

            table += '</tr >';
        }
        table += '</tbody>';
        table += '</table>';

        if (isReview) {
            $('#dvReviewExaminationInfo').hide();
            $('#spnReviewTotalExaminationCount').html(" - " + data.length);
        }
        else {
            $('#dvExaminationInfo').hide();
            $('#spnTotalExaminationCount').html(" - " + data.length);
            $('#btnDeleteAllExaminations').show();
        }
    }
    else {
        if (isReview) {
            table = '<div class="alert alert-danger" role="alert" id="dvReviewExaminationInfo">No record found</div>';
            $('#spnReviewTotalExaminationCount').html('');
        }
        else {
            table = '<div class="alert alert-danger" role="alert" id="dvExaminationInfo">No record found</div>';
            $('#spnTotalExaminationCount').html('');
            $('#btnDeleteAllExaminations').hide();
        }
    }

    if (isReview) {
        $('#divReviewAppointmentExaminations').empty();
        $('#divReviewAppointmentExaminations').append(table);
    }
    else {
        $('#divAppointmentExaminations').empty();
        $('#divAppointmentExaminations').append(table);
    }
}



//////////////////////////////////////////////////////////////////////////////////////////////////
function ShowInvestigationPopup() {
    $('#txtSearchInvestigation').val('');

    AppendSearchDivHeader('divSearchInvestigationResults', 'tbSearchInvestigations', 'Investigation');
    AppendSearchDivHeader('divInvestigationFavourites', 'tbFavInvestigations', 'Investigation');
    AppendSearchDivHeader('divInvestigationHistory', 'tbHistoryInvestigations', 'Investigation');

    //$("#dlgInvestigation").modal("show");

    SearchInvestigation('SearchInvestigation');
}


function SearchInvestigation(type) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchInvestigation').val();
    $.ajax({
        url: '../../handler/SearchInvestigationHandler.ashx',
        data: {
            'Action': type,
            'SearchText': searchText,
            'DoctorID': doctorId,
            'AppointmentId': appointmentId
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",
        responseType: "json",
        error: function () {
            alert('An error has occurred. Please try again1.');
        },
        success: function (data) {
            PopulateInvestigationTable(data.investigations, data.appointmentInvestigations, 'tbSearchInvestigations');
            PopulateFavouriteInvestigationTable(data.favourites, data.appointmentInvestigations, 'tbFavInvestigations');
            PopulateInvestigationHistoryTable(data.history, data.appointmentInvestigations, 'tbHistoryInvestigations');
        },
    });
}


function PopulateInvestigationTable(data, alreadyAddedData, tableId) {
    $("[id^='trSearchInvestigations_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trSearchInvestigations_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:50%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Cost;
        table += '</td>';

        table += '<td style="width:10%;">';
        //table += '<input type="button" class="btn btn-primary" width="30px" value="View" onClick="GetPopupData(' + data[i].SNo + ',' + name + ');"></>';
        table += '<input type="button" class="btn btn-soft-primary" width="30px" value="View" onClick="GetPopupData(' + data[i].SNo + ');"></>';
        table += '</td>';

        //table += '<td style="width:15%;">';

        //if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
        //    if (data[i].IsUrgent == 1)
        //        table += '<input id="chkIsUrgentInvestigation_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
        //    else
        //        table += '<input id="chkIsUrgentInvestigation_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        //}
        //else {
        //    table += '<input id="chkIsUrgentInvestigation_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        //}
        //table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteInvestigation_' + data[i].SNo + '" class="btn btn-success" disabled="true" style="width:80px">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteInvestigation_' + data[i].SNo + '" class="btn btn-primary" style="width:80px" onClick="return AddInvestigation(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function GetPopupData(testid) {
    $.ajax({
        type: "POST",
        url: "../../Handler/MTestDetail.ashx?TestId=" + testid + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#MyPopup").modal("show");
            //$("#MyPopup .modal-title").html(name + " details" + " -" + response.length + " component/s");
            $('#tblDetails').find('tbody').empty();
            if (response.length > 0) {
                $('#tblDetails').show();
                $("#noDetails").hide();
                TotalSubInvest
                $.each(response, function (index, itemData) {
                    var tr = "<tr>" +
                        "<td >" + itemData.SIName + "</td>" +
                        "<td>" + itemData.SIShortName + "</td>" +
                        "<td >" + itemData.SIRangeLow + "</td>" +
                        "<td >" + itemData.SIRangeHigh + "</td>" +
                        "<td >" + itemData.SIUnits + "</td>" +
                        "</tr>";
                    var investigationname = itemData.InvestigationName;
                    $("#MyPopup .modal-title").html(investigationname + " details" + " -" + response.length + " component/s");
                    $("#tblDetails").find("tbody").append(tr);
                });
            }
            else {
                $('#tblDetails').hide();
                $("#noDetails").show();
            }
        }
    });
}


function PopulateFavouriteInvestigationTable(data, alreadyAddedData, tableId) {
    $("[id^='trFavouriteInvestigations_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trFavouriteInvestigations_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:50%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:20%;">';
        table += data[i].Cost;
        table += '</td>';

        //table += '<td style="width:20%;">';
        //if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
        //    if (data[i].IsUrgent == 1)
        //        table += '<input id="chkIsUrgentInvestigationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
        //    else
        //        table += '<input id="chkIsUrgentInvestigationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        //}
        //else {
        //    table += '<input id="chkIsUrgentInvestigationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        //}
        //table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteInvestigation_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteInvestigation_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddFavouriteInvestigation(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateInvestigationHistoryTable(data, alreadyAddedData, tableId) {
    $("[id^='trHistoryInvestigations_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trHistoryInvestigations_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:60%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:20%;">';
        table += data[i].Cost;
        table += '</td>';

        //table += '<td style="width:10%;">';
        //if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
        //    if (data[i].IsUrgent == 1)
        //        table += '<input id="chkIsUrgentInvestigationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
        //    else
        //        table += '<input id="chkIsUrgentInvestigationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input"  disabled="true"> Is Urgent </input>';
        //}
        //else {
        //    table += '<input id="chkIsUrgentInvestigationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        //}
        //table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteInvestigation_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteInvestigation_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddHistoryInvestigation(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function AddInvestigation(InvestigationId, InvestigationCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchInvestigation').val();
    var isUrgent = $('#chkIsUrgentInvestigation_' + InvestigationId).is(':checked');
    $.ajax({
        url: '../../handler/SearchInvestigationHandler.ashx',
        data: {
            'Action': 'AddInvestigation',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'InvestigationID': InvestigationId,
            'InvestigationCost': InvestigationCost,
            'AppointmentId': appointmentId,
            'IsUrgent': isUrgent,
            'IsFav': false,
            'IsHistory': false
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",

        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again2.');
        },
        success: function (data) {
            GetAppointmentInvestigations();
            PopulateInvestigationTable(data.investigations, data.appointmentInvestigations, 'tbSearchInvestigations');
            PopulateFavouriteInvestigationTable(data.favourites, data.appointmentInvestigations, 'tbFavInvestigations');
            PopulateInvestigationHistoryTable(data.history, data.appointmentInvestigations, 'tbHistoryInvestigations');
            toastr.success('Investigation is added succesfully');
        }
    });
}

function AddFavouriteInvestigation(InvestigationId, InvestigationCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchInvestigation').val();
    var isUrgent = $('#chkIsUrgentInvestigationFavourite_' + InvestigationId).is(':checked');
    $.ajax({
        url: '../../handler/SearchInvestigationHandler.ashx',
        data: {
            'Action': 'AddInvestigation',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'InvestigationID': InvestigationId,
            'InvestigationCost': InvestigationCost,
            'AppointmentId': appointmentId,
            'IsUrgent': isUrgent,
            'IsFav': true,
            'IsHistory': false
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",

        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again3.');
        },
        success: function (data) {
            GetAppointmentInvestigations();
            PopulateInvestigationTable(data.investigations, data.appointmentInvestigations, 'tbSearchInvestigations');
            PopulateFavouriteInvestigationTable(data.favourites, data.appointmentInvestigations, 'tbFavInvestigations');
            PopulateInvestigationHistoryTable(data.history, data.appointmentInvestigations, 'tbHistoryInvestigations');
            toastr.success('Investigation from favourites is added succesfully');
        }
    });
}

function AddHistoryInvestigation(InvestigationId, InvestigationCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchInvestigation').val();
    var isUrgent = $('#chkIsUrgentInvestigationHistory_' + InvestigationId).is(':checked');
    $.ajax({
        url: '../../handler/SearchInvestigationHandler.ashx',
        data: {
            'Action': 'AddInvestigation',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'InvestigationID': InvestigationId,
            'InvestigationCost': InvestigationCost,
            'AppointmentId': appointmentId,
            'IsUrgent': isUrgent,
            'IsFav': false,
            'IsHistory': true
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",

        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again4.');
        },
        success: function (data) {
            GetAppointmentInvestigations();
            PopulateInvestigationTable(data.investigations, data.appointmentInvestigations, 'tbSearchInvestigations');
            PopulateFavouriteInvestigationTable(data.favourites, data.appointmentInvestigations, 'tbFavInvestigations');
            PopulateInvestigationHistoryTable(data.history, data.appointmentInvestigations, 'tbHistoryInvestigations');
            toastr.success('Investigation from history is added succesfully');
        }
    });
}

function DeleteAppointmentInvestigation(appointmentId, InvestigationId) {
    $.ajax({
        url: '../../handler/InvestigationHandler.ashx',
        data: {
            'Action': 'Delete',
            'InvestigationID': InvestigationId,
            'AppointmentId': appointmentId,
        },
        type: 'POST',
        async: false,
        contentType: "application/x-www-form-urlencoded",
        responseType: "text",
        error: function () {
            toastr.error('Some error has occured. Please try again5');
        },
        success: function (data) {
            _investigations = data.investigations;
            ShowAppointmentInvestigationMasterDetails(data.investigations, false);
            if (InvestigationId == 0)
                toastr.error('All investigations deleted successfully');
            else
                toastr.error('Investigation deleted successfully');
        }
    });
}

function GetAppointmentInvestigations() {
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    $.ajax({
        url: '../../handler/InvestigationHandler.ashx',
        data: {
            'AppointmentId': appointmentId
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",
        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
            _investigations = data.investigations;
            ShowAppointmentInvestigationMasterDetails(data.investigations, false);
        },
    });
}

function ShowAppointmentInvestigationMasterDetails(data, isReview) {
    var table = '';
    if (data != undefined && data.length > 0) {
        table = '<table class="table mb-0" id="gvInvestigations">';
        table += '<tbody>';
        table += '<tr >';
        //table += '<thead class="thead-light" style="width:100%;"><th>Ser.#</th><th scope="col">Investigation Name</th><th scope="col">Investigation Cost</th><th scope="col"> Is Urgent ?</th><th scope="col">Status</th>  </thead>'
        //table += '<thead class="thead-light"><th>Ser.#</th><th scope="col">Investigation Name</th><th scope="col">Urgent ?</th>'
        table += '<thead class="thead-light"><th>Ser.#</th><th scope="col">Investigation Name</th>'
        if (!isReview) {
            table += '<th scope="col" > Delete</th></thead>';
        }
        table += '</tr >';
        for (var i = 0; i < data.length; i++) {
            table += '<tr>';

            table += '<td style="width:10%;">';
            table += (i + 1);
            table += '</td>';

            table += '<td style="width:50%;">';
            table += data[i].Name;
            table += '</td>';

            //table += '<td style="width:5%;">';
            //table += data[i].Cost;
            //table += '</td>';

            //table += '<td style="width:20%;">';
            //table += data[i].IsUrgent;
            //table += '</td>';

            //table += '<td style="width:15%;">';
            //table += data[i].Status;
            //table += '</td>';

            if (!isReview) {
                table += '<td style="width:10%;">';
                table += '<input type="button" id="btnDelete" value="Delete" class="btn btn-danger" onclick="return funDeleteInvestigation(' + data[i].SNo + ',\'' + data[i].Name + '\');">';
                table += '</td>';
            }

            table += '</tr >';
        }
        table += '</tbody>';
        table += '</table>';

        if (isReview) {
            $('#dvReviewInvestigationInfo').hide();
            $('#spnReviewTotalInvestigationCount').html(" - " + data.length);
        }
        else {
            $('#dvInvestigationInfo').hide();
            $('#spnTotalInvestigationCount').html(" - " + data.length);
            $('#btnDeleteAllInvestigations').show();
        }
    }
    else {
        if (isReview) {
            table = '<div class="alert alert-danger" role="alert" id="dvReviewInvestigationInfo">No Investigation Added</div>';
            $('#spnReviewTotalInvestigationCount').html('');
        }
        else {
            table = '<div class="alert alert-danger" role="alert" id="dvInvestigationInfo">No Investigation Added</div>';
            $('#spnTotalInvestigationCount').html('');
            $('#btnDeleteAllInvestigations').hide();
        }
    }

    if (isReview) {
        $('#divReviewAppointmentInvestigations').empty();
        $('#divReviewAppointmentInvestigations').append(table);
    }
    else {
        $('#divAppointmentInvestigations').empty();
        $('#divAppointmentInvestigations').append(table);
    }
}


function ValidateAppointmentDetails() {
    if ($('#gvInvestigations').length == 0) {
        $('#divPopUpMessage').html('No investigation record found.');
        $('#dlgPopupDialog').modal('show');
        return false;
    }
    return true;
}

function FillReviewPopUpDetails() {
    //$("[id='lblLeftEyeDistSPH']").html($("input[id$='txtLeftEyeDistSPH']").val());
    //$("[id='lblLeftEyeDistCYL']").html($("input[id$='txtLeftEyeDistCYL']").val());
    //$("[id='lblLeftEyeDistAXIS']").html($("input[id$='txtLeftEyeDistAXIS']").val());
    //$("[id='lblLeftEyeDistVA']").html($("input[id$='txtLeftEyeDistVA']").val());
    //$("[id='lblLeftEyeNearSPH']").html($("input[id$='txtLeftEyeNearSPH']").val());
    //$("[id='lblLeftEyeNearCYL']").html($("input[id$='txtLeftEyeNearCYL']").val());
    //$("[id='lblLeftEyeNearAXIS']").html($("input[id$='txtLeftEyeNearAXIS']").val());
    //$("[id='lblLeftEyeNearVA']").html($("input[id$='txtLeftEyeNearVA']").val());
    //$("[id='lblRightEyeDistSPH']").html($("input[id$='txtRightEyeDistSPH']").val());
    //$("[id='lblRightEyeDistCYL']").html($("input[id$='txtRightEyeDistCYL']").val());
    //$("[id='lblRightEyeDistAXIS']").html($("input[id$='txtRightEyeDistAXIS']").val());
    //$("[id='lblRightEyeDistVA']").html($("input[id$='txtRightEyeDistVA']").val());
    //$("[id='lblRightEyeNearSPH']").html($("input[id$='txtRightEyeNearSPH']").val());
    //$("[id='lblRightEyeNearCYL']").html($("input[id$='txtRightEyeNearCYL']").val());
    //$("[id='lblRightEyeNearAXIS']").html($("input[id$='txtRightEyeNearAXIS']").val());
    //$("[id='lblRightEyeNearVA']").html($("input[id$='txtRightEyeNearVA']").val());

    //$("[id='chkReviewARC']").prop('checked', $("[id$='chkARC']").is(':checked'));
    //$("[id='chkReviewBifocal']").prop('checked', $("[id$='chkBifocal']").is(':checked'));
    //$("[id='chkReviewConstant']").prop('checked', $("[id$='chkConstant']").is(':checked'));
    //$("[id='chkReviewNear']").prop('checked', $("[id$='chkNear']").is(':checked'));
    //$("[id='chkReviewPhotoSun']").prop('checked', $("[id$='chkPhotoSun']").is(':checked'));
    //$("[id='chkReviewProgressive']").prop('checked', $("[id$='chkProgressive']").is(':checked'));

    //$("[id='lblIPD']").html($("input[id$='txtIPD']").val());
    //$('#txtReviewFinalOutcome').text($("[id$='txtFinalOutcome']").val());
    ShowAppointmentInvestigationMasterDetails(_investigations, true);
}