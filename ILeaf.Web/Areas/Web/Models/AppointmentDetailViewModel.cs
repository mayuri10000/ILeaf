using System;

namespace ILeaf.Web.Areas.Web.Models
{
    public class AppointmentDetailViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsAllDay { get; set; }
        public DateTime? EndTime { get; set; }
        public byte Visibily { get; set; }
    }
}