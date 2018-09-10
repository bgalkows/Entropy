using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth2 : MonoBehaviour
{
    public int startingBossHealth = 340;
    public int currentBossHealth;
    public Slider bossHealthSlider;

    public bool bossIsDead = false;
    public bool bossDamaged;

    public Image firstTick;
    public Image secondTick;
    public Image thirdTick;

    public SpriteRenderer renderer;

    public PhaseControl PhaseControl;
    public bool once;
    public int bossLives;

    BossHealth BossHealth;

    // Use this for initialization
    void Start()
    {
        bossHealthSlider.value = startingBossHealth;
        currentBossHealth = startingBossHealth;

        PhaseControl = GameObject.Find("Boss").GetComponent<PhaseControl>();

        renderer = GameObject.Find("Boss").GetComponent<SpriteRenderer>();
        once = false;
    }

    // Update is called once per frame
    void Update()
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

        if (bossHealthSlider.value <= 0)
        {
            BossDeath();
        }
    }

    void BossDeath()
    {
        if (!once)
        {
            bossLives++;
            Debug.Log("BossLives2" + " " + bossLives);
            once = true;
        }

        StartCoroutine("nextLevel");
    }

    IEnumerator nextLevel()
    {
        //RESTART BOSS HEALTH

        //flashing!
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.25f);
        renderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(1f);
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.25f);
        renderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.5f);
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.5f);
        renderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.5f);
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.25f);
        renderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.25f);
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.15f);
        renderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.15f);
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(255, 255, 255, 255);
        renderer.color = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(3f);

        //next level
        SceneManager.LoadScene("Lvl3");

    }
}