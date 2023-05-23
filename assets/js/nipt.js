
function CreateFormObject() {

    try {
        var jsonArray = [];

        jsonArray.push(CreateObject("ActiveBarcode", "txtBarCode", "TextBox", 100, "String", true, true));
        jsonArray.push(CreateObject("NationalId", "txtNationalID", "TextBox", 8, "Numeric", true, true));
        jsonArray.push(CreateObject("PatientMRN", "txtPatientMRN", "TextBox", 8, "Numeric", false, true));

        jsonArray.push(CreateObject("BarcodeDrop", "BarcodeDrop", "DropDownList", 100, "String", false, true));

        jsonArray.push(CreateObject("PatientSerNo", "hfPatientMasterId", "HiddenField", 100, "String", true, true));
        jsonArray.push(CreateObject("FirstName", "txtFirstName", "TextBox", 20, "String", true, true));
        jsonArray.push(CreateObject("MiddleName", "txtMiddleName", "TextBox", 20, "String", false, true));
        jsonArray.push(CreateObject("LastName", "txtLastName", "TextBox", 20, "String", true, true));
        jsonArray.push(CreateObject("DOB", "DobTxt", "TextBox", 20, "String", true, true));
        jsonArray.push(CreateObject("Mobile", "txtMobile", "TextBox", 20, "String", true, true));
        jsonArray.push(CreateObject("Email", "txtEmail", "TextBox", 20, "String", false, false));
        jsonArray.push(CreateObject("City", "CityDrop", "DropDownList", 8, "Numeric", false, true));
        jsonArray.push(CreateObject("Address", "txtAddress", "TextBox", 20, "String", false, true));
        jsonArray.push(CreateObject("Ethnic", "EthnicDrop", "DropDownList", 8, "Numeric", false, true));
        jsonArray.push(CreateObject("LastMenstrulPeriodDate", "MenstrualPeriodTxt", "TextBox", 10, "String", true, true));
        jsonArray.push(CreateObject("AgeOfGestation", "txtAgeOfGestation", "DropDownList", 100, "Numeric", true, true));
        jsonArray.push(CreateObject("PregnancyType", "PregnancyTypeDrop", "DropDownList", 8, "Numeric", true, true));
        jsonArray.push(CreateObject("MaternalWeight", "txtMaternalWeight", "TextBox", 8, "Numeric", true, true));
        jsonArray.push(CreateObject("MaternalHeight", "txtMaternalheight", "TextBox", 8, "Numeric", true, true));
        jsonArray.push(CreateObject("MarriagConsanguineous", "MarriageConsanguineousDrop", "DropDownList", 8, "Numeric", false, true));
        jsonArray.push(CreateObject("ModeConeption", "ModeConceptionDrop", "DropDownList", 8, "Numeric", true, true));
        jsonArray.push(CreateObject("HistoryGeneticTesting", "HistoryGeneticTestingDrop", "DropDownList", 8, "Numeric", false, true));
        jsonArray.push(CreateObject("HistoryofAbortion", "HistoryofAbortionDrop", "DropDownList", 8, "Numeric", true, true));
        jsonArray.push(CreateObject("Others", "txtOthers", "TextBox", 100, "String", false, true));
        jsonArray.push(CreateObject("LatestUtrasound", "LatestUltrasoundTxt", "TextBox", 10, "String", true, true));
        jsonArray.push(CreateObject("SampleCollectionDT", "SampleCollectionTxt", "TextBox", 100, "String", true, true));
        jsonArray.push(CreateObject("UltrasoundFindings", "txtUltrasoundFindings", "TextBox", 500, "String", false, true));
        jsonArray.push(CreateObject("FurtherClinicalDetails", "txtFurtherClinicalDetails", "TextBox", 500, "String", false, true));

        jsonArray.push(CreateObject("RequestorID", "hfUserId", "HiddenField", 100, "String", true, true));

        jsonArray.push(CreateObject("RequestorName", "txtRequestorName", "TextBox", 100, "String", true, true));
        jsonArray.push(CreateObject("RequestorMobile", "txtRequestorMobile", "TextBox", 100, "String", true, true));
        jsonArray.push(CreateObject("RequstorEmail", "txtRequstorEmail", "TextBox", 100, "String", true, true));
        jsonArray.push(CreateObject("FlxCheck", "flexCheckChecked", "CheckBox", 4, "String", true, true));
        jsonArray.push(CreateObject("ConcentForm", "dZUpload", "DIV", 100, "String", true, true));
        jsonArray.push(CreateObject("RecordID", "hfRecordID", "HiddenField", 100, "String", true, true));
        jsonArray.push(CreateObject("NIPTSerNo", "hfNIPTSerNo", "HiddenField", 100, "String", true, true));

        var jsonData = JSON.stringify(jsonArray);
        return jsonData;

    }
    catch (e) { }
}
  
  

    function CreateObject(fieldName, fieldControlId, fieldControlType, maxLength, inputType, isMandatory, isValidField) {
        var jsonObj = {};
        jsonObj.FieldName = fieldName;
        jsonObj.FieldControlId = fieldControlId;
        jsonObj.FieldControlType = fieldControlType; // TextBox/ DropDownList
        jsonObj.MaxLength = maxLength;
        jsonObj.InputType = inputType;
        jsonObj.IsMandatory = isMandatory;
        jsonObj.IsValidField = isValidField; // if error occurred related to field then make it false else true, by default true.
        return jsonObj;
    }

    function ValidateForm() {
        var errorMessage = "Enter / Select Mandatory Fields.";
        var errorCount = 0;
        try {
            var data = $.parseJSON(CreateFormObject());
            $.each(data, function (i, item) {
                $.each(item, function (key, value) {
                    if (key == "IsMandatory" && value == true) {
                        var formControlId = "#" + "ContentPlaceHolder1_" + (item["FieldControlId"]);
                        var formControlType = item["FieldControlType"];


                        if (formControlType == "DIV") {
                            if ($(formControlId).html() == "") {
                                $(formControlId).html("Upload atlest one file");
                                $(formControlId).addClass("mandatory");
                                errorCount = errorCount + 1;
                                item["IsValidField"] = false;
                            }
                            else {
                                $(formControlId).removeClass("mandatory");
                                item["IsValidField"] = false;
                            }

                        }
                        else if (formControlType == "TextBox") {
                            if ($(formControlId).val() == "") {
                                $(formControlId).addClass("mandatory");
                                errorCount = errorCount + 1;
                                item["IsValidField"] = false;
                            }
                            else {
                                $(formControlId).removeClass("mandatory");
                                item["IsValidField"] = false;
                            }
                        }
                        else if (formControlType == "DropDownList") {

                            if ($(formControlId).val() == "-1") {
                                $(formControlId).parent("div").addClass("mandatory");
                                errorCount = errorCount + 1;
                                item["IsValidField"] = false;
                            } else {
                                $(formControlId).parent("div").removeClass("mandatory");
                                item["IsValidField"] = false;
                            }
                        }
                        //else if (formControlType == "Label") {
                        //    $(formControlId).addClass("mandatory");
                        //    errorCount = errorCount + 1;
                        //}
                        else if (formControlType == "CheckBox") {
                            if ($(formControlId).prop("checked") == false) {
                                $(formControlId).addClass("mandatory");
                                errorCount = errorCount + 1;
                                item["IsValidField"] = false;
                            }
                            else {
                                $(formControlId).removeClass("mandatory");
                                item["IsValidField"] = false;
                            }
                        }
                    }
                });
            });

            //email validation
            errorCount = ValidateEmailId("#ContentPlaceHolder1_txtEmail", errorCount);
            errorCount = ValidateEmailId("#ContentPlaceHolder1_txtRequstorEmail", errorCount);

            //
            if ($("#ContentPlaceHolder1_HistoryGeneticTestingDrop").val() == "5") {
                if ($("#ContentPlaceHolder1_txtOthers").val() == "") {
                    $("#ContentPlaceHolder1_txtOthers").addClass("mandatory");
                    errorCount = errorCount + 1;
                    $("#spanOthersMandatory").show();
                }
                else {
                    $("#ContentPlaceHolder1_txtOthers").removeClass("mandatory");
                    $("#spanOthersMandatory").hide();
                }
            }

            //fileupload validation
            if ($("#hfFileCount").val() == 0) {
                $("#ContentPlaceHolder1_lblConsent").css("color", "red");
                errorCount = errorCount + 1;
            }
            else {
                $("#ContentPlaceHolder1_lblConsent").css("color", "black");
            }

            //focus on invalid field

            $(".mandatory").each(function (o) {
                if (o == 0) {
                    // $(this).focus();

                    $('html, body').animate({
                        scrollTop: $(this).offset().top - 100
                    }, 500);
                }
            });

        }
        catch (e) { }
        return errorCount;
    }

    function ValidateEmailId(fieldControlID, errorCount) {

        var emailId = $(fieldControlID).val();

        if (emailId != "") {
            if (IsEmail(emailId) == false) {
                $(fieldControlID).addClass("mandatory");
                if (errorCount == 0) {
                    $(fieldControlID).focus();
                }
                $(fieldControlID).attr('title', 'Invalid Email Id');

                errorCount = errorCount + 1;
            }
            else {
                $(fieldControlID).removeClass("mandatory");
                $(fieldControlID).attr('title', '');
            }
        }

        return errorCount;
    }

    function GetFormValues() {
        var data = $.parseJSON(CreateFormObject());

        var jsonArray = [];

        var jsonObj = {};
        $.each(data, function (i, item) {
            var fieldName = item["FieldName"];
            var formControlId = "#" + "ContentPlaceHolder1_" + (item["FieldControlId"]);
            var formControlType = item["FieldControlType"];
            var inputType = item["InputType"];
            var isMandatory = item["IsMandatory"];

            var formControlValue = "";
            if (formControlType == "CheckBox") {
                formControlValue = $(formControlId).prop("checked");
            }
            else {
                formControlValue = $(formControlId).val();
                if (inputType == "Numeric" && isMandatory == false && formControlValue == "") {
                    formControlValue = 0;
                }
                else if (inputType == "Numeric" && isMandatory == false && formControlValue == "") {
                    formControlValue = "";
                }
            }
            jsonObj[fieldName] = formControlValue;

        });


        var id = 0;

        if (id > 0) { // //fileupload array to delete the files on edit mode
            var fileUploadArray = [];

            $("#ulFileUploads").find("div").each(function () {
                if (($(this).is(":visible")) == false) {
                    var fileUploadObj = {};
                    fileUploadObj["FileName"] = $(this).find("a").html();
                    fileUploadObj["FilePathName"] = $(this).find("input[type=hidden]").val();
                    fileUploadArray.push(fileUploadObj);
                }
            });
            jsonObj["FileUploadList"] = fileUploadArray;
        }

        jsonArray.push(jsonObj);
        var jsonData = JSON.stringify(jsonArray);
        return jsonData;
    }

  


    function GetPatientMasterRow(obj) {
        var tr = $(obj).closest("tr");
        var patientSrNo = ($(tr).find("td").eq(1).text());
        var nationalID = ($(tr).find("td").eq(2).text());
        var patientMRN = ($(tr).find("td").eq(3).text());
        var firstName = ($(tr).find("td").eq(4).text());
        var middleName = ($(tr).find("td").eq(5).text());
        var lastName = ($(tr).find("td").eq(6).text());
        var DOB = ($(tr).find("td").eq(7).text());
        var mobile = ($(tr).find("td").eq(8).text());
        var email = ($(tr).find("td").eq(9).text());
        var city = ($(tr).find("td").eq(10).text());
        var ethnic = ($(tr).find("td").eq(11).text());
        var address = ($(tr).find("td").eq(12).text());


        $("#ContentPlaceHolder1_hfPatientMasterId").val(patientSrNo);
        $("#ContentPlaceHolder1_txtNationalID").val(nationalID);
        $("#ContentPlaceHolder1_txtPatientMRN").val(patientMRN);
        $("#ContentPlaceHolder1_txtFirstName").val(firstName);
        $("#ContentPlaceHolder1_txtMiddleName").val(middleName);
        $("#ContentPlaceHolder1_txtLastName").val(lastName);
        $("#ContentPlaceHolder1_DobTxt").val(DOB);
        $("#ContentPlaceHolder1_txtMobile").val(mobile);
        $("#ContentPlaceHolder1_txtEmail").val(email);
        $("#ContentPlaceHolder1_CityDrop").find("option[value=" + city + "]").prop("selected", true).trigger("change");
        $("#ContentPlaceHolder1_txtAddress").val(address);
        $("#ContentPlaceHolder1_EthnicDrop").find("option[value=" + ethnic + "]").prop("selected", true).trigger("change");

        EnableDisablePatientMasterData("Enable");

        $(".btn-close").click();
    }

    function ClearPatientData() {
        $("#ContentPlaceHolder1_hfPatientMasterId").val("0");
        $("#ContentPlaceHolder1_txtNationalID").val("");
        $("#ContentPlaceHolder1_txtPatientMRN").val("");
        $("#ContentPlaceHolder1_txtFirstName").val("");
        $("#ContentPlaceHolder1_txtMiddleName").val("");
        $("#ContentPlaceHolder1_txtLastName").val("");
        $("#ContentPlaceHolder1_DobTxt").val("");
        $("#ContentPlaceHolder1_txtMobile").val("");
        $("#ContentPlaceHolder1_txtEmail").val("");
        $("#ContentPlaceHolder1_txtAddress").val("");

        $("#ContentPlaceHolder1_CityDrop").find("option[value=-1]").prop("selected", true).trigger("change");
        $("#ContentPlaceHolder1_EthnicDrop").find("option[value=-1]").prop("selected", true).trigger("change");

        EnableDisablePatientMasterData("Enable");

    }

    function EnableDisablePatientMasterData(enableDisable) {

        if (enableDisable == "Disable") {

        }
        else if (enableDisable == "Enable") {
            enableDisable = "";
        }

        $("#ContentPlaceHolder1_txtNationalID").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_txtPatientMRN").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_txtFirstName").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_txtMiddleName").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_txtLastName").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_DobTxt").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_txtMobile").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_txtEmail").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_CityDrop").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_txtAddress").prop("disabled", enableDisable);
        $("#ContentPlaceHolder1_EthnicDrop").prop("disabled", enableDisable);
    }

    $("#btnClearPatientData").click(function () {
        ClearPatientData();
    });

    //#endregion

    function SetFormValues(formObjectData, strFieldName, strFieldValue) {

        $.each(formObjectData, function (i, item) {
            $.each(item, function (key, value) {

                var fieldName = item["FieldName"];
                var formControlId = "#" + "ContentPlaceHolder1_" + (item["FieldControlId"]);
                var formControlType = item["FieldControlType"];

                if (fieldName == strFieldName && formControlType == "TextBox") {
                    $(formControlId).val(strFieldValue);
                }
                else if (fieldName == strFieldName && formControlType == "HiddenField") {
                    $(formControlId).val(strFieldValue);
                }
                else if (fieldName == strFieldName && formControlType == "DropDownList") {
                    $(formControlId).find("option[value=" + strFieldValue + "]").prop("selected", true).trigger("change");
                }
            });
        });
    }

   

    function ShowDocument(obj) {

        var href = $(obj).attr('href');
        window.open(href);
        return false;
    }

    function RemoveFile(obj) {

        $(obj).closest("div").hide();
        var fileCount = $("#hfFileCount").val();
        fileCount = fileCount - 1;
        $("#hfFileCount").val(fileCount);

    }

    function DeleteUploadedFile(objFileName) {

        var FileUpload = {};
        FileUpload.FileName = objFileName;

        $.ajax({
            url: '../main/handler/DeleteFileHandler.ashx',
            type: 'POST',
            data: JSON.stringify(FileUpload),
            success: function (data) {

            },
            error: function (errorText) {
                alert("Error Occurred");
            }
        });

    }


    //#region "Document.Ready"
    $(document).ready(function () {
        $("#ContentPlaceHolder1_HistoryGeneticTestingDrop").change(function () {
            if ($(this).val() == 5) {
                $("#ContentPlaceHolder1_txtOthers").prop("disabled", "");
                $("#spanOthersMandatory").show();
            }
            else {
                $("#ContentPlaceHolder1_txtOthers").val("");
                $("#ContentPlaceHolder1_txtOthers").prop("disabled", "disabled");
                $("#spanOthersMandatory").hide();
            }
        });

        var id = 0;

     
        if (id == 0) {
            //Add Page

            //#region "Disable Form Fields on Page Load"
            $("#ContentPlaceHolder1_btnSubmit").prop("disabled", "disabled");
            EnableDisablePatientMasterData("Disable");
            $("#ContentPlaceHolder1_txtNationalID").prop("disabled", "");
            $("#ContentPlaceHolder1_txtRequestorName").prop("disabled", "disabled");
            $("#ContentPlaceHolder1_txtRequestorMobile").prop("disabled", "disabled");
            $("#ContentPlaceHolder1_txtRequstorEmail").prop("disabled", "disabled");
            $("#ContentPlaceHolder1_txtOthers").prop("disabled", "disabled");

            $("#ContentPlaceHolder1_hfPatientMasterId").val("0");
            $("#ContentPlaceHolder1_hfNIPTSerNo").val("0");
            //#endregion
        }
        else {
            // Edit Page
        
        }

        //#region "Get Modal Data's Patient master and user master"
        //GetPatientMasterData();
        //GetUserData();
        //#endregion

        /* $(document).ajaxStop(function () {*/
        // 0 === $.active
      
        //#region "DropZone FileUpload"

        var CountFiles = 0;

        $("#hfFileCount").val(CountFiles);
        //Simple Dropzonejs 
        Dropzone.autoDiscover = false;
        var mydropZone = $("#drop").dropzone({
            url: "../handler/NIPT/FileUploadHandler.ashx",
            maxFilesize: 10,
            init: function () {
                this.on("addedfile", function (file) {
                    CountFiles = $("#hfFileCount").val();
                    CountFiles++;
                    $("#hfFileCount").val(CountFiles);

                }),
                    this.on("removedfile", function (file) {
                        DeleteUploadedFile(file.name);
                        CountFiles = $("#hfFileCount").val();
                        if (CountFiles > 0) {
                            CountFiles--;
                        }
                        $("#hfFileCount").val(CountFiles);

                    })
                    ,
                    this.on("error", function (file, message) {
                        $("#dynamicModalHeader").text("Error");
                        $('#dynamicModal').modal('show');
                        $("#dynamicModalBody").text(file.name + " " + message);
                        this.removeFile(file);
                    });
            },
            addRemoveLinks: true,
            success: function (file, response) {
                var imgName = response;
                file.previewElement.classList.add("dz-success");
                console.log("Successfully uploaded : " + imgName);
            },
            error: function (file, response) {
                file.previewElement.classList.add("dz-error");
            }
        });
        /* });*/
        //#endregion


        $("#ContentPlaceHolder1_flexCheckChecked").change(function () {
            if ($(this).prop("checked") == true) {
                $("#ContentPlaceHolder1_btnSubmit").prop("disabled", "");
            }
            else {
                $("#ContentPlaceHolder1_btnSubmit").prop("disabled", "disabled");
            }
        });

       


        $("#ContentPlaceHolder1_btnSubmit").on('click', function (ev) {

            if (ValidateForm() > 0) {
                // alert("Enter / Select Mandatory Fields.");
            }
            else {
                ///open confirmation model
                $("#bd-example-modal-xl2").modal('show');
            }
            ev.preventDefault();
        })




       

        $('.numberonly').keypress(function (e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (String.fromCharCode(charCode).match(/[^0-9]/))
                return false;
        });

        $('.restrictinput').keypress(function (e) {
            return false;
        });

    });

    //#endregion

function OnLoad() {
    $("#ContentPlaceHolder1_HospitalDrop,#ContentPlaceHolder1_BarcodeDrop,#ContentPlaceHolder1_CityDrop,#ContentPlaceHolder1_ActiveBarcodeDrop,#ContentPlaceHolder1_MarriageConsanguineousDrop,#ContentPlaceHolder1_PregnancyTypeDrop,#ContentPlaceHolder1_HistoryofAbortionDrop,#ContentPlaceHolder1_ModeConceptionDrop,#ContentPlaceHolder1_HistoryGeneticTestingDrop,#ContentPlaceHolder1_EthnicDrop,#ContentPlaceHolder1_SingletonTwinsDrop,#ContentPlaceHolder1_FamilyMemberDrop,#ContentPlaceHolder1_RelationDrop,#ContentPlaceHolder1_GenderDrop,#ContentPlaceHolder1_txtAgeOfGestation").select2({ placeholder: '--' });
    $('#ContentPlaceHolder1_DobTxt,#ContentPlaceHolder1_LatestUltrasoundTxt,#ContentPlaceHolder1_MenstrualPeriodTxt').datepicker({ dateFormat: 'yyyy', format: "dd/mm/yyyy", todayHighlight: true, autoclose: true, orientation: 'bottom', endDate: "today" });
    $('.datetimepicker').datetimepicker({ format: 'dd/mm/yyyy HH:ii p', weekStart: 7, todayBtn: 1, autoclose: 1, todayHighlight: 1, startView: 2, forceParse: 0, showMeridian: 1, minuteStep: 1, startDate: "2021-01-1 06:00", endDate: new Date() });

    $("#ContentPlaceHolder1_HistoryGeneticTestingDrop").change(function () {
        if ($(this).val() == 5) {
            $("#ContentPlaceHolder1_txtOthers").prop("disabled", "");
            $("#spanOthersMandatory").show();
        }
        else {
            $("#ContentPlaceHolder1_txtOthers").val("");
            $("#ContentPlaceHolder1_txtOthers").prop("disabled", "disabled");
            $("#spanOthersMandatory").hide();
        }
    });

    var id = 0;


    if (id == 0) {
        //Add Page

        //#region "Disable Form Fields on Page Load"
        $("#ContentPlaceHolder1_btnSubmit").prop("disabled", "disabled");
        EnableDisablePatientMasterData("Disable");
        $("#ContentPlaceHolder1_txtNationalID").prop("disabled", "");
        $("#ContentPlaceHolder1_txtRequestorName").prop("disabled", "disabled");
        $("#ContentPlaceHolder1_txtRequestorMobile").prop("disabled", "disabled");
        $("#ContentPlaceHolder1_txtRequstorEmail").prop("disabled", "disabled");
        $("#ContentPlaceHolder1_txtOthers").prop("disabled", "disabled");

        $("#ContentPlaceHolder1_hfPatientMasterId").val("0");
        $("#ContentPlaceHolder1_hfNIPTSerNo").val("0");
        //#endregion
    }
    else {
        // Edit Page

    }

    //#region "Get Modal Data's Patient master and user master"
    //GetPatientMasterData();
    //GetUserData();
    //#endregion

    /* $(document).ajaxStop(function () {*/
    // 0 === $.active

    //#region "DropZone FileUpload"

    var CountFiles = 0;

    $("#hfFileCount").val(CountFiles);
    //Simple Dropzonejs 
    Dropzone.autoDiscover = false;
    var mydropZone = $("#drop").dropzone({
        url: "../handler/NIPT/FileUploadHandler.ashx",
        maxFilesize: 10,
        init: function () {
            this.on("addedfile", function (file) {
                CountFiles = $("#hfFileCount").val();
                CountFiles++;
                $("#hfFileCount").val(CountFiles);

            }),
                this.on("removedfile", function (file) {
                    DeleteUploadedFile(file.name);
                    CountFiles = $("#hfFileCount").val();
                    if (CountFiles > 0) {
                        CountFiles--;
                    }
                    $("#hfFileCount").val(CountFiles);

                })
                ,
                this.on("error", function (file, message) {
                    $("#dynamicModalHeader").text("Error");
                    $('#dynamicModal').modal('show');
                    $("#dynamicModalBody").text(file.name + " " + message);
                    this.removeFile(file);
                });
        },
        addRemoveLinks: true,
        success: function (file, response) {
            var imgName = response;
            file.previewElement.classList.add("dz-success");
            console.log("Successfully uploaded : " + imgName);
        },
        error: function (file, response) {
            file.previewElement.classList.add("dz-error");
        }
    });
    /* });*/
    //#endregion


    $("#ContentPlaceHolder1_flexCheckChecked").change(function () {
        if ($(this).prop("checked") == true) {
            $("#ContentPlaceHolder1_btnSubmit").prop("disabled", "");
        }
        else {
            $("#ContentPlaceHolder1_btnSubmit").prop("disabled", "disabled");
        }
    });




    $("#ContentPlaceHolder1_btnSubmit").on('click', function (ev) {

        if (ValidateForm() > 0) {
            // alert("Enter / Select Mandatory Fields.");
        }
        else {
            ///open confirmation model
            $("#bd-example-modal-xl2").modal('show');
        }
        ev.preventDefault();
    })






    $('.numberonly').keypress(function (e) {
        var charCode = (e.which) ? e.which : event.keyCode
        if (String.fromCharCode(charCode).match(/[^0-9]/))
            return false;
    });

    $('.restrictinput').keypress(function (e) {
        return false;
    });

}






