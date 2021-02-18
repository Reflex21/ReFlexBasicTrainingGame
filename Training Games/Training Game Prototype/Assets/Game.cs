#undef DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;


public class Game : MonoBehaviour
{
    Analytics clickAnalytics;
    Analytics gazeAnalytics;
    private const int MAX_TARGETS = 10;
    private const int MAX_DATA_POINTS = 1;
    public Sprite[] SPRITES;
    private bool finished = false;
    private GazePoint gazePoint;
    private int currentTarget = 0;
    private bool newTarget = true;

    // Start is called before the first frame update
    void Start()
    {
#if DEBUG
        Debug.Log("Debug mode active");
#else
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Debug.Log("screen x: " + screenSize.x + "screen y: " + screenSize.y);
        clickAnalytics = new Analytics(MAX_TARGETS, MAX_DATA_POINTS);
        gazeAnalytics = new Analytics(MAX_TARGETS, MAX_DATA_POINTS);
        for (int i = 0; i < MAX_TARGETS; i++)
        {
            string objName = "Target" + i;
            var gameObj = new GameObject(objName);
            Debug.Log("Created Target" +i);


            // Add Logic Script
            string ScriptName = "TargetLogic";
            System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
            gameObj.AddComponent(MyScriptType);

            Debug.Log("Added Script to Target" + i);

            // Add Box Collider
            gameObj.AddComponent<BoxCollider2D>();

            // Add Sprite
            gameObj.AddComponent<SpriteRenderer>();
            Console.Write(SPRITES);

            // Change Sprite
            gameObj.transform.GetComponent<SpriteRenderer>().sprite = SPRITES[i % 6];

        }
#endif
    }


    // Update is called once per frame
    void Update()
    {
#if DEBUG
        gazePoint = TobiiAPI.GetGazePoint();
        Debug.Log("X: " + gazePoint.Screen.x + ", Y: " + gazePoint.Screen.y);
#else
        if (finished)
        {
            Debug.Log("Complete!");
            this.clickAnalytics.printDataPoints();
            this.gazeAnalytics.printDataPoints();
            this.clickAnalytics.saveData("clickData.txt");
            this.gazeAnalytics.saveData("gazeData.txt");
            AppHelper.Quit();
        }
        else
        {
            if(newTarget)
            {
                // Grab the current target
                string objName = "Target" + currentTarget;
                var gameObj = GameObject.Find(objName);
                Debug.Log("grabbed target " + currentTarget);

                // Make it visible
                gameObj.GetComponent<TargetLogic>().showTarget();
                Debug.Log("making target " + currentTarget + "visible");
                newTarget = false;
            }
            else
            {
                // Grab the current target
                string objName = "Target" + currentTarget;
                var gameObj = GameObject.Find(objName);
                Debug.Log("grabbed target " + currentTarget);

                if (gameObj.GetComponent<TargetLogic>().clicked)
                {
                    // Store data into analytics class
                    this.clickAnalytics.addDataPoint(currentTarget, gameObj.GetComponent<TargetLogic>().sw_click.latency());
                    this.gazeAnalytics.addDataPoint(currentTarget, gameObj.GetComponent<TargetLogic>().sw_gaze.latency());
                    gameObj.SetActive(false);
                    Delay(5.0f);
                    newTarget = true;
                    currentTarget++;
                    
                }
            }

            if(currentTarget >= MAX_TARGETS)
            {
                finished = true;
            }
        }
#endif
    }

    private IEnumerable Delay(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
}
