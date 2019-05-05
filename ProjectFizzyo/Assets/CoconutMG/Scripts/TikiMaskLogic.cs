using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikiMaskLogic : MonoBehaviour {

	// Global variables and component references
	public Sprite[] maskSprites;

	public float movementSpeed = 1.0f;

	float angle = 0;
	public float radius = 5.0f;

	int randomMoveNumber;

	SpriteRenderer spriteRen;

	Vector3 m_Pivot;
	Vector3 m_PivotOffset;
	float m_Phase;
	bool m_Invert;
	float m_2PI = Mathf.PI * 2.0f;

	float m_YScale = 1.0f;
	float m_XScale = 1.0f;

	float randomXPos;

	//Get references to components and initialise mask
	void Start () 
	{
		spriteRen = GetComponent<SpriteRenderer>();
		GenerateNewMask ();
		GenerateMovementNumber ();
		GenerateNewPoint ();
		m_Pivot = transform.position -= new Vector3 (m_XScale, 0, 0);
	}

	//Generate a random number to determine mask movement pattern
	void GenerateMovementNumber () 
	{
		transform.position = new Vector2 (0, 0);
		randomMoveNumber = Random.Range (1, 5);
		Debug.Log ("RANDOM MOVE NUM: " + randomMoveNumber);
	}
		
	void Update() 
	{
		if (Input.GetKeyDown (KeyCode.N)) 
		{
			GenerateMovementNumber ();
		}

		//Call appropriate mask movement function
		switch (randomMoveNumber)
		{
		case 1:
			XAxisPath ();
			break;
		case 2:
			CirclePath ();
			break;
		case 3:
			FigureEightPath ();
			break;
		case 4:
			RandomXPath ();
			break;
		default:
			XAxisPath ();
			break;
		}
	}

	//Generate new mask sprite
	void GenerateNewMask () 
	{
		int randomNum = Random.Range (0, maskSprites.Length);
		spriteRen.sprite = maskSprites [randomNum];
	}

	//Move mask back and forth along X-axis using a sin wave
	void XAxisPath() 
	{
		float xPos = (8 / 2f) * Mathf.Sin (Time.time * movementSpeed);
		transform.position = new Vector3 (xPos, transform.position.y, 0);
	}

	//Move mask in a circular path around a point using equation of a circle
	void CirclePath() 
	{
		angle += movementSpeed * Time.deltaTime;
		// Using the angle and the radius find the new position on the circle.
		Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
		// Add the offset to the centre to move the balloon to the correct position in the circle.
		transform.position = new Vector2(0, 0) + offset;
	}

	//Move mask in figure-8 path
	void FigureEightPath () 
	{
		m_PivotOffset = Vector3.right * 2 * m_XScale;

		m_Phase += movementSpeed * 1.5f * Time.deltaTime;
		if (m_Phase > m_2PI) 
		{
			m_Invert = !m_Invert;
			m_Phase -= m_2PI;
		}
		if (m_Phase < 0) 
		{
			m_Phase += m_2PI;
		}

		transform.position = m_Pivot + (m_Invert ? m_PivotOffset : Vector3.zero);
		transform.position += new Vector3 (Mathf.Cos (m_Phase) * (m_Invert ? -1 : 1) * m_YScale, Mathf.Sin (m_Phase) * m_XScale, 0);
	}

	//Calculate new random position on X-axis
	void GenerateNewPoint () 
	{
		randomXPos = Random.Range (-7, 7);
	}

	//Move to current random point then calculate new point once reached
	void RandomXPath () 
	{
		float xPos = Mathf.Lerp (transform.position.x, randomXPos, Time.deltaTime);
		transform.position = new Vector3 (xPos, transform.position.y, transform.position.z);
		if (Mathf.Abs(xPos - randomXPos) <= 3.5f)
		{
			GenerateNewPoint ();
		}
	}
}
