﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace inzLessons.Common.Models
{
    public partial class Useringroup
    {
        public Useringroup()
        {
            Userinreservation = new HashSet<Userinreservation>();
        }

        public int Userid { get; set; }
        public int Groupid { get; set; }
        public double? Price { get; set; }

        public virtual Lessonsgroup Group { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Userinreservation> Userinreservation { get; set; }
    }
}