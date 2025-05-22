using UnityEngine;
using System.Collections.Generic;

public class LineGenerator : MonoBehaviour
{
    [Header("线条设置")]
    public float lineWidth = 0.01f;        // 线条宽度
    public Material lineMaterial;          // 线条材质
    public float generateInterval = 1.0f;  // 生成线条的时间间隔

    [Header("射线设置")]
    public Camera rayCamera;               // 用于射线检测的摄像机
    public GameObject targetObject;        // 目标物体
    [Tooltip("如果设置了目标物体，将忽略层级设置")]
    public LayerMask targetLayer;         // 目标层级

    private float timer = 0f;
    private List<Line> lines = new List<Line>();

    private void Start()
    {
        // 如果没有指定摄像机，使用主摄像机
        if (rayCamera == null)
        {
            rayCamera = Camera.main;
        }

        // 如果没有指定材质，创建一个默认的材质
        if (lineMaterial == null)
        {
            lineMaterial = new Material(Shader.Find("Sprites/Default"));
            lineMaterial.color = Color.white;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // 每隔指定时间生成一条线
        if (timer >= generateInterval)
        {
            GenerateLine();
            timer = 0f;
        }
    }

    private void GenerateLine()
    {
        Ray ray = rayCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (targetObject != null)
        {
            // 如果指定了目标物体，只检测该物体
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == targetObject)
            {
                CreateLine(hit.point);
            }
        }
        else
        {
            // 否则使用层级检测
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                CreateLine(hit.point);
            }
        }
    }

    private void CreateLine(Vector3 hitPoint)
    {
        Vector3 startPoint = rayCamera.transform.position;
        Vector3 endPoint = hitPoint;
        Line newLine = new Line(startPoint, endPoint, lineWidth, lineMaterial);
        lines.Add(newLine);
    }

    // 清除所有线条
    public void ClearLines()
    {
        foreach (Line line in lines)
        {
            line.Destroy();
        }
        lines.Clear();
    }

    // 设置线条宽度
    public void SetLineWidth(float width)
    {
        lineWidth = width;
        foreach (Line line in lines)
        {
            line.SetWidth(width);
        }
    }

    // 设置线条材质
    public void SetLineMaterial(Material material)
    {
        lineMaterial = material;
        foreach (Line line in lines)
        {
            line.SetMaterial(material);
        }
    }

    private void OnDestroy()
    {
        ClearLines();
    }
} 