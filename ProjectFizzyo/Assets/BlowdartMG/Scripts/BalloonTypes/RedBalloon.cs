using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child of the ParentBalloon class: The red balloon moves in a circular motion
public class RedBalloon : ParentBalloon
{
    // How wide the circle is.
    [Header("The size of the circular path:")]
    public float radius = 2.0f;

    // The centre of the circlular path the balloon follows.
    private Vector2 centre;
    private float angle;

    // Set up correct sprite, the speed, shadow animator and set the centre of the circle to be the balloons position.
    private void Start()
    {
        whichBalloon = 0;
        speed = 3.0f;
        shadowAnimator = GetComponentInChildren<Animator>();
        centre = transform.position;
    }

    // Moves in a circular motion
    private void Update()
    {
        CircularMove();
    }

    // Move the balloon in a circular motion.
    private void CircularMove()
    {
        // Increase the angle based off of deltatime and the set speed.
        angle += speed * Time.deltaTime;
        // Using the angle and the radius find the new position on the circle.
        Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        // Add the offset to the centre to move the balloon to the correct position in the circle.
        transform.position = centre + offset;
    }
}
