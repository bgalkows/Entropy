using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseControl : MonoBehaviour {
    public int hp;
    private int phases;
    public GameObject boss;

    public int loss;

	// Use this for initialization
	void Start ()
    {
        loss = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void hit()
    {
        hp -= 1;
        loss += 1;

        if (hp == 0)
        {
            phases-= 1;
            hp = 110 - (phases * 10);
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == "PlayerBullet Prefab(Clone)")
        { hit(); }
    }
    public int getPhase() { return phases; }
    public void setPhase(int i) { phases = i; hp = 110 - (phases * 10); }
}
