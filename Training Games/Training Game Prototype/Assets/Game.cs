using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    Analytics analytics;
    private const int MAX_TARGETS = 5;
    private const int MAX_DATA_POINTS = 1;
    public Sprite[] SPRITES;
    private bool finished = false;
    private bool isRunning = true; 

    // Start is called before the first frame update
    void Start()
    {
        analytics = new Analytics(MAX_TARGETS, MAX_DATA_POINTS);
        float currSpeed = 1;

        for (int i = 0; i < MAX_TARGETS; i++)
        {
            string objName = "Target" + i;
            var gameObj = new GameObject(objName);

            // Add Movement Script
            string ScriptName = "TargetMovement";
            System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
            gameObj.AddComponent(MyScriptType);

            // Set Speed
            gameObj.transform.GetComponent<TargetMovement>().speed = currSpeed;

            // Add Rigid 2D Body
            gameObj.AddComponent<Rigidbody2D>();

            // Add Box Collider
            gameObj.AddComponent<BoxCollider2D>();

            // Add Sprite
            gameObj.AddComponent<SpriteRenderer>();
            Console.Write(SPRITES);

            // Change Sprite
            gameObj.transform.GetComponent<SpriteRenderer>().sprite = SPRITES[i % 6];

            currSpeed++;
            //Debug.Log("Made 1 Target");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            Debug.Log("Complete!");
            this.analytics.printDataPoints();
            this.analytics.saveData();
            AppHelper.Quit();
        }
        else
        {
            bool allFinished = true;
            for (int i = 0; i < MAX_TARGETS; i++)
            {
                string objName = "Target" + i;
                var gameObj = GameObject.Find(objName);
                //Debug.Log(i+" - " +(gameObj.GetComponent<TargetMovement>().clicked));
                allFinished &= (gameObj.GetComponent<TargetMovement>().clicked);
                if ((gameObj.GetComponent<TargetMovement>().clicked) && !(gameObj.GetComponent<TargetMovement>().counted))
                {
                    this.analytics.addDataPoint(i, gameObj.GetComponent<TargetMovement>().sw.latency());
                    gameObj.GetComponent<TargetMovement>().counted = true;
                }
            }
            finished = allFinished;
        }
        
    }
}
