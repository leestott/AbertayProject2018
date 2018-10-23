using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    // Each layer that scrolls
    [Header("Scrollable Layers:")]
    public Transform[] layers;

    // The parallax multiplier for each layer
    [Header("Layers Scroll Amount:")]
    [Tooltip("Need same number of scroll amount as number of layers")]
    public float[] parallaxMulti;
    private float parallaxSpeed = 1;

    // Smooths the movement
    [Header("Parallex smoothing:")]
    public float smoothing;

    private Vector3 previousCameraPosition;

	// Use this for initialization
	void Start ()
    {
        previousCameraPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float deltaX = transform.position.x - previousCameraPosition.x;
        float deltaY = transform.position.y - previousCameraPosition.y;

        for (int i = 0; i< layers.Length;i++)
        {
            layers[i].position += Vector3.right * (deltaX * parallaxSpeed);
            layers[i].position += Vector3.up * (deltaY * parallaxSpeed);
        }

        previousCameraPosition = transform.position;
	}
}
