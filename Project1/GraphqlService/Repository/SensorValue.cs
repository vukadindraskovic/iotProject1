using System;
using System.Collections.Generic;

namespace GraphqlService.Repository
{
    public partial class SensorValue
    {
        public string Id { get; set; } = null!;
        public string RoomId { get; set; } = null!;
        public DateTime NotedDate { get; set; }
        public int Temp { get; set; }
        public string Outin { get; set; } = null!;
    }
}
