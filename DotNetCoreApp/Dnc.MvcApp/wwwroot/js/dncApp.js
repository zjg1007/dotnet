// 使用ajax方式提交表单
function postLogonForm() {
    var logonFormOptions = {
        dataType: 'json',
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                //$('#logonModal').modal('hide');
                document.getElementById("gotoSystem").style.display = "";//显示
            } else {
                var message = '<p class="bg-warning">'+data.message+'</p>';
                $('#logonTips').html(message);
            }
        }
    };
    $('#logonForm').ajaxSubmit(logonFormOptions);
}

