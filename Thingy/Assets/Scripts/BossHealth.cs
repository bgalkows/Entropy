using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int startingBossHealth = 340;
    public int currentBossHealth;
    public Slider bossHealthSlider;

    public bool bossIsDead = false;
    public bool bossDamaged;


    // Use this for initialization
    void Start ()
    {
        currentBossHealth = startingBossHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        bossDamaged = false;
    }

    public void BossTakeDamage(int amount)
    {
        bossDamaged = true;

        currentBossHealth -= amount;


        bossHealthSlider.value = currentBossHealth;

        if (currentBossHealth <= 0)
        {
            BossDeath();
        }
    }

    void BossDeath()
    {

        bossIsDead = true;
        
        //next level
    }
}
