using System;
using System.ComponentModel.DataAnnotations;

namespace WizardExample.Models
{
    [Serializable]
    public class RegistrationData
    {
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required, Range(0, 200)] public int? Age { get; set; }
        public string Hobbies { get; set; }
    }
}