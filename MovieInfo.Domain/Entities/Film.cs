using MovieInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities;
//TO DO: Categories
public class Film
{
    [Key]
    public int Id { get; set; }
    public required string Title { get; set; }
    public TimeSpan Duration { get; set; } //Reminder: TimeSpan.FromHours(2.5) for example
    public FilmType Type { get; set; }
    public ICollection<Rating>? Rating { get; set; }

}
