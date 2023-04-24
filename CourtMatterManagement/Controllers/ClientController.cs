using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.Constants;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtMatterManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _client;
        public ClientController(IClientRepository client)
        {
            _client = client;
        }

        /// <summary>
        /// Returns all Clients
        /// </summary>
        /// <returns>List of cutomers</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/clients
        ///     
        /// </remarks>
        /// <response code="200">Returns List of clients</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("clients")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                List<ClientDto> clients = _client.GetAllClients();
                if (clients.ToList().Count == 0)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.NoClients});
                else
                    return Ok(new ApiResponse<List<ClientDto>> { StatusCode = 200, Message = ApiResponseMessages.ClientList, Result = clients });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns specific client
        /// </summary>
        /// <returns>A requested client</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/client/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested client</response>
        /// <response code="404">client not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("client/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                ClientDto? client = _client.GetClientById(id);
                if (client == null)
                {
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.ClientNotFound });
                }
                else
                    return Ok(new ApiResponse<ClientDto> { StatusCode = 200, Message = ApiResponseMessages.ClientDetails, Result = client });
            }
            catch ( Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates a client.
        /// </summary>
        /// <returns>A newly created client</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/client
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created client</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost("client")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(ClientDto newClient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    _client.CreateClient(newClient);
                    return Ok(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.ClientCreated});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates a client.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/client/{id}
        ///
        /// </remarks>
        /// <response code="200">client updated successfully</response>
        /// <response code="404">client not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpPut("client/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, ClientDto client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ApiResponseMessages.DataFormat });
                }
                else
                {
                    ClientDto? updatedClient = _client.UpdateClient(id, client);
                    if (updatedClient == null)
                        return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.ClientNotFound });
                    else
                        return Ok(new ApiResponse<ClientDto> { StatusCode = 200, Message = ApiResponseMessages.ClientUpdated, Result = updatedClient });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/client/{id}
        ///
        /// </remarks>
        /// <response code="200">client deleted successfully</response>
        /// <response code="404">client not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpDelete("client/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                bool client = _client.DeleteClient(id);
                if (client)
                {
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.ClientDeleted});
                }
                else
                    return NotFound(new ApiResponse<object> { StatusCode = 200, Message = ApiResponseMessages.ClientNotFound});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
