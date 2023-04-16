using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RaycastUtilities
{
    public static List<RaycastResult> GetRaycastResults(Vector2 mousePosition, GraphicRaycaster graphicRaycaster)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();

        graphicRaycaster.Raycast(eventData, results);

        return results;
    }
}
