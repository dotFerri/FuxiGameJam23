using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VelocityPitcher : MonoBehaviour
{
    public Transform velocitySource;

    [Min(0.1f)]
    public float peakVelocity;

    [Min(0.02f)]
    public float basePitch = 1f;

    private Vector3 lastPosition;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        lastPosition = Vector3.Scale(velocitySource.position, new Vector3(1, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = Vector3.Scale(velocitySource.position, new Vector3(1, 0, 1));

        float velocity = (currentPosition - lastPosition).magnitude / Time.deltaTime;
        velocity /= peakVelocity;

        source.pitch = Mathf.Lerp(source.pitch, (0.5f + velocity) * basePitch, Time.deltaTime * 5f);

        lastPosition = currentPosition;
    }
}
