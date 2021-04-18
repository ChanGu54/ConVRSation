using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using UnityEngine;

public class InputEventCtrl : InputSystemGlobalListener, IMixedRealityInputHandler<Vector2>, IMixedRealityInputActionHandler
{
    public MixedRealityInputAction moveAction;
    public MixedRealityInputAction talkingAction;

    private Color bf_color;
    private Color af_color;

    public float moveSpeed = 1f;

    private Vector3 moving_delta;
    private Vector3 move;

    private Rigidbody rb;

    private Transform cam;

    public Renderer color_Panel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("MainCamera").transform;
        bf_color = new Color(0, 0, 1);
        af_color = new Color(0, 0, 0);
    }

    private void Update()
    {
        move = cam.localRotation * moving_delta * Time.deltaTime;
        move.y = rb.velocity.y;
        rb.velocity = move;
    }

    public void OnInputChanged(InputEventData<Vector2> eventData)
    {
        if (eventData.MixedRealityInputAction == moveAction && !(Mathf.Abs(eventData.InputData.x) < 0.1 && Mathf.Abs(eventData.InputData.y) < 0.1))
        {
            moving_delta = new Vector3(eventData.InputData.x, 0, eventData.InputData.y) * moveSpeed;
        }
        else if (Mathf.Abs(eventData.InputData.x) < 0.1 && Mathf.Abs(eventData.InputData.y) < 0.1)
        {
            moving_delta = Vector3.zero;
        }
    }

    public void OnActionStarted(BaseInputEventData eventData)
    {
        if (eventData.MixedRealityInputAction == talkingAction)
        {
            StartCoroutine(Change_Color(bf_color, af_color));
            GameManager.stt.StartRecordButtonOnClickHandler();
        }
    }

    public void OnActionEnded(BaseInputEventData eventData)
    {
        if (eventData.MixedRealityInputAction == talkingAction)
        {
            StartCoroutine(Change_Color(af_color, bf_color));
            GameManager.stt.StopRecordButtonOnClickHandler();
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