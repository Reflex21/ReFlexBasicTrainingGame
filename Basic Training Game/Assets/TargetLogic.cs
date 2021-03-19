using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


public class TargetLogic : MonoBehaviour
{
    
    public float speed = 1;
    public StopwatchWrapper sw_click;
    public StopwatchWrapper sw_gaze;
    public TobiiHelper tobii;
    public bool clicked;
    public bool counted;
    public bool isVisible;
    public bool isNoticed;
    public double distFromGaze = 0.0;
    public double x;
    public double y;
    public static double range = 0.1;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TargetLogic>().sw_click = new StopwatchWrapper();
        this.GetComponent<TargetLogic>().sw_gaze = new StopwatchWrapper();
        this.GetComponent<TargetLogic>().tobii = new TobiiHelper();
        this.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));

        this.GetComponent<TargetLogic>().cam = Camera.main;
        Vector3 original = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        Vector3 viewPort = cam.WorldToViewportPoint(original);
        this.GetComponent<TargetLogic>().x = viewPort.x;
        this.GetComponent<TargetLogic>().y = viewPort.y;
        this.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Track time until gaze within range
        if (this.GetComponent<TargetLogic>().isVisible && !this.GetComponent<TargetLogic>().isNoticed)
        {
            double currentX = this.transform.position.x;
            double currentY = this.transform.position.y;
            if (this.GetComponent<TargetLogic>().tobii.isWithinRange(currentX, currentY, TargetLogic.range))
            {
                this.GetComponent<TargetLogic>().sw_gaze.stop();
                this.GetComponent<TargetLogic>().isNoticed = true;
                Debug.Log("NOTICED");
            }
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Destroy(gameObject);
            this.GetComponent<TargetLogic>().sw_click.stop();
            this.GetComponent<TargetLogic>().clicked = true;
            //this.GetComponent<Renderer>().enabled = false;
            print("Click");
        }
    }

    public void showTarget()
    {
        this.GetComponent<TargetLogic>().isVisible = true;
        this.GetComponent<TargetLogic>().isNoticed = false;

        this.GetComponent<TargetLogic>().sw_click.reset();
        this.GetComponent<TargetLogic>().sw_gaze.reset();
        this.GetComponent<TargetLogic>().sw_click.start();
        this.GetComponent<TargetLogic>().sw_gaze.start();

        this.GetComponent<Renderer>().enabled = true;

        double currentX = this.transform.position.x;
        double currentY = this.transform.position.y;
        double dist = this.GetComponent<TargetLogic>().tobii.distanceFromGaze(currentX, currentY);
        if(dist == -1)
        {
            this.distFromGaze = -1;
        }
        else
        {
            this.distFromGaze = dist -TargetLogic.range;
        }
        
    }

    public bool isClicked()
    {
        return this.GetComponent<TargetLogic>().clicked;
    }

    public double getX()
    {
        return this.GetComponent<TargetLogic>().x;
    }

    public double getY()
    {
        return this.GetComponent<TargetLogic>().y;
    }
}
