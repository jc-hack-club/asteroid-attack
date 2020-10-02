using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    //http://answers.unity.com/answers/780069/view.html
    public static Bounds getCameraBounds()
    {
        Camera camera = Camera.main;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    public static bool IsInCameraBounds2d(Vector2 pos) { 
        return IsInCameraBoundsWithPadding2d(pos, 0);
    }

    public static bool IsInCameraBoundsWithPadding2d(Vector2 pos,float padding)
    {
        Bounds bounds = getCameraBounds();
        bounds.size = new Vector3(bounds.size.x + padding*2 , bounds.size.y + padding*2);

        Vector3 posVector3 = new Vector3(pos.x,pos.y,bounds.center.z);

        return bounds.Contains(posVector3);
    }
}
