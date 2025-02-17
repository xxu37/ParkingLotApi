﻿using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Exceptions;
using ParkingLotApi.Models;
using ParkingLotApi.Repositories;

namespace ParkingLotApi.Services
{
    public class ParkingLotsService
    {
        private readonly IParkingLotsRepository parkingLotsRepository;

        public ParkingLotsService(IParkingLotsRepository parkingLotsRepository)
        {
            this.parkingLotsRepository = parkingLotsRepository;
        }

        public async Task<ParkingLot> CreateAsync(ParkingLotDto parkingLotDto)
        {
            if (parkingLotDto.Capacity < 10)
            {
                throw new InvalidCapacityException("Capacity should larger than 10");
            }
            var parkingLotList = await parkingLotsRepository.GetAllParkingLot();
            if(parkingLotList.Exists(_ => parkingLotDto.Name == _.Name))
            {
                throw new NameConflitedException("Name Already existed");
            }
            return await parkingLotsRepository.CreateParkingLot(parkingLotDto.ToEntity());
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var isDelete = await parkingLotsRepository.RemoveParkingLot(id);
            if (!isDelete)
            {
                throw new IdNotFoundException("Delete ID does not exist");
            }
            return true;
        }

        public async Task<List<ParkingLot>> GetPageAsync(int pageIndex)
        {
            var parkingLotList = await parkingLotsRepository.GetAllParkingLot();
            return parkingLotList.Skip((int)((pageIndex - 1) * 15)).Take(15).ToList();
        }

        public async Task<ParkingLot> GetAsync(string id)
        {
            var parkingLot = await parkingLotsRepository.GetParkingLotById(id);
            if(parkingLot == null)
            {
                throw new IdNotFoundException("Get ID does not exist");
            }
            return parkingLot;
        }

        public async Task<ParkingLot> UpdateCapacityAsync(string id, UpdateParkingLotDto updateParkingLotDto)
        {
            if (updateParkingLotDto.Capacity < 10)
            {
                throw new InvalidCapacityException("Capacity should larger than 10");
            }

            var parkingLot = await parkingLotsRepository.UpdateParkingLotCapacity(id, updateParkingLotDto.Capacity);
            if (parkingLot == null)
            {
                throw new IdNotFoundException("Get ID does not exist");
            }
            return parkingLot;
        }
    }
}
