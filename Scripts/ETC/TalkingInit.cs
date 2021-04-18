using Syrus.Plugins.DFV2Client;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class TalkingInit : MonoBehaviour
{
    private DialogFlowV2Client global;
    private DF2ClientTester dialogflow;
    private void Start()
    {
        Debug.Log("동작함");
        var go = GameObject.Find("Dialogflow");
        global = go.GetComponent<DialogFlowV2Client>();
        dialogflow = go.GetComponent<DF2ClientTester>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("dialogflow"))
        {
            GameManager.status = true;
            GameObject go = collision.gameObject;
            GameManager.talking_target = go;
            string name = go.name;
            GameManager.tts_audiosource = go.GetComponent<AudioSource>();
            string strTmp = Regex.Replace(name, @"\D", "");
            var flag = int.Parse(strTmp) - 1;
            Debug.Log(flag);
            GameManager.index = flag.ToString();
            Debug.Log("Collided : " + flag);

            if (name.Equals("Adela1"))
            {
                global.index = 0;
            }
            else if (name.Equals("Maison12"))
            {
                global.index = 2;
            }
            else if (name.Equals("Bellisa13"))
            {
                global.index = 1;
            }
            else if (name.Equals("Brianna14"))
            {
                global.index = 2;
            }
            else if (name.Equals("Noah15"))
            {
                global.index = 1;
            }
            else if (name.Equals("Ethan16"))
            {
                global.index = 3;
            }
            else if (name.Equals("Camila17"))
            {
                global.index = 4;
            }
            else
            {
                return;
            }
            dialogflow.StartTalking();
        }
        else if (collision.gameObject.tag.Equals("convai"))
        {
            GameObject go = collision.gameObject;
            string name = go.name;
            GameManager.tts_audiosource = go.GetComponent<AudioSource>();
            GameManager.status = false;
            string strTmp = Regex.Replace(name, @"\D", "");
            var flag = int.Parse(strTmp) - 1;
            Debug.Log(flag);
            GameManager.index = flag.ToString();
            Debug.Log("Collided : " + flag);
        }
    }
}
