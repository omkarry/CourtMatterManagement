using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.Constants;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtMatterManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoice;
        public InvoiceController(IInvoiceRepository invoice)
        {
            _invoice = invoice;
        }

        /// <summary>
        /// Returns all Invoices
        /// </summary>
        /// <returns>List of cutomers</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/invoices
        ///     
        /// </remarks>
        /// <response code="200">Returns List of invoices</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("invoices")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                List<InvoiceDto> invoices = _invoice.GetAllInvoices();
                if (invoices.ToList().Count == 0)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.NoInvoices });
                else
                    return Ok(new ApiResponse<List<InvoiceDto>> { StatusCode = 200, Message = ApiResponseMessages.InvoiceList, Result = invoices });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns specific invoice
        /// </summary>
        /// <returns>A requested invoice</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/invoice/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested invoice</response>
        /// <response code="404">invoice not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("invoice/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                InvoiceDto? invoice = _invoice.GetInvoiceById(id);
                if (invoice == null)
                {
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.InvoiceNotFound });
                }
                else
                    return Ok(new ApiResponse<InvoiceDto> { StatusCode = 200, Message = ApiResponseMessages.InvoiceDetails, Result = invoice });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates a invoice.
        /// </summary>
        /// <returns>A newly created invoice</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/invoice
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created invoice</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost("invoice")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(InvoiceDto newInvoice)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    _invoice.CreateInvoice(newInvoice);
                    return Ok(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.InvoiceCreated });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates a invoice.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/invoice/{id}
        ///
        /// </remarks>
        /// <response code="200">invoice updated successfully</response>
        /// <response code="404">invoice not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpPut("invoice/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, InvoiceDto invoice)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    InvoiceDto? updatedInvoice = _invoice.UpdateInvoice(id, invoice);
                    if (updatedInvoice == null)
                        return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.InvoiceNotFound });
                    else
                        return Ok(new ApiResponse<InvoiceDto> { StatusCode = 200, Message = ApiResponseMessages.InvoiceUpdated, Result = updatedInvoice });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a invoice.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/invoice/{id}
        ///
        /// </remarks>
        /// <response code="200">invoice deleted successfully</response>
        /// <response code="404">invoice not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpDelete("invoice/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                bool invoice = _invoice.DeleteInvoice(id);
                if (invoice)
                {
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.InvoiceDeleted });
                }
                else
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.InvoiceNotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
