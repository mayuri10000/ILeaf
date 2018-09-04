$('#calendar').fullCalendar({
    eventSources: [
        '/Web/Calendar/GetUserDisplayEvents?userId=' + currAccount,
    ],
    editable: false
});