using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GurandAnimation : MonoBehaviour
{
    public float Spped;
    public Animator Anim;

    void Update()
    {
        Anim.speed = Spped;
    }

}
