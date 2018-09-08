
var currentFriendEvent = "";
// 初始化日历界面
$("#calendar").fullCalendar({
    header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay,listWeek'
    },
    themeSystem: "bootstrap3",
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
        },
        {
            url: '/Web/Calendar/GetUnconfirmedFriendEvents',
            color: 'gray',
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
    
    dayClick: function (date, jsEvent, view) {

        $("#calendar").fullCalendar('renderEvent', {
            id: 'placeholder',
            start: date.format(),
            editable: false,
            color: 'gray'
        });

        var placement = 'right';

        if (view.type == 'agendaDay')
            placement = 'bottom';
        else if (parseInt(date.format('d')) > 3 || parseInt(date.format('d')) == 0 )
            placement = 'left';

        $('#eventPlaceholder').popover({
            title: "添加日程",
            html: true, 
            content: $('#popoverAddEvent')[0].innerHTML,  
            placement: placement
        });

        $('#eventPlaceholder').popover('show');

        var formattedDate = date.format("YYYY-MM-DD/HH:mm");
        var endDate = date.add(1, "hours").format("YYYY-MM-DD/HH:mm");

        $("#_date").val(formattedDate.split('/')[0]);
        if (formattedDate.split('/')[1] != "00:00") {
            $("#_starttime").val(formattedDate.split('/')[1]);
            $("#_endtime").val(endDate.split('/')[1]);
        } else {
            if (view.type !== 'month') {
                $('#_allday').attr("checked", 'true');
                $("#_starttime").attr("disabled", "disabled");
                $("#_endtime").attr("disabled", "disabled");
            }
            $("#_starttime").val("09:00");
            $("#_endtime").val("10:00");
        }

        $('#_allday').change(function () {
            if ($('#_allday').is(':checked')) {
                $("#_starttime").attr("disabled", "disabled");
                $("#_endtime").attr("disabled", "disabled");
            }
            else {
                $('#_starttime').removeAttr('disabled');
                $('#_endtime').removeAttr('disabled');
            }
        })

        $("#_starttime").timepicker({
            showMeridian: false,
            defaultTime: $("#_starttime").val(),
        });

        $("#_endtime").timepicker({
            showMeridian: false,
            defaultTime: $("#_endtime").val(),
        });
    },

    eventRender: function (event, element, view) {
        if (event.id == 'placeholder') {
            element.attr('id', 'eventPlaceholder');
            element.attr('data-toggle', 'popover');
        }
        else if (event.id.indexOf('unconfirmed-') >= 0) {
            element.attr('id', event.id);
            element.attr('data-toggle', 'popover');
        }
    },

    eventClick: function (calEvent, jsEvent, view) {
        if (calEvent.id.indexOf('unconfirmed-') >= 0) {
            var content = $('#popoverConfirmEvent')[0].innerHTML.replace('{userName}', calEvent.userName);

            currentFriendEvent = calEvent.id.replace("unconfirmed-", "");

            $('#' + calEvent.id).popover({
                title: "好友日程",
                html: true,
                content: content,
                placement: "left"
            });

            $('#' + calEvent.id).popover('show');
        }
        else {
            if (calEvent.id!='placeholder')
                window.open("/Web/Calendar/AppointmentDetails?appointmentId=" + calEvent.id);
        }
    },
    eventDrop: function (event, delta, revertFunc) {
        updateEvent(event, revertFunc);
    },
    eventResize: function(event, delta, revertFunc) {
        updateEvent(event, revertFunc);
    }
});



function cancelQuickAdd() {
    $('#eventPlaceholder').popover('destroy');
    $('#calendar').fullCalendar('removeEvents', 'placeholder');
}

function onQuickAdd() {
    $.ajax({
        url: '/Web/Calendar/UpdateAppointment',
        data: {
            Title: $('#_title').val(),
            StartDate: $('#_date').val(),
            StartTime: $('#_allday').is(':checked') ? "" : $('#_starttime').val(),
            EndDate: $('#_date').val(),
            EndTime: $('#_allday').is(':checked') ? "" : $('#_endtime').val(),
            IsAllDay: $('#_allday').is(':checked'),
            Visiblity: "0",
        },
        method: 'POST',
        success: function (data) {
            if (data !== 'success') {
                alert('错误: ' + data);
                cancel();
            }

            $('#calendar').fullCalendar('refetchEvents');
        },
        error: function (data) {
            alert('网络错误, 请检查您的网络连接');
            cancel();
        }
    });
}

function jumpToDetail() {
    var json = JSON.stringify({
        title: $('#_title').val(),
        date: $('#_date').val(),
        start: $('#_starttime').val(),
        end: $('#_endtime').val(),
        allday: $('#_allday').is(':checked')
    });

    window.open("/Web/Calendar/AddAppointment?json=" + json);

    cancelQuickAdd();
}

function updateEvent(event, cancel) {
    $.ajax({
        url: '/Web/Calendar/UpdateAppointment',
        data: {
            Id: event.id,
            Title: event.title,
            StartDate: moment(event.start).format('YYYY-MM-DD'),
            StartTime: event.allDay ? "" : moment(event.start).format('HH:mm'),
            EndDate: moment(event.end).format('YYYY-MM-DD'),
            EndTime: event.allDay ? "" : moment(event.end).format('HH:mm'),
            IsAllDay: event.allDay,
            Place: event.place,
            Details: event.details,
            Visiblity: event.visiblity,
        },
        method: 'POST',
        success: function (data) {
            if (data !== 'success') {
                alert('错误: ' + data)
                cancel();
            }

            $('#calendar').fullCalendar('refetchEvents');
        },
        error: function (data) {
            alert('网络错误, 请检查您的网络连接');
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

function acceptFriendEvent() {
    $.ajax({
        url: '/Web/Calendar/AcceptAppointment?appointmentId=' + currentFriendEvent,
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

function declineFriendEvent() {
    $.ajax({
        url: '/Web/Calendar/DeclineAppointment?appointmentId=' + currentFriendEvent,
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

function cancelFriendEvent() {
    $('#unconfirmed-' + currentFriendEvent).popover('destroy');
    currentFriendEvent = "";
}