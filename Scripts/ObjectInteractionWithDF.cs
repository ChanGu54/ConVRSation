using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ObjectInteractionWithDF : MonoBehaviour
{
    private DF2ClientTester dialogflow;
    public GameObject passport;

    void Start()
    {
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
    }

    public void Interaction_DF(ManipulationEventData data)
    {
        if (GameManager.procedure == 8) {
            dialogflow.content = data.ManipulationSource.name;
            dialogflow.SendText();
        }
    }

    public void Give_Passport()
    {
        passport.SetActive(true);
        dialogflow.content = "give_passport";
        dialogflow.SendText();
    }
}
