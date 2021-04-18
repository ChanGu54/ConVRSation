using Syrus.Plugins.DFV2Client;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class RaycastTargeting : MonoBehaviour
{
    private AnimInteract anim;
    private DialogFlowV2Client global;
    private DF2ClientTester dialogflow;
    private GoogleTTS_Chan tts;
    public float hitRange;
    RaycastHit hit;
    int layerMask_dialogflow;
    int layerMask_convai;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("AnimationCtrler").GetComponent<AnimInteract>();
        hitRange = 5f;
        layerMask_dialogflow = LayerMask.GetMask("dialogflow");
        layerMask_convai = LayerMask.GetMask("convai");

        var go = GameObject.Find("Dialogflow");
        global = go.GetComponent<DialogFlowV2Client>();
        dialogflow = go.GetComponent<DF2ClientTester>();

        tts = GameObject.Find("GCTextToSpeech").GetComponent<GoogleTTS_Chan>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, hitRange, layerMask_dialogflow))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            Conv_With_Dialogflow(hit.transform.gameObject);
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, hitRange, layerMask_convai))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.blue);
            Conv_With_NLP(hit.transform.gameObject);

        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, hitRange))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
        }
    }

    public void Conv_With_Dialogflow(GameObject go)
    {
        GameManager.status = true;
        GameManager.talking_target = go;
        string name = go.name;

        if (name.Equals("Adela1"))
        {
            if (global.index == 0)
                return;
            global.index = 0;
            tts.voice_selected = 0;
        }
        else if (name.Equals("Maison12"))
        {
            if (global.index == 2)
                return;
            global.index = 2;
            tts.voice_selected = 2;
        }
        else if (name.Equals("Bellisa13"))
        {
            if (global.index == 1)
                return;
            global.index = 1;
            tts.voice_selected = 1;
        }
        else if (name.Equals("Brianna14"))
        {
            if (global.index == 2)
                return;
            global.index = 2;
            tts.voice_selected = 5;
        }
        else if (name.Equals("Noah15"))
        {
            if (global.index == 1)
                return;
            global.index = 1;
            tts.voice_selected = 6;
        }
        else if (name.Equals("Ethan16"))
        {
            if (global.index == 3)
                return;
            global.index = 3;
            tts.voice_selected = 8;
        }
        else if (name.Equals("Camila17"))
        {
            if (global.index == 4)
                return;
            global.index = 4;
            tts.voice_selected = 7;
        }
        else if (name.Equals("Tom23"))
        {
            if (global.index == 5)
                return;
            global.index = 5;
            tts.voice_selected = 10;
        }
        else if (name.Equals("Jack24"))
        {
            if (global.index == 6)
                return;
            global.index = 6;
            tts.voice_selected = 12;
        }
        else if (name.Equals("Tori0"))
        {
            if (global.index == 7)
                return;
            global.index = 7;
            tts.voice_selected = 7;
        }
        else if (name.Equals("Leo29"))
        {
            if (global.index == 8)
                return;
            global.index = 8;
            tts.voice_selected = 6;
        }
        else
        {
            return;
        }

        GameManager.tts_audiosource = go.GetComponent<AudioSource>();
        string strTmp = Regex.Replace(name, @"\D", "");
        var flag = int.Parse(strTmp) - 1;
        Debug.Log(flag);
        GameManager.index = flag.ToString();
        Debug.Log("Collided : " + flag);
        if (name.Equals("Maison12") || name.Equals("Brianna14"))
        {
            dialogflow.content = "arrive_security_checkpoint";
            dialogflow.SendText_Init();
            hitRange = 0f;
        }
        else if (name.Equals("Noah15") || name.Equals("Bellisa13"))
        {
            dialogflow.content = "go_to_scanner_again";
            dialogflow.SendText_Init();
        }
        else if (name.Equals("Ethan16") || name.Equals("Camila17"))
        {
            return;
        }
        else if (name.Equals("Tom23"))
        {
            anim.flag = 1;
            dialogflow.content = "arrive_immigration";
            dialogflow.SendText_Init();
            anim._procedure = 21;
        }
        else if (name.Equals("Jack24"))
        {
            anim.flag = 0;
            dialogflow.content = "arrive_immigration";
            dialogflow.SendText_Init();
            anim._procedure = 21;
        }
        else
            dialogflow.StartTalking();
    }
    
    public void Conv_With_NLP(GameObject go)
    {
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
