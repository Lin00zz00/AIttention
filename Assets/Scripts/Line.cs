using UnityEngine;

public class Line
{
    private LineRenderer lineRenderer;
    private GameObject lineObject;

    public Line(Vector3 startPoint, Vector3 endPoint, float width, Material material)
    {
        // 创建一个新的GameObject来持有LineRenderer组件
        lineObject = new GameObject("Line");
        lineRenderer = lineObject.AddComponent<LineRenderer>();

        // 设置线条的基本属性
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.material = material;
        lineRenderer.positionCount = 2;

        // 设置线条的起点和终点
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    // 更新线条位置
    public void UpdatePositions(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    // 设置线条宽度
    public void SetWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    // 设置线条材质
    public void SetMaterial(Material material)
    {
        lineRenderer.material = material;
    }

    // 销毁线条
    public void Destroy()
    {
        if (lineObject != null)
        {
            Object.Destroy(lineObject);
        }
    }
}
