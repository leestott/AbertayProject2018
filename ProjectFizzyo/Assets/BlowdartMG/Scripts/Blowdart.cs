using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class Blowdart : MonoBehaviour {

    // Variables determine how fast and high the dart moves.
    [Header("Controls how fast and high the dart moves:")]
    public float speed = 5f;
    public float height = 5f;

    // Input breath currently always at max for testing.
    [Header("Power of the dart (for testing):")]
    public float dartPower = 100.0f;

    // Has the dart been shot.
    private bool fired = false;

    // The darts rigidbody and starting pos
    private Rigidbody2D rb;
    private Vector3 startingPos;

    BreathRecogniser br = new BreathRecogniser();
    private float breathPressure;

    BreathMetre breathMetre;

    // Initialises the variables
    void Start ()
    {
        breathMetre = FindObjectOfType<BreathMetre>();

        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;

        br.BreathStarted += Br_BreathStarted;
        br.BreathComplete += Br_BreathComplete;
    }
	
	void Update ()
    {
        // Detect breath pressure but only if below a fixed value to prevent bad breaths.
        dartPower = breathMetre.fillAmount * 50;

        HandleInput();

        if (!fired)
        {
            MoveUpDown();
        } 
	}

    // TO DO: Remove the debug space bar.
    // Works off of button press and space bar for debugging.
    void HandleInput()
    {
        // Fire the dart on fizzyo button input.
        if (Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown())
        {
            fired = true;
            FireDart();
        }
    }

    // Moves the dart up and down via a sine wave.
    void MoveUpDown()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Fire the dart based on input breath.
    void FireDart()
    {
        rb.velocity = new Vector2(dartPower, 0);
        dartPower = 0;
        GameObject.Find("UIManager").GetComponent<BreathMetre>().reset = true;
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
            //Debug.Log("Pop");
        }
    }

    private void Br_BreathStarted(object sender)
    {
        br.MaxBreathLength = FizzyoFramework.Instance.Device.maxBreathCalibrated;
        br.MaxPressure = FizzyoFramework.Instance.Device.maxPressureCalibrated;
    }

    private void Br_BreathComplete(object sender, ExhalationCompleteEventArgs e)
    {
        Debug.LogFormat("Breath Complete.\n Results: Quality [{0}] : Percentage [{1}] : Breath Full [{2}] : Breath Count [{3}] ", e.BreathQuality, e.BreathPercentage, e.IsBreathFull, e.BreathCount);
    }
}
