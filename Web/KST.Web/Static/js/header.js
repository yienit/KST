// header method for dashboard

/* 初始化头部模板事件 */
$(function () {
    $('[data-toggle="popover"]').popover();
});


/* 切换科目 */
function exchangeCourse(courseID) {
    $.ajax({
        type: "POST",
        url: "/Item/ExchangeCourse?random=" + Math.random(),
        data: { course_id: courseID },
        success: function (json) {
            if (json.code == 1) {
                window.location.reload();
            }
            else {
                alert("exchange course faild:" + json.msg);
            }
        },
        error: function (xhr, testStatus, error) {
            alert("Send ajax request faild:" + error);
        }
    });
}

/* 显示修改密码操作DIV */
function showUpdatePwdWrapper() {
    $('#update_pwd_link').hide();
    $('#update_pwd_wrapper').show();

    $('#update_pwd_tip').text('');
    $('#old_pwd').val('');
    $('#new_pwd').val('');
    $('#new_pwd_again').val('');
    
}

/* 隐藏修改密码操作DIV */
function hideUpdatePwdWrapper() {
    $('#update_pwd_link').show();
    $('#update_pwd_wrapper').hide();
}

/* 更新管理员密码 */
function updatePwd(adminID) {

    $('#update_pwd_tip').text('');

    var oldPwd = $.trim($('#old_pwd').val());
    if (!oldPwd) {
        $("#update_pwd_tip").attr('class', 'red');
        $('#update_pwd_tip').text('请输入原密码');
        return;
    }

    var newPwd = $.trim($('#new_pwd').val());
    if (!newPwd) {
        $("#update_pwd_tip").attr('class', 'red');
        $('#update_pwd_tip').text('请输入新密码');
        return;
    }
    if (newPwd.length < 6) {
        $("#update_pwd_tip").attr('class', 'red');
        $('#update_pwd_tip').text('密码至少为6个字符');
        return;
    }

    var pwdConfirm = $.trim($('#new_pwd_again').val());
    if (pwdConfirm != newPwd) {
        $("#update_pwd_tip").attr('class', 'red');
        $('#update_pwd_tip').text('两次输入的密码不一致');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Agency/UpdateAdminPwd?random=" + Math.random(),
        data: { admin_id: adminID, old_pwd: $.md5(oldPwd), new_pwd: $.md5(newPwd) },
        success: function (json) {
            if (json.code == 1) {
                $("#update_pwd_tip").attr('class', 'green');
                $('#update_pwd_tip').text('修改密码成功,您可以新密码登录系统');
            }
            else {
                $("#update_pwd_tip").attr('class', 'red');
                $('#update_pwd_tip').text('修改密码失败: ' + json.msg);
            }
        },
        error: function (xhr, testStatus, error) {
            alert("发送ajax请求失败： " + error);
        }
    });
}

