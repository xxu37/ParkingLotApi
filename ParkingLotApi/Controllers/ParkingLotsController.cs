﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Exceptions;
using ParkingLotApi.Models;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {
        private readonly ParkingLotsService parkingLotsService;
        public ParkingLotsController(ParkingLotsService parkingLotsService) 
        {
            this.parkingLotsService = parkingLotsService;
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLot>> CreateParkingLotAsync([FromBody] ParkingLotDto parkingLotDto)
        {
            //try
            //{
            return StatusCode(StatusCodes.Status201Created, await parkingLotsService.CreateAsync(parkingLotDto));
            //}
            //catch(InvalidCapacityException ex)
            //{
                //return BadRequest("Capacity should larger than 10");
            //}
        }
    }
}
