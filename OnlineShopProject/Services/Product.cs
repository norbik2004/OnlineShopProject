﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OnlineShopProject.Services
{
	public class Product
	{
		[Required]
		[Key]
		public long ProductId { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[StringLength(150)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public decimal Price { get; set; }

		[Required]
		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }

		[Required]
		public string IMGFileLink { get; set; } = string.Empty;

        [JsonIgnore]
        [Required]
		public Category Category { get; set; }

	}
}
