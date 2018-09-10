using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedSpread : MonoBehaviour {
    private bool spawnsOnBoss;
    private float xDiff, yDiff, spd, shotSpd, xAway, yAway, spread;
    private int spawnDelay, number;
    private Vector2 pos;
    private bool vertBounce, horizBounce;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //aimed methods - sets
    public void setVB(bool b) { vertBounce = b; }
    public void setHB(bool b) { horizBounce = b; }
    public void setSpawnDelay(int i) { spawnDelay = i; }
    public void setSpeed(float f) { spd = f; }
    public void setShotSpeed(float f) { shotSpd = f; }
    public void toggleSpawnOnBoss() { spawnsOnBoss = !spawnsOnBoss; }
    public void setXDiff(float f) { xDiff = f; }
    public void setYDiff(float f) { yDiff = f; }
    public void setPos(Vector2 v) { pos = v; }
    public void setSpawnOnBoss(bool b) { spawnsOnBoss = b; }
    public void setXAway(float f) { xAway = f; }
    public void setYAway(float f) { yAway = f; }
    public void setSpread(float f) { spread = f; }
    public void setNum(int i) { number = i; }
    //aimed methods - gets
    public bool getVG() { return vertBounce; }
    public bool getHB() { return horizBounce; }
    public bool getSpawnOnBoss() { return spawnsOnBoss; }
    public int getSpawnDelay() { return spawnDelay; }
    public float getSpread() { return spread; }
    public int getNumber() { return number; }
    public float getSpeed() { return spd; }
    public float getShotSpeed() { return shotSpd; }
    public float getXDiff() { return xDiff; }
    public float getYDiff() { return yDiff; }
    public float getXAway() { return xAway; }
    public float getYAway() { return yAway; }
    public Vector2 getPos() { return pos; }
}
