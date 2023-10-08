using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class FrenchmanMovement : MonoBehaviour
{
    public float followForce = 4f;
    public float dodgeForce = 3f;
    public float followRadius = 30f;
    public float dodgeRadius = 5f;
    public float lethalHitThreshold = 8f;
    public bool isDodging = false;

    private Transform player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<CarMovement>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            float movementSpeed = followForce;
            if (distanceToPlayer > followRadius && isDodging == false)
            {
                // Move at X/2 (50%) speed.
                movementSpeed /= 2;
            }
            else if (distanceToPlayer < dodgeRadius || isDodging == true)
            {
                // Change movement speed to the opposite direction.
                isDodging = true;
                movementSpeed = -dodgeForce;
                StartCoroutine(ResetDodgingState(1f));
            }
            rb.velocity = directionToPlayer.normalized * movementSpeed;
        }
    }
    private IEnumerator ResetDodgingState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDodging = false;
    }

    private void OnCollisionEnter(Collision collision)
        {
            CarMovement car = collision.gameObject.GetComponent<CarMovement>();
            if (car && collision.relativeVelocity.magnitude < lethalHitThreshold)
            {
                rb.freezeRotation = false;
                Destroy(gameObject, 1.5f);
                Debug.Log("Frenchie Decapitated");
            }
        }
    }
    
