using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bug : MonoBehaviour
{
    void Update()
    {
        // Khi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if (results.Count > 0)
            {
                Debug.Log("--- UI Raycast Hit ---");
                foreach (var hit in results)
                {
                    Debug.Log($"Vật thể đang chặn: {hit.gameObject.name} | Layer: {LayerMask.LayerToName(hit.gameObject.layer)}");
                }
            }
            else
            {
                Debug.LogWarning("Raycast KHÔNG chạm vào bất cứ UI nào. Kiểm tra EventSystem hoặc GraphicRaycaster!");
            }
        }
    }
}