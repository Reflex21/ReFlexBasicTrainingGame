using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{

	public GameManager gameManager;
	Vector3 velocity; 

	[Range(0,1)]
	public float speed = 0.1f; 


    // Start is called before the first frame update
    void Start()
    {
  			ResetBall();
        
    }

    void ResetBall() {
    	transform.position = Vector3.zero;
    	  	// z axis movement
		float z = Random.Range(0,2) * 2f - 1f; 
		// x axis movement 
		float x = Random.Range(0,2) * 2f - 1f * Random.Range(0.2f, 1f); 
		// set the velocity
		// 2 d velocity
		velocity = new Vector3(x, 0, z);

    }

    // Update is called once per frame
    void FixedUpdate()
	{
		velocity = velocity.normalized * speed;
		transform.position += velocity; 

	}


	void OnCollisionEnter(Collision collision)
	{
		switch(collision.transform.name) {
			case "Bounds East":
			case "Bounds West":
				velocity.x *= -1f; 
				return; 
			case "Bounds North":
			case "Bounds South":
				ResetBall();
				gameManager.IncrementScore(collision.transform.name);
				return; 
			case "Player Paddle":
			case "Computer Paddle":
				velocity.z *= -1f; 
				return; 

		}
	}

}
