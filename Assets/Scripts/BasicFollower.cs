using UnityEngine;
using System.Collections;

public class BasicFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _smoothing = 0.5f;

    Vector3 _initOffset;

    #region Mono

    void Start ( )
    {
        _initOffset = transform.position - _target.position;
    }

    public void Update ( )
    {
        transform.position = Vector3.Lerp ( transform.position, _target.position + _initOffset, Time.deltaTime * _smoothing);
    }
    
    #endregion
}
