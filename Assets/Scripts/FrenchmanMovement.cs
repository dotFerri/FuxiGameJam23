using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class FrenchmanMovement : MonoBehaviour
{
    public string playerObjectName = "Car";
    public float FrenchmanSpeed = 10f;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find(playerObjectName);
            player = playerObject.transform;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            GetComponent<Rigidbody>().AddForce(directionToPlayer.normalized * FrenchmanSpeed, ForceMode.Force);

        }

    }

}
