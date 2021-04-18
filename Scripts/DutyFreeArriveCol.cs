using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DutyFreeArriveCol : MonoBehaviour
{
    private AnimInteract anim_Inter;
    private RaycastTargeting raycast_target;
    public GameObject target;
    private DF2ClientTester dialogflow;

    private GameObject go;
    public GameObject gift;
    public GameObject btn;
 
    private void Start()
    {
        anim_Inter = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
        raycast_target = GameObject.Find("MainCamera").GetComponent<RaycastTargeting>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MixedRealityPlayspace" && GameManager.procedure == 7)
        {
            go = other.gameObject;
            raycast_target.Conv_With_Dialogflow(target);
            raycast_target.hitRange = 0f;
            anim_Inter._procedure = 8;
            dialogflow.content = "arrive_dutyfreeshop";
            dialogflow.SendText();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "MixedRealityPlayspace" && GameManager.procedure == 7)
        {
            go = other.gameObject;
            raycast_target.Conv_With_Dialogflow(target);
            raycast_target.hitRange = 0f;
            anim_Inter._procedure = 8;
            dialogflow.content = "arrive_dutyfreeshop";
            dialogflow.SendText();
        }
    }

    private void Update()
    {
        if(GameManager.procedure == 9 && GameManager.tmp.text.Contains("You can take your stuff now"))
        {
            StartCoroutine(AnimInteract.AnimConversion("물품건네기", "면세점_물품건네기"));
            if (!GameManager.tmp.text.Contains("Okay"))
            {
                go.SetActive(false);
                go = gift;
            }
            go.SetActive(true);
            btn.SetActive(true);
            anim_Inter._procedure = 10;
        }
    }

    public void End_Purchase()
    {
        if(GameManager.procedure == 10)
        {
            Destroy(go);
            dialogflow.content = "good_bye";
            dialogflow.SendText();
        }
    }
}
