using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject shot, player;
    private Vector2 position;
    private float speed, direction, xAwayFromTarget, yAwayFromTarget;
    private bool isAimed;
    //Spiral shots will use direction, aimed shots will use the AwayFromTargets
    //The AwayFromTarget variables I used in my earlier game were the difference between the bullet's x and y and the target's x and y
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    public void Update() {
        if (isAimed)
        {
            position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget) - 90))), position.y - (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget) - 90))));
            shot.transform.position = position;
        }
        else
        {
            position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * direction)), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * direction)));
            shot.transform.position = position;
        }
    }

    public void SpawnDirectional(float x, float y, float s, float dir)
    {
        speed = s;
        direction = dir;
        isAimed = false;
        position = new Vector2(x, y);
    }

    public void SpawnAimed(float x, float y, float s, float xAway, float yAway)
    {
        speed = s;
        xAwayFromTarget = xAway;
        yAwayFromTarget = yAway;
        isAimed = true;
        position = new Vector2(x, y);
    }

    void onCollisionEnter(Collision c)
    {
        if (c.gameObject.tag=="Player")
        {
            //make the function for taking damage public in the player class
            Destroy(shot, .5f);
            Destroy(this, .5f);
        }
    }
    public float getSpeed() { return speed; }
    public float getDir() { return direction; }
    public float getXDiff() { return xAwayFromTarget; } 
    public float getYDiff() { return yAwayFromTarget; }
    public bool getAimed() { return isAimed; }
    public Vector2 getPos() { return position; }
}
