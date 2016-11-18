using UnityEngine;
using System.Collections;

public class SimpleRotator : MonoBehaviour
{
    public float speed = 10;
    public bool randomStartRot = false;

    void Start()
    {
        if ( randomStartRot )
            transform.Rotate ( Vector3.up * Random.Range ( 0, 130f ) );
    }

    void Update ( )
    {
        transform.Rotate ( new Vector3 ( 0, Time.deltaTime * speed, 0 ), Space.World );
    }
}
