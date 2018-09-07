var collapse = [];

function addFriend(id) {
    $.ajax({
        url: '/Web/Account/SendFriendRequest?userId=' + id,
        success: function (data) {
            if (data == 'success') {
                alert('请求已发送，等待对方确认中');
                window.location.reload();
            }
            else {
                alert(data);
            }
        }
    });
}

function acceptFriend(id) {
    $.ajax({
        url: '/Web/Account/AcceptFriendRequest?userId=' + id,
        success: function (data) {
            if (data == 'success') {
                window.location.reload();
            }
            else {
                alert(data);
            }
        }
    });
}

function breakUp(id, isNewFriend, name) {
    if (confirm(isNewFriend ? "您确认要拒绝来自 " + name + " 的好友请求？" : "您确定要删除好友 " + name + " ？")) {
        $.ajax({
            url: '/Web/Account/DeclineFriendRequestOrRemoveFriend?userId=' + id,
            success: function (data) {
                if (data == 'success') {
                    window.location.reload();
                }
                else {
                    alert(data);
                }
            }
        });
    }
}

function onSearchUser() {
    var search = $('#txtSearch').val();
    if (search == "")
        return;
    var result = [];
    $.ajax({
        url: '/Web/Account/SearchUser?keyword=' + search,
        success: function (data) {
            $('.search').remove();
            if (collapse.indexOf('classmate') == -1)
                toggleCollapse('classmate');
            if (collapse.indexOf('friend') == -1)
                toggleCollapse('friend');

            $('#search-result').removeAttr('style');
            
            var itemTmp = $('#userListItem')[0].innerHTML;

            data.forEach(function (value, index) {
                var item = itemTmp.replace('{userId}', value.id)
                    .replace('{userImg}', value.img)
                    .replace('{realName}', value.realName)
                    .replace('{userName}', value.userName);

                $('#userList').append(item);
            });
        }
    });
}

function toggleCollapse(name) {
    if (collapse.indexOf(name) == -1) {
        collapse.push(name);
        $('#' + name + '-toggle').attr('class', "fa fa-angle-down");
        $('.' + name).attr('style', 'display:none;');
    }
    else {
        collapse.splice(name);
        $('#' + name + '-toggle').attr('class', "fa fa-angle-up");
        $('.' + name).removeAttr('style');
    }
}