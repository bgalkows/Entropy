using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {
    private bool spawnsOnBoss;
    private float xDiff, yDiff, spd, shotSpd;
    private int spawnDelay, number;
    private Vector2 pos;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ring methods - sets
    public void setSpawnDelay(int i) { spawnDelay = i; }
    public void setSpeed(float f) { spd = f; }
    public void setShotSpeed(float f) { shotSpd = f; }
    public void toggleSpawnOnBoss() { spawnsOnBoss = !spawnsOnBoss; }
    public void setXDiff(float f) { xDiff = f; }
    public void setYDiff(float f) { yDiff = f; }
    public void setPos(Vector2 v) { pos = v; }
    public void setSpawnOnBoss(bool b) { spawnsOnBoss = b; }
    public void setNum(int i) { number = i; }
    //ring methods - gets
    public bool getSpawnOnBoss() { return spawnsOnBoss; }
    public int getSpawnDelay() { return spawnDelay; }
    public int getNumber() { return number; }
    public float getSpeed() { return spd; }
    public float getShotSpeed() { return shotSpd; }
    public float getXDiff() { return xDiff; }
    public float getYDiff() { return yDiff; }
    public Vector2 getPos() { return pos; }
}
