using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToLvl2 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        nextLevel();
    }

    // Update is called once per frame
    void nextLevel()
    {
        StartCoroutine("waitThreeSeconds");
    }

    IEnumerator waitThreeSeconds()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level Two");
    }
}