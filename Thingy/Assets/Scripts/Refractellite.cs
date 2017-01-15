using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refractellite : MonoBehaviour {
    public GameObject sat;
    private List<Bullet> refract;
    private List<bool> hasFired;
    private bool linear; //either linear or sin
    private bool flipped, moving, increasing;
    private float lBound, rBound, speed, bSpeed;
    private int angle, increment;
    private Vector2 position;
    // Use this for initialization
    void Start() {
        hasFired = new List<bool>();
        refract = new List<Bullet>();
        linear = true;
        lBound = -10;
        rBound = 10;
        speed = .1f;
        bSpeed = .03f;
        moving = false;
        angle = 60;
        increment = 5;
        position = new Vector2(20, 20);
    }

    // Update is called once per frame
    void Update() {
    }

    public void update()
    {
        if (angle < 90)
        {
            increasing = true;
        }
        if (angle > 120)
        {
            increasing = false;
        }
        if (increasing)
        { angle += increment; }
        else { angle -= increment; }
        for (int i = hasFired.Count - 1; i >= 0; i--)
        {
            if (hasFired[i])
            {
                refract.RemoveAt(i);
                hasFired.RemoveAt(i);
            }
        }
        if (moving)
        {
            if (!flipped)
            {
                if (linear)
                {
                    position = new Vector2(position.x + (speed * Mathf.Cos(0)), position.y);
                }
                if (position.x > rBound)
                { flipped = !flipped; }
            }
            else
            {
                if (linear)
                {
                    position = new Vector2(position.x - (speed * Mathf.Cos(0)), position.y);
                }
                if (position.x < lBound)
                { flipped = !flipped; }
            }
        }
        sat.transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name=="Bullet Prefab(Clone)")
        {
            refract.Add(c.gameObject.GetComponent<Bullet>());
            hasFired.Add(false);
        }
    }
    public List<Bullet> getRefract() { return refract; }
    public List<bool> getFired() { return hasFired; }
    public bool getLin() { return linear; }
    public float getSpeed() { return speed; }
    public Vector2 getPos() { return position; }
    public bool isMoving(){ return moving; }
    public float getBSpeed() { return bSpeed; }
    public float getAngle() { return angle; }

    public void setLin(bool b) { linear = b; }
    public void fire(int i) { hasFired[i] = true; }
    public void setSpd(float f) { speed = f; }
    public void startL(float f)
    { 
        moving = true;
        setPos(new Vector2(-12,f));
    }
    public void startR(float f)
    {
        moving = true;
        setPos(new Vector2(12, f));
    }
    public void setPos(Vector2 v) { position = v; }
    
}
