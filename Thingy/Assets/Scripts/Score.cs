using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    HealthBar HealthBar;
    BossHealth BossHealth;


    public float time = 0;
    public bool increaseTime;
    public float score;

    public int heartsLeft;

	// Use this for initialization
	void Start ()
    {
        score = 0;
        increaseTime = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (increaseTime == true)
        {
            time += Time.deltaTime;
        }



        if(HealthBar.livesLeft <= 0)
        {
            score = 0;
        }
	}

    public void addScore (int scoreValue)
    {
        score += scoreValue;
    }

    public void bossScore()
    {
        increaseTime = false;
        if(BossHealth.bossLives == 1)
        {
            score += (1000f - time * 10f);
        }
        else if(BossHealth.bossLives == 2)
        {
            score += (3000 - time * 10);
        }
        else
        {
            score += (6000 - time * 10);
        }

    }
}
