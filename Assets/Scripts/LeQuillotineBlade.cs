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
                StartCoroutine(BladeFallOnFrenchman(other));
                isRewinding = true;
            }
            else if (other.gameObject.CompareTag("Car"))
            {
                Debug.Log("Drop blade on Car");
                StartCoroutine(BladeFallOnCar());
                isRewinding = true;
                hitCar = true;

            }

        }
    }
	private IEnumerator BladeFallOnFrenchman(Collider other)
    {
        // Handle blade fall on Frenchman
        // You can add logic specific to Frenchman here

        yield return new WaitForSeconds(1.5f);

		float startTime = Time.time;
        Vector3 currentPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x,dropSpeed,transform.position.z);

        while (Time.time - startTime < fallTime)
        {
            float t = (Time.time - startTime) / fallTime;
            transform.position = Vector3.Lerp(currentPos, endPos, t);
            yield return null;
        }
        // Now, you can decide whether to rewind the blade or not
        if (hitCar == false)
        {
			Destroy(other.gameObject, 1.5f);
			Debug.Log("A frenchie got decapitated!");
			StartCoroutine(RewindBlade());
        }
    }
	
    private IEnumerator BladeFallOnCar()
    {
		if (CarMovement == null)
		{
			yield break;
		}
        float startTime = Time.time;
        Vector3 currentPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x,dropSpeed,transform.position.z);

        while (Time.time - startTime < fallTime)
        {
            float t = (Time.time - startTime) / fallTime;
            transform.position = Vector3.Lerp(currentPos, endPos, t);
            yield return null;
        }
//        Debug.Log("Blade is falling...");
        yield return new WaitForSeconds(0.5f);
        DisableGameObjectForSeconds(CarMovement.gameObject, disableTime);
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
			        Debug.Log("Disable car controls for 5 seconds.");

        }
            StartCoroutine(EnableGameObjectDelayed(Car, seconds));
    }

    private IEnumerator EnableGameObjectDelayed(GameObject Car, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CarMovement.Controllable = true;
        Debug.Log("Re-enable car controls.");

    }
}
