using Grpc.Core;
using GrpcService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Services
{
    public class SensorService : Sensor.SensorBase
    {
        private readonly SensorDbContext _dbContext;

        public SensorService(SensorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task GetAll(Empty em, IServerStreamWriter<SensorVal> responseStream, ServerCallContext context)
        {
            var sensorValues = await _dbContext.SensorValues.ToListAsync();
            foreach (var sv in sensorValues)
            {
                await responseStream.WriteAsync(new SensorVal
                {
                    Id = sv.Id,
                    RoomId = sv.RoomId,
                    NotedDate = sv.NotedDate.ToString(),
                    Temp = sv.Temp,
                    Outin = sv.Outin
                });
            }
        }

        public override async Task<SensorVal> GetById(SensorValId sensorValid, ServerCallContext context)
        {
            var sv = await _dbContext.SensorValues.FindAsync(sensorValid.Id);

            return await Task.FromResult(new SensorVal
            {
                Id = sv.Id,
                RoomId = sv.RoomId,
                NotedDate = sv.NotedDate.ToString(),
                Temp = sv.Temp,
                Outin = sv.Outin
            });
        }

        public override async Task<SensorVal> AddValue(SensorVal sensorVal, ServerCallContext context)
        {
            var sv = new SensorValue()
            {
                Id = sensorVal.Id,
                RoomId = sensorVal.RoomId,
                NotedDate = DateTime.Now,
                Temp = sensorVal.Temp,
                Outin = sensorVal.Outin
            };

            await _dbContext.SensorValues.AddAsync(sv);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new SensorVal
            {
                Id = sv.Id,
                RoomId = sv.RoomId,
                NotedDate = sv.NotedDate.ToString(),
                Temp = sv.Temp,
                Outin = sv.Outin
            });
        }

        public override async Task<SensorVal> UpdateValue(SensorVal sensorVal, ServerCallContext context)
        {
            var sv = await _dbContext.SensorValues.FindAsync(sensorVal.Id);
            sv.Temp = sensorVal.Temp;
            sv.Outin = sensorVal.Outin;
            sv.RoomId = sensorVal.RoomId;
            sv.NotedDate = DateTime.Now;

            _dbContext.SensorValues.Update(sv);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new SensorVal
            {
                Id = sv.Id,
                RoomId = sv.RoomId,
                NotedDate = sv.NotedDate.ToString(),
                Temp = sv.Temp,
                Outin = sv.Outin
            });
        }

        public override async Task<Empty> DeleteValue(SensorValId sensorValId, ServerCallContext context)
        {
            var sv = await _dbContext.SensorValues.FindAsync(sensorValId.Id);

            _dbContext.SensorValues.Remove(sv);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new Empty { });
        }
    }
}
