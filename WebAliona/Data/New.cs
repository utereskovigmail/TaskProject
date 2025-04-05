using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAliona.Data;

[Table("News")]
public class New
{
    [Key]public int id { get; set; }
    [Required]
    public string title { get; set; }
    [Required]
    public string slug { get; set; }
    [Required]
    public string summary { get; set; }
    [Required]
    public string content { get; set; }

    public string? photo { get; set; }
}