using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
}