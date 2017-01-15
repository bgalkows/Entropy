using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageThree: MonoBehaviour
{
    //Boss script will change variables on this based on health loss

    //Boss related
    public GameObject shot, bulletSpawner, boss, player, exploder, playerBullet, rShot, spirSat, spirSatL;
    private bool isAimed, aimedShots, firing, spawnerSpawnerStream, ascending;
    private int count, start; //start is startPhase value
    private float speed, direction, xAwayFromTarget, yAwayFromTarget, xSizeDiff, ySizeDiff;
    private Vector2 position;
    private int refractOffset, maxDeg, minDeg, inc;
    //lists of bullets
    private List<GameObject> spBullets, aimBullets, explosives, spirals, spreads, spawnerSpawners, explosiveSpawners, explosionBullets, playerBullets, refracted, ringStream, spirSatBullets, spirSatLBullets;
    private GameObject[] refractellites;
    //refracted bullets are just normal bullets with a different tag
    //the SizeDiffs are for in case we want to adjust the position the bullets spawn from on the object
    // Use this for initialization
    void Start()
    {
        start = 4;
        boss.GetComponent<PhaseControl>().setPhase(start); //Phase tbd when patterns are done
        spirSat.AddComponent<Spiral>();
        spirSatL.AddComponent<Spiral>();
        spBullets = new List<GameObject>();
        aimBullets = new List<GameObject>();
        explosionBullets = new List<GameObject>();
        ringStream = new List<GameObject>();
        spirSatBullets = new List<GameObject>();
        spirSatLBullets = new List<GameObject>();
        refractellites = GameObject.FindGameObjectsWithTag("Refractor");
        refracted = new List<GameObject>();
        refractOffset = 30;
        ascending = true;
        count = 0;
        xSizeDiff = 0;
        ySizeDiff = 0;
        inc = 0;
        speed = 0;
        direction = 0;
        spawnerSpawnerStream = true;
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

        //addSpiral(1, 5, .05f, 12, false, -8, 1.5f, true);
        //addSpiral(179, 5, .05f, -12, false, 8, 1.5f, true);
        //addSpiral(89, 5, .07f, 8, false, -5, 2.5f, true);
        //addSpiral(91, 5, .07f, -8, false, 5, 2.5f, true);
        //addSpread(160, 20, .1f, 8, true, 0, 0, true, false);
        for (int i = 0; i < 2; i++)
        {
            addSpiral(safeRandomValue()*360,(int)(safeRandomValue()*15)+1, safeRandomValue()/ 100,(int)(safeRandomValue() * 3),true,0,0,false);
        }
        spirSat.GetComponent<Spiral>().setDeg(110);
        spirSat.GetComponent<Spiral>().setSpiralSpawnDelay(1);
        spirSat.GetComponent<Spiral>().setSpiralShotSpeed(.05f);
        spirSat.GetComponent<Spiral>().setSpiralDegInc(6);
        spirSat.GetComponent<Spiral>().setSpawnOnBoss(false);
        spirSat.GetComponent<Spiral>().setBounce(false);
        maxDeg = 260;
        spirSatL.GetComponent<Spiral>().setDeg(290);
        spirSatL.GetComponent<Spiral>().setSpiralSpawnDelay(2);
        spirSatL.GetComponent<Spiral>().setSpiralShotSpeed(.04f);
        spirSatL.GetComponent<Spiral>().setSpiralDegInc(7);
        spirSatL.GetComponent<Spiral>().setSpawnOnBoss(false);
        spirSatL.GetComponent<Spiral>().setBounce(false);
        minDeg =440; //bottom for spirSatL
        addSpawnerSpawner(8, 90, .01f,true, 0, 0);

    }

    public float safeRandomValue()
    {
        float attemptedRand = Random.value;
        if(attemptedRand <= 0.000001)
        {
            return attemptedRand + 0.01f;
        }
        else
        {
            return attemptedRand;
        }
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
        if (boss.GetComponent<PhaseControl>().getPhase() != 0)
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
                    if (!b.GetComponent<Bullet>().getBounce() && ((tmpX > 12) || (tmpX < -12)) || (tmpY > 5) || (tmpY < -5)) { Destroy(b); }
                    if (b.GetComponent<Bullet>().getBounce() && tmpX > 12 || tmpX < -12) { b.GetComponent<Bullet>().bounce(); b.GetComponent<Bullet>().setBounce(false); }
                }
            }
            for (int i = ringStream.Count- 1; i >= 0; i--)
            {
                GameObject b = ringStream[i];
                if (b == null) { ringStream.Remove(b); }
                else
                {   
                    float tmpX = b.GetComponent<Bullet>().getPos().x;
                    float tmpY = b.GetComponent<Bullet>().getPos().y;
                    if (!b.GetComponent<Bullet>().getBounce() && ((tmpX > 12) || (tmpX < -12)) || (tmpY > 5) || (tmpY < -5)) { Destroy(b); }
                    if (b.GetComponent<Bullet>().getBounce() && tmpX > 12 || tmpX < -12) { b.GetComponent<Bullet>().bounce(); b.GetComponent<Bullet>().setBounce(false); }
                }
            }
            //if (boss.GetComponent<PhaseControl>().getPhase() <= start - 1)
            //{

            //    for (int i = 0; i < spreads.Count; i++)
            //    {

            //    }
            //}
            //for (int i = explosives.Count - 1; i >= 0; i--)
            //{
            //    GameObject b = explosives[i];
            //    if (b == null) { explosives.Remove(b); }
            //    else if (b.GetComponent<ExplodingShot>().getSpd() < 0 && !b.GetComponent<ExplodingShot>().getExploded())
            //    {
            //        //explodes it once direction reverses - aboslutely impossible to dodge with more than 2 fired with low speed
            //        for (int c = 0; c < b.GetComponent<ExplodingShot>().getNum(); c++)
            //        {
            //            explosionBullets.Add(Instantiate(shot));
            //            explosionBullets[explosionBullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(b.transform.position.x, b.transform.position.y, b.GetComponent<ExplodingShot>().getShotSpd(), c * 360 / b.GetComponent<ExplodingShot>().getNum(), false);
            //        }
            //        b.GetComponent<ExplodingShot>().explosion();
            //        Destroy(b);
            //    }
            //    else if (b.GetComponent<ExplodingShot>().getCD() <= 0) { Destroy(b); }
            //    else
            //    {
            //        float tmpX = b.GetComponent<ExplodingShot>().getPos().x;
            //        float tmpY = b.GetComponent<ExplodingShot>().getPos().y;
            //        if (tmpX > 12 || tmpX < -12 || tmpY > 12 || tmpY < -12) { Destroy(b); }
            //    }
            //}
            //for (int i = explosionBullets.Count - 1; i >= 0; i--)
            //{
            //    GameObject b = explosionBullets[i];
            //    if (b == null) { explosionBullets.Remove(b); }
            //    else
            //    {
            //        float tmpX = b.GetComponent<Bullet>().getPos().x;
            //        float tmpY = b.GetComponent<Bullet>().getPos().y;
            //        if (tmpX > 12 || tmpX < -12 || tmpY > 12 || tmpY < -12) { Destroy(b); }
            //    }
            //}
            //for (int i = explosiveSpawners.Count - 1; i >= 0; i--)
            //{
            //    GameObject b = explosiveSpawners[i];
            //    if (b == null) { explosiveSpawners.Remove(b); }
            //    else
            //    {
            //        float tmpX = b.GetComponent<BulletSpawner>().getPosition().x;
            //        float tmpY = b.GetComponent<BulletSpawner>().getPosition().y;
            //        if (tmpX > 12 || tmpX < -12 || tmpY > 12 || tmpY < -12) { Destroy(b); }
            //    }
            //}
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
                    spirals[i].GetComponent<Spiral>().setSpiralDegInc((int)(safeRandomValue() * 360));
                    spirals[i].GetComponent<Spiral>().setSpiralShotSpeed(safeRandomValue() / 100);
                    if (count % spirals[i].GetComponent<Spiral>().getSpiralSpawnDelay() == 0)
                    {
                        spBullets.Add(Instantiate(shot));
                        spBullets[spBullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(spirals[i].GetComponent<Spiral>().getSpiralPos().x + spirals[i].GetComponent<Spiral>().getXDiff(), spirals[i].GetComponent<Spiral>().getSpiralPos().y + spirals[i].GetComponent<Spiral>().getYDiff(), spirals[i].GetComponent<Spiral>().getSpiralShotSpeed(), spirals[i].GetComponent<Spiral>().getDeg(), spirals[i].GetComponent<Spiral>().getBounce());
                        spBullets[spBullets.Count - 1].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                    }
                }
            }
            if (boss.GetComponent<PhaseControl>().getPhase() <= start - 1)
            {
                if (ascending)
                {
                    spirSat.transform.position = new Vector2(spirSat.transform.position.x, spirSat.transform.position.y + Random.value / 50);
                    spirSatL.transform.position = new Vector2(spirSatL.transform.position.x, spirSatL.transform.position.y + Random.value / 50);
                }
                else
                {
                    spirSat.transform.position = new Vector2(spirSat.transform.position.x, spirSat.transform.position.y - Random.value / 50);
                    spirSatL.transform.position = new Vector2(spirSatL.transform.position.x, spirSatL.transform.position.y - Random.value / 50);
                }
                if (spirSat.transform.position.y > 4 && spirSatL.transform.position.y > 4)
                { ascending = false; }
                if (spirSat.transform.position.y < -4 && spirSatL.transform.position.y < -4)
                { ascending = true; }
                spirSat.GetComponent<Spiral>().setSpiralPos(spirSat.transform.position);
                spirSatL.GetComponent<Spiral>().setSpiralPos(spirSatL.transform.position);
                for (int i = spirSatBullets.Count-1;i>=0;i--)
                {
                    GameObject b = spirSatBullets[i];
                    if (b == null) { spirSatBullets.Remove(b); }
                    else
                    {
                        float tmpX = b.GetComponent<Bullet>().getPos().x;
                        float tmpY = b.GetComponent<Bullet>().getPos().y;
                        if (!b.GetComponent<Bullet>().getBounce() && ((tmpX > 12) || (tmpX < -12)) || (tmpY > 5) || (tmpY < -5)) { Destroy(b); }
                        if (b.GetComponent<Bullet>().getBounce() && tmpX > 12 || tmpX < -12) { b.GetComponent<Bullet>().bounce(); b.GetComponent<Bullet>().setBounce(false); }
                    }
                }
                    if (count % 1 == 0)
                {
                    if (spirSat.GetComponent<Spiral>().getDeg() > maxDeg || spirSat.GetComponent<Spiral>().getDeg() < 100)
                    { spirSat.GetComponent<Spiral>().swapSpiralDir(); }
                    spirSat.GetComponent<Spiral>().incrementDegS();
                    spirSatBullets.Add(Instantiate(shot));
                        spirSatBullets[spirSatBullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(spirSat.GetComponent<Spiral>().getSpiralPos().x + spirSat.GetComponent<Spiral>().getXDiff(), spirSat.GetComponent<Spiral>().getSpiralPos().y + spirSat.GetComponent<Spiral>().getYDiff(), spirSat.GetComponent<Spiral>().getSpiralShotSpeed(), spirSat.GetComponent<Spiral>().getDeg(), spirSat.GetComponent<Spiral>().getBounce());
                }
                for (int i = spirSatLBullets.Count - 1; i >= 0; i--)
                {
                    GameObject b = spirSatLBullets[i];
                    if (b == null) { spirSatLBullets.Remove(b); }
                    else
                    {
                        float tmpX = b.GetComponent<Bullet>().getPos().x;
                        float tmpY = b.GetComponent<Bullet>().getPos().y;
                        if (!b.GetComponent<Bullet>().getBounce() && ((tmpX > 12) || (tmpX < -12)) || (tmpY > 5) || (tmpY < -5)) { Destroy(b); }
                        if (b.GetComponent<Bullet>().getBounce() && tmpX > 12 || tmpX < -12) { b.GetComponent<Bullet>().bounce(); b.GetComponent<Bullet>().setBounce(false); }
                    }
                }
                if (count % 2 == 0)
                {
                    if (spirSatL.GetComponent<Spiral>().getDeg() < 300)
                    { spirSatL.GetComponent<Spiral>().setSpiralDegInc(10); }
                    if (spirSatL.GetComponent<Spiral>().getDeg() > 440)
                    { spirSatL.GetComponent<Spiral>().setSpiralDegInc(-10); }
                    spirSatL.GetComponent<Spiral>().incrementDegS();
                    spirSatLBullets.Add(Instantiate(shot));
                    spirSatLBullets[spirSatLBullets.Count - 1].GetComponent<Bullet>().SpawnDirectional(spirSatL.GetComponent<Spiral>().getSpiralPos().x + spirSatL.GetComponent<Spiral>().getXDiff(), spirSatL.GetComponent<Spiral>().getSpiralPos().y + spirSatL.GetComponent<Spiral>().getYDiff(), spirSatL.GetComponent<Spiral>().getSpiralShotSpeed(), spirSatL.GetComponent<Spiral>().getDeg(), spirSatL.GetComponent<Spiral>().getBounce());
                }
            }
            if (boss.GetComponent<PhaseControl>().getPhase() <= start - 2)
            {
                for (int i = 0; i < spawnerSpawners.Count; i++)
                {
                    spawnerSpawners[i].GetComponent<Ring>().setPos(position);
                    int interval = count % spawnerSpawners[i].GetComponent<Ring>().getSpawnDelay();
                    if (interval <= 100 && interval % 20== 0)
                    {
                        for (int b = 0; b < spawnerSpawners[i].GetComponent<Ring>().getNumber(); b++)
                        {
                            ringStream.Add(Instantiate(shot));
                            ringStream[ringStream.Count - 1].GetComponent<Bullet>().SpawnDirectional(spawnerSpawners[i].GetComponent<Ring>().getPos().x + spawnerSpawners[i].GetComponent<Ring>().getXDiff(), spawnerSpawners[i].GetComponent<Ring>().getPos().y + spawnerSpawners[i].GetComponent<Ring>().getYDiff(), spawnerSpawners[i].GetComponent<Ring>().getShotSpeed() + (5 - interval) * .004f, 360 * b / (spawnerSpawners[i].GetComponent<Ring>().getNumber()) + 22.5f+inc*((int)(Random.value)*10), false);
                            inc += 1;
                        }
                    }
                }
            }
            //if (boss.GetComponent<PhaseControl>().getPhase() <= start - 2)
            //{
            // for (int i = 0; i < refractellites.Length; i++)
            // {
            //  refractellites[i].GetComponent<Refractellite>().update();
            //  if (!refractellites[i].GetComponent<Refractellite>().isMoving())
            // {
            //      refractellites[i].GetComponent<Refractellite>().startL(.8f);
            //  }
            //  for (int b = 0; b < refractellites[i].GetComponent<Refractellite>().getFired().Count; b++)
            //  {
            //      if (!refractellites[i].GetComponent<Refractellite>().getFired()[b])
            //      {
            //         for (int c = 0; c < 2; c++)
            //         {
            //             Bullet refractor = refractellites[i].GetComponent<Refractellite>().getRefract()[b];
            //              refracted.Add(Instantiate(rShot));
            //               refracted[refracted.Count - 1].gameObject.tag = "Refracted";
            //               refracted[refracted.Count - 1].GetComponent<Bullet>().SpawnDirectional(refractellites[i].GetComponent<Refractellite>().getPos().x, refractellites[i].GetComponent<Refractellite>().getPos().y, .03f, refractellites[i].GetComponent<Refractellite>().getAngle() / 2 + c * 30, true);
            //            }
            //            refractellites[i].GetComponent<Refractellite>().fire(b);
            //       }

            //     }
            // }
            //        for (int i = refracted.Count - 1; i >= 0; i--)
            //        {
            //            GameObject b = refracted[i];
            //            if (b == null) { refracted.Remove(b); }
            //            else
            //            {
            //                float tmpX = b.GetComponent<Bullet>().getPos().x;
            //                float tmpY = b.GetComponent<Bullet>().getPos().y;
            //                bool hB = b.GetComponent<Bullet>().getBounce();
            //                bool vB = b.GetComponent<Bullet>().getVBounce();
            //                if ((!hB && (tmpX > 12 || tmpX < -12)) || (!vB && (tmpY > 5 || tmpY < -5))) { Destroy(b); }
            //                if (hB && (tmpX > 12 || tmpX < -12))
            //                { b.GetComponent<Bullet>().bounce(); }
            //                if (vB && (tmpY > 5 || tmpY < -5)) { b.GetComponent<Bullet>().vertBounce(); }
            //            }
            //        }
            //    }
            //    for (int i = 0; i < spawnerSpawners.Count; i++)
            //    {
            //        spawnerSpawners[i].GetComponent<Ring>().setPos(position);
            //        if (count % spawnerSpawners[i].GetComponent<Ring>().getSpawnDelay() == 0)
            //        {
            //            for (int b = 0; b < spawnerSpawners[i].GetComponent<Ring>().getNumber(); b++)
            //            {
            //                explosiveSpawners.Add(Instantiate(bulletSpawner));
            //                explosiveSpawners[explosiveSpawners.Count - 1].GetComponent<BulletSpawner>().SpawnDirectional(spawnerSpawners[i].GetComponent<Ring>().getPos().x + spawnerSpawners[i].GetComponent<Ring>().getXDiff(), spawnerSpawners[i].GetComponent<Ring>().getPos().y + spawnerSpawners[i].GetComponent<Ring>().getYDiff(), spawnerSpawners[i].GetComponent<Ring>().getShotSpeed(), 360 * b / (spawnerSpawners[i].GetComponent<Ring>().getNumber()), 50, .15f, 5, false, (360 / (spawnerSpawners[i].GetComponent<Ring>().getNumber())) / 2);
            //            }
            //        }
            //    }
            //    for (int i = 0; i < explosiveSpawners.Count; i++)
            //    {
            //        if (count % explosiveSpawners[i].GetComponent<BulletSpawner>().getDelay() == 0 && !(explosiveSpawners[i].GetComponent<BulletSpawner>().getPosition().y < -3))
            //        {
            //            for (int b = 0; b < explosiveSpawners[i].GetComponent<BulletSpawner>().getNum(); b++)
            //            {
            //                explosives.Add(Instantiate(exploder));
            //                explosives[explosives.Count - 1].GetComponent<ExplodingShot>().SpawnDirectional(explosiveSpawners[i].GetComponent<BulletSpawner>().getPosition().x, explosiveSpawners[i].GetComponent<BulletSpawner>().getPosition().y, explosiveSpawners[i].GetComponent<BulletSpawner>().getShotSpd(), 360 * b / explosiveSpawners[i].GetComponent<BulletSpawner>().getNum(), false, 3, .03f, 400);
            //            }
            //}
            //}
        }
    }

    void addSpiral(float deg, int delay, float speed, int inc, bool onBoss, float x, float y, bool bounce)
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

    void addSpread(float spread, int delay, float speed, int number, bool onBoss, float x, float y, bool vBounce, bool hBounce)
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
