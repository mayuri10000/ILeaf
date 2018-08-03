//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ILeaf.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attachment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attachment()
        {
            this.ReleatedCourses = new HashSet<AttachmentCourse>();
            this.AccessableUsers = new HashSet<Account>();
            this.AccessableClasses = new HashSet<ClassInfo>();
            this.AccessableGroups = new HashSet<Group>();
        }
    
        public long Id { get; set; }
        public string FileName { get; set; }
        public Nullable<long> FileSize { get; set; }
        public string MD5Hash { get; set; }
        public bool IsPublicAttachment { get; set; }
        public long UploaderId { get; set; }
        public System.DateTime UploadTime { get; set; }
        public System.DateTime ExpireTime { get; set; }
        public string StoragePath { get; set; }
    
        public virtual Account Uploader { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentCourse> ReleatedCourses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> AccessableUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassInfo> AccessableClasses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> AccessableGroups { get; set; }
    }
}