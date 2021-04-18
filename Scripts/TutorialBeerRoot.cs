using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeerRoot : MonoBehaviour
{
    private DF2ClientTester dialogflow;
    private AnimInteract anim;

    void Start()
    {
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
        anim = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "Interaction" && GameManager.procedure == 40)
        {
            dialogflow.content = "TUTORIAL_ENDING";
            dialogflow.SendText();
            anim._procedure = 41;
        }
    }
}
