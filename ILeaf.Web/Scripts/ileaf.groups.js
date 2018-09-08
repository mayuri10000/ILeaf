$('#btn-edit-member').click(function () {
    $('#btn-edit-member').attr('style', 'display:none;');
    $('.delete-btn').css('display', 'block');
    $('#btn-edit-member-complete').removeAttr('style');
});

$('#btn-edit-member-complete').click(function () {
    $('#btn-edit-member').removeAttr('style');
    $('.delete-btn').css('display', 'none');
    $('#btn-edit-member-complete').attr('style', 'display:none;');
})

function deleteMember(uid, name) {
    if (confirm('确认要删除组员 "' + name + '" ?')) {
        $.ajax({
            url: '/Web/Groups/RemoveMember?groupId=' + groupId + '&uid=' + uid,
            success: function (data) {
                if (data == 'success') {
                    alert('删除成功');
                    window.location.reload();
                }
                else {
                    alert(data);
                }
            }
        });
    }
}

$('#edit-anno').click(function () {
    $('#anno').attr('style', 'display:none;');
    $('#discard-anno').removeAttr('style');
    $('#save-anno').removeAttr('style');
    $('#txt-anno').removeAttr('style');
    $('#edit-anno').attr('style', 'display:none');
});

$('#discard-anno').click(function () {
    $('#anno').removeAttr('style');
    $('#save-anno').attr('style', 'display:none;');
    $('#discard-anno').attr('style', 'display:none;');
    $('#txt-anno').attr('style', 'display:none;');
    $('#txt-anno').val($('#anno')[0].innerText);
    $('#edit-anno').removeAttr('style');
});

$('#save-anno').click(function () {
    $.ajax({
        url: '/Web/Groups/EditAnnouncement',
        method: 'POST',
        data: {
            groupId: groupId,
            announcement: $('#txt-anno').val(),
        },
        success: function (data) {
            if (data == 'success')
                window.location.reload();
            else
                alert(data);
        }
    })
});

function confirmMember(uid) {
    $.ajax({
        url: '/Web/Groups/ConfirmMember?groupId=' + groupId + '&uid=' + uid,
        success: function (data) {
            if (data == 'success')
                window.location.reload();
            else
                alert(data);
        }
    });
}


$('#confirm-groupname').click(function () {
    $.ajax({
        url: '/Web/Groups/EditName?groupId=' + groupId + '&name=' + $('#group-name').val(),
        success: function (data) {
            if (data == 'success')
                window.location.reload();
            else
                alert(data);
        }
    });
});

$('#dissolve-group').click(function () {
    if (confirm("确认要解散该小组？")) {
        $.ajax({
            url: '/Web/Groups/DissolveGroup?groupId=' + groupId,
            success: function (data) {
                if (data == 'success')
                    window.location.replace("/Web/Groups/");
                else
                    alert(data);
            }
        });
    }
});

$('#leave-group').click(function () {
    if (confirm('确认要退出该小组？')) {
        $.ajax({
            url: '/Web/Groups/LeaveGroup?groupId=' + groupId,
            success: function (data) {
                if (data == 'success') {
                    alert('已成功退出该小组');
                    window.location.reload();
                }
                else {
                    alert(data);
                }
            }
        })
    }
});

$('#join-group').click(function () {
    $.ajax({
        url: '/Web/Groups/JoinGroup?groupId=' + groupId,
        success: function (data) {
            if (data == 'success') {
                alert('请求已发送，等待组长确认中。');
                window.location.reload();
            }
            else {
                alert(data);
            }
        }
    });
});

$('#create-group-btn').click(function () {
    $.ajax({
        url: '/Web/Groups/CreateGroup?name=' + $('#new-group-name').val(),
        success: function (data) {
            if (data == 'success')
                window.location.reload();
            else
                alert(data);
        }
    });
});

function changeHeadman(uid, name) {
    if (confirm('您确定要将小组 "' + groupName + '" 的组长变更为 "' + name + '" ?')) {
        $.ajax({
            url: '/Web/Groups/ChangeHeadman?groupId=' + groupId + '&uid=' + uid,
            success: function (data) {
                if (data == 'success') {
                    alert('组长修改成功');
                    window.location.reload();
                }
                else {
                    alert(data);
                }
            }
        });
    }
}

function addMember(uid) {
    $.ajax({
        url: '/Web/Groups/AddMember?groupId=' + groupId + '&uid=' + uid,
        success: function (data) {
            if (data == 'success')
                window.location.reload();
            else
                alert(data);
        }
    });
}



