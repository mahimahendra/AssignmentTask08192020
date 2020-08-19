using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Filters;

namespace SalesApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]    
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
