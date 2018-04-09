using System;

namespace Arkitektum.Orden.Models
{
    public class ChangeTrackingEntity
    {
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
    }
}