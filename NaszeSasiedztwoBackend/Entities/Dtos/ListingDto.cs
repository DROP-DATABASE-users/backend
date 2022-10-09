﻿namespace NaszeSasiedztwoBackend.Entities.Dtos;

public class ListingDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string CoordinatesX { get; set; }
	public string CoordinatesY { get; set; }
	public int AuthorId { get; set; }
	public int ContractorId { get; set; }

	public List<ListingUserDto> Users { get; set; }
}