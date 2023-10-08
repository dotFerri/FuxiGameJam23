using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{

    public float movementSpeed;
    public float turnSpeed;
    public bool isLethal = false;    // Check to see if a collision is lethal
    public bool Controllable = true;
    public int currentScore = 0;
    public int killCount = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshPro LicensePlate;

    private bool title1 = false;
    private bool title2 = false;
    private bool title3 = false;
    private bool title4 = false;
    private bool title5 = false;
    private bool title6 = false;
    private bool title7 = false;

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        if (Controllable)
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


            if (velocity.magnitude > 8)
                isLethal = true;
            else
                isLethal = false;
        }
        else if (!Controllable)
        {
            
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Frenchman")
        {
            currentScore++;
            HandleScore();
        }
    }
    private void HandleScore()
    {
        scoreText.text = "Frenchies Decapitated: " + currentScore;
        if (currentScore > 0 && currentScore < 10)
        {
            LicensePlate.text = "Accidents happen";
        }
        if (currentScore > 10 && !title1 && currentScore < 25)
        {
            LicensePlate.text = "Are you drunk bro?";
            Debug.Log("License Plate: Are you drunk bro?");
            title1 = true;
        }
        if (currentScore > 25 && !title2 && currentScore < 60)
        {
            LicensePlate.text = "Sunday Driver";
            Debug.Log("License Plate: Sunday Driver");
            title2 = true;

        }
        if (currentScore > 60 && !title3 && currentScore < 100)
        {
            LicensePlate.text = "Carmageddon!";
            title3 = true;

        }
        if (currentScore > 100 && !title4 && currentScore < 150)
        {
            LicensePlate.text = "Mass Murderer";
            title4 = true;

        }
        if (currentScore > 150 && !title5 && currentScore < 250)
        {
            LicensePlate.text = "You need rehabilitation";
            title5 = true;

        }
        if (currentScore > 250 && !title6 && currentScore < 500)
        {
            LicensePlate.text = "Revolutionary Slaughterer";
            title6 = true;
        }
        if (currentScore > 500 && !title6)
        {
            LicensePlate.text = "Get a life!";
            title7 = true;
        }
        else if (!title1 && !title2 && !title3 && !title4 && !title5 && !title6 && !title7)
        {
            LicensePlate.text = "";
            Debug.Log("License Plate: (empty)");
        }
    }
    
}
