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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAimed)
        {
            position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * direction)), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * direction)));
        }
        else
        {
            position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget) - 90))), position.y - (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget) - 90))));
        }
        hitCheck(player);
	}   

    public void SpawnDirectional(float x,float y,float s, float dir)
    {
        speed = s;
        direction = dir;
        isAimed = false;
        position = new Vector2(x, y);
        //Instantiate(shot, new Vector2(x, y), Quaternion.identity); This should probably go in a bullet spawner script
    }

    public void SpawnAimed(float x, float y, float s, float xAway, float yAway)
    {
        speed = s;
        xAwayFromTarget = xAway;
        yAwayFromTarget = yAway;
        isAimed = true;
        position = new Vector2(x, y);
    }

    void hitCheck(GameObject player)
    {
    }
}
