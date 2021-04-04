using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Core.Concrete.Files;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TissueImagesController : ControllerBase
    {
        private ITissueImageService _tissueImageService;
        public TissueImagesController(ITissueImageService tissueImageService)
        {
            _tissueImageService = tissueImageService;
        }

        [HttpPost("addimage")]
        public IActionResult Add([FromForm]IFormFile formFile, [FromForm]string tissueId)
        {
            Image image = new Image {File = formFile};
            try
            {
                var result = _tissueImageService.Add(image, Int32.Parse(tissueId));

                if (result.Success)
                {
                    return Ok("Uploaded");
                }

                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getimage")]
        public IActionResult Get(int tissueId)
        {
            try
            {
                var result = _tissueImageService.GetImagesPerTissue(tissueId);
                
                if (result.Data != default)
                {
                    return Ok(result);
                }

                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
