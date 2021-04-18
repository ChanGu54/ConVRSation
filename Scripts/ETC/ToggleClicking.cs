using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleClicking : MonoBehaviour
{
    // Start is called before the first frame update
    private Interactable inter;
    void Start()
    {
        inter = GetComponent<Interactable>();

        inter.TriggerOnClick();
    }
}
