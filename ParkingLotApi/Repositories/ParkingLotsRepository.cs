﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParkingLotApi.Models;

namespace ParkingLotApi.Repositories
{
    public class ParkingLotsRepository : IParkingLotsRepository
    {
        private readonly IMongoCollection<ParkingLot> parkingLotsCollection;
        public ParkingLotsRepository(IOptions<ParkingLotDatabaseSettings> parkingLotDBSettings)
        {
            var mongoClient = new MongoClient(parkingLotDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(parkingLotDBSettings.Value.DatabaseName);
            parkingLotsCollection = mongoDatabase.GetCollection<ParkingLot>(parkingLotDBSettings.Value.CollectionName);
        }
        public async Task<ParkingLot> CreateParkingLot(ParkingLot parkingLot)
        {
            await parkingLotsCollection.InsertOneAsync(parkingLot);
            return await parkingLotsCollection.Find(x => x.Name == parkingLot.Name).FirstAsync();
        }
    }
}
