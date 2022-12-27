using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abp.Crud.Models;

public class PersonModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayName("First Name")]
    public string FirstName { get; }

    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; }
    public string Avatar { get; }

    [Required]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required]
    [DisplayName("Birth Date")]
    public DateTime BirthDate { get; }
}
