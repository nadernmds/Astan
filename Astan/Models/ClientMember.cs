//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Astan.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientMember
    {
        public long clientMemberID { get; set; }
        public Nullable<long> clientID { get; set; }
        public string name { get; set; }
        public Nullable<byte> age { get; set; }
        public Nullable<byte> healthStateID { get; set; }
        public string need { get; set; }
    
        public virtual HealthState HealthState { get; set; }
        public virtual Client Client { get; set; }
    }
}