function GetAppointments(){
    $.ajax({
        type: "GET",
        url: "/Web/Calendar/GetAppointments",
        success: function(data){
            
        }
    })
}