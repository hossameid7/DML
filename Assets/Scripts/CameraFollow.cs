using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public float smoothSpeed = 0.5f;

    //���������� ������ �� ������� �� � � � ����, �� z ������ �� ����� ����������.
    void Update()
    {
        Vector3  smoothPos = Vector3.Slerp(this.transform.position, new Vector3(followTransform.position.x, followTransform.position.y, -10), smoothSpeed);
        this.transform.position = smoothPos;
    }
}
