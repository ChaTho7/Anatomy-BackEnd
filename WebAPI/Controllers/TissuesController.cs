using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TissuesController : ControllerBase
    {
        ITissueService _tissueService;

        public TissuesController(ITissueService tissueService)
        {
            _tissueService = tissueService;
        }

        /// <summary>
        /// Get All Tissues
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _tissueService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _tissueService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyfilter")]
        public IActionResult GetByFilter(int? id, int? sortId, int? regionId)
        {
            var result = _tissueService.GetByFilter(id, sortId, regionId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Tissue tissue)
        {
            var result = _tissueService.Add(tissue);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Tissue tissue)
        {
            var result = _tissueService.Update(tissue);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
