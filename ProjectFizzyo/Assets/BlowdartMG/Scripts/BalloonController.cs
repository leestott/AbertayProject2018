using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {

    // How fast the balloon moves.
    [Header("Speed of the balloons movement:")]
    public float speed;

    // Spritesheet of all the balloons.
    [Header("The spritesheet of the different balloons:")]
    public Sprite[] balloonTypes;

    // Keeps track of the balloon chosen on the spritesheet.
    private int whichBalloon;

    // Animator for the baloons shadow.
    private Animator shadowAnimator;

    // When scene loads, load all of the sprites from the spritesheet.
    private void Awake()
    {
        balloonTypes = Resources.LoadAll<Sprite>("BalloonSpriteSheet");
    }

    // Pick a random range for the balloons speed, initialise the animator and pick a balloon sprite.
    private void Start()
    {
        speed = Random.Range(0.1f, 3.0f);
        shadowAnimator = GetComponentInChildren<Animator>();
        whichBalloon = Random.Range(0, 3);
        GetComponent<SpriteRenderer>().sprite = balloonTypes[whichBalloon];
    }

    void Update ()
    {
        moveUpDown();
	}

    // Move the balloon up and down based on a sine wave and the balloons speed.
    void moveUpDown()
    {
        float newY = Mathf.Sin(Time.time * speed) * 3.5f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // When the balloon is hit by another collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When hit by the dart, pop the balloon.
        if (collision.gameObject.tag == "Dart")
        {
            DebugManager.SendDebug("Been hit by dart", "Blowdart");
            popBalloon();
        }
    }

    // Animates through the popping animation, triggers the shadow pop animation and the destroys the object.
    private void popBalloon()
    {
        shadowAnimator.SetTrigger("Pop");

        whichBalloon += 4;
        GetComponent<SpriteRenderer>().sprite = balloonTypes[whichBalloon];
        whichBalloon += 4;
        GetComponent<SpriteRenderer>().sprite = balloonTypes[whichBalloon];

        // Has a delay on the destuction of the game object
        Destroy(gameObject, 0.2f);
    }
}
