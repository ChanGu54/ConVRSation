using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : MonoBehaviour
{
    public AnimInteract anim;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.procedure == 31) {
            anim._procedure = 32;
        }
    }
}
