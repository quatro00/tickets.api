﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Helpers;
using tickets.api.Models;
using tickets.api.Models.DTO.Ticket;
using tickets.api.Models.Specifications;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Responsable
{
    [Route("api/responsable/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITicketRepository ticketRepository;
        private readonly ICatEstatusTicketRepository catEstatusTicketRepository;

        public TicketController(
            IMapper mapper,
            ITicketRepository ticketRepository,
            ICatEstatusTicketRepository catEstatusTicketRepository
            )
        {
            this.mapper = mapper;
            this.ticketRepository = ticketRepository;
            this.catEstatusTicketRepository = catEstatusTicketRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Responsable de area")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> Crear([FromForm] CrearTicketDto model)
        {
            // Validar si el modelo es válido
            if (!ModelState.IsValid)
            {
                User.GetId();
                return BadRequest("Modelo de datos invalido.");
            }

            try
            {
                // Devolver la respuesta con el nuevo paciente
                string rutaPublica = $"{Request.Scheme}://{Request.Host}/archivos/";

                await this.ticketRepository.CrearTicket(model, User.GetId(), rutaPublica);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }
        }

        [HttpGet("GetTicketsAbiertos")]
        [Authorize(Roles = "Responsable de area")]
        public async Task<IActionResult> GetTicketsAbiertos()
        {
            try
            {
                GetTicketsAbiertosDto model = new GetTicketsAbiertosDto();
                var result = await this.ticketRepository.GetTicketsAbiertosResponsable(model, User.GetId());
                if (result == null)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }
        }

        [HttpGet("GetDetalle/{id}")]
        [Authorize(Roles = "Responsable de area")]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            try
            {

                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = true, Id = id };
                var spec = new TicketSpecification(filtro);
                spec.IncludeStrings = new List<string> { "Area", "EstatusTicket", "Prioridad", "TicketArchivos", "TicketHistorials", "UsuarioAsignado", "UsuarioCreacion", "TicketHistorials.Usuario", "TicketHistorials.TicketHistorialArchivos" };


                var result = await this.ticketRepository.ListAsync(spec);
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<GetTicketDetalleDto>(result.FirstOrDefault());

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }
        }

        [HttpPost("AgregarMensaje")]
        [Authorize(Roles = "Responsable de area")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> AgregarMensaje([FromForm] AgregarMensajeDto model)
        {
            // Validar si el modelo es válido
            if (!ModelState.IsValid)
            {
                User.GetId();
                return BadRequest("Modelo de datos invalido.");
            }

            try
            {
                // Devolver la respuesta con el nuevo paciente
                string rutaPublica = $"{Request.Scheme}://{Request.Host}/archivos/";

                await this.ticketRepository.AgregarMensaje(model, User.GetId(), rutaPublica);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }
        }

        [HttpPost("AgregarArchivos")]
        [Authorize(Roles = "Responsable de area")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> AgregarArchivos([FromForm] AgregarArchivosDto model)
        {
            // Validar si el modelo es válido
            if (!ModelState.IsValid)
            {
                User.GetId();
                return BadRequest("Modelo de datos invalido.");
            }

            try
            {
                // Devolver la respuesta con el nuevo paciente
                string rutaPublica = $"{Request.Scheme}://{Request.Host}/archivos/";

                await this.ticketRepository.AgregarArchivos(model, User.GetId(), rutaPublica);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }
        }
    }
}
