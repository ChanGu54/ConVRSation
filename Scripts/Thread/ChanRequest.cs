using AsyncIO;
using IBM.Cloud.SDK.Utilities;
using NetMQ;
using NetMQ.Sockets;
using System.Collections;
using UnityEngine;

/// <summary>
///     Example of requester who only sends Hello. Very nice guy.
///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.
/// </summary>
public class ChanRequest : MonoBehaviour
{
    public GameObject TTS;
    RequestSocket client;
    public string str;
    public int flag = -1;
    public int exit = -1;
    /// <summary>
    ///     Request Hello message to server and receive message back. Do it 10 times.
    ///     Stop requesting when Running=false.
    /// </summary>
    /// 

    void Start()
    {
        Runnable.Run(PythonService());
    }

    private IEnumerator PythonService()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        //using (RequestSocket client = new RequestSocket())
        using (var client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5556");

            while (true)
            {
                str = "Hello";
                if (exit == 0)
                    break;
                if (flag == 0)
                {
                    Debug.Log("Sending" + str);
                    client.SendFrame(str);
                    // ReceiveFrameString() blocks the thread until you receive the string, but TryReceiveFrameString()
                    // do not block the thread, you can try commenting one and see what the other does, try to reason why
                    // unity freezes when you use ReceiveFrameString() and play and stop the scene without running the server
                    //                string message = client.ReceiveFrameString();
                    //                Debug.Log("Received: " + message);
                    string message = null;
                    bool gotMessage = false;
                    while (true)
                    {
                        gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                        if (gotMessage) break;
                        yield return null;
                    }

                    if (gotMessage) Debug.Log("Received " + message);
                    TTS.GetComponent<TTS_Chan>().CallTextToSpeech(message);

                    flag = -1;
                }
            }
        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }
}