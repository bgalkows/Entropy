using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refractellite : MonoBehaviour {
    private List<GameObject> refract;
    private List<bool> hasFired;
    // Use this for initialization
    void Start() {
        refract = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        for (int i = hasFired.Count - 1; i >= 0; i--)
        {
            if (hasFired[i])
            {
                refract.RemoveAt(i);
                hasFired.RemoveAt(i);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            refract.Add(c.gameObject);
            hasFired.Add(false);
        }
    }
    public List<GameObject> getRefract() { return refract; }
    public List<bool> getFired() { return hasFired; }
    public void fire(int i) { hasFired[i] = true; }
    
}
