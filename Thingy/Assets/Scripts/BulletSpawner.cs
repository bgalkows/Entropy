using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    public GameObject shot;
    private Vector2 position;
    private float shotSpeed, xAway, yAway, xDiff, yDiff, selfDir, selfSpd;
    private int count, rate, increment, num;
    private bool isAimed, onBoss,isBomber;
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        if (isAimed)
        {
            position = new Vector2(position.x + (selfSpd * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAway, yAway)) - 90))), position.y - (selfSpd * Mathf.Sin(Mathf.Deg2Rad * (Mathf.Rad2Deg * (Mathf.Atan2(xAway, yAway)) - 90))));
            shot.transform.position = position;
        }
        else
        {
            position = new Vector2(position.x + (selfSpd * Mathf.Cos(Mathf.Deg2Rad * selfDir)), position.y - (selfSpd * Mathf.Sin(Mathf.Deg2Rad * selfDir)));
            shot.transform.position = position;
        }
    }
    public void SpawnDirectional(float x, float y, float s, float dir, int delay, float shotSpd, int number, bool bomber)
    {
        selfSpd = s;
        selfDir = dir;
        isAimed = false;
        position = new Vector2(x, y);
        rate = delay;
        shotSpeed = shotSpd;
        num = number;
        isBomber = bomber;
    }

    //aimed is unfinished for now
    public void SpawnAimed(float x, float y, float s, float xA, float yA, int delay)
    {
        selfSpd = s;
        xAway = xA;
        yAway = yA;
        isAimed = true;
        position = new Vector2(x, y);
        rate = delay;
    }
    //gets
    public float getShotSpeed() { return shotSpeed; }
    public float getXA() { return xAway; }
    public float getYA() { return yAway; }
    public float getXDiff() { return xDiff; }
    public float getYDiff() { return yDiff; }
    public float getSelfDir() { return selfDir; }
    public float getSelfSpd() { return selfSpd; }
    public float getShotSpd() { return shotSpeed; }
    public int getNum() { return num; }
    public int getDelay() { return rate; }
    public int getInc() { return increment; }
    public bool getAimed() { return isAimed; }
    public bool getOnBoss() { return onBoss; }
    public bool getIsBomber() { return isBomber; }
    //sets
    public void setPosition(Vector2 v) { position = v; }
    public void setShotSpeed(float f) { shotSpeed = f; }
    public void setXA(float f) { xAway = f; }
    public void setYA(float f) { yAway = f; }
    public void setXDiff(float f) { xDiff = f; }
    public void setYDiff(float f) { yDiff = f; }
    public void setSelfDir(float f) { selfDir = f; }
    public void setSelfSpd(float f) { selfSpd = f; }
    public void setShotSpd(float f) { shotSpeed = f; }
    public void setNum(int i) { num = i; }
    public void setAimed(bool b) { isAimed = b; }
    public void setOnBoss(bool b) { onBoss = b; }
    public void setInc(int i) { increment = i; }
    public void setDelay(int i) { rate = i; }
    public void setIsBomber(bool b) { isBomber = b; }
    Vector2 getPosition() { return position; }

}