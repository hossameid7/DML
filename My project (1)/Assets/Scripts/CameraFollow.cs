using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public float smoothSpeed = 0.5f;
    public float yOffset = 2f;
    public float zOffset = -10f;
    public float xOffset;
    public float leftMostBorder = 930f;
    public float rightMostBroder = 930f;

    //Следование камеры за игроком по у и х осям, по z всегда на одном расстоянии.
    void Update()
    {
        Vector3 smoothPos = Vector3.Lerp(this.transform.position, 
                                         new Vector3(this.transform.position.x, this.transform.position.y, Median(leftMostBorder, 
                                                                                                                  followTransform.position.z,
                                                                                                                  rightMostBroder)),
                                         smoothSpeed);
        this.transform.position = smoothPos;
    }

    float Median(float a, float b, float c)
    {
        return Mathf.Max(Mathf.Min(a, b), Mathf.Min(Mathf.Max(a, b), c));
    }
}
