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
    public float minPower = 30.0f;

    // Has the dart been shot.
    private bool fired = false;

    // The darts rigidbody and starting pos
    private Rigidbody2D rb;
    private Vector3 startingPos;

    public GameObject blowPipe;

    BreathRecogniser br = new BreathRecogniser();
    private float breathPressure;

    BreathMetre breathMetre;

    public BalloonSpawner balloonSpawner;

    AudioSource audioSource;
    public AudioClip[] dartLaunchSounds;
    public AudioClip[] balloonPoppingSounds;

    private GlobalSessionScore globalSessionScore;
    private bool gameFinished = false;

    [SerializeField]
    private Animator howTo;

    float timePassed = 0;

    // Initialises the variables
    void Start ()
    {
        breathMetre = FindObjectOfType<BreathMetre>();

        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;

        br.BreathStarted += Br_BreathStarted;
        br.BreathComplete += Br_BreathComplete;

        audioSource = GameObject.FindObjectOfType<AudioSource>();
        globalSessionScore = FindObjectOfType<GlobalSessionScore>();
    }
	
	void Update ()
    {
        timePassed += Time.deltaTime;

        if (!gameFinished)
        {
            // Detect breath pressure but only if below a fixed value to prevent bad breaths.
            dartPower = breathMetre.fillAmount * 50;

            if (dartPower < minPower)
                dartPower = minPower;


            HandleInput();

            if (!fired)
            {
                MoveUpDown();
            }
            else
            {
                float angle;
                float dot = Vector2.right.x * rb.velocity.x + Vector2.right.y * rb.velocity.y;      //Works out dot product
                float det = Vector2.right.x * rb.velocity.y - Vector2.right.y * rb.velocity.x;      //Works out determinant
                angle = Mathf.Atan2(det, dot);                                        //Calculates angle in radians
                angle = angle * (180.0f / 3.1415f);                                 //Converts angle to degrees

                rb.transform.eulerAngles = new Vector3(rb.transform.rotation.eulerAngles.x, rb.transform.eulerAngles.y, angle);
            }
        }
    }

    // TO DO: Remove the debug space bar.
    // Works off of button press and space bar for debugging.
    void HandleInput()
    {
        if (timePassed > 2.0f)
        {
            // Fire the dart on fizzyo button input.
            if ((Input.GetKeyDown(KeyCode.Space) || FizzyoFramework.Instance.Device.ButtonDown()) && !fired)
            {
                fired = true;
                breathMetre.lockBar = true;
                FireDart();
            }
        }
    }

    // Moves the dart up and down via a sine wave.
    void MoveUpDown()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        blowPipe.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Fire the dart based on input breath.
    void FireDart()
    {
        rb.transform.position = blowPipe.transform.position;
        int soundSelection = Random.Range(0, dartLaunchSounds.Length);
        audioSource.PlayOneShot(dartLaunchSounds[soundSelection]);
        rb.velocity = new Vector2(dartPower, 0);
        dartPower = 0;

    }

    // When the dart hits a trigger collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // It has hit the reset collider and should be moved back to the starting location.
        if (collision.gameObject.tag == "Reset")
        {
            if (globalSessionScore.boxDisplayed == false)
            {
                globalSessionScore.EndSessionScore();
            }
            else
            {
                gameFinished = true;
                rb.gravityScale = 0;
            }

            breathMetre.reset = true;
            rb.velocity = new Vector2(0, 0);
            rb.transform.eulerAngles = new Vector3(0.0f,0.0f,0.0f);
            fired = false;
            transform.position = startingPos;
            blowPipe.transform.position = startingPos;


            if (balloonSpawner.AllPopped())
            {
                balloonSpawner.NextLevel();
            }
        }

        // TO DO: Add scoring functionality here.
        // It has hit a balloon. 
        if (collision.gameObject.tag == "Balloon")
        {
            balloonSpawner.BalloonPopped();
            AchievementTracker.PopBalloon_Ach();
            int soundSelection = Random.Range(0, balloonPoppingSounds.Length);
            audioSource.PlayOneShot(balloonPoppingSounds[soundSelection]);
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
