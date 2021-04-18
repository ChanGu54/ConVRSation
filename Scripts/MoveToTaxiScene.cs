using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToTaxiScene : MonoBehaviour
{
    private float time;

    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 13)
        {
            ArrivalSceneMove();
        }
    }

    public void ArrivalSceneMove()
    {
        SceneManager.LoadScene("택시");
    }
}
