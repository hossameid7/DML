using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public float smoothSpeed = 0.5f;
    public float yOffset = 2f;
    public float zOffset = -10f;

    //Следование камеры за игроком по у и х осям, по z всегда на одном расстоянии.
    void Update()
    {
        Vector3 smoothPos = Vector3.Lerp(this.transform.position, new Vector3(followTransform.position.x, yOffset, zOffset), smoothSpeed);
        this.transform.position = smoothPos;
    }
}
