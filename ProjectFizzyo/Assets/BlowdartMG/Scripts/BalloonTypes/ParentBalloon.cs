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

    // The score manager
    public MinigameScoring minigameScoring;

    // When scene loads, load all of the sprites from the spritesheet.
    private void Awake()
    {
        minigameScoring = FindObjectOfType<MinigameScoring>();
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
            int score;

            switch (whichBalloon)
            {
                // Red balloon score
                case 0:
                    score = 40;
                    break;
                // Blue balloon score
                case 1:
                    score = 20;
                    break;
                // Green balloon score
                case 2:
                    score = 30;
                    break;
                // Yellow balloon score
                case 3:
                    score = 10;
                    break;
                default:
                    score = 0;
                    break;
            }

            // Updating the score
            minigameScoring.AddScore(score);

            // Calling the pop animation
            PopBalloon();
        }
    }

    // Animates through the popping animation, triggers the shadow pop animation and the destroys the object.
    private void PopBalloon()
    {
        shadowAnimator.SetTrigger("Pop");

        whichBalloon += 4;
        GetComponent<SpriteRenderer>().sprite = balloonSprites[whichBalloon];
        whichBalloon += 4;
        GetComponent<SpriteRenderer>().sprite = balloonSprites[whichBalloon];
        Destroy(gameObject, 0.2f);
    }
}
