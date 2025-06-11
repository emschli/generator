using System.Collections.Generic;
using MessagePack;

namespace Dto
{
    [MessagePackObject]
    public class Save
    {
        [Key(0)]
        public List<HalfEdgeDto> halfEdges { get; set; }

        [Key(1)]
        public float Thickness { get; set; }

        [Key(2)]
        public List<WallDto> Walls { get; set; }
    }
}
