using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPassport : MonoBehaviour
{
    public GameObject passport;
    private DF2ClientTester dialogflow;
   

    private void Start()
    {
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
    }

    public void PassportInteraction()
    {
        if(GameManager.procedure == 36)
        {
            passport.SetActive(true);
            dialogflow.content = "GIVING_PASSPORT";
            dialogflow.SendText();
        }
        else if (GameManager.procedure == 38)
        {
            passport.SetActive(false);
            dialogflow.content = "GET_PASSPORT";
            dialogflow.SendText();
        }
    }

    public void MoveSceneToGame()
    {
        if(GameManager.procedure == 41)
            SceneManager.LoadScene("출국");
    }
}
