using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Boss script will change variables on this based on health loss

    //Boss related
    public GameObject shot,bulletSpawner, boss,player;
    private bool isAimed,aimedShots;
    private int count;
    private float speed, direction, xAwayFromTarget, yAwayFromTarget, xSizeDiff, ySizeDiff;
    private Vector2 position;

    //lists of bullets
    private List<GameObject> bullets,explosives,spirals,spreads,spawnerSpawners,explosiveSpawners;
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
        spirals = new List<GameObject>();
        spreads = new List<GameObject>();
        //AimedSpreads can be used to create rings of bullets with one aimed at the player- just set the spread parameter to 360
        //Gonna use Ring for rings not aimed at the player
        spawnerSpawners = new List<GameObject>();
        explosiveSpawners = new List<GameObject>();
        explosives = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            addSpiral(i * 120, 5 - 1, .1f, 5 - i, true);
            if (i % 2 == 0)
            {
                spirals[i].GetComponent<Spiral>().swapSpiralDir();
            }
        }
        //Have these come in as the boss loses health in the actual
        addSpread(90, 10, .3f, 4, true);
        addSpread(150, 15, .2f, 5, true);
        addSpawnerSpawner(8, 100, .1f, true);
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
            spirals[i].GetComponent<Spiral>().incrementDeg();
            spirals[i].GetComponent<Spiral>().setSpiralPos(position);
            if (count % spirals[i].GetComponent<Spiral>().getSpiralSpawnDelay() == 0)
            {
                bullets.Add(Instantiate(shot));
                bullets[bullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(spirals[i].GetComponent<Spiral>().getSpiralPos().x + spirals[i].GetComponent<Spiral>().getXDiff(), spirals[i].GetComponent<Spiral>().getSpiralPos().y + spirals[i].GetComponent<Spiral>().getYDiff(), spirals[i].GetComponent<Spiral>().getSpiralShotSpeed(), spirals[i].GetComponent<Spiral>().getDeg());
            }
        }
        for (int i = 0; i < spreads.Count; i++)
        {
            spreads[i].GetComponent<AimedSpread>().setPos(position);
            if (count % spreads[i].GetComponent<AimedSpread>().getSpawnDelay() == 0)
            {
                float xAway = player.transform.position.x - position.x;
                float yAway = player.transform.position.y - position.y;
                for (int b = 0; b < spreads[i].GetComponent<AimedSpread>().getNumber(); b++)
                {
                    bullets.Add(Instantiate(shot));
                    if (spreads[i].GetComponent<AimedSpread>().getNumber() > 1) { bullets[bullets.Count - 1].GetComponent<Bullet>().SpawnSpread(spreads[i].transform.position.x + spreads[i].GetComponent<AimedSpread>().getXDiff(), spreads[i].GetComponent<AimedSpread>().transform.position.y + spreads[i].GetComponent<AimedSpread>().getYDiff(), spreads[i].GetComponent<AimedSpread>().getShotSpeed(), xAway, yAway, b, spreads[i].GetComponent<AimedSpread>().getNumber() - 1, spreads[i].GetComponent<AimedSpread>().getSpread()); }
                    else { bullets[bullets.Count - 1].GetComponent<Bullet>().SpawnSpread(spreads[i].transform.position.x + spreads[i].GetComponent<AimedSpread>().getXDiff(), spreads[i].GetComponent<AimedSpread>().transform.position.y + spreads[i].GetComponent<AimedSpread>().getYDiff(), spreads[i].GetComponent<AimedSpread>().getShotSpeed(), xAway, yAway, b, spreads[i].GetComponent<AimedSpread>().getNumber(), spreads[i].GetComponent<AimedSpread>().getSpread()); }
                }
            }
        }
        for (int i = 0; i < spawnerSpawners.Count; i++)
        {
            spawnerSpawners[i].GetComponent<Ring>().setPos(position);
            if (count % spawnerSpawners[i].GetComponent<Ring>().getSpawnDelay() == 0)
            {
                for (int b = 0; b < spawnerSpawners[i].GetComponent<Ring>().getNumber(); b++)
                {
                    explosiveSpawners.Add(Instantiate(bulletSpawner));
                    explosiveSpawners[explosiveSpawners.Count - 1].GetComponent<BulletSpawner>().SpawnDirectional(spawnerSpawners[i].GetComponent<Ring>().getPos().x + spawnerSpawners[i].GetComponent<Ring>().getXDiff(), spawnerSpawners[i].GetComponent<Ring>().getPos().y + spawnerSpawners[i].GetComponent<Ring>().getYDiff(), spawnerSpawners[i].GetComponent<Ring>().getShotSpeed(), 360 /spawnerSpawners[i].GetComponent<Ring>().getNumber()*i,25);
                }
            }
        }
        for (int i = 0; i < explosiveSpawners.Count; i++)
        {
            if (count % explosiveSpawners[i].GetComponent<BulletSpawner>().getDelay() == 0)
            {
               // explosives.Add((
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
        List<GameObject> Tmp = explosiveSpawners;
        for (int i = 0; i < explosiveSpawners.Count; i++)
        {
            float tmpX = explosiveSpawners[i].transform.position.x;
            float tmpY = explosiveSpawners[i].transform.position.y;
            if (tmpX > 800 || tmpX < -20 || tmpY > 800 || tmpY < -20) { Destroy(tmp[i], .5f); Tmp.RemoveAt(i); }
        }
        explosiveSpawners = Tmp;
    }

    void addSpiral(float deg, int delay, float speed, int inc, bool onBoss)
    {
        spirals.Add(new GameObject());
        spirals[spirals.Count - 1].AddComponent<Spiral>();
        spirals[spirals.Count - 1].GetComponent<Spiral>().setDeg(deg);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralSpawnDelay(delay);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralShotSpeed(speed);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralDegInc(inc);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpawnOnBoss(onBoss);
    }

    void addSpread(float spread, int delay, float speed, int number, bool onBoss)
    {
        spreads.Add(new GameObject());
        spreads[spreads.Count - 1].AddComponent<AimedSpread>();
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpread(spread);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpawnDelay(delay);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setShotSpeed(speed);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setNum(number);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpawnOnBoss(onBoss);
    }

    void addSpawner(float deg, int delay, float speed, int inc, bool onBoss)
    {
        explosiveSpawners.Add(new GameObject());
        explosiveSpawners[explosiveSpawners.Count - 1].AddComponent<BulletSpawner>();
        explosiveSpawners[explosiveSpawners.Count - 1].GetComponent<BulletSpawner>().setDegree(deg);
        explosiveSpawners[explosiveSpawners.Count - 1].GetComponent<BulletSpawner>().setDelay(delay);
        explosiveSpawners[explosiveSpawners.Count - 1].GetComponent<BulletSpawner>().setShotSpeed(speed);
        explosiveSpawners[explosiveSpawners.Count - 1].GetComponent<BulletSpawner>().setInc(inc);
        explosiveSpawners[explosiveSpawners.Count - 1].GetComponent<BulletSpawner>().setOnBoss(onBoss);
    }

    void addSpawnerSpawner(int num,int delay,float speed,bool onBoss)
    {
        spawnerSpawners.Add(new GameObject());
        spawnerSpawners[spawnerSpawners.Count - 1].AddComponent<Ring>();
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setNum(num);
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setSpawnDelay(delay);
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setShotSpeed(speed);
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setSpawnOnBoss(onBoss);
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
