using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.Constants;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourtMatterManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class AttorneyController : ControllerBase
    {
        private readonly IAttorneyRepository _attorney;
        public AttorneyController(IAttorneyRepository attorney)
        {
            _attorney = attorney;
        }

        /// <summary>
        /// Returns all Attorneys
        /// </summary>
        /// <returns>List of cutomers</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/attorneys
        ///     
        /// </remarks>
        /// <response code="200">Returns List of attorneys</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("attorneys")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                List<AttorneyDto> attorneys = _attorney.GetAllAttorneys();
                if (attorneys.ToList().Count == 0)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.NoAttorneys });
                else
                    return Ok(new ApiResponse<List<AttorneyDto>> { StatusCode = 200, Message = ApiResponseMessages.AttorneyList, Result = attorneys });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns specific attorney
        /// </summary>
        /// <returns>A requested attorney</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/attorney/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested attorney</response>
        /// <response code="404">attorney not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("attorney/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                AttorneyDto? attorney = _attorney.GetAttorneyById(id);
                if (attorney == null)
                {
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.AttorneyNotFound });
                }
                else
                    return Ok(new ApiResponse<AttorneyDto> { StatusCode = 200, Message = ApiResponseMessages.AttorneyDetails, Result = attorney });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates a attorney.
        /// </summary>
        /// <returns>A newly created attorney</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/attorney
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created attorney</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost("attorney")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(AttorneyDto newAttorney)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    _attorney.CreateAttorney(newAttorney);
                    return Ok(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.AttorneyCreated });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates a attorney.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/attorney/{id}
        ///
        /// </remarks>
        /// <response code="200">attorney updated successfully</response>
        /// <response code="404">attorney not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpPut("attorney/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, AttorneyDto attorney)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    AttorneyDto? updatedAttorney = _attorney.UpdateAttorney(id, attorney);
                    if (updatedAttorney == null)
                        return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.AttorneyNotFound });
                    else 
                        return Ok(new ApiResponse<AttorneyDto> { StatusCode = 200, Message = ApiResponseMessages.AttorneyUpdated, Result = updatedAttorney });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a attorney.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/attorney/{id}
        ///
        /// </remarks>
        /// <response code="200">attorney deleted successfully</response>
        /// <response code="404">attorney not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpDelete("attorney/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                bool attorney = _attorney.DeleteAttorney(id);
                if (attorney)
                {
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.AttorneyDeleted });
                }
                else
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.AttorneyNotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns specific attorneys by jurisdiction 
        /// </summary>
        /// <returns>A requested attorney</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/attorney/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the list of attorneys for jurisdiction</response>
        /// <response code="404">attorney not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("attorney/{jurisdictionId}/attorneys")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAttorneys(int jurisdictionId)
        {
            try
            {
                List<AttorneyDto>? attorney = _attorney.GetAttorneysByJurisdictionId(jurisdictionId);
                if (attorney == null)
                {
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.AttorneyNotFound });
                }
                else
                    return Ok(new ApiResponse<List<AttorneyDto>>{ StatusCode = 200, Message = ApiResponseMessages.AttorneyDetails, Result = attorney });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
