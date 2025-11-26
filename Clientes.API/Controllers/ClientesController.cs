using Clientes.Application.DTOs;
using Clientes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClienteResponseDTO>), 200)]
        public async Task<ActionResult<IEnumerable<ClienteResponseDTO>>> Get()
        {
            var clientes = await _clienteService.ObterTodosAsync();
            return Ok(clientes);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ClienteResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ClienteResponseDTO>> Get(Guid id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
            {
                return NotFound($"Cliente com ID '{id}' não encontrado.");
            }

            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteResponseDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ClienteResponseDTO>> Post([FromBody] ClienteRequestDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var novoCliente = await _clienteService.AdicionarAsync(clienteDTO);

                return CreatedAtAction(nameof(Get), new { id = novoCliente.Id }, novoCliente);
            }
            catch (ApplicationException ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ClienteResponseDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ClienteResponseDTO>> Put(Guid id, [FromBody] ClienteRequestDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var clienteAtualizado = await _clienteService.AtualizarAsync(id, clienteDTO);
                return Ok(clienteAtualizado);
            }
            catch (ApplicationException ex) when (ex.Message.Contains("não encontrado"))
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ApplicationException ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)] 
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _clienteService.DeletarAsync(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}