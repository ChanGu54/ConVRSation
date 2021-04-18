using FrostweepGames.Plugin.GoogleCloud.TextToSpeech;
using System;
using System.Collections.Generic;
using UMA.PoseTools;
using UnityEngine;
using UnityEngine.UI;

public class GoogleTTS_Chan : MonoBehaviour
{
    private GCTextToSpeech _gcTextToSpeech;

    [Tooltip("임시로 입력받는 창 설정")]
    public string content;

    // 0 ~ 16 : Wavenet English
    [Tooltip("0-16 : Wavenet 적용된 영어(자세한건 Debug창 참고)")]
    private Voice[] _voices;
    public int voice_selected;

    // en_AU : 0, en_GB : 2, en_US : 3
    private List<string> languageCodes;

    // WAVENET : 0, STANDARD : 1
    private List<string> voiceTypes;

    public float pitch = 1.0f;
    public float speaking_rate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _gcTextToSpeech = GCTextToSpeech.Instance;

        _gcTextToSpeech.GetVoicesSuccessEvent += _gcTextToSpeech_GetVoicesSuccessEvent;
        _gcTextToSpeech.SynthesizeSuccessEvent += _gcTextToSpeech_SynthesizeSuccessEvent;

        _gcTextToSpeech.GetVoicesFailedEvent += _gcTextToSpeech_GetVoicesFailedEvent;
        _gcTextToSpeech.SynthesizeFailedEvent += _gcTextToSpeech_SynthesizeFailedEvent;

        int length = Enum.GetNames(typeof(Enumerators.LanguageCode)).Length;
        languageCodes = new List<string>();

        for (int i = 0; i < length; i++)
        {
            languageCodes.Add(((Enumerators.LanguageCode)i).ToString());
            //Debug.Log(languageCodes[i]);
        }

        length = Enum.GetNames(typeof(Enumerators.VoiceType)).Length;
        voiceTypes = new List<string>();

        for (int i = 0; i < length; i++)
        {
            voiceTypes.Add(((Enumerators.VoiceType)i).ToString());
            //Debug.Log(voiceTypes[i]);
        }

        // en_AU : 0, en_GB : 2, en_US : 3
        GetVoicesHandler(Enumerators.LanguageCode.en_AU);
    }

    public void Synthesize()
    {
        var _currentVoice = _voices[voice_selected];

        Debug.Log("Synthesize Started");

        if (string.IsNullOrEmpty(content) || _currentVoice == null)
        {
            Debug.Log(content);
            Debug.Log(_currentVoice);
            return;
        }

        _gcTextToSpeech.Synthesize(content, new VoiceConfig()
        {
            gender = _currentVoice.ssmlGender,
            languageCode = _currentVoice.languageCodes[0],
            name = _currentVoice.name
        },
         false,
         pitch,
         speaking_rate,
         _currentVoice.naturalSampleRateHertz);

        Debug.Log("Synthesize Send to Server!");

        var emo = GameManager.talking_target.GetComponent<UMAExpressionPlayer>();

        if (content.Contains("happiness"))
        {
            emo.Emotion_index = 0;
        }
        else if (content.Contains("sadness"))
        {
            emo.Emotion_index = 1;
        }
        else if (content.Contains("neutral"))
        {
            emo.Emotion_index = 2;
        }
        else if (content.Contains("anger"))
        {
            emo.Emotion_index = 3;
        }
        else if (content.Contains("excitement"))
        {
            emo.Emotion_index = 4;
        }
        else if (content.Contains("frustration"))
        {
            emo.Emotion_index = 5;
        }
        else
        {
            emo.Emotion_index = 2;
        }
    }

    private void GetVoicesHandler(Enumerators.LanguageCode lang)
    {
        _gcTextToSpeech.GetVoices(new GetVoicesRequest()
        {
            languageCode = _gcTextToSpeech.PrepareLanguage(lang)
        });
    }

    #region failed handlers

    private void _gcTextToSpeech_SynthesizeFailedEvent(string error)
    {
        Debug.Log(error);
    }

    private void _gcTextToSpeech_GetVoicesFailedEvent(string error)
    {
        Debug.Log(error);
    }

    #endregion failed handlers

    #region sucess handlers

    private void _gcTextToSpeech_SynthesizeSuccessEvent(PostSynthesizeResponse response)
    {
        GameManager.tts_audiosource.clip = _gcTextToSpeech.GetAudioClipFromBase64(response.audioContent, Constants.DEFAULT_AUDIO_ENCODING);
        GameManager.tts_audiosource.Play();
        Debug.Log("Audio Playing");
    }

    private void _gcTextToSpeech_GetVoicesSuccessEvent(GetVoicesResponse response)
    {
        _voices = response.voices;

        Debug.Log("Voice Count : " + _voices.Length);

        for (int i = 0; i < _voices.Length; i++)
        {
            Debug.Log("Index " + i + " : " + _voices[i].name);
        }
    }

    #endregion sucess handlers
}
