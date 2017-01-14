using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour {
    private bool spiralSpawnsOnBoss, cw;
    private float deg, dir, xDiff, yDiff, spd, shotSpd;
    private int spiralSpawnDelay, spiralDegIncrement,count;
    private Vector2 pos;
    // Use this for initialization
    void Start () {
	}
	// Update is called once per frame
	void Update () {
        count++;
    }
    public void incrementDeg()
    {
        if (count % spiralSpawnDelay == 0)
        {
            if (cw)
            {
                if (deg >= 360) { deg = 0; }
                deg += spiralDegIncrement;
            }
            else
            {
                if (deg <= 0) { deg = 360; }
                deg -= spiralDegIncrement;
            }
        }
    }
    //spiral methods - sets
    public void swapSpiralDir() { cw = !cw; }
    public void setDeg(float f) { deg = f; }
    public void setSpiralSpawnDelay(int i) { spiralSpawnDelay = i; }
    public void setSpiralDegInc(int i) { spiralDegIncrement = i; }
    public void setSpiralPosSpeed(float f) { spd = f; }
    public void setSpiralShotSpeed(float f) { shotSpd = f; }
    public void toggleSpawnOnBoss() { spiralSpawnsOnBoss = !spiralSpawnsOnBoss; }
    public void setXDiff(float f) { xDiff = f; }
    public void setYDiff(float f) { yDiff = f; }
    public void setSpiralPos(Vector2 v) { pos = v; }
    public void setSpawnOnBoss(bool b) { spiralSpawnsOnBoss = b; }
    //spiral methods - gets
    public bool getSpiralDir() { return cw; }
    public bool getSpawnOnBoss() { return spiralSpawnsOnBoss; }
    public int getSpiralSpawnDelay() { return spiralSpawnDelay; }
    public int getSpiralDegInc() { return spiralDegIncrement; }
    public float getSpiralPosSpeed() { return spd; }
    public float getSpiralShotSpeed() { return shotSpd; }
    public float getXDiff() { return xDiff; }
    public float getYDiff() { return yDiff; }
    public float getDeg() { return deg; }
    public Vector2 getSpiralPos() { return pos; }
}
