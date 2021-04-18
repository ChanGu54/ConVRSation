using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackCtrl : MonoBehaviour
{
    public GameObject backpack;
    private AnimInteract anim;

    private void Start()
    {
        anim = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
    }

    public void BackPackCtrler()
    {
        if (GameManager.procedure == 27)
        {
            backpack.SetActive(false);
            anim._procedure = 28;
        }
    }
}
