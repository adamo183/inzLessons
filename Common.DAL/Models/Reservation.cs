﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace inzLessons.Common.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public int Groupid { get; set; }
        public int Userid { get; set; }
        public bool? Isonline { get; set; }
        public DateTime Reservationdate { get; set; }
        public DateTime ReservationEndDate { get; set; }

        public virtual Useringroup Useringroup { get; set; }
    }
}