using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Interfaces;
public interface ICurrentUser
{
    string? Name { get; }

}
