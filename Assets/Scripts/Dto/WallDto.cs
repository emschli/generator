using System;
using MessagePack;

namespace Dto
{
    [MessagePackObject]
    public class WallDto
    {
        [Key(0)]
        public Guid WallId { get; set; }

        [Key(1)]
        public Vector2DDto InnerStart { get; set; }

        [Key(2)]
        public Vector2DDto InnerEnd { get; set; }

        [Key(3)]
        public Vector2DDto OuterStart { get; set; }

        [Key(4)]
        public Vector2DDto OuterEnd { get; set; }
    }
}
