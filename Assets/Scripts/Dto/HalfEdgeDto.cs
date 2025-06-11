
using System;
using MessagePack;

namespace Dto
{
    [MessagePackObject]
    public class HalfEdgeDto
    {
        [Key(0)]
        public Vector2DDto Start { get; set; }

        [Key(1)]
        public Vector2DDto End { get; set; }

        [Key(2)]
        public Guid WallId { get; set; }
    }
}
