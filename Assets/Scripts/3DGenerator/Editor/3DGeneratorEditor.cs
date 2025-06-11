
using UnityEditor;
using UnityEngine;

namespace Generator3D
{
    [CustomEditor(typeof(Generator3D))]
    public class Generator3DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Generate"))
            {
                Generator3D myScript = (Generator3D)target;
                myScript.Generate();
            }
        }
    }
}
