using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlsManager : MonoBehaviour
{
    public static event Action<Vector2> OnTap;
    
    private void Update()
    {
        DetectTap();
    }

    private void DetectTap()
    {
        // Check for touch input (for mobile devices)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Detect tap (when the touch has ended)
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 tapPosition = touch.position;
                Debug.Log("Tap detected at position: " + tapPosition);
                OnTap?.Invoke(tapPosition);
            }
        }
        
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPosition = Input.mousePosition;
            Debug.Log("Tap detected at position: " + tapPosition);
            OnTap?.Invoke(tapPosition);
        }
    }
    
    public static bool IsPointerOverUI(Vector2 position)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = position
        };
        
        List<RaycastResult> results = new List<RaycastResult>();
        
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
