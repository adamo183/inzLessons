﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace inzLessons.Common.Models
{
    public partial class Useringroup
    {
        public int Userid { get; set; }
        public int Groupid { get; set; }
        public int Condidionid { get; set; }

        public virtual Lessoncondition Condidion { get; set; }
        public virtual Lessonsgroup Group { get; set; }
        public virtual Users User { get; set; }
    }
}