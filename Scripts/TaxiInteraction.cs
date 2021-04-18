using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiInteraction : MonoBehaviour
{
    public GameObject card;
    private DF2ClientTester dialogflow;
    private AnimInteract anim;

    private void Start()
    {
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
        anim = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
    }

    public void CardInteraction() {
        if(GameManager.procedure == 54)
        {
            StartCoroutine(Delay());
        }
        else if(GameManager.procedure == 56)
        {
            card.SetActive(false);
            anim._procedure = 57;
        }
    }

    IEnumerator Delay()
    {
        card.SetActive(true);
        yield return new WaitForSeconds(1f);
        anim._procedure = 55;
    }
}
