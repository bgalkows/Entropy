using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingShot : MonoBehaviour {
    public GameObject shot;
    private Vector2 pos;
    private float speed, direction,shotSpd,initSpd;
    public int num,countdown;
    private bool isBomber,exploded;
	// Use this for initialization
	void Start () {
		
	}

	
	// Update is called once per frame
	void Update ()
    {
        pos = new Vector2(pos.x + (speed * Mathf.Cos(Mathf.Deg2Rad * direction)), pos.y - (speed * Mathf.Sin(Mathf.Deg2Rad * direction)));
        shot.transform.position = pos;
        if (!isBomber)
        {
            if (speed > 0) { speed -= .01f; }
            else { countdown--; }
        }
    }
    public void SpawnDirectional(float x, float y, float s, float dir,bool bomber,int number,float shSpd,int cd)
    {
        pos = new Vector2(x, y);
        speed = s;
        initSpd = s;
        direction = dir;
        isBomber = bomber;
        num = number;
        shotSpd = shSpd;
        countdown = cd;
        exploded = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("BULLET HIT PLAYER");
            Destroy(gameObject);
        }
    }


    public float getSpd() { return speed; }
    public float getDir() { return direction; }
    public Vector2 getPos() { return pos; }
    public bool getBomber() { return isBomber; }
    public int getNum() { return num; }
    public float getShotSpd() { return shotSpd; }
    public float getInitSpd() { return initSpd; }
    public int getCD() { return countdown; }
    public bool getExploded() { return exploded; }

    public void setSpd(float f) { speed = f; }
    public void setDir(float f) { direction = f; }
    public void setPos(Vector2 v) { pos = v; }
    public void setBomber(bool b) { isBomber = b; }
    public void setNum(int i) { num = i; }
    public void setShotSpd(int i) { shotSpd = i; }
    public void explosion() { exploded = true; }
}
