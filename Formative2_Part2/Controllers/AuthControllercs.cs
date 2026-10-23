using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Formative2_Part2.Data;

namespace Formative2_Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


    private readonly ApplicationDBcontext  _context;
        public AuthController (ApplicationDBcontext context)
        {
            _context = context;
        }


    }
}
