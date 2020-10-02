using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructWhenOutOfCameraView : MonoBehaviour
{

    public int selfDestructPadding = 5;

    void FixedUpdate()
    {
        if (!Utils.IsInCameraBoundsWithPadding2d(new Vector2(transform.position.x, transform.position.y), selfDestructPadding))
        {
            Destroy(gameObject);
        }
    }
}
