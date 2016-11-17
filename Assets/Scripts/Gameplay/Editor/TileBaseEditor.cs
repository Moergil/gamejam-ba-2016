using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor ( typeof ( TileBase ) )]
public class TileBaseEditor : Editor
{
    TileBase _myTarget;
    float initYCollSize;
    bool isSet = false;

    void OnEnable ( )
    {
        _myTarget = ( TileBase ) target;

        //  Save init Y size of collider
        var bc = _myTarget.GetComponent<Collider> ( ) as BoxCollider;
        initYCollSize = bc.size.y;

        if ( !isSet )
        {
            // Set it up
            SetCollider ( _myTarget.yScaleRatio );
            isSet = true;
        }
    }

    void OnDisable ( )
    {
        isSet = false;
    }

    public override void OnInspectorGUI ( )
    {
        base.OnInspectorGUI ( );
        SetCollider ( _myTarget.yScaleRatio );
    }

    private void SetCollider ( float yScaleRatio )
    {
        var boxColl = _myTarget.GetComponent<Collider> ( ) as BoxCollider;
        var boxCollSize = boxColl.size;
        boxCollSize.y = initYCollSize * yScaleRatio;
        boxColl.size = boxCollSize;
    }
}