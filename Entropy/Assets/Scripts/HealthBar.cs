﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImg;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public float flashSpeed = 10f;
    public int livesLeft = 3;
    public Color flashColor = new Color(255, 0f, 0f, 255f);
    public bool isDead = false;

    public bool damaged;

    // Use this for initialization
    void Start ()
    {
        //player = GameObject.FindWithTag("Player");

        currentHealth = startingHealth;

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (damaged)
        {
            damageImg.color = flashColor;
        }

        else
        {
            damageImg.color = Color.Lerp(damageImg.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;

        if (livesLeft == 3)
        {
            //DONT DELETE THIS
        }

        else if (livesLeft == 2)
        {
            heart3.color = new Color(0, 0, 0, 0);
        }

        else if (livesLeft == 1)
        {
            heart2.color = new Color(0, 0, 0, 0);
        }

        else
        {
            heart1.color = new Color(0, 0, 0, 0);
        }

    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;


        healthSlider.value = currentHealth;

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        livesLeft -= 1;

        if (livesLeft <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            //end game
        }

        else
        {
            //restart level
        }
    }
}
