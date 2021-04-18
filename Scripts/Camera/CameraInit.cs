using UnityEngine;

public class CameraInit : MonoBehaviour
{
    public float y_axis;
    private Transform tf;

    private void Start()
    {
        tf = GameObject.Find("MixedRealityPlayspace").transform;
    }

    private void Update()
    {
        tf.position = new Vector3(tf.position.x, y_axis, tf.position.z);
    }

    //private void OnApplicationQuit()
    //{
    //    transform.localPosition = new Vector3(0, 0, 0);
    //    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    //}
}
