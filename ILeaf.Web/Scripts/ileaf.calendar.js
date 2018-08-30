$("#calendar").fullcalendar({
    nowIndicator: true,
    defaultView: 'month',
    height: 'parent',
    eventSources: [
        {
            url: "/Web/Calendar/GetPersonalEvents",         
            color: 'green',
            textColor: 'white',
            editable: true
        },
        {
            url: '/Web/Calendar/GetFriendAndGroupEvents',
            color: 'blue',
            textColor: 'white',
            editable: false
        },
        {
            events: function (start, end, timezone, callback) {
                callback(getCourses(start, end));
            },
            color: 'red',
            textColor: 'white',
            editable: false
        }
    ],

    eventClick: function (calEvent, jsEvent, view) {
        viewEvent(calEvent);
    },
    eventDrop: function (event, delta, revertFunc) {
        updateEvent(event, revertFunc);
    },
    eventResize: function(event, delta, revertFunc) {
        updateEvent(event, revertFunc);
    }
});

function updateEvent(event, cancel) {
    $.ajax({
        url: '/Web/Calendar/ModifyEvent',
        data: event,
        method: 'POST',
        success: function (data) {
            if (data != 'success')
                addPageNotification('错误', data)
                cancel();
        },
        error: function (data) {
            addPageNotification('网络错误', '请检查您的网络连接');
            cancel();
        }
    });
} 

function viewEvent(event) {

}