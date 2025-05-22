using System.Collections;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject targetObject; // 在 Inspector 里拖入目标对象

    void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            StartCoroutine(ShowAfterDelay(2f));
        }
    }

    IEnumerator ShowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        targetObject.SetActive(true);
    }
}
