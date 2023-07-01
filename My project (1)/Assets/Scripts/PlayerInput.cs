using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput I;
    public float inputZ = 0f;
    public bool jump = false;
    public bool grounded = true;
    private void Awake() {
        if(!I)I=this;
    }

    // Update is called once per frame
    void Update()
    {
        inputZ = Input.GetAxis("Horizontal");

        jump = Input.GetButtonDown("Jump");
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Ground")){
            grounded = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if(other.collider.CompareTag("Ground")){
            grounded = false;
        }
    }
}
