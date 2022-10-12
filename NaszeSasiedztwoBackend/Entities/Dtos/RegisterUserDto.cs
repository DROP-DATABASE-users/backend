using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaszeSasiedztwoBackend.Entities.Dtos
{
	public class RegisterUserDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
