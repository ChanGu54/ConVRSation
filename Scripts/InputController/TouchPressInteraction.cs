using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPressInteraction : InputSystemGlobalListener, IMixedRealityInputActionHandler
{
    public MixedRealityInputAction TalkingAction;
    public GCSTT_Chan stt;

    public Renderer color_Panel;

    private Color bf_color;
    private Color af_color;

    // Start is called before the first frame update
    void Start()
    {
        bf_color = new Color(0, 0, 1);
        af_color = new Color(0, 0, 0);
    }

    public void OnActionStarted(BaseInputEventData eventData)
    {
        if (eventData.MixedRealityInputAction == TalkingAction)
        {
            StartCoroutine(Change_Color(bf_color, af_color));
            stt.StartRecordButtonOnClickHandler();
        }
    }

    public void OnActionEnded(BaseInputEventData eventData)
    {
        if (eventData.MixedRealityInputAction == TalkingAction)
        {
            StartCoroutine(Change_Color(af_color, bf_color));
            stt.StopRecordButtonOnClickHandler();
        }
    }

    IEnumerator Change_Color(Color a, Color b)
    {
        float k = 0.0f;
        while (k <= 1)
        {
            color_Panel.material.color = Color.Lerp(a, b, k);
            k += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
        color_Panel.material.color = b;
    }
}
