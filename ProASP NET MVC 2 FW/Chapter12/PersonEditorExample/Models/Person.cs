using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PersonEditorExample.Models
{
    public class Person
    {
        [HiddenInput(DisplayValue = false)] // Don't want the user to see or edit this
        public int PersonId { get; set; }

        // [DisplayName] specifies user-friendly names for the properties
        [DisplayName("First name")] public string FirstName { get; set; }
        [DisplayName("Last name")] public string LastName { get; set; }
        
        [DataType(DataType.Date)] // Show only the date, ignoring any time data
        [DisplayName("Born")] public DateTime BirthDate { get; set; }

        public Address HomeAddress { get; set; }

        [DisplayName("May log in")] public bool IsApproved { get; set; }
    }
}