using NaszeSasiedztwoBackend.Entities;

namespace NaszeSasiedztwoBackend.Utils;

public interface IRelationHelper
{
	void CreateManyToManyRelationForListingUser(Listing listing);
}