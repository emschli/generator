using MessagePack;

namespace Dto
{
    [MessagePackObject]
    public class DoorDto
    {
        [Key(0)]
        public Vector2DDto LowerLeft { get; set; }

        [Key(1)]
        public Vector2DDto UpperLeft { get; set; }

        [Key(2)]
        public Vector2DDto UpperRight { get; set; }

        [Key(3)]
        public Vector2DDto LowerRight { get; set; }
    }
}
