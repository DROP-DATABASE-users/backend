using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaszeSasiedztwoBackend.Entities;

namespace NaszeSasiedztwoBackend.Utils
{
	public class RelationHelper : IRelationHelper
	{
		private readonly NaszeSasiedztwoDbContext _context;

		public RelationHelper(NaszeSasiedztwoDbContext context)
		{
			_context = context;
		}

		public void CreateManyToManyRelationForListingUser(Listing listing)
		{
			var currentRelations = _context.ListingsUsers.Where(x => x.ListingId == listing.Id);
			_context.ListingsUsers.RemoveRange(currentRelations);

			_context.Add(
				new ListingUser
				{
					ListingId = listing.Id,
					UserId = listing.AuthorId,
				});

			if (listing.ContractorId != 0)
			{
				_context.Add(
					new ListingUser
					{
						ListingId = listing.Id,
						UserId = listing.ContractorId,
					});
			}
		}
	}
}
