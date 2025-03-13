using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Vector3 destination; // Destination position (assign in the Unity editor)
    public float speed = 2f; // Movement speed

    void OnTriggerEnter(Collider other)
    {
        // Move the object towards the destination
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}