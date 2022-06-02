using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Footballer_Catalog.Models;

public class Footballer
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
    [Required]
    public Team Team { get; set; }
    [Required]
    public Country Country { get; set; }
}