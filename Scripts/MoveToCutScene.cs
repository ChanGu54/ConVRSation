using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MoveToCutScene : MonoBehaviour
{
    private float time;

    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time > 26)
        {
            ArrivalSceneMove();
        }
    }


    public void MoveCutScene()
    {
        SceneManager.LoadScene("컷씬");
    }

    public void ArrivalSceneMove()
    {
        SceneManager.LoadScene("입국");
    }
}
