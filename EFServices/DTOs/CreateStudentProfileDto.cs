﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFServices.DTOs
{
    public class CreateStudentProfileDto
    {
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = null!;
    }
}