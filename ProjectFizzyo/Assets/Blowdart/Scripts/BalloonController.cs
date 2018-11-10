using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {

    public float speed = 0.25f;
    public Sprite[] balloonTypes;
    private int whichBalloon;
    private Animator shadowAnimator;

    private void Awake()
    {
        balloonTypes = Resources.LoadAll<Sprite>("BalloonSpriteSheet");
    }

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

    void moveUpDown()
    {
        float newY = Mathf.Sin(Time.time * speed) * 3.5f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dart")
        {
            Debug.Log("hit by dart");
            popBalloon();
        }
    }

    private void popBalloon()
    {
        shadowAnimator.SetTrigger("Pop");

        whichBalloon += 4;
        GetComponent<SpriteRenderer>().sprite = balloonTypes[whichBalloon];
        whichBalloon += 4;
        GetComponent<SpriteRenderer>().sprite = balloonTypes[whichBalloon];

        Destroy(gameObject, 0.2f);
    }
}
