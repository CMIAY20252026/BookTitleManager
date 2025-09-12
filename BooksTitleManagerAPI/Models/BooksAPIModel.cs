using System.ComponentModel.DataAnnotations;

namespace BooksTitleManagerAPI.Models
{
    public class BooksAPIModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; } = String.Empty;
    }
}
