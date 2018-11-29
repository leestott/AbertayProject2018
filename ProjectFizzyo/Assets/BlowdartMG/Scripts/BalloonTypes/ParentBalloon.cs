using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class of the balloons, contains generic functions and shared variables.
public class ParentBalloon : MonoBehaviour {

    // How fast the balloon moves.
    [Header("Speed of the balloons movement:")]
    public float speed;

    // The spritesheet for the different balloons.
    [Header("Spritesheet for the balloons:")]
    public Sprite[] balloonSprites;

    // Each balloon type corresponds to a different balloon: 0 = Red, 1 = Blue, 2 = Green, 3 = Yellow.
    protected int whichBalloon;

    // Animator for the baloons shadow.
    protected Animator shadowAnimator;

    // When scene loads, load all of the sprites from the spritesheet.
    private void Awake()
    {
        balloonSprites = Resources.LoadAll<Sprite>("BalloonSpriteSheet");
    }

    // Sets the current sprite and the shadow animator.
    private void Start()
    {
        balloonSprites[whichBalloon] = GetComponent<SpriteRenderer>().sprite;
        shadowAnimator = this.GetComponentInChildren<Animator>();
    }

    // When the balloon is hit by another collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When hit by the dart, pop the balloon.
        if (collision.gameObject.tag == "Dart")
        {
            Debug.Log("hit by dart");
            popBalloon();
        }
    }

    // Animates through the popping animation, triggers the shadow pop animation and the destroys the object.
    private void popBalloon()
    {
        shadowAnimator.SetTrigger("Pop");

        whichBalloon += 4;
        GetComponent<SpriteRenderer>().sprite = balloonSprites[whichBalloon];
        whichBalloon += 4;
        GetComponent<SpriteRenderer>().sprite = balloonSprites[whichBalloon];
        Debug.Log("I am in the parent balloon popping function");
        Destroy(gameObject, 0.2f);
    }
}
