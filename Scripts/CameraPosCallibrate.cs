using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraPosCallibrate : MonoBehaviour
{
    private Transform mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = transform.Find("MainCamera");
        Callibrate();
    }

    public void Callibrate()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Equals("출국"))
        {
            transform.position = new Vector3(0, 0.75f - mainCamera.position.y, transform.position.z);
            Debug.Log("Camera Postion Sum Set To " + transform.position.y + mainCamera.position.y);
        }
        else if (sceneName.Equals("튜토리얼"))
        {
            transform.position = new Vector3(0, 1f - mainCamera.position.y, transform.position.z);
            Debug.Log("Camera Postion Sum Set To " + transform.position.y + mainCamera.position.y);
        }
        else if (sceneName.Equals("입국"))
        {
            transform.position = new Vector3(0, 1f - mainCamera.position.y, transform.position.z);
            Debug.Log("Camera Postion Sum Set To " + transform.position.y + mainCamera.position.y);
        }
        else if (sceneName.Equals("맵"))
        {
            transform.position = GameObject.Find("Pos").transform.position;
            transform.rotation = GameObject.Find("Pos").transform.rotation;
            Debug.Log("Camera Postion Sum Set To " + transform.position.y + mainCamera.position.y);
        }
    }
}
