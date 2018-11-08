using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowdart : MonoBehaviour {

    public float speed = 5f;
    public float height = 5f;
    public float breath = 100f;

    private bool fired = false;

    private Rigidbody2D rb;

    private Vector3 startingPos;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        handleUpdate();

        if (!fired)
        {
            moveUpDown();
        } 
	}

    void handleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fired = true;
            fireDart();
        }
    }

    void moveUpDown()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void fireDart()
    {
        rb.velocity = new Vector2(breath, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Reset")
        {
            rb.velocity = new Vector2(0, 0);
            fired = false;
            transform.position = startingPos;
        }

        if (collision.gameObject.tag == "Balloon")
        {
            Debug.Log("Pop");
        }
    }
}
