using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialInter : MonoBehaviour
{
    public GameObject go;

    public void Inter()
    {
        if (go.activeSelf)
        {
            go.SetActive(false);
        }
        else
            go.SetActive(true);
    }

    public void MoveScene()
    {
        Destroy(GameObject.Find("GCSpeechToText"));
        SceneManager.LoadScene("출국");
    }
}
