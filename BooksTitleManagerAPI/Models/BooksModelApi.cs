using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksTitleManagerAPI.Models;

public partial class BooksModelApi
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 100 characters")]
    public string Title { get; set; } = null!;
}
