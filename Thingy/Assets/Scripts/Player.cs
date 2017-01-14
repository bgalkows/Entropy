using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float speed = 4;
    public int lives;
    public int currentHealth;
    public bool invincible;

    HealthBar HealthBar;

	// Use this for initialization
	void Awake()
    {
        lives = 3;
        currentHealth = 100;
        HealthBar = GameObject.Find("Player").GetComponent<HealthBar>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // DIAGONALS
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("shift-Move Right/Up");
                this.transform.Translate((speed / 2) * Time.deltaTime, (speed / 2) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("shift-Move Right/Down");
                this.transform.Translate((speed / 2) * Time.deltaTime, (-speed / 2) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("shift-Move Left/Up");
                this.transform.Translate((-speed / 2) * Time.deltaTime, (speed / 2) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("shift-Move Left/Down");
                this.transform.Translate((-speed / 2) * Time.deltaTime, (-speed / 2) * Time.deltaTime, 0);
            }

            // REGULAR
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("shift-Move right");
                this.transform.Translate((speed / 2) * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("shift-Move left");
                this.transform.Translate((-speed / 2) * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("shift-Move down");
                this.transform.Translate(0, (-speed / 2) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("shift-Move up");
                this.transform.Translate(0, (speed / 2) * Time.deltaTime, 0);
            }
        }
        else
        {
            //DIAGONALS
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("Move Right/Up");
                this.transform.Translate(speed * Time.deltaTime, speed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("Move Right/Down");
                this.transform.Translate(speed * Time.deltaTime, -speed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("Move Left/Up");
                this.transform.Translate(-speed * Time.deltaTime, speed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("Move Left/Down");
                this.transform.Translate(-speed * Time.deltaTime, -speed * Time.deltaTime, 0);
            }

            //REGULAR
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("Move right");
                this.transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("Move left");
                this.transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("Move down");
                this.transform.Translate(0, -speed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("Move up");
                this.transform.Translate(0, speed * Time.deltaTime, 0);
            }
        }


        if (Input.GetKey(KeyCode.Z))
        {
            Debug.Log("Z press");
            //shooting here later
        }
    }
       
	

    public void setPosition(float x, float y)
    {
        this.transform.position = new Vector2(x, y);
    }

    //public void Respawn()
    //{
    //    invincible = true;
    //    updateLine(); 
    //    invincible = false;

    //    currentHealth = 100; 
    //    HealthBar.healthSlider.value = currentHealth;
    //    Debug.Log("You respawned with 100 health.");
    //    setPosition(0, -4);
    //}

    void Respawn()
    {
        StartCoroutine("waitThreeSeconds");
    }

    IEnumerator waitThreeSeconds()
    {

        currentHealth = 100;
        HealthBar.healthSlider.value = currentHealth;
        Debug.Log("You respawned with 100 health.");
        setPosition(0, -4);

        invincible = true;
        yield return new WaitForSeconds(3);
        invincible = false;
    }


    public void Death()
    {
        if (lives > 0)
        {
            Respawn();
            lives--;
            HealthBar.livesLeft--;
            Debug.Log(lives + " lives left!");
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        //SceneManager.LoadScene("GameOverScene");
    }

    public void TakeDamage(int amount)
    {
        if(!invincible)
        {
            currentHealth -= amount;
            Debug.Log("Player lost" + amount + "health, " + currentHealth + "remaining.");

            HealthBar.damaged = true;

            HealthBar.healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                Death();
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("HIT");
        if(other.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit by bullet");
            TakeDamage(20);
        }
    }
}
