using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    public float movementSpeed;
    public float turnSpeed;

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(turn, 0) && Mathf.Approximately(movement, 0))
            movement = 0.5f;

        // Restrict movement to the X/Z plane
        Vector3 movementVec = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
        
        // Thanks to Unity, we have to implement a velocity system ourselves ._.
        velocity = Vector3.Lerp(velocity, movementVec * movement * movementSpeed, Time.deltaTime);

        transform.position += velocity * Time.deltaTime;

        // Decelerate the car
        velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime / 2f);

        // Turn the car
        transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
    }
}
