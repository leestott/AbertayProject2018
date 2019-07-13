using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class Blowdart : MonoBehaviour {

    // The tutorial panel.
    [SerializeField]
    private Animator howTo;

    // Variables determine how fast and high the dart moves.
    [Header("Controls how fast and high the dart moves vertically:")]
    public float speed = 5f;
    public float height = 5f;

    // Input breath currently always at max for testing.
    [Header("Power of the dart (for testing) and its multiplier:")]
    public float dartPowerMultiplier = 50.0f;
    public float dartPower = 100.0f;
    public float minPower = 30.0f;

    // The pipe covering the dart.
    public GameObject blowPipe;

    // The darts rigidbody and starting pos.
    private Rigidbody2D rb;
    private Vector3 startingPos;

    // Has the dart been shot.
    private bool fired = false;

    public bool HasBeenFired() { return fired; }

    // Controls the spawning and levels.
    public BalloonSpawner balloonSpawner;

    // The breath metre.
    BreathMetre breathMetre;

    // Timer variable for stopping dart firing with UI.
    float timePassed = 0;

    float resetTimer = 0.0f;
    float resetTime = 1.47f;

    // Shows breath and sets information.
    public SetDisplayInfo setDisplayInfo;

    // Scoring.
    private GlobalSessionScore globalSessionScore;
    private bool gameFinished = false;

    // Audio.
    AudioSource audioSource;
    public AudioClip[] dartLaunchSounds;
    public AudioClip[] balloonPoppingSounds;

    // Initialises the variables
    void Start ()
    {
        // Find the objects in the scene
        breathMetre = FindObjectOfType<BreathMetre>();
        audioSource = GameObject.FindObjectOfType<AudioSource>();
        globalSessionScore = FindObjectOfType<GlobalSessionScore>();

        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
    }
	
	void Update ()
    {
        if (breathMetre.lockBar == false)
        {
            // Increment the timer.
            timePassed += Time.deltaTime;
        }

        if(fired)
        {
            resetTimer += Time.deltaTime;
        }

        if(resetTimer >= resetTime)
        {
            resetTimer = 0.0f;
            ResetDart();
        }

        // Check if popup is displayed if so reset the timer.
        if (setDisplayInfo.GetIsPopupDisplayed())
        {
            timePassed = 0;
        }

        // If the game isn't finished run through the core loop.
        if (!gameFinished)
        {
            // Set the darts power based on the current fill amount multiplied by a constant.
            // Also sets a cap on the speed of the dart if user has gone over bar.
            if (breathMetre.fillAmount >= 1.0f)
            {
                dartPower = dartPowerMultiplier;
                resetTime = 0.6f;
            }
            else
            {
                dartPower = breathMetre.fillAmount * dartPowerMultiplier;
            }

            // Set the power to a minimum if too weak so it doesnt just go straight down.
            if (dartPower < minPower)
            {
                dartPower = minPower;
                resetTime = 1.5f;
            }
            else if(resetTime != 0.6f)
            {
                resetTime = 1.0f;
            }

            // Check for input from the player.
            HandleInput();

            // If not fired move the dart up and down
            // Else the dart has been fired and make it rotate based on its velocity.
            if (!fired)
            {
                MoveUpDown();
            }
            else
            {
                float angle;
                float dot = Vector2.right.x * rb.velocity.x + Vector2.right.y * rb.velocity.y;      //Works out dot product
                float det = Vector2.right.x * rb.velocity.y - Vector2.right.y * rb.velocity.x;      //Works out determinant
                angle = Mathf.Atan2(det, dot);                                                      //Calculates angle in radians
                angle = angle * (180.0f / 3.1415f);                                                 //Converts angle to degrees

                // Apply the new angle.
                rb.transform.eulerAngles = new Vector3(rb.transform.rotation.eulerAngles.x, rb.transform.eulerAngles.y, angle);
            }
        }
    }

    // Works off of button press and space bar for debugging.
    void HandleInput()
    {
        // As long as time has passed ( To stop the dart from firing when the user is clicking for the tutorial ).
        if (timePassed > 0.1f)
        {
            // Fire the dart on fizzyo button input and lock the bar so it cant be filled.
            if ((Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown()) && !fired)
            {
                // Can only fire if they have stopped breathing.
                if (breathMetre.inUse == false)
                {
                    fired = true;
                    breathMetre.lockBar = true;
                    FireDart();
                }
            }
        }
    }

    // Moves the dart ( and the attached blowpipe) up and down via a sine wave.
    void MoveUpDown()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        blowPipe.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Fire the dart based on input breath.
    void FireDart()
    {
        // Ensure the dart comes out of the pipe.
        rb.transform.position = blowPipe.transform.position;

        // Pick a random sound and play on firing.
        int soundSelection = Random.Range(0, dartLaunchSounds.Length);
        audioSource.PlayOneShot(dartLaunchSounds[soundSelection]);

        // Give it a veloctiy for the dart power then reset the dart power.
        rb.velocity = new Vector2(dartPower, 0);
        dartPower = 0;
    }

    void ResetDart()
    {
        // Check if the final score box is not displayed and then check if the session should be finished else the game has ended.
        if (globalSessionScore.boxDisplayed == false)
        {
            globalSessionScore.IsSessionFinished();
        }
        else
        {
            gameFinished = true;
            rb.gravityScale = 0;
        }

        // Reset everything.
        DebugManager.SendDebug("The blowdart is resetting the breath bar in blowdart", "BreathBar");
        breathMetre.reset = true;
        rb.velocity = new Vector2(0, 0);
        rb.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        fired = false;
        transform.position = startingPos;
        blowPipe.transform.position = startingPos;
    }

    // When the dart hits a trigger collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // It has hit a balloon, tell the spawner and the achievements then play a popping sound.
        if (collision.gameObject.tag == "Balloon")
        {
            balloonSpawner.BalloonPopped();
            AchievementTracker.PopBalloon_Ach();
            int soundSelection = Random.Range(0, balloonPoppingSounds.Length);
            audioSource.PlayOneShot(balloonPoppingSounds[soundSelection]);
        }
    }

}
