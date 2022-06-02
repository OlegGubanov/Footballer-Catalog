using System.ComponentModel.DataAnnotations;

namespace Footballer_Catalog.Models;

public class Team
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}