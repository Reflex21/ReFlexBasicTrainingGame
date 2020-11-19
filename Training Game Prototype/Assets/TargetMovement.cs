using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


public class TargetMovement : MonoBehaviour
{
    
    public float speed = 1;
    public StopwatchWrapper sw;
    public bool clicked;
    public bool counted;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TargetMovement>().sw = new StopwatchWrapper();
        this.GetComponent<TargetMovement>().sw.reset();
        this.GetComponent<TargetMovement>().sw.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!clicked)
        {
            // Create random variable
            float xPos = Random.Range(-10, 10);
            float yVel = Random.Range(speed * 3, speed * 4);
            float xVel = Random.Range(-4, 4);

            // Move object somewhere
            if (this.transform.position.y < -6.5)
            {
                xPos = Random.Range(-8, 8);

                if (xPos > 0)
                    xVel = Random.Range(-6, 1);
                if (xPos < 0)
                    xVel = Random.Range(-1, 6);
                this.transform.position = new Vector2(xPos, -6);

                this.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
                this.GetComponent<Rigidbody2D>().AddTorque(100);
            }
        }
        else
        {
            this.GetComponent<Renderer>().enabled = false;
        }
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Destroy(gameObject);
            this.GetComponent<TargetMovement>().sw.stop();
            this.GetComponent<TargetMovement>().clicked = true;
            //print("Click");
        }
    }
}
