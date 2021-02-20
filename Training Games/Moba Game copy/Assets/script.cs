using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug=UnityEngine.Debug;
 
 

public class script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ring;
    public Renderer rend;
    public Vector3 pos;
    public Vector3 position;
    public float time;

    void start(){
    	position = new Vector3();
    	pos = new Vector3();
    	time = Time.time;
    }


	void OnMouseDown(){
		Animator anim = GameObject.Find("Jammo").GetComponent<Animator> ();
		if (!anim.GetBool("MidActivity")){
			time = Time.time - (time + Time.deltaTime);
			reflex("ReFlex: " + time + "sec\nKey: MouseClick");
			TextMesh text = GameObject.FindWithTag("MainText").GetComponent<TextMesh>();
			text.text = "Nice!";
			rend = GetComponent<Renderer>();
			pos = gameObject.transform.position;
			position = new Vector3();
			rend.material.SetColor("_Color", new Color(0.2f, .8f, .2f, .55f));
		}
	}

	IEnumerator Wait(){
		Animator anim = GameObject.Find("Jammo").GetComponent<Animator> ();
		anim.SetBool("MidActivity", true);
		TextMesh text = GameObject.FindWithTag("MainText").GetComponent<TextMesh>();
		string[] keys = new string[] {"Q", "W", "E", "R", "1", "2", "3"};
		int rand = Random.Range(2, 7);
		for (int j = 0; j < rand; j++){
			int i = 0;
			string x = keys[Random.Range(0, 7)];
	    	text.text = "Press \"" + x + "\"";
	    	time = Time.time;
	    	while(!Input.GetKeyDown(x.ToLower())){
	    		if(Input.anyKeyDown){
	    			i++;
	    		}
	    		yield return null;
	    	}
	    	time = Time.time - (time + Time.deltaTime);
			reflex("ReFlex: " + time + "sec\nKey: " + x + ", Attempts: " + (i+1));
			text.text = "";
			yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
    	}
		text.text = "Well Done!";
		yield return new WaitForSeconds(.5f);
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		rend.material.SetColor("_Color", new Color(0.878f, .0784f, .6509f, 1.0f));
		text.text = "Click the Pink Target!";
		anim.SetBool("MidActivity", false);
		time =  Time.time;
	}

	public void reflex(string s){
		TextMesh text = GameObject.FindWithTag("Feedback").GetComponent<TextMesh>();
		string str = s + "\n\n" + text.text;
		string[] ss = str.Split('\n');
		if (ss.Length > 10){
			text.text = ss[0] + "\n" + ss[1] + "\n\n" + 
			ss[3] + "\n" + ss[4] + "\n\n" +
			ss[6] + "\n" + ss[7];
		}
		else{ 
			text.text = str;
		}

	}

	public void OnTriggerEnter(Collider other)
    {
		for (int i = 0; i < 10; i++){
			position = new Vector3(Random.Range(-7.0F, 7.0F), 0.2F, Random.Range(-7.0F, 6.0F));
			if (Vector3.Distance(pos, position) > 6f){
				break;
			}
		}
		transform.position = position;
		rend = GetComponent<Renderer>();
		rend.enabled = false;
    }

    public void OnTriggerExit(Collider collisionInfo){
    	TextMesh text = GameObject.FindWithTag("MainText").GetComponent<TextMesh>();
    	StartCoroutine(Wait());
    }
}
