var _examinations = {};
var _investigations = {};
var _diagonosis = {};
var _treatments = {};
var _medications = {};

function funDeleteALLExaminations() {
    $('#hdnConfirmDialogAction').val("DELETE_ALL_EXAMINATIONS");
    $('#hdnConfirmDialogVal').val(0);
    $('#ConfirmModalMessage').html("Do you want to delete all examinations?");
    $("#dlgConfirmDialog").modal("show");
}

function funDeleteExamination(examinationId, name) {
    $('#hdnConfirmDialogAction').val("DELETE_EXAMINATION");
    $('#hdnConfirmDialogVal').val(examinationId);
    $('#ConfirmModalMessage').html("Do you want to delete '" + name + "' from examination?");
    $("#dlgConfirmDialog").modal("show");
}

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

function funDeleteALLDiagnosis() {
    $('#hdnConfirmDialogAction').val("DELETE_ALL_DIAGNOSIS");
    $('#hdnConfirmDialogVal').val(0);
    $('#ConfirmModalMessage').html("Do you want to delete all diagnosis?");
    $("#dlgConfirmDialog").modal("show");
}

function funDeleteDiagnosis(diagnosisId, name) {
    $('#hdnConfirmDialogAction').val("DELETE_DIAGNOSIS");
    $('#hdnConfirmDialogVal').val(diagnosisId);
    $('#ConfirmModalMessage').html("Do you want to delete '" + name + "' from diagnosis?");
    $("#dlgConfirmDialog").modal("show");
}

function funDeleteALLTreatments() {
    $('#hdnConfirmDialogAction').val("DELETE_ALL_TREATMENTS");
    $('#hdnConfirmDialogVal').val(0);
    $('#ConfirmModalMessage').html("Do you want to delete all treatments?");
    $("#dlgConfirmDialog").modal("show");
}

function funDeleteTreatment(treatmentId, name) {
    $('#hdnConfirmDialogAction').val("DELETE_TREATMENT");
    $('#hdnConfirmDialogVal').val(treatmentId);
    $('#ConfirmModalMessage').html("Do you want to delete '" + name + "' from treatment?");
    $("#dlgConfirmDialog").modal("show");
}

function funDeleteALLMedications() {
    $('#hdnConfirmDialogAction').val("DELETE_ALL_MEDICATIONS");
    $('#hdnConfirmDialogVal').val(0);
    $('#ConfirmModalMessage').html("Do you want to delete all medications?");
    $("#dlgConfirmDialog").modal("show");
}

function funDeleteMedication(medicationMasterID, name) {
    $('#hdnConfirmDialogAction').val("DELETE_MEDICATION");
    $('#hdnConfirmDialogVal').val(medicationMasterID);
    $('#ConfirmModalMessage').html("Do you want to delete '" + name + "' from medication?");
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

    if (action == "DELETE_ALL_EXAMINATIONS") {
        DeleteAppointmentExamination(appointmentId, 0);
    }
    else if (action == "DELETE_EXAMINATION") {
        DeleteAppointmentExamination(appointmentId, val);
    }
    else if (action == "DELETE_ALL_INVESTIGATIONS") {
        DeleteAppointmentInvestigation(appointmentId, 0);
    }
    else if (action == "DELETE_INVESTIGATION") {
        DeleteAppointmentInvestigation(appointmentId, val);
    }
    else if (action == "DELETE_ALL_DIAGNOSIS") {
        DeleteAppointmentDiagnosis(appointmentId, 0);
    }
    else if (action == "DELETE_DIAGNOSIS") {
        DeleteAppointmentDiagnosis(appointmentId, val);
    }
    else if (action == "DELETE_ALL_TREATMENTS") {
        DeleteAppointmentTreatment(appointmentId, 0);
    }
    else if (action == "DELETE_TREATMENT") {
        DeleteAppointmentTreatment(appointmentId, val);
    }
    else if (action == "DELETE_ALL_MEDICATIONS") {
        DeleteAppointmentMedication(appointmentId, 0);
    }
    else if (action == "DELETE_MEDICATION") {
        DeleteAppointmentMedication(appointmentId, val);
    }
}

function ConfirmModalCancel() {
    return false;
}

function ShowExaminationPopup() {
    $('#txtSearchExamination').val('');
    AppendSearchDivHeader('divSearchExaminationResults', 'tbSearchExaminations', 'Examination');
    AppendSearchDivHeader('divExaminationFavourites', 'tbFavExaminations', 'Examination');
    AppendSearchDivHeader('divExaminationHistory', 'tbHistoryExaminations', 'Examination');
    $("#dlgExamination").modal("show");
    SearchExamination('SearchExamination');
}

function AppendSearchDivHeader(divSearchResult, tbName, searchFor) {
    $('#' + divSearchResult).empty();
    var table = '<table class="table table-hover" cellspacing="0" id="' + tbName + '" style="width: 100 %; border - collapse: collapse;">';
    table += '<tbody>';
    table += '<tr class = "thead-light" >';
    table += '<th scope = "col" >SR.NO.</th > <th scope="col">' + searchFor + '</th>';
    if (searchFor != 'Diagnosis')
        table += '<th scope = "col" > Cost</th>';
    table += '<th scope = "col" > Is Urgent ?</th > <th scope="col">Add</th>';
    table += '</tr>'
    table += "</tbody>";
    table += "</table>";
    $('#' + divSearchResult).append(table);
}

function SearchExamination(type) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchExamination').val();
    $.ajax({
        url: '../handler/SearchExaminationHandler.ashx',
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
            table += '<button type="button"  value="' + btnText + '" id="btnAddFavouriteExamination_' + data[i].SNo + '" class="btn btn-success" disabled="true"><i class="mdi mdi-check-all me-2"></i>Added</button>';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteExamination_' + data[i].SNo + '" class="btn btn-primary" onClick="return AddExamination(' + data[i].SNo + ',' + data[i].Cost + ');">';
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
        url: '../handler/SearchExaminationHandler.ashx',
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
        url: '../handler/SearchExaminationHandler.ashx',
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
        url: '../handler/SearchExaminationHandler.ashx',
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
        url: '../handler/ExaminationHandler.ashx',
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
        url: '../handler/ExaminationHandler.ashx',
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
        table += '<tr class = "thead-light" >';
        table += '<th scope="col">Ser.#</th><th scope="col">Examination Name</th><th scope="col">Examination Cost</th><th scope="col"> Is Urgent ?</th><th scope="col">Status</th>';
        if (!isReview) {
            table += '<th scope="col">Delete</th>';
        }
        table += '</tr >';
        for (var i = 0; i < data.length; i++) {
            table += '<tr>';

            table += '<td style="width:5%;">';
            table += (i + 1);
            table += '</td>';

            table += '<td style="width:45%;">';
            table += data[i].Name;
            table += '</td>';

            table += '<td style="width:15ss%;">';
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

    $("#dlgInvestigation").modal("show");

    SearchInvestigation('SearchInvestigation');
}


function SearchInvestigation(type) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchInvestigation').val();
    $.ajax({
        url: '../handler/SearchInvestigationHandler.ashx',
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

        table += '<td style="width:60%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Cost;
        table += '</td>';

        table += '<td style="width:10%;">';

        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentInvestigation_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentInvestigation_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentInvestigation_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

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
            table += '<input value="Add" id="btnAddFavouriteInvestigation_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddInvestigation(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateFavouriteInvestigationTable(data, alreadyAddedData, tableId) {
    $("[id^='trFavouriteInvestigations_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trFavouriteInvestigations_' + data[i].SNo + '">';

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
                table += '<input id="chkIsUrgentInvestigationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentInvestigationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentInvestigationFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

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

        table += '<td style="width:10%;">';
        table += data[i].Cost;
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentInvestigationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentInvestigationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input"  disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentInvestigationHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

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
        url: '../handler/SearchInvestigationHandler.ashx',
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
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
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
        url: '../handler/SearchInvestigationHandler.ashx',
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
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
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
        url: '../handler/SearchInvestigationHandler.ashx',
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
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
            PopulateInvestigationTable(data.investigations, data.appointmentInvestigations, 'tbSearchInvestigations');
            PopulateFavouriteInvestigationTable(data.favourites, data.appointmentInvestigations, 'tbFavInvestigations');
            PopulateInvestigationHistoryTable(data.history, data.appointmentInvestigations, 'tbHistoryInvestigations');
            toastr.success('Investigation from history is added succesfully');
        }
    });
}

function DeleteAppointmentInvestigation(appointmentId, InvestigationId) {
    $.ajax({
        url: '../handler/InvestigationHandler.ashx',
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
            toastr.error('Some error has occured. Please try again');
        },
        success: function (data) {
            _investigations = data.investigations;
            ShowAppointmentInvestigationMasterDetails(data.investigations, false);
            if (InvestigationId == 0)
                toastr.success('All investigations are deleted successfully');
            else
                toastr.success('Investigation is deleted successfully');
        }
    });
}

function GetAppointmentInvestigations() {
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    $.ajax({
        url: '../handler/InvestigationHandler.ashx',
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
        table = '<table class="table table-hover" cellspacing="0" id="gvInvestigations" style="width:100%;border-collapse:collapse;">';
        table += '<tbody>';
        table += '<tr style="color:White;background-color:Black;">';
        table += '<th scope="col">Ser.#</th><th scope="col">Investigation Name</th><th scope="col">Investigation Cost</th><th scope="col"> Is Urgent ?</th><th scope="col">Status</th>'
        if (!isReview) {
            table += '<th scope="col" > Delete</th>';
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
            table = '<div class="alert alert-danger" role="alert" id="dvReviewInvestigationInfo">No record found</div>';
            $('#spnReviewTotalInvestigationCount').html('');
        }
        else {
            table = '<div class="alert alert-danger" role="alert" id="dvInvestigationInfo">No record found</div>';
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

//////////////////////////////////////////////////////////////////////////////////////////////////
function ShowTreatmentPopup() {
    $('#txtSearchTreatment').val('');

    AppendSearchDivHeader('divSearchTreatmentResults', 'tbSearchTreatments', 'Treatment');
    AppendSearchDivHeader('divTreatmentFavourites', 'tbFavTreatments', 'Treatment');
    AppendSearchDivHeader('divTreatmentHistory', 'tbHistoryTreatments', 'Treatment');

    $("#dlgTreatment").modal("show");

    SearchTreatment('SearchTreatment');
}


function SearchTreatment(type) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchTreatment').val();
    $.ajax({
        url: '../handler/SearchTreatmentHandler.ashx',
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
            PopulateTreatmentTable(data.treatments, data.appointmentTreatments, 'tbSearchTreatments');
            PopulateFavouriteTreatmentTable(data.favourites, data.appointmentTreatments, 'tbFavTreatments');
            PopulateTreatmentHistoryTable(data.history, data.appointmentTreatments, 'tbHistoryTreatments');
        },
    });
}


function PopulateTreatmentTable(data, alreadyAddedData, tableId) {
    $("[id^='trSearchTreatments_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trSearchTreatments_' + data[i].SNo + '">';

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
                table += '<input id="chkIsUrgentTreatment_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentTreatment_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentTreatment_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteTreatment_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteTreatment_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddTreatment(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateFavouriteTreatmentTable(data, alreadyAddedData, tableId) {
    $("[id^='trFavouriteTreatments_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trFavouriteTreatments_' + data[i].SNo + '">';

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
                table += '<input id="chkIsUrgentTreatmentFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentTreatmentFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentTreatmentFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteTreatment_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteTreatment_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddFavouriteTreatment(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateTreatmentHistoryTable(data, alreadyAddedData, tableId) {
    $("[id^='trHistoryTreatments_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trHistoryTreatments_' + data[i].SNo + '">';

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
                table += '<input id="chkIsUrgentTreatmentHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentTreatmentHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input"  disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentTreatmentHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteTreatment_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteTreatment_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddHistoryTreatment(' + data[i].SNo + ',' + data[i].Cost + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function AddTreatment(treatmentId, treatmentCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchTreatment').val();
    var isUrgent = $('#chkIsUrgentTreatment_' + treatmentId).is(':checked');
    $.ajax({
        url: '../handler/SearchTreatmentHandler.ashx',
        data: {
            'Action': 'AddTreatment',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'TreatmentID': treatmentId,
            'TreatmentCost': treatmentCost,
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
            PopulateTreatmentTable(data.treatments, data.appointmentTreatments, 'tbSearchTreatments');
            PopulateFavouriteTreatmentTable(data.favourites, data.appointmentTreatments, 'tbFavTreatments');
            PopulateTreatmentHistoryTable(data.history, data.appointmentTreatments, 'tbHistoryTreatments');
            toastr.success('Treatment is added succesfully');
        }
    });
}

function AddFavouriteTreatment(treatmentId, TreatmentCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchTreatment').val();
    var isUrgent = $('#chkIsUrgentTreatmentFavourite_' + treatmentId).is(':checked');
    $.ajax({
        url: '../handler/SearchTreatmentHandler.ashx',
        data: {
            'Action': 'AddTreatment',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'TreatmentID': treatmentId,
            'TreatmentCost': TreatmentCost,
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
            PopulateTreatmentTable(data.treatments, data.appointmentTreatments, 'tbSearchTreatments');
            PopulateFavouriteTreatmentTable(data.favourites, data.appointmentTreatments, 'tbFavTreatments');
            PopulateTreatmentHistoryTable(data.history, data.appointmentTreatments, 'tbHistoryTreatments');
            toastr.success('Treatment from favourites is added succesfully');
        }
    });
}

function AddHistoryTreatment(treatmentId, TreatmentCost) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchTreatment').val();
    var isUrgent = $('#chkIsUrgentTreatmentHistory_' + treatmentId).is(':checked');
    $.ajax({
        url: '../handler/SearchTreatmentHandler.ashx',
        data: {
            'Action': 'AddTreatment',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'TreatmentID': treatmentId,
            'TreatmentCost': TreatmentCost,
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
            PopulateTreatmentTable(data.treatments, data.appointmentTreatments, 'tbSearchTreatments');
            PopulateFavouriteTreatmentTable(data.favourites, data.appointmentTreatments, 'tbFavTreatments');
            PopulateTreatmentHistoryTable(data.history, data.appointmentTreatments, 'tbHistoryTreatments');
            toastr.success('Treatment from history is added succesfully');
        }
    });
}

function DeleteAppointmentTreatment(appointmentId, TreatmentId) {
    $.ajax({
        url: '../handler/TreatmentHandler.ashx',
        data: {
            'Action': 'Delete',
            'TreatmentID': TreatmentId,
            'AppointmentId': appointmentId,
        },
        type: 'POST',
        async: false,
        contentType: "application/x-www-form-urlencoded",
        responseType: "text",
        error: function () {
            toastr.error('Some error has occured. Please try again');
        },
        success: function (data) {
            _treatments = data.treatments;
            ShowAppointmentTreatmentMasterDetails(data.treatments, false);
            if (TreatmentId == 0)
                toastr.success('All treatments are deleted successfully');
            else
                toastr.success('Treatment is deleted successfully');
        }
    });
}

function GetAppointmentTreatments() {
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    $.ajax({
        url: '../handler/TreatmentHandler.ashx',
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
            _treatments = data.treatments;
            ShowAppointmentTreatmentMasterDetails(data.treatments, false);
        },
    });
}

function ShowAppointmentTreatmentMasterDetails(data, isReview) {
    var table = '';
    if (data != undefined && data.length > 0) {
        table = '<table class="table table-hover" cellspacing="0" id="gvTreatments" style="width:100%;border-collapse:collapse;">';
        table += '<tbody>';
        table += '<tr style="color:White;background-color:Black;">';
        table += '<th scope="col">Ser.#</th><th scope="col">Treatment Name</th><th scope="col">Treatment Cost</th><th scope="col"> Is Urgent ?</th><th scope="col">Status</th>';
        if (!isReview) {
            table += '<th scope="col" > Delete</th>';
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
                table += '<input type="button" id="btnDelete" value="Delete" class="btn btn-danger" onclick="return funDeleteTreatment(' + data[i].SNo + ',\'' + data[i].Name + '\');">';
                table += '</td>';
            }

            table += '</tr >';
        }
        table += '</tbody>';
        table += '</table>';

        if (isReview) {
            $('#dvReviewTreatmentInfo').hide();
            $('#spnReviewTotalTreatmentsCount').html(" - " + data.length);
        }
        else {
            $('#dvTreatmentInfo').hide();
            $('#spnTotalTreatmentsCount').html(" - " + data.length);
            $('#btnDeleteAllTreatments').show();
        }
    }
    else {
        if (isReview) {
            table = '<div class="alert alert-danger" role="alert" id="dvReviewTreatmentInfo">No record found</div>';
            $('#spnReviewTotalTreatmentsCount').html('');
        }
        else {
            table = '<div class="alert alert-danger" role="alert" id="dvTreatmentInfo">No record found</div>';
            $('#spnTotalTreatmentsCount').html('');
            $('#btnDeleteAllTreatments').hide();
        }
    }

    if (isReview) {
        $('#divReviewAppointmentTreatments').empty();
        $('#divReviewAppointmentTreatments').append(table);
    }
    else {
        $('#divAppointmentTreatments').empty();
        $('#divAppointmentTreatments').append(table);
    }
}


//////////////////////////////////////////////////////////////////////////////////////////////////
function ShowDiagnosisPopup() {
    $('#txtSearchDiagnosis').val('');

    AppendSearchDivHeader('divSearchDiagnosisResults', 'tbSearchDiagnosis', 'Diagnosis');
    AppendSearchDivHeader('divDiagnosisFavourites', 'tbFavDiagnosis', 'Diagnosis');
    AppendSearchDivHeader('divDiagnosisHistory', 'tbHistoryDiagnosis', 'Diagnosis');

    $("#dlgDiagnosis").modal("show");

    SearchDiagnosis('SearchDiagnosis');
}


function SearchDiagnosis(type) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchDiagnosis').val();
    $.ajax({
        url: '../handler/SearchDiagnosisHandler.ashx',
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
            PopulateDiagnosisTable(data.diagnosis, data.appointmentDiagnosis, 'tbSearchDiagnosis');
            PopulateFavouriteDiagnosisTable(data.favourites, data.appointmentDiagnosis, 'tbFavDiagnosis');
            PopulateDiagnosisHistoryTable(data.history, data.appointmentDiagnosis, 'tbHistoryDiagnosis');
        },
    });
}


function PopulateDiagnosisTable(data, alreadyAddedData, tableId) {
    $("[id^='trSearchDiagnosis_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trSearchDiagnosis_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:70%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';

        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentDiagnosis_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentDiagnosis_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentDiagnosis_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteDiagnosis_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteDiagnosis_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddDiagnosis(' + data[i].SNo + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateFavouriteDiagnosisTable(data, alreadyAddedData, tableId) {
    $("[id^='trFavouriteDiagnosis_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trFavouriteDiagnosis_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:70%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentDiagnosisFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentDiagnosisFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentDiagnosisFavourite_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteDiagnosis_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteDiagnosis_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddFavouriteDiagnosis(' + data[i].SNo + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateDiagnosisHistoryTable(data, alreadyAddedData, tableId) {
    $("[id^='trHistoryDiagnosis_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        table += '<tr id="trHistoryDiagnosis_' + data[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:70%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            if (data[i].IsUrgent == 1)
                table += '<input id="chkIsUrgentDiagnosisHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" checked="checked" disabled="true"> Is Urgent </input>';
            else
                table += '<input id="chkIsUrgentDiagnosisHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input"  disabled="true"> Is Urgent </input>';
        }
        else {
            table += '<input id="chkIsUrgentDiagnosisHistory_' + data[i].SNo + '" type="checkbox" class="form-check-input" > Is Urgent </input>';
        }
        table += '</td>';

        table += '<td style="width:10%;">';
        if (alreadyAddedData.indexOf(data[i].SNo) > -1) {
            var btnText = "Added";
            if (data[i].IsFav == 1)
                btnText = "Added From Favourite";
            else if (data[i].IsHistory == 1)
                btnText = "Added From History";
            table += '<input value="' + btnText + '" id="btnAddFavouriteDiagnosis_' + data[i].SNo + '" class="btn  btn-secondary" disabled="true">';
        }
        else {
            table += '<input value="Add" id="btnAddFavouriteDiagnosis_' + data[i].SNo + '" class="btn btn-secondary" onClick="return AddHistoryDiagnosis(' + data[i].SNo + ');">';
        }
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function AddDiagnosis(diagnosisId) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchDiagnosis').val();
    var isUrgent = $('#chkIsUrgentDiagnosis_' + diagnosisId).is(':checked');
    $.ajax({
        url: '../handler/SearchDiagnosisHandler.ashx',
        data: {
            'Action': 'AddDiagnosis',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'DiagnosisID': diagnosisId,
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
            PopulateDiagnosisTable(data.diagnosis, data.appointmentDiagnosis, 'tbSearchDiagnosis');
            PopulateFavouriteDiagnosisTable(data.favourites, data.appointmentDiagnosis, 'tbFavDiagnosis');
            PopulateDiagnosisHistoryTable(data.history, data.appointmentDiagnosis, 'tbHistoryDiagnosis');
            toastr.success('Diagnosis is added succesfully');
        }
    });
}

function AddFavouriteDiagnosis(diagnosisId) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchDiagnosis').val();
    var isUrgent = $('#chkIsUrgentDiagnosisFavourite_' + diagnosisId).is(':checked');
    $.ajax({
        url: '../handler/SearchDiagnosisHandler.ashx',
        data: {
            'Action': 'AddDiagnosis',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'DiagnosisID': diagnosisId,
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
            PopulateDiagnosisTable(data.diagnosis, data.appointmentDiagnosis, 'tbSearchDiagnosis');
            PopulateFavouriteDiagnosisTable(data.favourites, data.appointmentDiagnosis, 'tbFavDiagnosis');
            PopulateDiagnosisHistoryTable(data.history, data.appointmentDiagnosis, 'tbHistoryDiagnosis');
            toastr.success('Diagnosis from favourites is added succesfully');
        }
    });
}

function AddHistoryDiagnosis(diagnosisId) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchDiagnosis').val();
    var isUrgent = $('#chkIsUrgentDiagnosisHistory_' + diagnosisId).is(':checked');
    $.ajax({
        url: '../handler/SearchDiagnosisHandler.ashx',
        data: {
            'Action': 'AddDiagnosis',
            'SearchText': searchText,
            'DoctorID': doctorId,
            'DiagnosisID': diagnosisId,
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
            PopulateDiagnosisTable(data.diagnosis, data.appointmentDiagnosis, 'tbSearchDiagnosis');
            PopulateFavouriteDiagnosisTable(data.favourites, data.appointmentDiagnosis, 'tbFavDiagnosis');
            PopulateDiagnosisHistoryTable(data.history, data.appointmentDiagnosis, 'tbHistoryDiagnosis');
            toastr.success('Diagnosis from history is added succesfully');
        }
    });
}

function DeleteAppointmentDiagnosis(appointmentId, diagnosisId) {
    $.ajax({
        url: '../handler/DiagnosisHandler.ashx',
        data: {
            'Action': 'Delete',
            'DiagnosisID': diagnosisId,
            'AppointmentId': appointmentId,
        },
        type: 'POST',
        async: false,
        contentType: "application/x-www-form-urlencoded",
        responseType: "text",
        error: function () {
            toastr.error('Some error has occured. Please try again');
        },
        success: function (data) {
            _diagonosis = data.diagnosis;
            ShowAppointmentDiagnosisMasterDetails(data.diagnosis, false);
            if (diagnosisId == 0)
                toastr.success('All diagnosis are deleted successfully');
            else
                toastr.success('Diagnosis is deleted successfully');
        }
    });
}

function GetAppointmentDiagnosis() {
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    $.ajax({
        url: '../handler/DiagnosisHandler.ashx',
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
            _diagonosis = data.diagnosis;
            ShowAppointmentDiagnosisMasterDetails(data.diagnosis, false);
        },
    });
}

function ShowAppointmentDiagnosisMasterDetails(data, isReview) {
    var table = '';
    if (data != undefined && data.length > 0) {
        table = '<table class="table table-hover" cellspacing="0" id="gvDiagnosis" style="width:100%;border-collapse:collapse;">';
        table += '<tbody>';
        table += '<tr style="color:White;background-color:Black;">';
        table += '<th scope="col">Ser.#</th><th scope="col">Diagnosis Name</th><th scope="col"> Is Urgent ?</th><th scope="col">Status</th>';
        if (!isReview) {
            table += '<th scope="col" > Delete</th>';
        }
        table += '</tr >';
        for (var i = 0; i < data.length; i++) {
            table += '<tr>';

            table += '<td style="width:10%;">';
            table += (i + 1);
            table += '</td>';

            table += '<td style="width:55%;">';
            table += data[i].Name;
            table += '</td>';


            table += '<td style="width:10%;">';
            table += data[i].IsUrgent;
            table += '</td>';

            table += '<td style="width:15%;">';
            table += data[i].Status;
            table += '</td>';

            if (!isReview) {
                table += '<td style="width:10%;">';
                table += '<input type="button" id="btnDelete" value="Delete" class="btn btn-danger" onclick="return funDeleteDiagnosis(' + data[i].SNo + ',\'' + data[i].Name + '\');">';
                table += '</td>';
            }

            table += '</tr >';
        }
        table += '</tbody>';
        table += '</table>';

        if (isReview) {
            $('#dvReviewDiagnosisInfo').hide();
            $('#spnReviewTotalDiagnosisCount').html(" - " + data.length);
        }
        else {
            $('#dvDiagnosisInfo').hide();
            $('#spnTotalDiagnosisCount').html(" - " + data.length);
            $('#btnDeleteAllDiagnosis').show();
        }
    }
    else {
        if (isReview) {
            table = '<div class="alert alert-danger" role="alert" id="dvReviewDiagnosisInfo">No record found</div>';
            $('#spnReviewTotalDiagnosisCount').html('');
        }
        else {
            table = '<div class="alert alert-danger" role="alert" id="dvDiagnosisInfo">No record found</div>';
            $('#spnTotalDiagnosisCount').html('');
            $('#btnDeleteAllDiagnosis').hide();
        }
    }

    if (isReview) {
        $('#divReviewAppointmentDiagnosis').empty();
        $('#divReviewAppointmentDiagnosis').append(table);
    }
    else {
        $('#divAppointmentDiagnosis').empty();
        $('#divAppointmentDiagnosis').append(table);
    }
}

/////////////////////////////////////////////////////////////////////////////////////////

function ShowMedicationPopup() {
    $('#txtSearchMedication').val('');

    AppendSearchMedicationDivHeader('divSearchMedications', 'tbSearchMedications', 'Medication');
    AppendFavouriteMedicationDivHeader('divMedicationFavourites', 'tbFavMedications', 'Medication');
    AppendFavouriteMedicationDivHeader('divMedicationHistory', 'tbHistoryMedications', 'Medication');

    AppendMedicationDosageTypeHeader('divSearchMedicationDetails', 'tbSearchMedicationDetails');
    AppendMedicationHeader('divSearchMedicationDuration', 'tbSearchMedicationDuration', 'Duration');
    AppendMedicationHeader('divSearchMedicationToDO', 'tbSearchMedicationToDO', 'ToD');


    $("#dlgMedication").modal("show");

    SearchMedication('SearchMedication', '');

}

function AppendSearchMedicationDivHeader(divSearchResult, tbName, searchFor) {
    $('#' + divSearchResult).empty();
    var table = '<table class="table table-hover" cellspacing="0" id="' + tbName + '" style="width: 100 %; border - collapse: collapse;">';
    table += '<tbody>';
    table += '<tr style = "color:White;background-color:Black;" >';
    table += '<th scope="col"></th><th scope = "col" >SR.NO.</th > <th scope="col">' + searchFor + '</th>';
    table += '</tr>'
    table += "</tbody>";
    table += "</table>";
    $('#' + divSearchResult).append(table);
}

function AppendFavouriteMedicationDivHeader(divSearchResult, tbName, searchFor) {
    $('#' + divSearchResult).empty();
    var table = '<table class="table table-hover" cellspacing="0" id="' + tbName + '" style="width: 100 %; border - collapse: collapse;">';
    table += '<tbody>';
    table += '<tr style = "color:White;background-color:Black;" >';
    table += '<th scope="col"></th><th scope = "col" >SR.NO.</th > <th scope="col">' + searchFor + '</th>';
    table += '<th scope="col">Dosage</th><th scope = "col" >Type</th > <th scope="col">Duration</th><th scope="col">ToD</th>';
    table += '</tr>'
    table += "</tbody>";
    table += "</table>";
    $('#' + divSearchResult).append(table);
}

function AppendMedicationDosageTypeHeader(divSearchResult, tbName) {
    $('#' + divSearchResult).empty();
    var table = '<table class="table table-hover" cellspacing="0" id="' + tbName + '" style="width: 100 %; border - collapse: collapse;">';
    table += '<tbody>';
    table += '<tr style = "color:White;background-color:Black;" >';
    table += '<th scope="col"></th>';
    table += '<th scope="col">Dosage</th>';
    table += '<th scope="col">Type</th>';
    table += '</tr>'
    table += "</tbody>";
    table += "</table>";
    $('#' + divSearchResult).append(table);
}

function AppendMedicationHeader(divSearchResult, tbName, searchFor) {
    $('#' + divSearchResult).empty();
    var table = '<table class="table table-hover" cellspacing="0" id="' + tbName + '" style="width: 100 %; border - collapse: collapse;">';
    table += '<tbody>';
    table += '<tr style = "color:White;background-color:Black;" >';
    table += '<th scope="col"></th>';
    table += '<th scope="col">' + searchFor + '</th>';
    table += '</tr>'
    table += "</tbody>";
    table += "</table>";
    $('#' + divSearchResult).append(table);
}

function PopulateDurationsAndToDo(durations, todos) {
    $("[id^='trSearchDuration_']").remove();
    var table = '';
    for (var i = 0; i < durations.length; i++) {
        table += '<tr id="trSearchDuration_' + durations[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += '<input type="radio" name="rdgSearchDurations"  value="' + durations[i].SNo + '"  id="rdSearchDuration_' + durations[i].SNo + '"/>';
        table += '</td>';

        table += '<td style="width:90%;">';
        table += durations[i].Name;
        table += '</td>';

        table += '</tr>';
    }
    $('#tbSearchMedicationDuration').append(table);


    $("[id^='trSearchToDo_']").remove();
    table = '';
    for (var i = 0; i < todos.length; i++) {
        table += '<tr id="trSearchToDo_' + todos[i].SNo + '">';

        table += '<td style="width:10%;">';
        table += '<input type="radio" name="rdgSearchToDos"  value="' + todos[i].SNo + '"  id="rdSearchToDo_' + todos[i].SNo + '"/>';
        table += '</td>';

        table += '<td style="width:90%;">';
        table += todos[i].Name;
        table += '</td>';

        table += '</tr>';
    }
    $('#tbSearchMedicationToDO').append(table);
}

function SearchMedication(type, calledBy) {
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    var searchText = $('#txtSearchMedication').val();
    $.ajax({
        url: '../handler/SearchMedicationHandler.ashx',
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
            PopulateSearchMedicationTable(data.medications, data.appointmentMedications, 'tbSearchMedications');
            if (calledBy.trim() == '') {
                PopulateFavouriteMedicationTable(data.favourites, data.appointmentMedications, 'tbFavMedications');
                PopulateMedicationHistoryTable(data.history, data.appointmentMedications, 'tbHistoryMedications');
                PopulateDurationsAndToDo(data.durations, data.todos);
            }
        },
    });
}

function PopulateSearchMedicationTable(data, alreadyAddedData, tableId) {
    $("[id^='trSearchMedication_']").remove();
    $("[id^='trSearchMedicationDetails_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        var isAlreadyExists = alreadyAddedData.indexOf(data[i].SNo)
        if (isAlreadyExists > -1) {
            table += '<tr id="trSearchMedication_' + data[i].SNo + '" style="background-color:#00CC00">';
        }
        else {
            table += '<tr id="trSearchMedication_' + data[i].SNo + '">';
        }
        table += '<td style="width:5%;">';
        if (isAlreadyExists > -1) {
            table += '<input type="radio" name="rdgMedications" disabled="true" id="rdMedication_' + data[i].SNo + '"  value="' + data[i].SNo + '" />';
        }
        else {
            table += '<input type="radio" name="rdgMedications" id="rdMedication_' + data[i].SNo + '"  value="' + data[i].SNo + '"  onclick="return GetMedicationDetails(' + data[i].SNo + ',\'tbSearchMedicationDetails\',\'Search\')"/>';
        }
        table += '</td>';

        table += '<td style="width:5%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:90%;">';
        table += data[i].Name;
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateFavouriteMedicationTable(data, alreadyAddedData, tableId) {
    $("[id^='trFavouriteMedication_']").remove();
    $("[id^='trFavouriteMedicationDetails_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        var isAlreadyExists = alreadyAddedData.indexOf(data[i].SNo)
        if (isAlreadyExists > -1) {
            table += '<tr id="trFavouriteMedication_' + data[i].SNo + '" style="background-color:#00CC00">';
        }
        else {
            table += '<tr id="trFavouriteMedication_' + data[i].SNo + '">';
        }
        var ctlId = data[i].SNo + '_' + data[i].DosageID + '_' + data[i].MedicationTypeID + '_' + data[i].MedicationToDId + '_' + data[i].DurationID;
        table += '<td style="width:5%;">';
        if (isAlreadyExists > -1) {
            table += '<input type="radio" name="rdgFavouriteMedications" disabled="true" id="rdFavouriteMedication_' + ctlId + '"   value="' + ctlId + '" />';
        }
        else {
            table += '<input type="radio" name="rdgFavouriteMedications" id="rdFavouriteMedication_' + ctlId + '"   value="' + ctlId + '" />';
        }
        table += '</td>';

        table += '<td style="width:5%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:30%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Dosage;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].MedicationType;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Duration;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].MedicationTOD;
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function PopulateMedicationHistoryTable(data, alreadyAddedData, tableId) {
    $("[id^='trHistoryMedication_']").remove();
    $("[id^='trHistoryMedicationDetails_']").remove();
    var table = '';
    for (var i = 0; i < data.length; i++) {
        var isAlreadyExists = alreadyAddedData.indexOf(data[i].SNo)
        if (isAlreadyExists > -1) {
            table += '<tr id="trHistoryMedication_' + data[i].SNo + '" style="background-color:#00CC00">';
        }
        else {
            table += '<tr id="trHistoryMedication_' + data[i].SNo + '">';
        }
        var ctlId = data[i].SNo + '_' + data[i].DosageID + '_' + data[i].MedicationTypeID + '_' + data[i].MedicationToDId + '_' + data[i].DurationID;
        table += '<td style="width:5%;">';
        if (isAlreadyExists > -1) {
            table += '<input type="radio" name="rdgHistoryMedications" disabled="true" id="rdHistroyMedication_' + ctlId + '"   value="' + ctlId + '" />';
        }
        else {
            table += '<input type="radio" name="rdgHistoryMedications" id="rdHistroyMedication_' + ctlId + '" value="' + ctlId + '" />';
        }
        table += '</td>';

        table += '<td style="width:5%;">';
        table += (i + 1);
        table += '</td>';

        table += '<td style="width:30%;">';
        table += data[i].Name;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Dosage;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].MedicationType;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].Duration;
        table += '</td>';

        table += '<td style="width:10%;">';
        table += data[i].MedicationTOD;
        table += '</td>';

        table += '</tr>';
    }
    $('#' + tableId).append(table);
}

function GetMedicationDetails(medicationID, tableID, type) {
    $.ajax({
        url: '../handler/SearchMedicationHandler.ashx',
        data: {
            'Action': "MEDICATION_INFO",
            'MedicationID': medicationID
        },
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",
        responseType: "json",
        error: function () {
            alert('An error has occurred. Please try again.');
        },
        success: function (data) {
            PopulateMedicationDetails(data.medicationDetails, tableID, type);
        },
    });
}

function PopulateMedicationDetails(medicationDetails, tableID, type) {
    $("[id^='tr" + type + "MedicationDetails_']").remove();
    var table = '';
    for (var i = 0; i < medicationDetails.length; i++) {
        if (medicationDetails[i].DosageID > 0 || medicationDetails[i].DosageID > 0) {
            table += '<tr id="tr' + type + 'MedicationDetails_' + medicationDetails[i].DosageID + '_' + medicationDetails[i].MedicationTypeID + '">';

            table += '<td style="width:5%;">';
            table += '<input type="radio" name="rdg' + type + 'DosageTypes" id="rd' + type + 'DosageType_' + medicationDetails[i].DosageID + '_' + medicationDetails[i].MedicationTypeID + '" value="' + medicationDetails[i].DosageID + '_' + medicationDetails[i].MedicationTypeID + '"/>';
            table += '</td>';

            table += '<td style="width:45%;">';
            table += medicationDetails[i].Dosage;
            table += '</td>';

            table += '<td style="width:50%;">';
            table += medicationDetails[i].MedicationType;
            table += '</td>';

            table += '</tr>';
        }
        else {
            table = '<tr id="tr' + type + 'MedicationDetails_0"><td colspan="2">No record found</td></tr>';
        }
    }
    $('#' + tableID).append(table);
}

function DeleteAppointmentMedication(appointmentId, medicationMasterID) {
    $.ajax({
        url: '../handler/MedicationHandler.ashx',
        data: {
            'Action': 'Delete',
            'MedicationMasterID': medicationMasterID,
            'AppointmentId': appointmentId,
        },
        type: 'POST',
        async: false,
        contentType: "application/x-www-form-urlencoded",
        responseType: "text",
        error: function () {
        },
        success: function (data) {
            _medications = data.medications;
            ShowAppointmentMedicationMasterDetails(data.medications, false);
            if (medicationMasterID == 0)
                toastr.success('All medications are deleted successfully');
            else
                toastr.success('Medication is deleted successfully');
        }
    });
}


function GetAppointmentMedications() {
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    $.ajax({
        url: '../handler/MedicationHandler.ashx',
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
            _medications = data.medications;
            ShowAppointmentMedicationMasterDetails(data.medications, false);
        },
    });
}

function ShowAppointmentMedicationMasterDetails(data, isReview) {
    var table = '';
    if (data != undefined && data.length > 0) {
        table = '<table class="table table-hover" cellspacing="0" id="gvMedications" style="width:100%;border-collapse:collapse;">';
        table += '<tbody>';
        table += '<tr style="color:White;background-color:Black;">';
        table += '<th scope="col">Ser.#</th><th scope="col">Medication Name</th><th scope="col">Type</th><th scope="col">Dosage</th><th scope="col">Duration</th><th scope="col">ToD</th><th scope="col">Status</th>';
        if (!isReview) {
            table += '<th scope="col" > Delete</th>';
        }
        table += '</tr >';
        for (var i = 0; i < data.length; i++) {
            table += '<tr>';

            table += '<td style="width:10%;">';
            table += (i + 1);
            table += '</td>';

            table += '<td style="width:30%;">';
            table += data[i].MedicationName;
            table += '</td>';

            table += '<td style="width:10%;">';
            table += data[i].MedicationType;
            table += '</td>';

            table += '<td style="width:10%;">';
            table += data[i].MedicationDosage;
            table += '</td>';

            table += '<td style="width:10%;">';
            table += data[i].MedicationDuration;
            table += '</td>';

            table += '<td style="width:10%;">';
            table += data[i].ToD;
            table += '</td>';

            table += '<td style="width:10%;">';
            table += data[i].Status;
            table += '</td>';

            if (!isReview) {
                table += '<td style="width:10%;">';
                table += '<input type="button" id="btnDelete" value="Delete" class="btn btn-danger" onclick="return funDeleteMedication(' + data[i].MedicationMasterID + ',\'' + data[i].MedicationName + '\');">';
                table += '</td>';
            }

            table += '</tr >';
        }
        table += '</tbody>';
        table += '</table>';

        if (isReview) {
            $('#dvReviewMedicationInfo').hide();
            $('#spnReviewTotalMedicationsCount').html(" - " + data.length);
        }
        else {
            $('#dvMedicationInfo').hide();
            $('#spnTotalMedicationsCount').html(" - " + data.length);
            $('#btnDeleteALLMedications').show();
        }
    }
    else {
        if (isReview) {
            table = '<div class="alert alert-danger" role="alert" id="dvReviewMedicationInfo">No record found</div>';
            $('#spnReviewTotalMedicationsCount').html('');
        }
        else {
            table = '<div class="alert alert-danger" role="alert" id="dvMedicationInfo">No record found</div>';
            $('#spnTotalMedicationsCount').html('');
            $('#btnDeleteALLMedications').hide();
        }
    }

    if (isReview) {
        $('#divReviewAppointmentMedications').empty();
        $('#divReviewAppointmentMedications').append(table);
    }
    else {
        $('#divAppointmentMedications').empty();
        $('#divAppointmentMedications').append(table);
    }
}



function AddMedication() {
    var medicationID;
    var medicationTypeID;
    var medicationDosageID;
    var medicationDurationID;
    var medicationToDoID;
    var dosageType;
    var isFav = 'false';
    var isHistory = 'false'

    var selectedElement = $("#nav-tab4").find(".active");
    var tabName = selectedElement[0].innerText;
    var doctorId = $("input[id$='hdnDoctorID']").val();
    var appointmentId = $("input[id$='hdnAppointmentID']").val();
    if (tabName == 'Medication') {
        medicationID = $("input[name='rdgMedications']:checked").val();
        dosageType = $("input[name='rdgSearchDosageTypes']:checked").val();
        medicationDurationID = $("input[name='rdgSearchDurations']:checked").val();
        medicationToDoID = $("input[name='rdgSearchToDos']:checked").val();
        if (medicationID == undefined) {
            toastr.error('Medication is not selected');
            $('#divPopUpMessage').html('Medication is not selected');
            $('#dlgPopupDialog').modal('show');
            return false;
        }
        if (dosageType == undefined) {
            toastr.error('Dosage type is not selected');
            $('#divPopUpMessage').html('Dosage type is not selected');
            $('#dlgPopupDialog').modal('show');
            return false;
        }
        if (medicationDurationID == undefined) {
            toastr.error('Duration is not selected');
            $('#divPopUpMessage').html('Duration is not selected');
            $('#dlgPopupDialog').modal('show');
            return false;
        }
        if (medicationToDoID == undefined) {
            toastr.error('ToD is not selected');
            $('#divPopUpMessage').html('ToD is not selected');
            $('#dlgPopupDialog').modal('show');
            return false;
        }
        medicationDosageID = dosageType.split('_')[0];
        medicationTypeID = dosageType.split('_')[1];
    }
    else if (tabName == 'Favourite') {
        var medication = $("input[name='rdgFavouriteMedications']:checked").val();
        if (medication == undefined) {
            toastr.error('Medication is not selected');
            $('#divPopUpMessage').html('Medication is not selected');
            $('#dlgPopupDialog').modal('show');
            return false;
        }
        var values = medication.split('_');
        medicationID = values[0];
        medicationDosageID = values[1];
        medicationTypeID = values[2];
        medicationToDoID = values[3];
        medicationDurationID = values[4];
        isFav = 'true';
    }
    else if (tabName == 'History') {
        var medication = $("input[name='rdgHistoryMedications']:checked").val();
        if (medication == undefined) {
            $('#divPopUpMessage').html('Medication is not selected');
            $('#dlgPopupDialog').modal('show');
            return false;
        }
        var values = medication.split('_');
        medicationID = values[0];
        medicationDosageID = values[1];
        medicationTypeID = values[2];
        medicationToDoID = values[3];
        medicationDurationID = values[4];
        isHistory = 'true';
    }
    var result = true;
    $.ajax({
        url: '../handler/SearchMedicationHandler.ashx',
        data: {
            'Action': 'AddMedication',
            'AppointmentID': appointmentId,
            'DoctorID': doctorId,
            'MedicationID': medicationID,
            'MedicationTypeID': medicationTypeID,
            'MedicationDosageID': medicationDosageID,
            'MedicationDurationID': medicationDurationID,
            'MedicationToDoID': medicationToDoID,
            'IsFav': isFav,
            'IsHistory': isHistory
        },
        async: false,
        type: 'POST',
        contentType: "application/x-www-form-urlencoded",
        responseType: "json",
        error: function () {
            toastr.error('An error has occurred. Please try again.');
        },
        success: function (data) {
            if (data.status == 'SUCCESS') {
                toastr.success('Medication is added successfully');
                SearchMedication('SearchMedication', '');
            }
            else if (data.status == 'EXISTS') {
                toastr.error('Medication already exists in the appointment medications');
                $('#divPopUpMessage').html('Medication already exists in the appointment medications');
                $('#dlgPopupDialog').modal('show');
                result = false;
            }
            else if (data.status == 'ERROR') {
                toastr.error('Some error has occured. Please try again');
                $('#divPopUpMessage').html('Some error has occured. Please try again.');
                $('#dlgPopupDialog').modal('show');
                result = false;
            }
        }
    });
    return result;
}

function ValidateAppointmentDetails() {
    if ($('#gvExaminations').length == 0) {
        $('#divPopUpMessage').html('No examination record found.');
        $('#dlgPopupDialog').modal('show');
        return false;
    }
    if ($('#gvInvestigations').length == 0) {
        $('#divPopUpMessage').html('No investigation record found.');
        $('#dlgPopupDialog').modal('show');
        return false;
    }
    if ($('#gvTreatments').length == 0) {
        $('#divPopUpMessage').html('No treatment record found.');
        $('#dlgPopupDialog').modal('show');
        return false;
    }
    if ($('#gvDiagnosis').length == 0) {
        $('#divPopUpMessage').html('No diagnosis record found.');
        $('#dlgPopupDialog').modal('show');
        return false;
    }
    if ($('#gvMedications').length == 0) {
        $('#divPopUpMessage').html('No medication record found.');
        $('#dlgPopupDialog').modal('show');
        return false;
    }
    return true;
}

function FillReviewPopUpDetails() {
    $("[id='lblLeftEyeDistSPH']").html($("input[id$='txtLeftEyeDistSPH']").val());
    $("[id='lblLeftEyeDistCYL']").html($("input[id$='txtLeftEyeDistCYL']").val());
    $("[id='lblLeftEyeDistAXIS']").html($("input[id$='txtLeftEyeDistAXIS']").val());
    $("[id='lblLeftEyeDistVA']").html($("input[id$='txtLeftEyeDistVA']").val());
    $("[id='lblLeftEyeNearSPH']").html($("input[id$='txtLeftEyeNearSPH']").val());
    $("[id='lblLeftEyeNearCYL']").html($("input[id$='txtLeftEyeNearCYL']").val());
    $("[id='lblLeftEyeNearAXIS']").html($("input[id$='txtLeftEyeNearAXIS']").val());
    $("[id='lblLeftEyeNearVA']").html($("input[id$='txtLeftEyeNearVA']").val());
    $("[id='lblRightEyeDistSPH']").html($("input[id$='txtRightEyeDistSPH']").val());
    $("[id='lblRightEyeDistCYL']").html($("input[id$='txtRightEyeDistCYL']").val());
    $("[id='lblRightEyeDistAXIS']").html($("input[id$='txtRightEyeDistAXIS']").val());
    $("[id='lblRightEyeDistVA']").html($("input[id$='txtRightEyeDistVA']").val());
    $("[id='lblRightEyeNearSPH']").html($("input[id$='txtRightEyeNearSPH']").val());
    $("[id='lblRightEyeNearCYL']").html($("input[id$='txtRightEyeNearCYL']").val());
    $("[id='lblRightEyeNearAXIS']").html($("input[id$='txtRightEyeNearAXIS']").val());
    $("[id='lblRightEyeNearVA']").html($("input[id$='txtRightEyeNearVA']").val());

    $("[id='chkReviewARC']").prop('checked', $("[id$='chkARC']").is(':checked'));
    $("[id='chkReviewBifocal']").prop('checked', $("[id$='chkBifocal']").is(':checked'));
    $("[id='chkReviewConstant']").prop('checked', $("[id$='chkConstant']").is(':checked'));
    $("[id='chkReviewNear']").prop('checked', $("[id$='chkNear']").is(':checked'));
    $("[id='chkReviewPhotoSun']").prop('checked', $("[id$='chkPhotoSun']").is(':checked'));
    $("[id='chkReviewProgressive']").prop('checked', $("[id$='chkProgressive']").is(':checked'));

    $("[id='lblIPD']").html($("input[id$='txtIPD']").val());
    $('#txtReviewFinalOutcome').text($("[id$='txtFinalOutcome']").val());

    ShowAppointmentExaminationMasterDetails(_examinations, true);
    ShowAppointmentInvestigationMasterDetails(_investigations, true);
    ShowAppointmentDiagnosisMasterDetails(_diagonosis, true);
    ShowAppointmentTreatmentMasterDetails(_treatments, true);
    ShowAppointmentMedicationMasterDetails(_medications, true);
}