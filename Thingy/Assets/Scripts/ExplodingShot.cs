using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingShot : MonoBehaviour {
    public GameObject shot;
    private Vector2 pos;
    private float speed, direction;
    private bool isBomber;
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
            if (speed > 0)
            {
                speed -= .01f;
            }
        }
    }
    void SpawnDirectional(float x, float y, float s, float dir,bool bomber)
    {
        pos = new Vector2(x, y);
        speed = s;
        direction = dir;
        isBomber = bomber;
    }
    public float getSpd() { return speed; }
    public float getDir() { return direction; }
    public Vector2 getPos() { return pos; }
    public bool getBomber() { return isBomber; }
}
