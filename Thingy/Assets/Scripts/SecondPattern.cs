using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPattern : MonoBehaviour
{
    //Boss script will change variables on this based on health loss

    //Boss related
    public GameObject shot, bulletSpawner, boss, player, exploder, playerBullet;
    private bool isAimed, aimedShots,firing;
    private int count,hp,phases;
    private float speed, direction, xAwayFromTarget, yAwayFromTarget, xSizeDiff, ySizeDiff;
    private Vector2 position;

    //lists of bullets
    private List<GameObject> spBullets, aimBullets, explosives, spirals, spreads, spawnerSpawners, explosiveSpawners, explosionBullets, playerBullets;
    //the SizeDiffs are for in case we want to adjust the position the bullets spawn from on the object
    // Use this for initialization
    void Start()
    {
        spBullets = new List<GameObject>();
        aimBullets = new List<GameObject>();
        explosionBullets = new List<GameObject>();
        count = 0;
        xSizeDiff = 0;
        ySizeDiff = 0;
        speed = 0;
        direction = 0;
        phases = 0;
        hp = 110 - (phases * 10);
        playerBullets = new List<GameObject>();
        spirals = new List<GameObject>();
        spreads = new List<GameObject>();
        //AimedSpreads can be used to create rings of bullets with one aimed at the player- just set the spread parameter to 360
        //Gonna use Ring for rings not aimed at the player
        spawnerSpawners = new List<GameObject>();
        explosiveSpawners = new List<GameObject>();
        explosives = new List<GameObject>();
        ////The things I put for the first pattern:
        //for (int i = 0; i < 3; i++)
        //{
        //    addSpiral(i * 120, 5 + i, .05f, 20 - i * 2, true,0,0);
        //    if (i % 2 == 0)
        //    {
        //        spirals[i].GetComponent<Spiral>().swapSpiralDir();
        //    }
        //}
        //Have these come in as the boss loses health in the actual
        //addSpread(90, 10, .3f, 4, true,0,0);
        //addSpread(150, 20, .08f, 5, true,0,0);
        //addSpawnerSpawner(8, 100, .1f, true,0,0);
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
        for (int i = 0; i < playerBullets.Count; i++)
        {
            GameObject b = playerBullets[i];
            if (b == null) { aimBullets.Remove(b); }
            else
            {
                float tmpX = b.GetComponent<PlayerBullet>().getPos().x;
                float tmpY = b.GetComponent<PlayerBullet>().getPos().y;
                if (tmpX > 12 || tmpX < -12 || tmpY > 12 || tmpY < -12) { Destroy(b); }
            }
        }
        for (int i = spBullets.Count - 1; i >= 0; i--)
        {
            GameObject b = spBullets[i];
            if (b == null) { spBullets.Remove(b); }
            else
            {
                float tmpX = b.GetComponent<Bullet>().getPos().x;
                float tmpY = b.GetComponent<Bullet>().getPos().y;
                if ((tmpX > 12) || (tmpX < -12) || (tmpY > 12) || (tmpY < -12)) { Destroy(b); }
            }
        }
        for (int i = aimBullets.Count - 1; i >= 0; i--)
        {
            GameObject b = aimBullets[i];
            if (b == null) { aimBullets.Remove(b); }
            else
            {
                float tmpX = b.GetComponent<Bullet>().getPos().x;
                float tmpY = b.GetComponent<Bullet>().getPos().y;
                if (tmpX > 12 || tmpX < -12 || tmpY > 12 || tmpY < -12) { Destroy(b); }
            }
        }
        for (int i = explosives.Count - 1; i >= 0; i--)
        {
            GameObject b = explosives[i];
            if (b == null) { explosives.Remove(b); }
            else if (b.GetComponent<ExplodingShot>().getSpd() < 0 && !b.GetComponent<ExplodingShot>().getExploded())
            {
                //explodes it once direction reverses - aboslutely impossible to dodge with more than 2 fired with low speed
                for (int c = 0; c < b.GetComponent<ExplodingShot>().getNum(); c++)
                {
                    explosionBullets.Add(Instantiate(shot));
                    explosionBullets[explosionBullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(b.transform.position.x, b.transform.position.y, b.GetComponent<ExplodingShot>().getShotSpd(), c * 360 / b.GetComponent<ExplodingShot>().getNum(),false);
                }
                b.GetComponent<ExplodingShot>().explosion();
                Destroy(b);
            }
            else if (b.GetComponent<ExplodingShot>().getCD() <= 0) { Destroy(b); }
            else
            {
                float tmpX = b.GetComponent<ExplodingShot>().getPos().x;
                float tmpY = b.GetComponent<ExplodingShot>().getPos().y;
                if (tmpX > 12 || tmpX < -12 || tmpY > 12 || tmpY < -12) { Destroy(b); }
            }
        }
        for (int i = explosionBullets.Count - 1; i >= 0; i--)
        {
            GameObject b = explosionBullets[i];
            if (b == null) { explosionBullets.Remove(b); }
            else
            {
                float tmpX = b.GetComponent<Bullet>().getPos().x;
                float tmpY = b.GetComponent<Bullet>().getPos().y;
                if (tmpX > 12 || tmpX < -12 || tmpY > 12 || tmpY < -12) { Destroy(b); }
            }
        }
        for (int i = explosiveSpawners.Count - 1; i >= 0; i--)
        {
            GameObject b = explosiveSpawners[i];
            if (b == null) { explosiveSpawners.Remove(b); }
            else
            {
                float tmpX = b.GetComponent<BulletSpawner>().getPosition().x;
                float tmpY = b.GetComponent<BulletSpawner>().getPosition().y;
                if (tmpX > 12 || tmpX < -12 || tmpY > 12 || tmpY < -12) { Destroy(b); }
            }
        }
        //Spiral spawner movement
        for (int i = 0; i < spirals.Count; i++)
        {
            spirals[i].GetComponent<Spiral>().incrementDeg();
            spirals[i].GetComponent<Spiral>().setSpiralPos(position);
            if (count % spirals[i].GetComponent<Spiral>().getSpiralSpawnDelay() == 0)
            {
                spBullets.Add(Instantiate(shot));
                spBullets[spBullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(spirals[i].GetComponent<Spiral>().getSpiralPos().x + spirals[i].GetComponent<Spiral>().getXDiff(), spirals[i].GetComponent<Spiral>().getSpiralPos().y + spirals[i].GetComponent<Spiral>().getYDiff(), spirals[i].GetComponent<Spiral>().getSpiralShotSpeed(), spirals[i].GetComponent<Spiral>().getDeg(),true);
                spBullets[spBullets.Count - 1].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
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
                    aimBullets.Add(Instantiate(shot));
                    if (spreads[i].GetComponent<AimedSpread>().getNumber() > 1) { aimBullets[aimBullets.Count - 1].GetComponent<Bullet>().SpawnSpread(spreads[i].GetComponent<AimedSpread>().getPos().x + spreads[i].GetComponent<AimedSpread>().getXDiff(), spreads[i].GetComponent<AimedSpread>().GetComponent<AimedSpread>().getPos().y + spreads[i].GetComponent<AimedSpread>().getYDiff(), spreads[i].GetComponent<AimedSpread>().getShotSpeed(), xAway, yAway, b, spreads[i].GetComponent<AimedSpread>().getNumber() - 1, spreads[i].GetComponent<AimedSpread>().getSpread(),false); }
                    else { aimBullets[aimBullets.Count - 1].GetComponent<Bullet>().SpawnSpread(spreads[i].GetComponent<AimedSpread>().getPos().x + spreads[i].GetComponent<AimedSpread>().getXDiff(), spreads[i].GetComponent<AimedSpread>().GetComponent<AimedSpread>().getPos().y + spreads[i].GetComponent<AimedSpread>().getYDiff(), spreads[i].GetComponent<AimedSpread>().getShotSpeed(), xAway, yAway, b, spreads[i].GetComponent<AimedSpread>().getNumber(), spreads[i].GetComponent<AimedSpread>().getSpread(),false); }
                    aimBullets[aimBullets.Count - 1].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
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
                    explosiveSpawners[explosiveSpawners.Count - 1].GetComponent<BulletSpawner>().SpawnDirectional(spawnerSpawners[i].GetComponent<Ring>().getPos().x + spawnerSpawners[i].GetComponent<Ring>().getXDiff(), spawnerSpawners[i].GetComponent<Ring>().getPos().y + spawnerSpawners[i].GetComponent<Ring>().getYDiff(), spawnerSpawners[i].GetComponent<Ring>().getShotSpeed(), 360 * b / (spawnerSpawners[i].GetComponent<Ring>().getNumber()), 50, .15f, 5, false, (360 / (spawnerSpawners[i].GetComponent<Ring>().getNumber())) / 2);
                }
            }
        }
        for (int i = 0; i < explosiveSpawners.Count; i++)
        {
            if (count % explosiveSpawners[i].GetComponent<BulletSpawner>().getDelay() == 0 && !(explosiveSpawners[i].GetComponent<BulletSpawner>().getPosition().y < -3))
            {
                for (int b = 0; b < explosiveSpawners[i].GetComponent<BulletSpawner>().getNum(); b++)
                {
                    explosives.Add(Instantiate(exploder));
                    explosives[explosives.Count - 1].GetComponent<ExplodingShot>().SpawnDirectional(explosiveSpawners[i].GetComponent<BulletSpawner>().getPosition().x, explosiveSpawners[i].GetComponent<BulletSpawner>().getPosition().y, explosiveSpawners[i].GetComponent<BulletSpawner>().getShotSpd(), 360 * b / explosiveSpawners[i].GetComponent<BulletSpawner>().getNum(), false, 3, .03f, 400);
                }
            }
        }

        if (Input.GetKey(KeyCode.Z))
        {
            if (firing)
            {
                if (count % 3 == 0)
                {
                    playerBullets.Add(Instantiate(playerBullet));
                    playerBullets[playerBullets.Count - 1].GetComponent<PlayerBullet>().SpawnDirectional(player.transform.position.x, player.transform.position.y, 8, 270);
                }
            }
        }
    }

    void addSpiral(float deg, int delay, float speed, int inc, bool onBoss,float x,float y)
    {
        spirals.Add(new GameObject());
        spirals[spirals.Count - 1].AddComponent<Spiral>();
        spirals[spirals.Count - 1].GetComponent<Spiral>().setDeg(deg);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralSpawnDelay(delay);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralShotSpeed(speed);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralDegInc(inc);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpawnOnBoss(onBoss);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralPos(new Vector2(x, y));
    }

    void addSpread(float spread, int delay, float speed, int number, bool onBoss,float x, float y)
    {
        spreads.Add(new GameObject());
        spreads[spreads.Count - 1].AddComponent<AimedSpread>();
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpread(spread);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpawnDelay(delay);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setShotSpeed(speed);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setNum(number);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpawnOnBoss(onBoss);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setPos(new Vector2(x, y));
    }

    void addSpawnerSpawner(int num, int delay, float speed, bool onBoss, float x, float y)
    {
        spawnerSpawners.Add(new GameObject());
        spawnerSpawners[spawnerSpawners.Count - 1].AddComponent<Ring>();
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setNum(num);
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setSpawnDelay(delay);
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setShotSpeed(speed);
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setSpawnOnBoss(onBoss);
        spawnerSpawners[spawnerSpawners.Count - 1].GetComponent<Ring>().setPos(new Vector2(x, y));
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
