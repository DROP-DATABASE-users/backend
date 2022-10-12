using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaszeSasiedztwoBackend.Entities.Dtos
{
	public class RegisterUserDto
	{
		public string UserName { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Description { get; set; }
		public string Password { get; set; }
	}
}
