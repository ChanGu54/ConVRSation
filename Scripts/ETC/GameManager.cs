using Syrus.Plugins.DFV2Client;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UMA.PoseTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameObject target;
    public static Animator animator;
    public static TextMeshPro tmp;

    
    public static GameObject talking_target
    {
        get
        {
            return target;
        }
        set
        {
            if (target != null)
                target.GetComponent<UMAExpressionPlayer>().enabled = false;
            target = value;
            target.GetComponent<UMAExpressionPlayer>().enabled = true;
            animator = target.GetComponent<Animator>();
            tmp = target.transform.Find("Panel/TextContent/Content").GetComponent<TextMeshPro>();
        }
    }

    public static GCSTT_Chan stt;
    public static AudioSource tts_audiosource;
    public static string index;
    // true : dialogflow, false : convai
    public static bool status;

    public static int procedure;
    // 0 : At the Store

    private void Start()
    {
        stt = GameObject.Find("GCSpeechToText").GetComponent<GCSTT_Chan>();
    }

    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
