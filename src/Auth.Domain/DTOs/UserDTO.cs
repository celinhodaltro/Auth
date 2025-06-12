using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; } = String.Empty;
        public UserToken UserToken { get; set; } = new UserToken();
    }
}
