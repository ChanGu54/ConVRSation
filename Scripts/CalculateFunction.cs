using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateFunction : MonoBehaviour
{
    private AnimInteract anim_Inter;
    private DF2ClientTester dialogflow;
    private RaycastTargeting raycast_target;

    public GameObject target;

    private void Start()
    {
        anim_Inter = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
        raycast_target = GameObject.Find("MainCamera").GetComponent<RaycastTargeting>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.name == "MixedRealityPlayspace") && GameManager.procedure == 8)
        {
            anim_Inter._procedure = 8;
            raycast_target.Conv_With_Dialogflow(target);
            dialogflow.content = "arrive_counter";
            dialogflow.SendText();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!(other.gameObject.name == "MixedRealityPlayspace") && GameManager.procedure == 8)
        {
            anim_Inter._procedure = 9;
            raycast_target.Conv_With_Dialogflow(target);
            dialogflow.content = "arrive_counter";
            dialogflow.SendText();
        }
    }
}
