using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAnimator : MonoBehaviour {

	public Animator anim;

	public Collider2D col;
	private Rigidbody2D rb;

	bool rotateToFaceVelocity = false;

	void Start () 
	{
		//Initialise references
		col = GetComponent<Collider2D> ();
		anim = GetComponentInChildren<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		switch (StaticGameState.currentCharacter) 
		{
		case StaticGameState.CharacterState.Batboy:
			rotateToFaceVelocity = true;
			break;
		case StaticGameState.CharacterState.Alien:
			rotateToFaceVelocity = true;
			break;
		}
	}

	void Update () 
	{
		switch (StaticGameState.currentCharacter) 
		{
		case StaticGameState.CharacterState.Unicorn:
			anim.SetFloat ("VelocityY", rb.velocity.y);	
			break;
		}

		//If turned on rotate the sprite to face the direction of travel
		if (rotateToFaceVelocity) 
		{
			Vector2 dir = rb.velocity;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90.0f;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
			
	}

	//On collision with water play character jump animation
	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.collider.tag == "Water") 
		{
			if (StaticGameState.currentCharacter == StaticGameState.CharacterState.Batboy)
			{
				anim.SetTrigger ("Jump");
			}
		}		
	}
}
