using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmigrationCtrler : MonoBehaviour
{
    private AnimInteract anim;
    private DF2ClientTester dialogflow;
    public GameObject[] passport;
    public GameObject letter;
    public GameObject returnticket;

    private void Start()
    {
        anim = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
    }

    public void Give_Passport()
    {
        if (GameManager.procedure == 21)
        {
            passport[anim.flag].SetActive(true);
            dialogflow.content = "give_passport";
            anim._procedure = 22;
            dialogflow.SendText();
        }
    }

    public void Give_Letter()
    {
        if (GameManager.procedure == 23)
        {
            letter.SetActive(true);
            dialogflow.content = "confirm_reservation";
            GameManager.tmp.text = "";
            anim._procedure = 22;
            dialogflow.SendText();
        }
    }


    public void Give_ReturnTicket()
    {
        if (GameManager.procedure == 26)
        {
            returnticket.SetActive(true);
            dialogflow.content = "return_ticket";
            GameManager.tmp.text = "";
            anim._procedure = 22;
            dialogflow.SendText();
        }
    }

    public void Take_Passport()
    {
        if (GameManager.procedure == 24)
        {
            passport[anim.flag].SetActive(false);
            anim._procedure = 27;
        }
    }

    public void Take_Stuff_Left()
    {
        if (GameManager.procedure == 25)
        {
            passport[anim.flag].SetActive(false);
            letter.SetActive(false);
            returnticket.SetActive(false);
            anim._procedure = 27;
        }
    }
}
