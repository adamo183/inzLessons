﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace inzLessons.Common.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public DateTime Createdate { get; set; }
        public int? RoleId { get; set; }
        public int? MembershipId { get; set; }
        public string Username { get; set; }

        public virtual Membership Membership { get; set; }
        public virtual Role Role { get; set; }
    }
}