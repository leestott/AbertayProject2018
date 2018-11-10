using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour {

    public Transform balloon;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(balloon, new Vector3(i * 3.0f, 0 ,0), Quaternion.identity);
        }
	}
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.R))
        {
            spawnMore();
        }
	}

    void spawnMore()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(balloon, new Vector3(i * 3.0f, 0, 0), Quaternion.identity);
        }
    }
}
