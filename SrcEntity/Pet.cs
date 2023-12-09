//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myPetCare
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pet()
        {
            this.Appointments = new HashSet<Appointment>();
            this.Health_Records = new HashSet<Health_Record>();
            this.Medications = new HashSet<Medication>();
        }
    
        public int IDPet { get; set; }
        public int C_OwnerID { get; set; }
        public string C_petName { get; set; }
        public string C_sex { get; set; }
        public string C_breed { get; set; }
        public string C_color { get; set; }
        public System.DateTime C_birthdate { get; set; }
        public string C_petType { get; set; }
        public string C_photo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Health_Record> Health_Records { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medication> Medications { get; set; }
        public virtual User User { get; set; }
    }
}
