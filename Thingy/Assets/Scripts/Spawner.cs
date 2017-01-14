using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Boss script will change variables on this based on health loss

    //Boss related
    public GameObject shot, boss,player;
    private bool isAimed,aimedShots;
    private int count;
    private float speed, direction, xAwayFromTarget, yAwayFromTarget, xSizeDiff, ySizeDiff;
    private Vector2 position;

    //lists of bullets
    private List<GameObject> bullets;
    private List<Spiral> spirals;
    private List<AimedSpread> spreads;
    //the SizeDiffs are for in case we want to adjust the position the bullets spawn from on the object
    // Use this for initialization
    void Start()
    {
        bullets = new List<GameObject>();
        count = 0;
        xSizeDiff = 0;
        ySizeDiff = 0;
        speed = 0;
        direction = 0;
        spirals = new List<Spiral>();
        spreads = new List<AimedSpread>();
        for (int i = 0; i < 3; i++)
        {
            addSpiral(i * 120, 5 - 1, .1f, 5 - i, true);
            if (i % 2 == 0)
            {
                spirals[i].swapSpiralDir();
            }
        }
        addSpread(90, 10, .3f, 4, true);
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        //Boss movement
        if (!isAimed)
        {
            position = boss.transform.position;
          //position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * direction)), position.y - (speed * Mathf.Sin(Mathf.Deg2Rad * direction)));
          //boss.transform.position = position;
        }
        else
        {
            position = boss.transform.position;
          //position = new Vector2(position.x + (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget) - 90))), position.y - (speed * Mathf.Cos(Mathf.Deg2Rad * (Mathf.Atan2(xAwayFromTarget, yAwayFromTarget) - 90))));
          //boss.transform.position = position;
        }
        //Spiral spawner movement
        for (int i = 0; i < spirals.Count; i++)
        {
            spirals[i].incrementDeg();
            spirals[i].setSpiralPos(position);
            if (count % spirals[i].getSpiralSpawnDelay() == 0)
            {
                bullets.Add((GameObject)Instantiate(shot));
                bullets[bullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(spirals[i].getSpiralPos().x + spirals[i].getXDiff(), spirals[i].getSpiralPos().y + spirals[i].getYDiff(), spirals[i].getSpiralShotSpeed(), spirals[i].getDeg());
            }
        }
        for (int i = 0; i < spreads.Count; i++)
        {
            spreads[i].setPos(position);
            if (count % spreads[i].getSpawnDelay() == 0)
            {
                float xAway = player.transform.position.x - position.x;
                float yAway = player.transform.position.y - position.y;
                for (int b = 0; b < spreads[i].getNumber(); b++)
                {
                    bullets.Add((GameObject)Instantiate(shot));
                    bullets[bullets.Count - 1].GetComponent<Bullet>().SpawnSpread(spreads[i].getPos().x + spreads[i].getXDiff(), spreads[i].getPos().y + spreads[i].getYDiff(), spreads[i].getShotSpeed(), xAway, yAway, b ,spreads[i].getNumber()-1, spreads[i].getSpread());
                }
            }
        }
        List<GameObject> tmp = bullets;
        for (int i = 0; i < bullets.Count; i++)
        {
            float tmpX = bullets[i].transform.position.x;
            float tmpY = bullets[i].transform.position.y;
            if (tmpX > 800 || tmpX < -20 || tmpY > 800 || tmpY < -20) { Destroy(tmp[i], .5f); tmp.RemoveAt(i); }
        }
        bullets = tmp;
    }

    void addSpiral(float deg, int delay, float speed, int inc, bool onBoss)
    {
        spirals.Add(new Spiral());
        spirals[spirals.Count - 1].setDeg(deg);
        spirals[spirals.Count - 1].setSpiralSpawnDelay(delay);
        spirals[spirals.Count - 1].setSpiralShotSpeed(speed);
        spirals[spirals.Count - 1].setSpiralDegInc(inc);
        spirals[spirals.Count - 1].setSpawnOnBoss(onBoss);
    }

    void addSpread(float spread, int delay, float speed, int number, bool onBoss)
    {
        spreads.Add(new AimedSpread());
        spreads[spreads.Count - 1].setSpread(spread);
        spreads[spreads.Count - 1].setSpawnDelay(delay);
        spreads[spreads.Count - 1].setShotSpeed(speed);
        spreads[spreads.Count - 1].setNum(number);
        spreads[spreads.Count - 1].setSpawnOnBoss(onBoss);
    }

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
