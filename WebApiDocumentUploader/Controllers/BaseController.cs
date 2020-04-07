using Microsoft.AspNetCore.Mvc;

namespace WebApiDocumentUploader.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        /// <summary>
        /// Return OK 200
        /// </summary>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IActionResult ApiResponseWithData<T>(T result)
        {
            return Ok(result);
        }

        /// <summary>
        /// Return Ok 200 if data != null
        /// otherwise return NotFound 404
        /// </summary>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IActionResult ApiResponseWithDataRequired<T>(T result) where T : class
            => result == null ? 
                NotFound() as IActionResult
                :
                Ok(result);
    }
}