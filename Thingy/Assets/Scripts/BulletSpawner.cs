using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    public GameObject shot;
    private Vector2 position;
    private float shotSpeed, direction, xAway, yAway, xDiff, yDiff, speed, selfDir, selfSpd;
    private int count, rate, increment;
    private bool isAimed, onBoss;
    // Use this for initialization
    void Start() {
        position = new Vector2(0, 0);
        xDiff = 0;
        yDiff = 0;
        xAway = 0;
        yAway = 0;
        shotSpeed = .05f;
        direction = 0;
        count = 0;
        rate = 100;
        increment = 45;
        isAimed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAimed)
        {
            position = new Vector2(position.x + (selfSpd * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAway, yAway)) - 90))), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAway, yAway)) - 90))));
            shot.transform.position = position;
        }
        else
        {
            position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * selfDir)), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * selfDir)));
            shot.transform.position = position;
        }
    }
    //gets
    public float getShotSpeed() { return shotSpeed; }
    public float getDir() { return direction; }
    public float getXA() { return xAway; }
    public float getYA() { return yAway; }
    public float getXDiff() { return xDiff; }
    public float getYDiff() { return yDiff; }
    public float getSelfDir() { return selfDir; }
    public float getSelfSpd() { return selfSpd; }
    public int getDelay() { return rate; }
    public int getInc() { return increment; }
    public bool getAimed() { return isAimed; }
    public bool getOnBoss() { return onBoss; }
    //sets
    public void setPosition(Vector2 v) { position = v; }
    public void setDegree(float f) { direction = f; }
    public void setShotSpeed(float f) { shotSpeed = f; }
    public void setXA(float f) { xAway = f; }
    public void setYA(float f) { yAway = f; }
    public void setXDiff(float f) { xDiff = f; }
    public void setYDiff(float f) { yDiff = f; }
    public void setSelfDir(float f) { selfDir = f; }
    public void setSelfSpd(float f) { selfSpd = f; }
    public void setAimed(bool b) { isAimed = b; }
    public void setOnBoss(bool b) { onBoss = b; }
    public void setInc(int i) { increment = i; }
    public void setDelay(int i) { rate = i; }
    Vector2 getPosition() { return position; }

}