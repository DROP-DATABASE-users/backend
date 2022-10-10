﻿using System.ComponentModel.DataAnnotations;

namespace NaszeSasiedztwoBackend.Entities.Dtos;

public class CreateListingDto
{
	[Required]
	[MaxLength(255)]
	public string Title { get; set; }
	[Required]
	[MaxLength(255)]
	public string Description { get; set; }
	[Required]
	[MaxLength(255)]
	public string CoordinatesX { get; set; }
	[Required]
	[MaxLength(255)]
	public string CoordinatesY { get; set; }

	[Required]
	public int AuthorId { get; set; }
}