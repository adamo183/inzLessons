// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace inzLessons.Common.Models
{
    public partial class Reservationmessage
    {
        public long Id { get; set; }
        public int Reservationid { get; set; }
        public string Text { get; set; }
        public int? Fileid { get; set; }
        public int Userid { get; set; }
        public DateTime Creationdate { get; set; }

        public virtual Messagefile File { get; set; }
        public virtual Reservation Reservation { get; set; }
        public virtual Users User { get; set; }
    }
}