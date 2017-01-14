using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Boss script will change variables on this based on health loss

    //Boss related
    public GameObject shot;
    private bool isAimed,aimedShots;
    private int count;
    private float speed, direction, xAwayFromTarget, yAwayFromTarget, xSizeDiff, ySizeDiff;
    private Vector2 position;
    //spiral related
    private bool spiralSpawnsOnBoss, isSpiral,cw;
    private float aimDegree,spiralSpeed,spiralDir,spXDiff,spYDiff,spSpeed,spShotSpeed;
    private int spiralSpawnDelay, spiralDegIncrement;
    private Vector2 spiralPos;

    //lists of bullets
    private List<GameObject> spiral, ring, aimed, spread;
    //the SizeDiffs are for in case we want to adjust the position the bullets spawn from on the object
    // Use this for initialization
    void Start()
    {
        count = 0;
        xSizeDiff = 0;
        ySizeDiff = 0;
        spiralSpawnsOnBoss = true;
        spXDiff = 0;
        spYDiff = 0;
        speed = 0;
        direction = 0;
        isAimed = false;
        isSpiral = true;
        cw = true;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        //Boss movement
        if (!isAimed)
        {
            position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * direction)), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * direction)));
        }
        else
        {
            position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget) - 90))), position.y - (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget) - 90))));
        }
        //Spiral spawner movement
        if (isSpiral)
        {
            if (spiralSpawnsOnBoss) { spiralPos = position; }
            if (count % spiralSpawnDelay == 0)
            {
                if (cw)
                {
                    if (aimDegree >= 360) { aimDegree = 0; }
                    aimDegree += spiralDegIncrement;
                }
                else
                {
                    if (aimDegree <= 0) { aimDegree = 360; }
                    aimDegree -= spiralDegIncrement;
                }
                spiral.Add(Instantiate(shot));
                spiral[spiral.Count - 1].GetComponent<Bullet>().SpawnDirectional(spiralPos.x + spXDiff, spiralPos.y + spYDiff, spShotSpeed, aimDegree);
            }
            List<GameObject> tmp = spiral;
            for (int i = 0; i < spiral.Count; i++)
            {
                float tmpX = spiral[i].transform.position.x;
                float tmpY = spiral[i].transform.position.y;
                if (tmpX > 800 || tmpX < -20 || tmpY > 800 || tmpY < -20) { Destroy(tmp[i], .5f); }
            }
            spiral = tmp;
        }
    }
    //spiral methods - sets
    public void swapSpiralDir() { cw = !cw; }
    public void setSpiralSpawnDelay(int i) { spiralSpawnDelay = i; }
    public void setSpiralDegInc(int i) { spiralDegIncrement = i; }
    public void setSpiralPosSpeed(float f) { spSpeed = f; }
    public void setSpiralShotSpeed(float f) { spShotSpeed = f; }
    //spiral methods - gets
    public bool getSpiralDir() { return cw; }
    public int getSpiralSpawnDealy() { return spiralSpawnDelay; }
    public int getSpiralDegInc() { return spiralDegIncrement; }
    public float getSpiralPosSpeed() { return spSpeed; }
    public float getSpiralShotSpeed() { return spShotSpeed; }
    public Vector2 getSpiralPos() { return spiralPos; }

    //boss methods - sets
    void setSizeDiff(int x, int y) { xSizeDiff = x; ySizeDiff = y; }
    void setSpeed(float f) { speed = f; }
    void incrementSpeed(float f) { speed += f; }
    void setDir(float f) { direction = f; }
    void incrementDir(float f) { direction = f; }
    void setXSizeDiff(float f) { xSizeDiff = f; }
    void setYSizeDiff(float f) { ySizeDiff = f; }
    void toggleAimedMove() { isAimed = !isAimed; }
    void setBossPos(Vector2 v) { position = v; }
    //boss methods - gets
    public float getSpeed() { return speed; }
    public float getDir() { return direction; }
    public float getXDiff() { return xAwayFromTarget; }
    public float getYDiff() { return yAwayFromTarget; }
    public float getXSizeDiff() { return xSizeDiff; }
    public float getYSizeDiff() { return ySizeDiff; }
    public bool getAimed() { return isAimed; }
    public Vector2 getPos() { return position; }
}
