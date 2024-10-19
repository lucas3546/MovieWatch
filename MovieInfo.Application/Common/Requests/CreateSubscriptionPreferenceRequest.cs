using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests;
public record CreateSubscriptionPreferenceRequest
{
    [MaxLength(15)]
    public required string Title { get; set; }
    [MaxLength(20)]
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}