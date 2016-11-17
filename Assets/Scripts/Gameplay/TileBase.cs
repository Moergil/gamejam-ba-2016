using UnityEngine;

[RequireComponent ( typeof ( BoxCollider ) )]
public class TileBase : MonoBehaviour
{
    [SerializeField]
    private bool _randomizeOnStart = true;

    public float yScaleRatio = 0.6f;

    void Start ( )
    {
        if ( _randomizeOnStart ) RandomizeInitRotation ( );
    }

    private void RandomizeInitRotation ( )
    {
        transform.Rotate ( Vector3.up, Random.Range ( 0, 3 ) * 90, Space.World );
    }
}
