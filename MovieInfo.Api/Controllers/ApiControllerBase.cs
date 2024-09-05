using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //TO DO: Add global exception handler here
    public class ApiControllerBase : ControllerBase
    {

    }

}
