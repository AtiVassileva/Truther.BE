using System.ComponentModel.DataAnnotations;
using static Truther.API.Common.ValidationConstants;

namespace Truther.API.Models.RequestModels
{
    public class CommentCreateModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = ContentLengthErrorMessage)]
        public string Content { get; set; } = null!;
    }
}