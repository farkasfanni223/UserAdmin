using System;
using System.Collections.Generic;
using System.Text;

namespace UserAdmin.Models
{
    class User
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }
    }
}
