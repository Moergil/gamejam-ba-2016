using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor ( typeof ( EnvTrigger ) )]
public class TriggerEditor : Editor
{
    EnvTrigger _myTarget;

    void OnEnable ( )
    {
        _myTarget = ( EnvTrigger ) target;
    }

    public override void OnInspectorGUI ( )
    {
        base.OnInspectorGUI ( );
        _myTarget.size = _myTarget.GetComponent<Collider> ( ).bounds.size;
    }
}
