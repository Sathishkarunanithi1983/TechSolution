(function () {
    GetGroup();
    $('#btnUserAdd').on('click', AddUser);
    $('#myForm').validate({  // initialize the plugin
        // options 
    });
    //$.validator.addMethod('numeric', function (value, element, param) {
    //    return value == '' || value.match(/^\d+$/);
    //}, 'Enter a valid numeric value.');
 ///   $('#txtSetValue').rules("add", { required: true, numeric: true });
})();

function ValidateUserForm() {
    var success = true;
    var validator = $('#myForm').validate();
    var validationMessage = '';
    if (!validator.element('#txtUserName')) {
        validationMessage = validationMessage + "Enter User Name. " + " \n";
        success = false;
    }
    if (!validator.element('#selectGroupName')) {
        validationMessage = validationMessage + "Select group . " + " \n";
        success = false;
    }
    //if (!validator.element('#selectShiftTime')) {
    //    validationMessage = "Select Shift." + " \n";
    //    success = false;
    //}
    if (!validator.element('#selectAction')) {
        validationMessage = "Select Action." + " \n";
        success = false;
    }
    if (validationMessage != '') {
        return false;
    }
    return success;
}

function AddUser() {
    var success = false;
    success = ValidateUserForm();
    if (success) {
        var UserName = $('#txtUserName').val();
        var GroupId = $('#selectGroupName').val();
        var Action = parseInt($('#selectAction').val()) ? false : true;
        $.ajax({
            type: "POST",
            data: {
                "UserName": UserName,
                "GroupId": GroupId,
                "Action": Action
            },
            dataType: "json",
            url: '/api/Values/AddUser',
            async: false,
            success: function (data) {
                if (data.success) {
                    customAlert(true, data.message, '/Home/Index/');
                } else {
                    customAlert(false, data.message, '');
                }
            }
        });
    }
}

function GetGroup() {
    $.ajax
        (
            {
                type: "GET",
                //url: baseURL + '/api/Home/GetActiveShiftTime',
                url: "/api/Values/GetGroup",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.success) {
                        $(data.result).each
                            (
                                function () {
                                    $('#selectGroupName').append("<option value='" + this.GroupId + "' style='position: relative'>" + this.GroupName + "</option>");
                                }
                            );
                    }
                },
                error: function (request, error) {
                    console.log(arguments);
                }
            }
        );
}