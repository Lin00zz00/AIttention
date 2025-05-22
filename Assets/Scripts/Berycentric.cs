using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class AddBarycentric : MonoBehaviour
{
    void Start()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        var tris = mesh.triangles;
        var bary = new Vector3[mesh.vertexCount];

        for (int i = 0; i < tris.Length; i += 3)
        {
            bary[tris[i]] = new Vector3(1, 0, 0);
            bary[tris[i + 1]] = new Vector3(0, 1, 0);
            bary[tris[i + 2]] = new Vector3(0, 0, 1);
        }
        mesh.SetUVs(1, new System.Collections.Generic.List<Vector3>(bary));
    }
}