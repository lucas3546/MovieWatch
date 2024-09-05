using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities;
//TO DO: Relate this with user
public class Rating
{
    public int Id { get; set; }
    public double Value { get; set; }

    public int FilmId { get; set; }
    public Film Film { get; set; }

}
