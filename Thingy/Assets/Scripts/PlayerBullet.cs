using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    private Vector2 position;
    public GameObject shot;
    private float speed, direction;
    private bool isAimed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        position = new Vector2(position.x + (speed * Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad * direction)), position.y - (speed * Time.deltaTime * Mathf.Sin(Mathf.Deg2Rad * direction)));
        shot.transform.position = position;
    }

    public void SpawnDirectional(float x, float y, float s, float dir)
    {
        speed = s;
        direction = dir;
        isAimed = false;
        position = new Vector2(x, y);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == "Boss")
        {
            Destroy(gameObject);
        }
    }
    public Vector2 getPos()
    {
        return position;
    }

}
