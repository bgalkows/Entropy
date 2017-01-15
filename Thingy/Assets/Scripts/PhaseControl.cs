using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseControl : MonoBehaviour {
    private int hp;
    private int phases;
    public GameObject boss;
	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void hit()
    {
        hp -= 1;
        if (hp == 0)
        {
            phases-= 1;
            hp = 110 - (phases * 10);
        }
    }
    void onTriggerEnter2D(Collider2D c)
    {
        print("thing");
        if (c.gameObject.tag == "PBullet")
        hit();
    }
    public int getPhase() { return phases; }
    public void setPhase(int i) { phases = i; hp = 110 - (phases * 10); }
}
