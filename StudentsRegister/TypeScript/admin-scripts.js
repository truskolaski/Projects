var AdminScripts = (function () {
    function AdminScripts() {
        $("input").on("input", function () {
            adminScripts.setAddUserErrorAndSuccessMessagesInvisible();
        });
    }
    AdminScripts.prototype.setAddUserErrorAndSuccessMessagesInvisible = function () {
        $('#register-error-admin-div').css('display', 'none');
        $('#register-success-admin-div').css('display', 'none');
    };
    AdminScripts.prototype.setAddUserErrorMessageVisible = function () {
        $('#register-error-admin-div').css('display', 'block');
        $('#register-success-admin-div').css('display', 'none');
    };
    AdminScripts.prototype.setAddUserSuccessMessageVisible = function () {
        $('#register-error-admin-div').css('display', 'none');
        $('#register-success-admin-div').css('display', 'block');
    };
    return AdminScripts;
})();
var adminScripts = new AdminScripts();
//# sourceMappingURL=admin-scripts.js.map