﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NaszeSasiedztwoBackend.Entities;

namespace NaszeSasiedztwoBackend.Authorization
{
	public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Listing>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
			Listing listing)
		{
			if (requirement.ResourceOperation == ResourceOperation.Read || requirement.ResourceOperation == ResourceOperation.Create)
			{
				context.Succeed(requirement);
			}

			var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
			if (listing.Author.Id.ToString() == userId)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
