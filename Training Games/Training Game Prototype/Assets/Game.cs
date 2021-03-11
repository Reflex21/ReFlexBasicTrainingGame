#undef DEBUG

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;


public class Game : MonoBehaviour
{
    Analytics performanceAnalytics;
    private const int MAX_TARGETS = 20;
    private const int MAX_DATA_POINTS = 5; // clickTime, gazeTime, gazeDistance, TargetX, TargetY
    public Sprite[] SPRITES;
    private bool finished = false;
    private GazePoint gazePoint;
    private int currentTarget = 0;
    private bool newTarget = true;
    private GameObject[] targets = new GameObject[MAX_TARGETS];

    // Start is called before the first frame update
    void Start()
    {
#if DEBUG
        Debug.Log("Debug mode active");
#else
        performanceAnalytics = new Analytics(MAX_TARGETS, MAX_DATA_POINTS, false); 
        performanceAnalytics.setName(0, "ClickTime");
        performanceAnalytics.setName(1, "GazeTime");
        performanceAnalytics.setName(2, "GazeDistance");
        performanceAnalytics.setName(3, "TargetX");
        performanceAnalytics.setName(4, "TargetY");

        for (int i = 0; i < MAX_TARGETS; i++)
        {
            string objName = "Target" + i;
            var gameObj = new GameObject(objName);

            //Add to array
            targets[i] = gameObj;

            // Add Logic Script
            string ScriptName = "TargetLogic";
            System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
            gameObj.AddComponent(MyScriptType);

            // Add Box Collider
            gameObj.AddComponent<BoxCollider2D>();

            // Add Sprite
            gameObj.AddComponent<SpriteRenderer>();
            Console.Write(SPRITES);

            // Change Sprite
            gameObj.transform.GetComponent<SpriteRenderer>().sprite = SPRITES[i % 6];

            // Disable for now
            gameObj.SetActive(false);

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
            // Debug.Log("Complete!");

            this.performanceAnalytics.saveData("performance.csv");
            AppHelper.Quit();
        }
        else
        {
            if(newTarget)
            {
                // Enable
                var gameObj = targets[currentTarget];
                gameObj.SetActive(true);

                // Make it visible
                gameObj.GetComponent<TargetLogic>().showTarget();
                newTarget = false;
            }
            else
            {
                // Grab the current target
                string objName = "Target" + currentTarget;
                var gameObj = GameObject.Find(objName);

                if (gameObj.GetComponent<TargetLogic>().isClicked())
                {
                    // Store data into analytics class

                    performanceAnalytics.addDataPoint(currentTarget, gameObj.GetComponent<TargetLogic>().sw_click.latency());
                    performanceAnalytics.addDataPoint(currentTarget, gameObj.GetComponent<TargetLogic>().sw_gaze.latency());
                    performanceAnalytics.addDataPoint(currentTarget, gameObj.GetComponent<TargetLogic>().distFromGaze);
                    performanceAnalytics.addDataPoint(currentTarget, gameObj.GetComponent<TargetLogic>().getX());
                    performanceAnalytics.addDataPoint(currentTarget, gameObj.GetComponent<TargetLogic>().getY());

                    gameObj.SetActive(false);
                    StartCoroutine(Delay(5.0f));
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

        private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
}
