using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    Analytics analytics;
    private const int MAX_TARGETS = 6;
    private const int MAX_DATA_POINTS = 10;
    public Sprite[] SPRITES;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
