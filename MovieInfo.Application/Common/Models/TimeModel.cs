using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Models;
public class TimeModel
{
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Second { get; set; }

    public TimeSpan ToTimeSpan() => new TimeSpan(Hour, Minute, Second);
}