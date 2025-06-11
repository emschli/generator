using System.Collections.Generic;
using System.IO;
using Dto;
using UnityEngine;

namespace Generator3D
{
    public class Generator3D : MonoBehaviour
    {
        public string path = "/home/mirjam/map-editor-saves/walls.dat";
        public float wallHeight = 10.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            byte[] data = File.ReadAllBytes(path);
            Save save = MessagePack.MessagePackSerializer.Deserialize<Save>(data);
            List<WallDto> wallDtos = save.Walls;

            foreach (WallDto wallDto in wallDtos)
            {
                GenerateWall(wallDto, save.Thickness);
            }
        }

        private void GenerateWall(WallDto wallDto, float thickness)
        {
            Vector3 start = new Vector3(
                wallDto.OuterStart.x,
                wallDto.OuterStart.y,
                0
            );

            Vector3 end = new Vector3(
                wallDto.OuterEnd.x,
                wallDto.OuterEnd.y,
                0
            );

            Vector3 length = end - start;

            // The wallParent represents the outerStart of the wall
            GameObject wallParent = new GameObject($"Wall_{wallDto.OuterStart.x}_{wallDto.OuterStart.y}_{wallDto.OuterEnd.x}_{wallDto.OuterEnd.y}");
            wallParent.transform.position = start;
            wallParent.transform.SetParent(transform, true);

            // Create the actual wall object
            GameObject wallObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallObject.transform.parent = wallParent.transform;

            // Create and assign Material
            Material wallMaterial = new Material(Shader.Find("Standard"));
            wallMaterial.color = Color.gray;
            wallObject.GetComponent<Renderer>().material = wallMaterial;

            Vector3 scale = new Vector3(
                thickness,
                wallHeight,
                length.magnitude
            );

            wallObject.transform.localScale = scale;

            // Now Offset the wall so that the start of the wall object
            // is at the position of the parent
            Vector3 offset = new Vector3(
                0,
                scale.y / 2.0f,
                scale.z / 2.0f
            );
            wallObject.transform.localPosition = offset;
        }
    }
}
