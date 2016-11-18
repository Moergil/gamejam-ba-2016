using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public FoucsPoint focusPoint;
    public float smoothing = 3f;
    Vector3 offset;

    // Use this for initialization
    void Start ( )
    {
        offset = transform.position - Target.position;
    }

    // Update is called once per frame
    void Update ( )
    {
        transform.position = Vector3.Lerp ( transform.position, Target.position + offset,Time.deltaTime * smoothing );
    }

    public void Blur ( E_FocusMode mode )
    {
        focusPoint.Mode = mode;
    }
}
