var courses = [];
var courseChanges = [];

$.ajax({
    url: '/Web/Course/GetCourses',
    success: function (data) {
        if (data.errCode === 0) {
            data.courses.forEach(function (value, i) {
                courses.push(value);
            });

            $('#calendar').fullCalendar('refetchEvents');
        }
        else {
            addPageNotification("错误", data.message + " 错误代码：" + data.errCode);
        }
    },
    error: function (data) {
        addPageNotification("错误", "网络错误，请检查网络连接");
    }
});
$.ajax({
    url: '/Web/Course/GetCourseChanges',
    success: function (data) {
        if (data.errCode === 0) {
            data.changes.forEach(function (value, i) {
                courseChanges.push(value);
            });

            $('#calendar').fullCalendar('refetchEvents');
        }
        else {
            addPageNotification("错误", data.message + " 错误代码：" + data.errCode);
        }
    },
    error: function (data) {
        addPageNotification("错误", "网络错误，请检查网络连接");
    }
});

function onChangeReceived(change) {
    courseChanges.push(change);
    $('#calendar').fullCalendar('refetchEvents');
}

function getCourses(start, end) {
    var courseEvents = [];
    var i = 1;

    courses.forEach(function (value, index) {
        var current = moment(value.semester);
        var weekday = parseInt(current.format('d'));
        var week = 1;

        while (current.isBefore(end)) {

            if (value.weekday == weekday && $.inArray(week, value.weeks) > 0) {
                var _start = moment(current.format()).add(value.startHour, 'hours').add(value.startMinute, 'minutes');
                var _end = moment(current.format()).add(value.endHour, 'hours').add(value.endMinute, 'minutes');

                courseEvents.push({
                    id: 'c-' + i,
                    title: value.title,
                    courseId: value.id,
                    start: _start,
                    end: _end,
                    _date: current.format(),
                    color: 'red',
                    editable: false
                });
            }
            current.add(1, 'days');
            weekday = parseInt(current.format('d'));
            if (weekday == 1)
                week++;
        }
    });

    
    courseChanges.forEach(function (value, i) {
        var eventIndex = -1;

        courseEvents.forEach(function (val, index) {
            if (value.courseId == val.courseId && moment(value.date).format("YYYYMMDD") == moment(val._date).format("YYYYMMDD")) {
                eventIndex = index;
                switch (value.type) {
                    case "TimeModified":
                        var startTime = moment(value.changedValue.split('-')[0]);
                        var endTime = moment(value.changedValue.split('-')[1]);

                        courseEvents[eventIndex].start = startTime;

                        courseEvents[eventIndex].end = endTime;
                        break;
                    case "Cancelled":
                        courseEvents.splice(eventIndex, 1);
                        index--;
                        break;
                    case "TeacherChanged":
                        courseEvents[eventIndex].teacher = value.changedValue;
                        break;
                    case "ClassroomChanged":
                        courseEvents[eventIndex].classroom = value.changedValue;
                        break;
                }
            }
        });

        
    });
    
    return courseEvents;
}
