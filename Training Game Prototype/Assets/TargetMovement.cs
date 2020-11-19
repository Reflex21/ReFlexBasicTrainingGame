using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create random variable
        float xPos = Random.Range(-10, 10);
        float yVel = Random.Range(9, 16);
        float xVel = Random.Range(-4, 4);

        // Move object somewhere
        if (this.transform.position.y < -6.5){
            xPos = Random.Range(-8, 8);

            if(xPos > 0)
                xVel = Random.Range(-6, 1);
            if (xPos < 0)
                xVel = Random.Range(-1, 6);
            this.transform.position = new Vector2(xPos, -6);

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
            this.GetComponent<Rigidbody2D>().AddTorque(100);
        }


        // Throw object upwards

        // Reset when at bottom

        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            print("Click");
        }
    }
}
