using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace Entities.DTOs
{
    public class UserUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
