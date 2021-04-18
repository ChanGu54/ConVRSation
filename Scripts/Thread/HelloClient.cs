using UnityEngine;

public class HelloClient : MonoBehaviour
{
    public HelloRequester _helloRequester;
    public GameObject TTS;

    private void Start()
    {
        _helloRequester = new HelloRequester();
        _helloRequester.Start();
        _helloRequester.Join();
        TTS.GetComponent<TTS_Chan>().CallTextToSpeech(_helloRequester.message);
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}