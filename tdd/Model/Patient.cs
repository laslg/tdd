using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TDD.Model
{
    public class Patient : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "The name should be between 2 & 40 characters.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{7,12})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(1,50)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        public bool IsAdmitted { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Only Male, Female or Other gender are allowed
            if (Gender.Equals("Male", StringComparison.CurrentCultureIgnoreCase) == false &&
            Gender.Equals("Female", StringComparison.CurrentCultureIgnoreCase) == false &&
            Gender.Equals("Other", StringComparison.CurrentCultureIgnoreCase) == false)
            {
                yield return new ValidationResult("The gender can either be Male, Female or Other");
            }

            yield return ValidationResult.Success;
        }
    }
}