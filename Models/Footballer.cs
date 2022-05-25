namespace Footballer_Catalog.Models;

public class Footballer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime Birthdate { get; set; }
    public string Team { get; set; }
    public Country Country { get; set; }
}