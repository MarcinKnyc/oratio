﻿using Oratio.Areas.Identity.Data;

namespace Oratio.Models
{
    public class Intention : Entity
    {
        public string AskedIntention { get; set; }
        public float? Offering { get; set; }
        public bool isPaid { get; set; }
        public bool isApproved { get; set; }
        public Guid? MassId { get; set; }
        public Mass? Mass { get; set; }

        public bool ValidateOfferingAmount(int? threshold)
        {
            if (threshold== null) { return true; }
            if (Offering == null) { return false; }
            return threshold < Offering;
        }
    }
}
