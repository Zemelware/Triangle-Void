using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetector : MonoBehaviour
{
    [HideInInspector] public int currentTouchID;
    [HideInInspector] public bool isTouching = false;

    private void Start()
    {
#if !UNITY_IOS && !UNITY_ANDROID
        gameObject.SetActive(false);
#endif
    }

    private void Update()
    {
        if (isTouching)
        {
            if (isStillTouching() == false)
                isTouching = false; // Player took off touch
            else
                return; // Still touching
        }
        else
            TryToFindTouch();
    }

    private bool isStillTouching()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == currentTouchID)
            {
                return true;
            }
        }

        return false;
    }

    private void TryToFindTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                continue; // Touched UI, ignore this touch

            isTouching = true;
            currentTouchID = touch.fingerId; // Found good touch, start following it
            return;
        }
    }
}