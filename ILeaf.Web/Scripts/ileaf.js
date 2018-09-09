function addPageNotification(title, conntent) {
    $.gritter.add({
        title: title,
        text: content,
        //image: 'assets/img/moreNoti.jpg',
        sticky: false,
        time: 5
    });
}

function initSignalR() {

}