using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using System.Collections;
using TMPro;
using UMA.PoseTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GCSTT_Chan : MonoBehaviour
{
    private GCSpeechRecognition _speechRecognition;

    public string _resultText;

    public bool _voiceDetection,
                   _recognizeDirectly,
                   _longRunningRecognize;

    public int mic_idx;

    public GameObject dialogFlow;

    TextMeshPro toggle;

    private void Start()
    {
        toggle = GameObject.Find("STTContents").GetComponent<TextMeshPro>();
        _speechRecognition = GetComponent<GCSpeechRecognition>();
        _speechRecognition = GCSpeechRecognition.Instance;

        _speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[mic_idx]);

        _speechRecognition.RecognizeSuccessEvent += RecognizeSuccessEventHandler;
        _speechRecognition.RecognizeFailedEvent += RecognizeFailedEventHandler;
        _speechRecognition.LongRunningRecognizeSuccessEvent += LongRunningRecognizeSuccessEventHandler;
        _speechRecognition.LongRunningRecognizeFailedEvent += LongRunningRecognizeFailedEventHandler;
        _speechRecognition.GetOperationSuccessEvent += GetOperationSuccessEventHandler;
        _speechRecognition.GetOperationFailedEvent += GetOperationFailedEventHandler;
        _speechRecognition.ListOperationsSuccessEvent += ListOperationsSuccessEventHandler;
        _speechRecognition.ListOperationsFailedEvent += ListOperationsFailedEventHandler;

        _speechRecognition.FinishedRecordEvent += FinishedRecordEventHandler;
        _speechRecognition.RecordFailedEvent += RecordFailedEventHandler;

        _speechRecognition.BeginTalkigEvent += BeginTalkigEventHandler;
        _speechRecognition.EndTalkigEvent += EndTalkigEventHandler;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(dialogFlow);
    }

    private void OnDestroy()
    {
        _speechRecognition.RecognizeSuccessEvent -= RecognizeSuccessEventHandler;
        _speechRecognition.RecognizeFailedEvent -= RecognizeFailedEventHandler;
        _speechRecognition.LongRunningRecognizeSuccessEvent -= LongRunningRecognizeSuccessEventHandler;
        _speechRecognition.LongRunningRecognizeFailedEvent -= LongRunningRecognizeFailedEventHandler;
        _speechRecognition.GetOperationSuccessEvent -= GetOperationSuccessEventHandler;
        _speechRecognition.GetOperationFailedEvent -= GetOperationFailedEventHandler;
        _speechRecognition.ListOperationsSuccessEvent -= ListOperationsSuccessEventHandler;
        _speechRecognition.ListOperationsFailedEvent -= ListOperationsFailedEventHandler;

        _speechRecognition.FinishedRecordEvent -= FinishedRecordEventHandler;
        _speechRecognition.RecordFailedEvent -= RecordFailedEventHandler;

        _speechRecognition.EndTalkigEvent -= EndTalkigEventHandler;
    }

    public void StartRecordButtonOnClickHandler()
    {
        _resultText = string.Empty;

        _speechRecognition.StartRecord(_voiceDetection);
    }

    public void StopRecordButtonOnClickHandler()
    {
        _speechRecognition.StopRecord();
    }

    private void RecordFailedEventHandler()
    {
        Debug.LogError("Start record Failed. Please check microphone device and try again.");
    }

    private void BeginTalkigEventHandler()
    {
        Debug.Log("Talk Began.");
    }

    private void EndTalkigEventHandler(AudioClip clip, float[] raw)
    {
        Debug.Log("Talk Ended.");

        FinishedRecordEventHandler(clip, raw);
    }

    private void FinishedRecordEventHandler(AudioClip clip, float[] raw)
    {

        if (clip == null || !_recognizeDirectly)
            return;

        RecognitionConfig config = RecognitionConfig.GetDefault();
        config.languageCode = ((Enumerators.LanguageCode)24).Parse(); //EN-US
        config.speechContexts = new SpeechContext[]
        { };
        config.audioChannelCount = clip.channels;
        // configure other parameters of the config if need

        GeneralRecognitionRequest recognitionRequest = new GeneralRecognitionRequest()
        {
            audio = new RecognitionAudioContent()
            {
                content = raw.ToBase64()
            },
            //audio = new RecognitionAudioUri() // for Google Cloud Storage object
            //{
            //	uri = "gs://bucketName/object_name"
            //},
            config = config
        };

        if (_longRunningRecognize)
        {
            _speechRecognition.LongRunningRecognize(recognitionRequest);
        }
        else
        {
            _speechRecognition.Recognize(recognitionRequest);
        }
    }

    private void GetOperationFailedEventHandler(string error)
    {
        Debug.LogError("Get Operation Failed: " + error);
    }

    private void ListOperationsFailedEventHandler(string error)
    {
        Debug.LogError("List Operations Failed: " + error);
    }

    private void RecognizeFailedEventHandler(string error)
    {
        Debug.LogError("Recognize Failed: " + error);
    }

    private void LongRunningRecognizeFailedEventHandler(string error)
    {
        Debug.LogError("Long Running Recognize Failed: " + error);
    }

    private void ListOperationsSuccessEventHandler(ListOperationsResponse operationsResponse)
    {
        Debug.Log("List Operations Success.\n");

        if (operationsResponse.operations != null)
        {
            foreach (var item in operationsResponse.operations)
            {
                Debug.Log("name: " + item.name + "; done: " + item.done + "\n");
            }
        }
    }

    private void GetOperationSuccessEventHandler(Operation operation)
    {
        Debug.Log("Get Operation Success.\n");
        Debug.Log("name: " + operation.name + "; done: " + operation.done);

        if (operation.done && (operation.error == null || string.IsNullOrEmpty(operation.error.message)))
        {
            InsertRecognitionResponseInfo(operation.response);
        }
    }

    private void RecognizeSuccessEventHandler(RecognitionResponse recognitionResponse)
    {
        Debug.Log("Recognize Success.");
        InsertRecognitionResponseInfo(recognitionResponse);
    }

    private void LongRunningRecognizeSuccessEventHandler(Operation operation)
    {
        if (operation.error != null || !string.IsNullOrEmpty(operation.error.message))
            return;

        Debug.Log("Long Running Recognize Success.\n Operation name: " + operation.name);

        if (operation != null && operation.response != null && operation.response.results.Length > 0)
        {
            Debug.Log("Long Running Recognize Success.");
            _resultText = operation.response.results[0].alternatives[0].transcript;
            toggle.text = _resultText;

            string other = "\nDetected alternatives:\n";

            foreach (var result in operation.response.results)
            {
                foreach (var alternative in result.alternatives)
                {
                    if (operation.response.results[0].alternatives[0] != alternative)
                    {
                        other += alternative.transcript + ", ";
                    }
                }
            }
            Debug.Log(other);
            StartCoroutine(SendText());
        }
        else
        {
            Debug.Log("Long Running Recognize Success. Words not detected.");
        }
    }

    private void InsertRecognitionResponseInfo(RecognitionResponse recognitionResponse)
    {
        if (recognitionResponse == null || recognitionResponse.results.Length == 0)
        {
            Debug.LogWarning("\nWords not detected.");
            return;
        }

        _resultText = recognitionResponse.results[0].alternatives[0].transcript;
        toggle.text = _resultText;

        StartCoroutine(SendText());

        //var words = recognitionResponse.results[0].alternatives[0].words;

        //if (words != null)
        //{
        //    string times = string.Empty;

        //    foreach (var item in recognitionResponse.results[0].alternatives[0].words)
        //    {
        //        times += "<color=green>" + item.word + "</color> -  start: " + item.startTime + "; end: " + item.endTime + "\n";
        //    }

        //    _resultText.text += "\n" + times;
        //}

        //string other = "\nDetected alternatives: ";

        //foreach (var result in recognitionResponse.results)
        //{
        //    foreach (var alternative in result.alternatives)
        //    {
        //        if (recognitionResponse.results[0].alternatives[0] != alternative)
        //        {
        //            other += alternative.transcript + ", ";
        //        }
        //    }
        //}

        //_resultText.text += other;
    }


    IEnumerator SendText()
    {
        while (true)
        {
            if (_resultText != "")
            {
                var comp = dialogFlow.GetComponent<DF2ClientTester>();
                comp.content = _resultText;
                _resultText = "";
                Debug.Log(comp.content + " Sended!");
                if (GameManager.status)
                {
                    comp.SendText();
                    break;
                }
                else
                {
                    comp.Direct_Link_to_Server();
                    break;
                }
                //_helloRequester.str = temp + sending_to_chatbot;
                //temp = "";
                //sending_to_chatbot = "";
                //_helloRequester.Start();
            }
            else
            {
                Debug.Log("Sending message must not be empty!");
                break;
            }
        }
        yield return null;
    }
}
