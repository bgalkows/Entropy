using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject shot, player;
    private Vector2 position;
    private float speed, direction, xAwayFromTarget, yAwayFromTarget, spr;
    private int cur, number;
    public bool isAimed, isSpread,wallBounce,bounced,vBounce,vBounced;
    //Spiral shots will use direction, aimed shots will use the AwayFromTargets
    //The AwayFromTarget variables I used in my earlier game were the difference between the bullet's x and y and the target's x and y
    // Use this for initialization
    void Start() {
        bounced = false; //horizontal bounce
    }

    // Update is called once per frame
    public void Update() {
        if (!bounced && !vBounced)
        {
            if (isAimed)
            {
                direction = (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90);
                position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90))), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90))));
                shot.transform.position = position;
            }
            else if (isSpread)
            {
                direction = (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number));
                position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number)))), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number)))));
                shot.transform.position = position;
            }
            else
            {
                position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * direction)), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * direction)));
                shot.transform.position = position;
            }
        }
        else if (bounced && !vBounced)
        {
            if (isAimed)
            {
                direction = (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90);
                position = new Vector2(position.x - (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90))), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90))));
                shot.transform.position = position;
            }
            else if (isSpread)
            {
                direction = (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number));
                position = new Vector2(position.x - (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number)))), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number)))));
                shot.transform.position = position;
            }
            else
            {
                position = new Vector2(position.x - (speed * Mathf.Cos(Mathf.Deg2Rad * direction)), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * direction)));
                shot.transform.position = position;
            }
        }
        else if (!bounced && vBounced)
        {
            if (isAimed)
            {
                direction = (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90);
                position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90))), position.y + (speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90))));
                shot.transform.position = position;
            }
            else if (isSpread)
            {
                direction = (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number));
                position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number)))), position.y + (speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number)))));
                shot.transform.position = position;
            }
            else
            {
                position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * direction)), position.y + (speed * Mathf.Sin(Mathf.Deg2Rad * direction)));
                shot.transform.position = position;
            }
        }
        else
        {
            if (isAimed)
            {
                direction = (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90);
                position = new Vector2(-1*(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90)))), -1*(position.y - ( speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90)))));
                shot.transform.position = position;
            }
            else if (isSpread)
            {
                direction = (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number));
                position = new Vector2(-1*(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number))))), -1*(position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget)) - 90 - (spr / 2) + (cur * spr / number))))));
                shot.transform.position = position;
            }
            else
            {
                position = new Vector2(-1*(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * direction))),-1*( position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * direction))));
                shot.transform.position = position;
            }
        }
    }

    public void SpawnDirectional(float x, float y, float s, float dir,bool bounce)
    {
        speed = s;
        direction = dir;
        isAimed = false;
        position = new Vector2(x, y);
        wallBounce = bounce;
    }

    public void SpawnAimed(float x, float y, float s, float xAway, float yAway,bool horizBounce,bool vertBounce)
    {
        speed = s;
        xAwayFromTarget = xAway;
        yAwayFromTarget = yAway;
        isAimed = true;
        position = new Vector2(x, y);
        wallBounce = horizBounce;
        vBounce = vertBounce;
    }
    public void SpawnSpread(float x, float y, float s, float xAway, float yAway,int current,int total, float spread,bool horizBounce,bool vertBounce)
    {
        speed = s;
        xAwayFromTarget = xAway;
        yAwayFromTarget = yAway;
        position = new Vector2(x, y);
        cur = current;
        number = total;
        spr = spread;
        isSpread = true;
        wallBounce = horizBounce;
        vBounce = vertBounce;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public float getSpeed() { return speed; }
    public float getDir() { return direction; }
    public float getXDiff() { return xAwayFromTarget; } 
    public float getYDiff() { return yAwayFromTarget; }
    public bool getVBounce() { return vBounce; }
    public bool getAimed() { return isAimed; }
    public bool getBounce() { return wallBounce; }
    public Vector2 getPos() { return position; }
    public void setBounce(bool b) { wallBounce = b; }
    public void bounce() { bounced = true;wallBounce = false; }
    public void setVBounce(bool b) { vBounce = b; }
    public void vertBounce() { vBounced = true; vBounce = false; }
    }
