﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace inzLessons.Common.Models
{
    public partial class Messagefile
    {
        public Messagefile()
        {
            Reservationmessage = new HashSet<Reservationmessage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }

        public virtual ICollection<Reservationmessage> Reservationmessage { get; set; }
    }
}