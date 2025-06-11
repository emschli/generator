using MessagePack;

namespace Dto
{
    [MessagePackObject]
    public class Vector2DDto
    {
        [Key(0)]
        public float x;

        [Key(1)]
        public float y;

        public Vector2DDto(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

