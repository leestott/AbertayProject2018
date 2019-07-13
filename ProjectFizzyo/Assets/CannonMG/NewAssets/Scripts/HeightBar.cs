using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightBar : MonoBehaviour {

	BoxCollider2D box;

	GameObject arrowPoint;
	GameObject projectile;

	public float minimumHeight;
	public float maximumHeight;

	float minimumBarHeight;
	float maximumBarHeight;

	void Start () 
	{
		//Initialse variables and references
		box = GetComponent<BoxCollider2D> ();
		arrowPoint = GameObject.Find ("ArrowHeightPoint");
		minimumBarHeight = arrowPoint.transform.localPosition.y - (box.size.y / 2.4f);
		maximumBarHeight = arrowPoint.transform.localPosition.y + (box.size.y / 2.0f);

		arrowPoint.transform.localPosition = new Vector3 (0.75f, minimumBarHeight, transform.position.z);
	}

	void Update () 
	{
		if (projectile == null) 
		{
			projectile = GameObject.FindGameObjectWithTag ("CharacterProjectile");
		} 
		else 
		{
			//Normalize height between 0 and 1 by using min and max range
			float scaledHeight = ((projectile.transform.position.y - minimumHeight) / (maximumHeight - minimumHeight));
			if (scaledHeight < 0) 
			{
				scaledHeight = 0;
			}
			else if (scaledHeight > 1) 
			{
				scaledHeight = 1;
			}
			//Move arrow to scaled height on bar
			float arrowHeight = Mathf.Lerp (minimumBarHeight, maximumBarHeight, scaledHeight);
			arrowPoint.transform.localPosition = new Vector3 (arrowPoint.transform.localPosition.x, arrowHeight, arrowPoint.transform.localPosition.z);
		}
	}
}
