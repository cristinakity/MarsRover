using MarsRovers.Core;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MarsRovers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NavigationController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(string inputData)
        {
            try
            {
                var navigation = new Navigation();
                var resutls =navigation.Navigate(inputData);
                return Ok(resutls);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
