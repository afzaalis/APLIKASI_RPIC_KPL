using Microsoft.AspNetCore.Mvc;
using ManajementReservasiPC_API.Managers;
using ManajementReservasiPC_API.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ManajementReservasiPC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasiController : ControllerBase
    {
        private readonly ReservationManager _reservationManager;
        private readonly ILogger<ReservasiController> _logger;

        public ReservasiController(ILogger<ReservasiController> logger)
        {
            _logger = logger;
            _reservationManager = new ReservationManager();
        }

        [HttpGet("pcs")]
        public ActionResult<IEnumerable<PC>> GetPCs()
        {
            return Ok(_reservationManager.GetPCs());
        }

        [HttpGet("reservations/{pcNumber}")]
        public ActionResult<Reservation> GetReservation(int pcNumber)
        {
            var reservation = _reservationManager.GetReservation(pcNumber);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost("reservations")]
        public ActionResult AddReservation([FromBody] Reservation reservation)
        {
            _logger.LogInformation($"Attempting to add reservation for PC number {reservation.PCNumber}");
            var existingPC = _reservationManager.GetPCs().FirstOrDefault(p => p.Number == reservation.PCNumber);
            if (existingPC == null)
            {
                _logger.LogWarning($"PC number {reservation.PCNumber} not found");
                return NotFound("PC not found");
            }

            if (existingPC.IsReserved)
            {
                _logger.LogWarning($"PC number {reservation.PCNumber} is already reserved");
                return BadRequest("PC is already reserved");
            }

            _reservationManager.AddReservation(reservation);
            _logger.LogInformation($"Successfully added reservation for PC number {reservation.PCNumber}");
            return CreatedAtAction(nameof(GetReservation), new { pcNumber = reservation.PCNumber }, reservation);
        }

        [HttpPut("reservations/{pcNumber}")]
        public ActionResult UpdateReservation(int pcNumber, [FromBody] Reservation updatedReservation)
        {
            var existingReservation = _reservationManager.GetReservation(pcNumber);
            if (existingReservation == null)
            {
                return NotFound();
            }
            _reservationManager.UpdateReservation(pcNumber, updatedReservation);
            return NoContent();
        }

        [HttpDelete("reservations/{pcNumber}")]
        public ActionResult DeleteReservation(int pcNumber)
        {
            _logger.LogInformation($"Attempting to delete reservation for PC number {pcNumber}");
            var reservation = _reservationManager.GetReservation(pcNumber);
            if (reservation == null)
            {
                _logger.LogWarning($"No reservation found for PC number {pcNumber}");
                return NotFound();
            }
            _reservationManager.DeleteReservation(pcNumber);
            _logger.LogInformation($"Successfully deleted reservation for PC number {pcNumber}");
            return NoContent();
        }
    }
}
