using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor ( typeof ( TileBase ) )]
public class TileBaseEditor : Editor
{
    TileBase _myTarget;
    float initYCollSize;
    private string Name
    {
        get
        {
            return _myTarget.gameObject.name;
        }
    }

    void OnEnable ( )
    {
        _myTarget = ( TileBase ) target;

        //  Save init Y size of collider
        var bc = _myTarget.GetComponent<Collider> ( ) as BoxCollider;
        initYCollSize = bc.size.y;
    }

    public override void OnInspectorGUI ( )
    {
        base.OnInspectorGUI ( );
        UpdateYPosition ( );
        SetCollider ( _myTarget.yScaleRatio );

    }

    private void SetCollider ( float yScaleRatio )
    {
        if ( Name.Contains ( "Pillar" ) ) return;

        var boxColl = _myTarget.GetComponent<Collider> ( ) as BoxCollider;
        var boxCollSize = boxColl.size;
        boxCollSize.y = initYCollSize * yScaleRatio;
        boxColl.size = boxCollSize;
    }

    private void UpdateYPosition ( )
    {
        if ( Name.Contains ( "Pillar" ) )
        {
            var t = _myTarget.transform.position;
            t.y = 0.06f;
            _myTarget.transform.position = t;
        }
    }
}