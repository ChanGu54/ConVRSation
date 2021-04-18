using UnityEngine;
using Newtonsoft.Json;
using Syrus.Plugins.DFV2Client;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UMA.PoseTools;

public class DF2ClientTester : MonoBehaviour
{
    public string session;

    public string content;

    private string chatbotText;

    private DialogFlowV2Client client;

    private HelloRequester _helloRequester;

    private GameObject tts;
    
    // Start is called before the first frame update
    void Start()
    {
        tts = GameObject.Find("GCTextToSpeech");
        _helloRequester = new HelloRequester();
        client = GetComponent<DialogFlowV2Client>();
    }

    public void StartTalking()
    {
        // Adjustes session name if it is blank.
        string sessionName = GetSessionName();

        Debug.Log(sessionName);

        client.ChatbotResponded -= LogResponseText;
        client.DetectIntentError -= LogError;
        client.ChatbotResponded += LogResponseText;
        client.DetectIntentError += LogError;
        client.ReactToContext("DefaultWelcomeIntentpassport-followup",
            context => Debug.Log("Reacting to welcome followup"));
        client.SessionCleared += sess => Debug.Log("Cleared session " + session);
        client.AddInputContext(new DF2Context("userdata", 1, ("name", "George")), sessionName);

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "name", "George" }
        };
        client.DetectIntentFromEvent("Welcome", parameters, sessionName);
    }

    private void LogResponseText(DF2Response response)
    {
        Debug.Log(JsonConvert.SerializeObject(response, Formatting.Indented));
        Debug.Log(GetSessionName() + " said: \"" + response.queryResult.fulfillmentText + "\"");

        chatbotText = response.queryResult.fulfillmentText;

        StartCoroutine(TTS_Ctrl());
    }

    public void Direct_Link_to_Server()
    {
        chatbotText = "FALLBACK";
        StartCoroutine(TTS_Ctrl());
    }

    private void LogError(DF2ErrorResponse errorResponse)
    {
        Debug.LogError(string.Format("Error {0}: {1}", errorResponse.error.code.ToString(),
            errorResponse.error.message));
    }


    public void SendText_Init()
    {
        string sessionName = GetSessionName();

        Debug.Log(sessionName);

        client.ChatbotResponded -= LogResponseText;
        client.DetectIntentError -= LogError;
        client.ChatbotResponded += LogResponseText;
        client.DetectIntentError += LogError;
        client.SessionCleared += sess => Debug.Log("Cleared session " + session);
        client.AddInputContext(new DF2Context("userdata", 1, ("name", "empty")), sessionName);

        client.DetectIntentFromText(content, sessionName);
    }

    public void SendText()
    {
        //DF2Entity name0 = new DF2Entity("George", "George");
        //DF2Entity name1 = new DF2Entity("Greg", "Greg");
        //DF2Entity potion = new DF2Entity("Potion", "Potion", "Cure", "Healing potion");
        //DF2Entity antidote = new DF2Entity("Antidote", "Antidote", "Poison cure");
        //DF2EntityType names = new DF2EntityType("names", DF2EntityType.DF2EntityOverrideMode.ENTITY_OVERRIDE_MODE_SUPPLEMENT,
        //	new DF2Entity[] { name0, name1 });
        //DF2EntityType items = new DF2EntityType("items", DF2EntityType.DF2EntityOverrideMode.ENTITY_OVERRIDE_MODE_SUPPLEMENT,
        //	new DF2Entity[] { potion, antidote });

        string sessionName = GetSessionName();
        //      client.AddEntityType(names, sessionName);
        //client.AddEntityType(items, sessionName);

        client.DetectIntentFromText(content, sessionName);
    }


    public void SendEvent()
    {
        client.DetectIntentFromEvent(content,
            new Dictionary<string, object>(), GetSessionName());
    }

    public void Clear()
    {
        client.ClearSession(GetSessionName());
    }


    private string GetSessionName(string defaultFallback = "DefaultSession")
    {
        string sessionName = session;
        if (sessionName.Trim().Length == 0)
            sessionName = defaultFallback;
        return sessionName;
    }

    IEnumerator TTS_Ctrl()
    {
        Debug.Log("TTS Func");
        if (chatbotText == "FALLBACK")
        {
            if(_helloRequester.str == null)
            {
                _helloRequester.str = GameManager.index + " " + content;
                _helloRequester.Start();
            }

            yield return null;
        }

        while (_helloRequester.message == null && chatbotText == "FALLBACK")
            yield return null;

        // 챗봇 응신 받기
        if(chatbotText == "FALLBACK")
            chatbotText = _helloRequester.message;

        // 스레드 통신 과정에서 안터지고 살아남았을때
        if (_helloRequester.message != "FALLBACK")
        {
            GameManager.talking_target.transform.Find("Panel/TextContent/Content").GetComponent<TextMeshPro>().text = chatbotText;

            var comp = tts.GetComponent<GoogleTTS_Chan>();
            comp.content = chatbotText;
            comp.Synthesize();
        }

        //스레드 초기화 과정
        if (_helloRequester.message != null)
        {
            _helloRequester.Stop();
            _helloRequester = new HelloRequester();
        }

        yield return null;
    }
}
