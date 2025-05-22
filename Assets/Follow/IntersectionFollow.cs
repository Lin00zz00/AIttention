using UnityEngine;

public class IntersectionFollow : MonoBehaviour
{
    [Header("设置")]
    [Tooltip("需要跟随的物体")]
    public GameObject followObject;
    
    [Tooltip("射线检测的目标物体")]
    public GameObject targetMesh;
    
    [Tooltip("发射射线的相机")]
    public Camera rayCamera;

    [Tooltip("射线最大距离")]
    public float maxDistance = 100f;

    [Tooltip("是否显示调试信息")]
    public bool showDebug = true;

    private void Update()
    {
        if (followObject == null || targetMesh == null || rayCamera == null)
        {
            Debug.LogWarning("有必要组件未设置!");
            return;
        }

        // 从相机发射射线
        Ray ray = rayCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;

        // 进行射线检测
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (showDebug)
            {
                // 绘制射线（绿色表示击中，持续时间为一帧）
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                Debug.Log($"射线击中物体: {hit.collider.gameObject.name} 在位置: {hit.point}");
            }

            // 确认击中的是目标物体
            if (hit.collider.gameObject == targetMesh)
            {
                // 更新跟随物体的位置到交点位置
                followObject.transform.position = hit.point;
                if (showDebug)
                {
                    Debug.Log("成功更新跟随物体位置!");
                }
            }
            else if (showDebug)
            {
                Debug.Log($"射线击中了错误的物体: {hit.collider.gameObject.name}");
            }
        }
        else if (showDebug)
        {
            // 射线未击中任何物体时，绘制红色射线
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
            Debug.Log("射线未击中任何物体");
        }
    }
}
