﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace inzLessons.Common.Models
{
    public partial class Lessonsgroup
    {
        public Lessonsgroup()
        {
            Allowedreservation = new HashSet<Allowedreservation>();
            Useringroup = new HashSet<Useringroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Creationdate { get; set; }
        public int Teacherid { get; set; }

        public virtual ICollection<Allowedreservation> Allowedreservation { get; set; }
        public virtual ICollection<Useringroup> Useringroup { get; set; }
    }
}