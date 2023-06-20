using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace UploadToServer.Controllers
{
    [ApiController]// use the model binding feature of ASP.NET Core to bind
                   // the incoming HTTP request data to the action method parameters.
    [Route("[controller]")]
    public class UploadFileController : Controller
    {//The ILogger is used for logging purposes,
     //and IWebHostEnvironment provides information about the web hosting environment.
        private readonly ILogger<UploadFileController> _logger;
        private readonly IWebHostEnvironment _environment;
        public UploadFileController (ILogger<UploadFileController>logger,IWebHostEnvironment environment)
        {
            _logger = logger; 
            _environment= environment ?? throw new ArgumentNullException(nameof(environment));
          
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                if (httpRequest.Form.Files.Count>0) 
                { 
                    foreach(var file in httpRequest.Form.Files)
                    {
                        var filePath = Path.Combine(_environment.ContentRootPath, "Uploads");

                        if(!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        using (var memoryStream=new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            System.IO.File.WriteAllBytes(Path.Combine(filePath,file.FileName),memoryStream.ToArray());
                        }
                        return Ok();
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error");
                return new StatusCodeResult(500);
            }
            return BadRequest();
        }
    }
}
