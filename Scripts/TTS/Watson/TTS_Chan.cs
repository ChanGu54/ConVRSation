using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IBM.Watson.TextToSpeech.V1;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK;
using IBM.Watson.Assistant.V2;

public class TTS_Chan : MonoBehaviour
{
    [Space(10)]
    [Tooltip("Audio Output Position")]
    [SerializeField]
    public AudioSource audiosource;

    [Space(10)]
    [Tooltip("The service URL (optional). This defaults to \"https://stream.watsonplatform.net/text-to-speech/api\"")]
    [SerializeField]
    private string _serviceUrl = "https://stream.watsonplatform.net/text-to-speech/api/";

    [Header("IAM Authentication")]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    private string _iamApikey = "YcWoIMRlVxnhMrhOr_n8CDKZvlDaCFKp59Ar9zPynmer";

    [Header("Parameters")]
    // https://www.ibm.com/watson/developercloud/speech-to-text/api/v1/curl.html?curl#get-model
    [Tooltip("The Model to use. This defaults to en-US_BroadbandModel")]
    [SerializeField]
    private string _recognizeModel;

    // en-GB_KateVoice, en-GB_KateV3Voice, en-US_AllisonVoice, en-US_AllisonV3Voice, en-US_LisaVoice, en-US_LisaV3Voice, en-US_MichaelVoice, en-US_MichaelV3Voice,
    private string voice = "en-US_MichaelV3Voice";

    private TextToSpeechService _textToSpeech;

    void Start()
    {
        Runnable.Run(CreateService());
    }

    public void CallTextToSpeech(string str)
    {
        Debug.Log("Sent to Watson Text To Speech: " + str);

        byte[] synthesizeResponse = null;
        AudioClip clip = null;

        _textToSpeech.Synthesize(
            callback: (DetailedResponse<byte[]> response, IBMError error) =>
            {
                synthesizeResponse = response.Result;
                clip = WaveFile.ParseWAV("myClip", synthesizeResponse);
                //PlayClip(clip);
                audiosource.clip = clip;
                audiosource.Play();
            },
            text: str,
            voice: voice,
            accept: "audio/wav"
        );
    }

    private IEnumerator CreateService()
    {
        Credentials asst_credentials = null;
        TokenOptions asst_tokenOptions = new TokenOptions()
        {
            IamApiKey = _iamApikey,
        };

        asst_credentials = new Credentials(asst_tokenOptions, _serviceUrl);

        while (!asst_credentials.HasIamTokenData())
            yield return null;

        _textToSpeech = new TextToSpeechService(asst_credentials);
    }

    private void PlayClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            Destroy(audioObject, clip.length);
        }
    }
}
