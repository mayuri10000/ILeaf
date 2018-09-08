var courses = [];
var courseChanges = [];

$.ajax({
   url: '/Web/Course/GetCourses',
   success: function (data) {
        if (data.errCode === 0) {
            data.courses.forEach(function (value, i) {
               courses.push(value);
            });
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
    var current = start;
    var weekday = parseInt(current.format('d'));
    var week = 1;
    var courseEvents = [];

    while (current.isBefore(end)) {
        courses.forEach(function (value, index) {
            if (weekday === value.weekday && $.inArray(week, value.weeks))
                courseEvents.push({
                    id: value.id,
                    title: value.title,
                    start: current.add(value.startHour, 'hours').add(value.startMinute, 'minutes'),
                    end: current.add(value.endHour, 'hours').add(value.endMinute, 'minutes'),
                    _date: current,
                    teacher: value.teacher,
                    classroom: value.classroom
                });
        });
        current = current.add(1, 'days');
        weekday = parseInt(current.format('d'));
        if (weekday === 0)
            week++;
    }

    courseChanges.forEach(function (value, i) {
        var eventIndex = -1;

        courseEvents.forEach(function (val, index) {
            if (value.courseId === val.id && value.date === val._date) {
                eventIndex = index;
            }
        });

        switch (value.type) {
            case "dateChanged":
                courseEvents[eventIndex].start.year(moment(value.changeValue).year());
                courseEvents[eventIndex].end.year(moment(value.changeValue).year());

                courseEvents[eventIndex].start.dayOfYear(moment(value.changeValue).dayOfYear());
                courseEvents[eventIndex].end.dayOfYear(moment(value.changeValue).dayOfYear());
                break;
            case "timeChanged":
                var startTime = moment(value.changeValue.spilt('-')[0]);
                var endTime = moment(value.changeValue.spilt('-')[1]);

                courseEvents[eventIndex].start.hour(startTime.hour());
                courseEvents[eventIndex].start.minute(startTime.minute());
                
                courseEvents[eventIndex].end.minute(startTime.minute());
                courseEvents[eventIndex].end.minute(startTime.minute());
                break;
            case "canelled":
                array.splice(eventIndex, 1);
                break;
            case "teacherChanged":
                courseEvents[eventIndex].teacher = value.changeValue;
                break;
            case "classroomChanged":
                courseEvents[eventIndex].classroom = value.changeValue;
                break;
        }
    });

    return courseEvents;
}
