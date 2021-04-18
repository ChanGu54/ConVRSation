using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerCol : MonoBehaviour
{
    private AnimInteract anim_Inter;
    private RaycastTargeting raycast_target;
    public GameObject[] target;

    private void Start()
    {
        anim_Inter = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
        raycast_target = GameObject.Find("MainCamera").GetComponent<RaycastTargeting>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MixedRealityPlayspace" && GameManager.procedure == 4)
        {
            raycast_target.Conv_With_Dialogflow(target[anim_Inter.flag]);
            anim_Inter._procedure = 5;
        }
        else if (other.gameObject.name == "MixedRealityPlayspace" && GameManager.procedure == 6)
        {
            anim_Inter._procedure = 7;
            raycast_target.hitRange = 5f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "MixedRealityPlayspace" && GameManager.procedure == 4)
        {
            raycast_target.Conv_With_Dialogflow(target[anim_Inter.flag]);
            anim_Inter._procedure = 5;
        }
        else if (other.gameObject.name == "MixedRealityPlayspace" && GameManager.procedure == 6)
        {
            anim_Inter._procedure = 7;
            raycast_target.hitRange = 5f;
        }
    }
}
