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

    public PhaseControl PhaseControl;


    // Use this for initialization
    void Start ()
    {
        bossHealthSlider.value = startingBossHealth;
        currentBossHealth = startingBossHealth;

        PhaseControl = GameObject.Find("Boss").GetComponent<PhaseControl>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        bossDamaged = false;

        bossHealthSlider.value = startingBossHealth - PhaseControl.loss;
    }


    void BossDeath()
    {

        bossIsDead = true;
        
        //next level
    }
}
