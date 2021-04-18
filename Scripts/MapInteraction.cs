using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapInteraction : MonoBehaviour
{
    public void MoveSceneToHotel()
    {
        SceneManager.LoadScene("호텔");
    }
}
