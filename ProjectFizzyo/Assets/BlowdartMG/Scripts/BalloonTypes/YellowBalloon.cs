using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child of the ParentBalloon class: The Yellow balloon is stationary.
public class YellowBalloon : ParentBalloon
{
    // Initialises which colour of sprite
    private void Start()
    {
        whichBalloon = 3;
        shadowAnimator = GetComponentInChildren<Animator>();
    }
}
