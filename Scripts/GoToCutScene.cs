using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCutScene : MonoBehaviour
{
    public void SceneToCutScene()
    {
        SceneManager.LoadScene("컷씬");
    }
}
