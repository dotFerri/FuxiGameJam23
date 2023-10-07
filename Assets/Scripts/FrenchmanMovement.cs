using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class FrenchmanMovement : MonoBehaviour
{
    public string playerObjectName = "Car";
    public float pullForce = 1f;
    public float dodgeForce = -2f;
    private Transform player;
    private Rigidbody rb;
    public bool isDodging = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find(playerObjectName);
        player = playerObject.transform;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if ((distanceToPlayer > 30) && (isDodging == false))  // If the Frenchmen are further than 30 meters from the target, they´ll move a bit more slow
            {
                Vector3 towardPlayerDirection = directionToPlayer.normalized;   // Calculate a direction toward the player
                rb.velocity = towardPlayerDirection * pullForce;    // Set the velocity to chase after the player
                rb.freezeRotation = true;

            }
            else if ((distanceToPlayer <= 30) && (distanceToPlayer > 3) && (isDodging == false))   // And they´ll sprint towards the player once they are close enough
            {
                // Dodge maneuver
                Vector3 towardPlayerDirection = directionToPlayer.normalized;
                rb.velocity = towardPlayerDirection * pullForce * 2;    // Set the velocity to chase after the player
                rb.freezeRotation = true;
  //              Debug.Log("CHASE");
            }
            // UNFINISHED - -
            else if (distanceToPlayer <= 2)     // They should try to dodge the player if the player car is coming straight towards...
            {
                isDodging = true;
                
                Vector3 towardPlayerDirection = directionToPlayer.normalized;   // Calculate a direction toward the player
                rb.velocity = towardPlayerDirection * pullForce * -2;    // Set the velocity to chase after the player
                rb.freezeRotation = true;
 //               Debug.Log("DODGE!");
            }
 /*           else
            {
                // Calculate the desired velocity based on pullForce
                Vector3 desiredVelocity = directionToPlayer.normalized * pullForce;
                // Set the velocity of the Rigidbody
                rb.velocity = desiredVelocity;
            }
   */     }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.GetComponent<CarMovement>())
            {
                if (collision.gameObject.GetComponent<CarMovement>().isLethal == true)
                {
                    rb.freezeRotation = false;
                    Destroy(gameObject, 1.5f);
//                    Debug.Log("DESTROY");
                }
            }
        }
    }
}
