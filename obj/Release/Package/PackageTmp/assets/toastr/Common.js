function PageFunctions() {
    $(".decimalOnly").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which == 46) {
            return true;
        }
        else if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            $("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
}


function ShowSuccessMessage(message)
{
    toastr.options = {
        "closeButton": true,
        "debug": true,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "3000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
};

    toastr.success(message);
}

function ShowErrorMessage(message) {

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "3000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr.error(message);
}

function AjaxMethod( strUrl , strType , jsonData)
{
    var flag = false;

    try {
        $.blockUI({
            message: '<h4 style="font-size:12px"><img src="../../Images/busy.gif" /> Please Wait..</h4>',
            css: {
                border: '1px solid silver',
                backgroundColor: 'transparent'
            }
        });

        $.ajax({
            url: strUrl,
            async: false,
            type: strType,
            data: jsonData,
            success: function (data) {
                alert("success");
                return true;
            },
            error: function (jqXHR, status, err) {
                alert("error");
                return false;
            },
            complete: function () {
                alert("complete");
                $.unblockUI();
            }
        });
    }
    catch (e) { alert("ajaxerror" + e);}
}

