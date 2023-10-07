using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeQuillotineBlade : MonoBehaviour
{
    public float rewindTime = 8f;
    public float fallTime = 1f;
    public float disableTime = 5f;
    public float dropSpeed = 0.7f;
    public bool isRewinding = false;
    public bool hitCar = false;
    private Vector3 initialPosition;
    public CarMovement CarMovement;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
          

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isRewinding)
        {
            if (other.gameObject.CompareTag("Frenchman"))
            {
                Debug.Log("Drop blade on Frenchman");
                StartCoroutine(BladeFall(other.gameObject));
                isRewinding = true;
            }
            if (other.gameObject.CompareTag("Car"))
            {
                Debug.Log("Drop blade on Car");
                StartCoroutine(BladeFall(other.gameObject));
                isRewinding = true;
                hitCar = true;

            }

        }
    }
    private IEnumerator BladeFall(GameObject Car)
    {

        float startTime = Time.time;
        Vector3 currentPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x,dropSpeed,transform.position.z);

        while (Time.time - startTime < fallTime)
        {
            float t = (Time.time - startTime) / fallTime;
            transform.position = Vector3.Lerp(currentPos, endPos, t);
            yield return null;
        }
        Debug.Log("Blade is falling...");
        yield return new WaitForSeconds(0.5f);
        DisableGameObjectForSeconds(Car, disableTime);
        StartCoroutine(RewindBlade());
    }

    private IEnumerator RewindBlade()
    {
        yield return new WaitForSeconds(3f);

        float startTime = Time.time;
        Vector3 currentPos = transform.position;


        while (Time.time - startTime < rewindTime)
        {
           float t = (Time.time - startTime) / rewindTime;
            transform.position = Vector3.Lerp(currentPos, initialPosition, t);
            yield return null;
        }
        transform.position = initialPosition;
        isRewinding = false;
        Debug.Log("Blade is rewinding...");
        yield return new WaitForSeconds(0.5f);
        isRewinding = false;
    }
    private void DisableGameObjectForSeconds(GameObject Car, float seconds)
    {
        if (hitCar == true)
        {
            CarMovement.Controllable = false;
        }
            StartCoroutine(EnableGameObjectDelayed(Car, seconds));
        
    }

    private IEnumerator EnableGameObjectDelayed(GameObject Car, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CarMovement.Controllable = true;

    }
}
