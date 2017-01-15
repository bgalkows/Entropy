using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPattern : MonoBehaviour
{
    //Boss script will change variables on this based on health loss

    //Boss related
    public GameObject shot, bulletSpawner, boss, player, exploder, playerBullet,rShot;
    private bool isAimed, aimedShots,firing;
    private int count,start; //start is startPhase value
    private float speed, direction, xAwayFromTarget, yAwayFromTarget, xSizeDiff, ySizeDiff;
    private Vector2 position;
    private int refractOffset; //30 degrees for a 60 degree cone
    //lists of bullets
    private List<GameObject> spBullets, aimBullets, explosives, spirals, spreads, spawnerSpawners, explosiveSpawners, explosionBullets, playerBullets,refracted;
    private GameObject[] refractellites;
    //refracted bullets are just normal bullets with a different tag
    //the SizeDiffs are for in case we want to adjust the position the bullets spawn from on the object
    // Use this for initialization
    void Start()
    {
        start = 4;
        boss.GetComponent<PhaseControl>().setPhase(start); //Phase tbd when patterns are done
        spBullets = new List<GameObject>();
        aimBullets = new List<GameObject>();
        explosionBullets = new List<GameObject>();
        refractellites = GameObject.FindGameObjectsWithTag("Refractor");
        for (int i = 0; i < refractellites.Length;i++)
        {
            refractellites[i].GetComponent<Refractellite>().setPos(new Vector2(20, 20));
        }
        refracted = new List<GameObject>();
        refractOffset = 30;
        count = 0;
        xSizeDiff = 0;
        ySizeDiff = 0;
        speed = 0;
        direction = 0;
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

        addSpiral(1, 5, .05f, 12, false, -8, 1.5f,true);
        addSpiral(179, 5, .05f, -12, false, 8, 1.5f,true);
        addSpiral(89, 5, .07f, 8, false, -5, 2.5f, true);
        addSpiral(91, 5, .07f, -8, false, 5, 2.5f, true);
        addSpread(160,20,.1f,8,true,0,0,true,false);
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
        if (boss.GetComponent<PhaseControl>().getPhase()!=0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                if (count % 15 == 0)
                {
                    firing = !firing;
                }
                if (firing)
                {
                    if (count % 3 == 0)
                    {
                        playerBullets.Add(Instantiate(playerBullet));
                        playerBullets[playerBullets.Count - 1].GetComponent<PlayerBullet>().SpawnDirectional(player.transform.position.x, player.transform.position.y, 8, 270);
                    }
                }
            }
            for (int i = 0; i < playerBullets.Count; i++)
            {
                GameObject b = playerBullets[i];
                if (b == null) { playerBullets.Remove(b); }
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
                    if (!b.GetComponent<Bullet>().getBounce()&&((tmpX > 12) || (tmpX < -12)) || (tmpY > 5) || (tmpY < -5)) { Destroy(b); }
                    if (b.GetComponent<Bullet>().getBounce() && tmpX > 12 || tmpX < -12) { b.GetComponent<Bullet>().bounce();b.GetComponent<Bullet>().setBounce(false); }
                }
            }
            if (boss.GetComponent<PhaseControl>().getPhase() <= start - 1)
            {
                for (int i = aimBullets.Count - 1; i >= 0; i--)
                {
                    GameObject b = aimBullets[i];
                    if (b == null) { aimBullets.Remove(b); }
                    else
                    {
                        float tmpX = b.GetComponent<Bullet>().getPos().x;
                        float tmpY = b.GetComponent<Bullet>().getPos().y;
                        bool hB = b.GetComponent<Bullet>().getBounce();
                        bool vB = b.GetComponent<Bullet>().getVBounce();
                        if ((!hB && (tmpX > 12 || tmpX < -12)) || (!vB && (tmpY > 5 || tmpY < -5))) { Destroy(b); }
                        if (hB && (tmpX > 12 || tmpX < -12))
                        { b.GetComponent<Bullet>().bounce(); }
                        if (vB && (tmpY > 5 || tmpY < -5)) { b.GetComponent<Bullet>().vertBounce(); }
                    }
                }
                for (int i = 0; i < spreads.Count; i++)
                {
                    if (spreads[i].GetComponent<AimedSpread>().getSpawnOnBoss())
                    { spreads[i].GetComponent<AimedSpread>().setPos(position); }
                    if (count % spreads[i].GetComponent<AimedSpread>().getSpawnDelay() == 0)
                    {
                        float xAway = player.transform.position.x - position.x;
                        float yAway = player.transform.position.y - position.y;
                        for (int b = 0; b < spreads[i].GetComponent<AimedSpread>().getNumber(); b++)
                        {
                            aimBullets.Add(Instantiate(shot));
                            if (spreads[i].GetComponent<AimedSpread>().getNumber() > 1) { aimBullets[aimBullets.Count - 1].GetComponent<Bullet>().SpawnSpread(spreads[i].GetComponent<AimedSpread>().getPos().x + spreads[i].GetComponent<AimedSpread>().getXDiff(), spreads[i].GetComponent<AimedSpread>().GetComponent<AimedSpread>().getPos().y + spreads[i].GetComponent<AimedSpread>().getYDiff(), spreads[i].GetComponent<AimedSpread>().getShotSpeed(), xAway, yAway, b, spreads[i].GetComponent<AimedSpread>().getNumber() - 1, spreads[i].GetComponent<AimedSpread>().getSpread(), true, true); }
                            else { aimBullets[aimBullets.Count - 1].GetComponent<Bullet>().SpawnSpread(spreads[i].GetComponent<AimedSpread>().getPos().x + spreads[i].GetComponent<AimedSpread>().getXDiff(), spreads[i].GetComponent<AimedSpread>().GetComponent<AimedSpread>().getPos().y + spreads[i].GetComponent<AimedSpread>().getYDiff(), spreads[i].GetComponent<AimedSpread>().getShotSpeed(), xAway, yAway, b, spreads[i].GetComponent<AimedSpread>().getNumber(), spreads[i].GetComponent<AimedSpread>().getSpread(), true, true); }
                            aimBullets[aimBullets.Count - 1].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                        }
                    }
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
                        explosionBullets[explosionBullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(b.transform.position.x, b.transform.position.y, b.GetComponent<ExplodingShot>().getShotSpd(), c * 360 / b.GetComponent<ExplodingShot>().getNum(), false);
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
            if (boss.GetComponent<PhaseControl>().getPhase() <= start)
            {
                for (int i = 0; i < spirals.Count; i++)
                {
                    spirals[i].GetComponent<Spiral>().incrementDeg();
                    if (spirals[i].GetComponent<Spiral>().getSpawnOnBoss())
                    {
                        spirals[i].GetComponent<Spiral>().setSpiralPos(position);
                    }
                    if (count % spirals[i].GetComponent<Spiral>().getSpiralSpawnDelay() == 0)
                    {
                        spBullets.Add(Instantiate(shot));
                        spBullets[spBullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(spirals[i].GetComponent<Spiral>().getSpiralPos().x + spirals[i].GetComponent<Spiral>().getXDiff(), spirals[i].GetComponent<Spiral>().getSpiralPos().y + spirals[i].GetComponent<Spiral>().getYDiff(), spirals[i].GetComponent<Spiral>().getSpiralShotSpeed(), spirals[i].GetComponent<Spiral>().getDeg(), spirals[i].GetComponent<Spiral>().getBounce());
                        spBullets[spBullets.Count - 1].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                    }
                }
            }
            if (boss.GetComponent<PhaseControl>().getPhase() <= start-2)
            {
                for (int i = 0; i < refractellites.Length; i++)
                {
                    refractellites[i].GetComponent<Refractellite>().update();
                    if (!refractellites[i].GetComponent<Refractellite>().isMoving())
                    {
                        refractellites[i].GetComponent<Refractellite>().startL(1.5f);
                    }
                    for (int b = 0; b < refractellites[i].GetComponent<Refractellite>().getFired().Count; b++)
                    {
                        if (!refractellites[i].GetComponent<Refractellite>().getFired()[b])
                        {
                            for (int c = 0; c < 2; c++)
                            {
                                Bullet refractor = refractellites[i].GetComponent<Refractellite>().getRefract()[b];
                                refracted.Add(Instantiate(rShot));
                                refracted[refracted.Count - 1].gameObject.tag = "Refracted";
                                refracted[refracted.Count - 1].GetComponent<Bullet>().SpawnDirectional(refractellites[i].GetComponent<Refractellite>().getPos().x, refractellites[i].GetComponent<Refractellite>().getPos().y, .03f, refractellites[i].GetComponent<Refractellite>().getAngle() / 2 + c * 30, true);
                            }
                            refractellites[i].GetComponent<Refractellite>().fire(b);
                        }
                        
                    }
                }
                for (int i = refracted.Count-1;i>=0;i--)
                {
                    GameObject b = refracted[i];
                    if (b == null) { refracted.Remove(b); }
                    else
                    {
                        float tmpX = b.GetComponent<Bullet>().getPos().x;
                        float tmpY = b.GetComponent<Bullet>().getPos().y;
                        bool hB = b.GetComponent<Bullet>().getBounce();
                        bool vB = b.GetComponent<Bullet>().getVBounce();
                        if ((!hB && (tmpX > 12 || tmpX < -12)) || (!vB && (tmpY > 5 || tmpY < -5))) { Destroy(b); }
                        if (hB && (tmpX > 12 || tmpX < -12))
                        { b.GetComponent<Bullet>().bounce(); }
                        if (vB && (tmpY > 5 || tmpY < -5)) { b.GetComponent<Bullet>().vertBounce(); }
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
        }
    }

    void addSpiral(float deg, int delay, float speed, int inc, bool onBoss,float x,float y,bool bounce)
    {
        spirals.Add(new GameObject());
        spirals[spirals.Count - 1].AddComponent<Spiral>();
        spirals[spirals.Count - 1].GetComponent<Spiral>().setDeg(deg);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralSpawnDelay(delay);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralShotSpeed(speed);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralDegInc(inc);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpawnOnBoss(onBoss);
        spirals[spirals.Count - 1].GetComponent<Spiral>().setSpiralPos(new Vector2(x, y));
        spirals[spirals.Count - 1].GetComponent<Spiral>().setBounce(bounce);
    }

    void addSpread(float spread, int delay, float speed, int number, bool onBoss,float x, float y,bool vBounce,bool hBounce)
    {
        spreads.Add(new GameObject());
        spreads[spreads.Count - 1].AddComponent<AimedSpread>();
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpread(spread);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpawnDelay(delay);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setShotSpeed(speed);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setNum(number);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setSpawnOnBoss(onBoss);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setPos(new Vector2(x, y));
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setVB(vBounce);
        spreads[spreads.Count - 1].GetComponent<AimedSpread>().setHB(hBounce);
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
