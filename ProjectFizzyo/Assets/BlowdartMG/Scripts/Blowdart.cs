using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowdart : MonoBehaviour {

    // Variables determine how fast and high the dart moves.
    [Header("Controls how fast and high the dart moves:")]
    public float speed = 5f;
    public float height = 5f;

    // Input breath currently always at max for testing.
    [Header("Power of the breath (for testing):")]
    public float breath = 100f;

    // Has the dart been shot.
    private bool fired = false;

    // The darts rigidbody and starting pos
    private Rigidbody2D rb;
    private Vector3 startingPos;

    // Initialises the variables
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
	}
	
	void Update ()
    {
        handleInput();

        if (!fired)
        {
            moveUpDown();
        } 
	}

    // TO DO: Remove the debug space bar.
    // Works off of button press and space bar for debugging.
    void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fired = true;
            fireDart();
        }
    }

    // Moves the dart up and down via a sine wave.
    void moveUpDown()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Fire the dart based on input breath.
    void fireDart()
    {
        rb.velocity = new Vector2(breath, 0);
    }

    // When the dart hits a trigger collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // It has hit the reset collider and should be moved back to the starting location.
        if (collision.gameObject.tag == "Reset")
        {
            rb.velocity = new Vector2(0, 0);
            fired = false;
            transform.position = startingPos;
        }

        // TO DO: Add scoring functionality here.
        // It has hit a balloon. 
        if (collision.gameObject.tag == "Balloon")
        {
            Debug.Log("Pop");
        }
    }
}
