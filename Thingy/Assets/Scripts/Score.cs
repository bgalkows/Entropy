using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text timeText;
    public float time;
    public bool increaseTime;


	// Use this for initialization
	void Start ()
    {
        time = 0;
        timeText.text = time.ToString();
        increaseTime = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (increaseTime == true)
        {
            time += Time.deltaTime;
        }

        timeText.text = time.ToString();
    }
}
