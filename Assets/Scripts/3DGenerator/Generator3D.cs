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

        private int wallCount = 0;
        private int structureCount = 1;

        [ContextMenu("Generate")]
        public void Generate()
        {
            byte[] data = File.ReadAllBytes(path);
            Save save = MessagePack.MessagePackSerializer.Deserialize<Save>(data);
            List<WallDto> wallDtos = save.Walls;

            GameObject structureParent = new GameObject($"Structure_{structureCount}");
            structureParent.transform.SetParent(transform, true);

            foreach (WallDto wallDto in wallDtos)
            {
                GenerateWall(wallDto, save.Thickness, structureParent);
            }

            wallCount = 0;
            structureCount++;
        }

        private void GenerateWall(WallDto wallDto, float thickness, GameObject structureParent)
        {
            Vector3 start = new Vector3(
                wallDto.OuterStart.x,
                0,
                wallDto.OuterStart.y
            );

            Vector3 end = new Vector3(
                wallDto.OuterEnd.x,
                0,
                wallDto.OuterEnd.y
            );

            Vector3 direction = end - start;

            // Container Object for the wall
            GameObject wallParent = new GameObject($"WallContainer{wallCount++}");
            wallParent.transform.SetParent(structureParent.transform, true);

            // The wallParent represents the outerStart of the wall
            GameObject wallStart = new GameObject($"WallStart_{wallDto.OuterStart.x}_{wallDto.OuterStart.y}");
            wallStart.transform.position = start;
            wallStart.transform.SetParent(wallParent.transform, true);

            GameObject wallEnd = new GameObject($"WallEnd_{wallDto.OuterEnd.x}_{wallDto.OuterEnd.y}");
            wallEnd.transform.position = end;
            wallEnd.transform.SetParent(wallParent.transform, true);

            // Create the actual wall object
            GameObject wallObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallObject.transform.parent = wallStart.transform;

            // Create and assign Material
            Material wallMaterial = new Material(Shader.Find("Standard"));
            wallMaterial.color = Color.gray;
            wallObject.GetComponent<Renderer>().material = wallMaterial;

            Vector3 scale = new Vector3(
                thickness,
                wallHeight,
                direction.magnitude
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

            // Rotate the wall object to align with the wall direction
            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            wallStart.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

    }
}
