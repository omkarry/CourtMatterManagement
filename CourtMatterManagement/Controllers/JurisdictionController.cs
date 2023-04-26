using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.Constants;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtMatterManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class JurisdictionController : ControllerBase
    {
        private readonly IJurisdictionRepository _jurisdiction;
        public JurisdictionController(IJurisdictionRepository jurisdiction)
        {
            _jurisdiction = jurisdiction;
        }

        /// <summary>
        /// Returns all Jurisdictions
        /// </summary>
        /// <returns>List of cutomers</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/jurisdictions
        ///     
        /// </remarks>
        /// <response code="200">Returns List of jurisdictions</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("jurisdictions")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var jurisdictions = _jurisdiction.GetAllJurisdictions();
                if (jurisdictions.ToList().Count == 0)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.NoJurisdictions });
                else
                    return Ok(new ApiResponse<List<JurisdictionDto>> { StatusCode = 200, Message = ApiResponseMessages.JurisdictionList, Result = jurisdictions });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns specific jurisdiction
        /// </summary>
        /// <returns>A requested jurisdiction</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/jurisdiction/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested jurisdiction</response>
        /// <response code="404">jurisdiction not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("jurisdiction/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                var jurisdiction = _jurisdiction.GetJurisdictionById(id);
                if (jurisdiction == null)
                {
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.JurisdictionNotFound });
                }
                else
                    return Ok(new ApiResponse<JurisdictionDto> { StatusCode = 200, Message = ApiResponseMessages.JurisdictionDetails, Result = jurisdiction });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates a jurisdiction.
        /// </summary>
        /// <returns>A newly created jurisdiction</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/jurisdiction
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created jurisdiction</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost("jurisdiction")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(JurisdictionDto newJurisdiction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    _jurisdiction.CreateJurisdiction(newJurisdiction);
                    return Ok(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.JurisdictionCreated });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
