using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Entities.Dtos;

namespace NaszeSasiedztwoBackend.Services
{
	public class AccountService : IAccountService
	{
		private readonly NaszeSasiedztwoDbContext _context;
		private readonly IMapper _mapper;
		private readonly PasswordHasher<User> _passwordHasher;

		public AccountService(NaszeSasiedztwoDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
			_passwordHasher = new PasswordHasher<User>();
		}

		public int RegisterUser(RegisterUserDto dto)
		{
			if (_context.Users.Any(x => x.UserName == dto.UserName))
			{
				throw new ArgumentException("Name is already in use");
			}

			var user = _mapper.Map<User>(dto);
			user.HashedPassword = _passwordHasher.HashPassword(user, dto.Password);

			_context.Users.Add(user);
			_context.SaveChanges();

			return user.Id;
		}

	}
}
