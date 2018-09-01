// 初始化日历界面
$("#calendar").fullCalendar({
    header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay,listWeek'
    },
    themeSystem: "bootstrap3",
    defaultDate: '2018-03-12',
    editable: true,
    nowIndicator: true,
    navLinks: true,
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
        }/*,
        {
            events: function (start, end, timezone, callback) {
                callback(getCourses(start, end));
            },
            color: 'red',
            textColor: 'white',
            editable: false
        }*/
    ],
    
    dayClick: function (date, jsEvent, view) {

        var formattedDate = date.format("YYYY-MM-DD/HH:MM");
        var endDate = date.add(1, "hours").format("YYYY-MM-DD/HH:MM");

        $("#StartDate").val(formattedDate.split('/')[0]);
        if (formattedDate.split('/')[1] != "00:03") {
            $("#StartTime").val(formattedDate.split('/')[1]);
            $("#EndTime").val(endDate.split('/')[1]);
        } else {
            $("#StartTime").val("09:00");
            $("#EndTime").val("10:00");
        }

        $("#StartDate").datepicker();
        $("#EndDate").datepicker();
        $("#StartTime").timepicker({
            showMeridian: false,
            defaultTime: $("#StartTime").val(),
        });
        $("#EndTime").timepicker({
            showMeridian: false,
            defaultTime: $("#EndTime").val(),
        });


        $("#EndDate").val(formattedDate.split('/')[0]);
        $("#mdlAddAppointment").modal();


    },

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
        url: '/Web/Calendar/UpdateAppointment',
        data: {
            Id: event.id,
            Title: event.title,
            StartDate: moment(event.start).format('YYYY-MM-DD'),
            StartTime: event.allDay ? "" : moment(event.start).format('HH:mm'),
            EndDate: event.allDay ? "" : moment(event.end).format('YYYY-MM-DD'),
            EndTime: event.allDay ? "" : moment(event.end).format('HH:mm'),
            IsAllDay: event.allDay,
            Place: event.place,
            Details: event.details,
            Visiblity: event.visiblity,
        },
        method: 'POST',
        success: function (data) {
            if (data !== 'success') {
                addPageNotification('错误', data)
                cancel();
            }

            $('#calendar').fullCalendar('refetchEvents');
        },
        error: function (data) {
            addPageNotification('网络错误', '请检查您的网络连接');
            cancel();
        }
    });
} 

function viewEvent(event) {

}

function submitEvent() {
    $('#frmAddEvent').ajaxSubmit(function () {
        $('#calendar').fullCalendar('refetchEvents');
    });

    return false;
}