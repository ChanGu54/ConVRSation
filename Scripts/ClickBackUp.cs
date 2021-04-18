using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBackUp : MonoBehaviour
{
    public GameObject[] bag;
    private AnimInteract anim;

    private void Start()
    {
        anim = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
    }

    public void BackUp_Left()
    {
        if (GameManager.procedure == 12)
        {
            bag[0].SetActive(true);
            anim._procedure = 1;
        }
    }

    public void BackUp_Right()
    {
        if (GameManager.procedure == 12)
        {
            bag[1].SetActive(true);
            anim._procedure = 1;
        }
    }
}
