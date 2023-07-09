using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraulicPress : MonoBehaviour
{
     PauseMenu pauseMenuObject;

    public float cycleTime;
    float oneWayCycleTime;
    public float maxDistance;
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 destination;

    private void Awake()
    {
        pauseMenuObject = FindObjectOfType<PauseMenu>();

        startPosition = transform.position;
        endPosition = transform.position + -Vector3.up * maxDistance;
        destination = endPosition;
        oneWayCycleTime = cycleTime / 2;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, endPosition) < 0.001f)
            destination = startPosition;
        if (Vector3.Distance(transform.position, startPosition) < 0.001f)
            destination = endPosition;

        transform.position = Vector3.MoveTowards(transform.position, destination,  maxDistance / oneWayCycleTime * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pauseMenuObject.SetWinOrGameOver(false);
           
        }
    }
}
