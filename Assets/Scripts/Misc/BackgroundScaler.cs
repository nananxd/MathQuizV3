using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private void OnEnable()
    {
        AdjustBackground();
    }
    public void AdjustBackground()
    {
        RectTransform rt = GetComponent<RectTransform>();
        //rt.position = new Vector3(0, 0, rt.position.z);
        float camHeight = Camera.main.orthographicSize * 2;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, camHeight);
        float targetRectWidth = camHeight * Camera.main.aspect;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetRectWidth);
    }

    

   
}
