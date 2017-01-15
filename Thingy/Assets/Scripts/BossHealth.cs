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

    public Image firstTick;
    public Image secondTick;
    public Image thirdTick;

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

        if (bossHealthSlider.value <= 270)
        {
            firstTick.color = new Color(0, 0, 0, 0);
        }

        if (bossHealthSlider.value <= 190)
        {
            secondTick.color = new Color(0, 0, 0, 0);
        }

        if (bossHealthSlider.value <= 100)
        {
            thirdTick.color = new Color(0, 0, 0, 0);
        }
    }


    void BossDeath()
    {

        bossIsDead = true;
        
        //next level
    }
}
