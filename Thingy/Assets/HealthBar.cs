using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image healthImg;
    public Image damageImg;
    public float flashSpeed = 5f;
    public int livesLeft = 3;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public bool isDead = false;

    bool damaged;

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

	}

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        //float ratio = currentHealth / 100;
        //healthImg.rectTransform.localScale = new Vector3(ratio, 1, 1);

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
