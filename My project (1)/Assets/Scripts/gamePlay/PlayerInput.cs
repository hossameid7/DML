using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput I;
    public bool grounded = true;
    public float speed = 5f;

    public Rigidbody rb;
    private void Awake() {
        if(!I)I=this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical,transform.position.y,moveHorizontal);
        rb.velocity = movement * speed;
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
