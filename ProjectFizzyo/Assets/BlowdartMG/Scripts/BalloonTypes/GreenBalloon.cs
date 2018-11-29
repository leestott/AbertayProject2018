using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child of the ParentBalloon class: The green balloon moves up and down (It is faster than blue)
public class GreenBalloon : ParentBalloon
{
    // Set up the correct sprite, randomise the speed and find the shadow animator.
    private void Start()
    {
        whichBalloon = 2;
        speed = Random.Range(2.0f, 4.0f);
        shadowAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        MoveUpDown();
    }

    // Move the balloon up and down based on a sine wave and the balloons speed.
    void MoveUpDown()
    {
        float newY = Mathf.Sin(Time.time * speed) * 3.5f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
