using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.Constants;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtMatterManagement.Controllers
{
    [ApiController]
    [Route("api")]
    public class MatterController : ControllerBase
    {
        private readonly IMatterRepository _matters;

        public MatterController(IMatterRepository matters)
        {
            _matters = matters;
        }

        /// <summary>
        /// Returns all Matters
        /// </summary>  
        /// <returns>List of cutomers</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/matters
        ///     
        /// </remarks>
        /// <response code="200">Returns List of matters</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("matters")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var matters = _matters.GetAllMatters();
                if (matters.ToList().Count == 0)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.NoMatters });
                else
                    return Ok(new ApiResponse<List<MatterDto>> { StatusCode = 200, Message = ApiResponseMessages.MatterList, Result = matters });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns specific matter
        /// </summary>
        /// <returns>A requested matter</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/matter/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested matter</response>
        /// <response code="404">matter not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                var matter = _matters.GetMatterById(id);
                if (matter == null)
                {
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.MatterNotFound });
                }
                else
                    return Ok(new ApiResponse<MatterDto> { StatusCode = 200, Message = ApiResponseMessages.MatterDetails, Result = matter });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates a matter.
        /// </summary>
        /// <returns>A newly created matter</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/matter
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created matter</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost("matter")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(MatterDto newMatter)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    _matters.CreateMatter(newMatter);
                    return Ok(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.MatterCreated });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates a matter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/matter/{id}
        ///
        /// </remarks>
        /// <response code="200">matter updated successfully</response>
        /// <response code="404">matter not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpPut("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, MatterDto matter)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    MatterDto? updatedMatter = _matters.UpdateMatter(id, matter);
                    if (updatedMatter == null)
                        return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.MatterNotFound });
                    else
                        return Ok(new ApiResponse<MatterDto> { StatusCode = 200, Message = ApiResponseMessages.MatterUpdated, Result = updatedMatter });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a matter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/matter/{id}
        ///
        /// </remarks>
        /// <response code="200">matter deleted successfully</response>
        /// <response code="404">matter not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpDelete("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var matter = _matters.DeleteMatter(id);
                if (matter)
                {
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.MatterDeleted });
                }
                else
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.MatterNotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get matters for a client.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/matters/{clientId}/matters
        ///
        /// </remarks>
        /// <response code="200">list of matters for a client</response>
        [HttpGet("/matters/{clientId}/matters")]
        public IActionResult GetMattersByClient(int clientId)
        {
            try
            {
                List<MatterDto> matters = _matters.GetMattersByClient(clientId);
                return Ok(new ApiResponse<List<MatterDto>> { StatusCode = 200, Message = ApiResponseMessages.MattersByClient, Result = matters });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get invoices for a matter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/matters/{matterId}/invoices
        ///
        /// </remarks>
        /// <response code="200">list of invoices for a matter</response>
        [HttpGet("/matters/{matterId}/invoices")]
        public IActionResult GetInvoicesByMatter(int matterId)
        {
            try
            {
                List<InvoiceDto> invoices = _matters.GetInvoicesByMatter(matterId);
                return Ok(new ApiResponse<List<InvoiceDto>> { StatusCode = 200, Message = ApiResponseMessages.InvoicesByMatter, Result = invoices });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get invoices for a attorney.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/matters/{attorneyId}/invoices
        ///
        /// </remarks>
        /// <response code="200">list of invoices by matters</response>
        [HttpGet("/matters/{attorneyId}/invoices")]
        public IActionResult GetLastWeeksBillingByMatter(int attorneyId)
        {
            try
            {
                List<InvoiceDto> invoices = _matters.GetLastWeeksBillingByAttorney(attorneyId);
                return Ok(new ApiResponse<List<InvoiceDto>> { StatusCode = 200, Message = ApiResponseMessages.InvoicesByAttorney, Result = invoices });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get invoices by matters.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/invoicesByMatters
        ///
        /// </remarks>
        /// <response code="200">list of invoices by matters</response>
        [HttpGet("/mattersByClients")]
        public IActionResult GetMattersByClients()
        {
            try
            {
                List<IGrouping<int, Matter>> matters = _matters.GetAllMattersByClients();
                return Ok(new ApiResponse<List<IGrouping<int, Matter>>> { StatusCode = 200, Message = ApiResponseMessages.MattersByClient, Result = matters });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get invoices by matters.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/invoicesByMatters
        ///
        /// </remarks>
        /// <response code="200">list of invoices by matters</response>
        [HttpGet("/invoicesByMatters")]
        public IActionResult GetInvoicesByMatters()
        {
            List<IGrouping<int, Invoice>> invoices = _matters.GetAllInvoices();
            return Ok(new ApiResponse<List<IGrouping<int, Invoice>>> { StatusCode = 200, Message = ApiResponseMessages.InvoicesByMatter, Result = invoices });
        }

        /// <summary>
        /// Get Last week invoices for attorneys.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/invoicesBillingByAttorney
        ///
        /// </remarks>
        /// <response code="200">List of invoices for last week by matters</response>
        [HttpGet("/invoicesBillingByAttorney")]
        public IActionResult GetLastWeeksBillingByAttorney()
        {
            List<IGrouping<int, Invoice>> invoices = _matters.GetLastWeekBillingsByAttorney();
            return Ok(new ApiResponse<List<IGrouping<int, Invoice>>> { StatusCode = 200, Message = ApiResponseMessages.InvoicesByAttorney, Result = invoices });
        }
    }
}
