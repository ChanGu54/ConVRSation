using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLineCol : MonoBehaviour
{
    private AnimInteract anim_Inter;
    private DF2ClientTester dialogflow;

    private void Start()
    {
        anim_Inter = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MixedRealityPlayspace" && GameManager.procedure == 3)
        {
            anim_Inter._procedure = 4;
            dialogflow.content = "go_to_scanner";
            dialogflow.SendText();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "MixedRealityPlayspace" && GameManager.procedure == 3)
        {
            anim_Inter._procedure = 4;
            dialogflow.content = "go_to_scanner";
            dialogflow.SendText();
        }
    }
}
