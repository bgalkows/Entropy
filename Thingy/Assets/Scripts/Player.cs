using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float speed = 4;
    public int lives;
    public int currentHealth;
    public bool invincible;
    private float minX, maxX, minY, maxY;
    public Collider2D collide;
    HealthBar HealthBar;

	// Use this for initialization
	void Awake()
    {
        lives = 3;
        currentHealth = 100;
        HealthBar = GameObject.Find("Player").GetComponent<HealthBar>();

        //Camera shenanigans
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;

        collide = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // DIAGONALS
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate((speed / 2) / (Mathf.Sqrt(2)) * Time.deltaTime, (speed / 2) / (Mathf.Sqrt(2)) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate((speed / 2) / (Mathf.Sqrt(2)) * Time.deltaTime, (-speed / 2) / (Mathf.Sqrt(2)) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate((-speed / 2) / (Mathf.Sqrt(2)) * Time.deltaTime, (speed / 2) / (Mathf.Sqrt(2)) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate((-speed / 2) / (Mathf.Sqrt(2)) * Time.deltaTime, (-speed / 2) / (Mathf.Sqrt(2)) * Time.deltaTime, 0);
            }

            // REGULAR
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate((speed / 2) * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate((-speed / 2) * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0, (-speed / 2) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0, (speed / 2) * Time.deltaTime, 0);
            }
        }
        else
        {
            //DIAGONALS
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(speed / (Mathf.Sqrt(2)) * Time.deltaTime, speed / (Mathf.Sqrt(2)) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(speed / (Mathf.Sqrt(2)) * Time.deltaTime, -speed / (Mathf.Sqrt(2)) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(-speed / (Mathf.Sqrt(2)) * Time.deltaTime, speed / (Mathf.Sqrt(2)) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(-speed / (Mathf.Sqrt(2)) * Time.deltaTime, -speed / (Mathf.Sqrt(2)) * Time.deltaTime, 0);
            }

            //REGULAR
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0, -speed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0, speed * Time.deltaTime, 0);
            }
        }



        // Get current position
        Vector3 pos = transform.position;

        // Horizontal contraint
        if (pos.x- (collide.bounds.size.x/2 ) < minX) pos.x = minX+ (collide.bounds.size.x );
        if (pos.x+ (collide.bounds.size.x/2 ) > maxX) pos.x = maxX- (collide.bounds.size.x );

        // vertical contraint
        if (pos.y-(collide.bounds.size.y/2 ) < minY) pos.y = minY+ (collide.bounds.size.y );
        if (pos.y+ (collide.bounds.size.y/2 ) > maxY) pos.y = maxY- (collide.bounds.size.y );

        // Update position
        transform.position = pos;

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
