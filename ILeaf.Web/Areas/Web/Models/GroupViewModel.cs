using ILeaf.Core.Models;
using ILeaf.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILeaf.Web.Areas.Web.Models
{
    public class GroupViewModel : BaseViewModel
    {
        public List<Group> Groups { get; set; }
        public Group CurrentGroup { get; set; }
        public bool IsHeadman { get; set; }
        public bool IsGroupMember { get; set; }
        public bool IsPendingMember { get; set; }
    }
}