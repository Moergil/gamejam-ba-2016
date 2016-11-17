using UnityEngine;
using System.Collections;
using System;

public class TestObstacle : ObstacleBase, IIsMoveAble
{
    [SerializeField]
    private Vector3 _newPos;

    public override void Activate ( )
    {
        base.Activate ( );
        if ( IsMoveable ) MoveObstacle ( _newPos );
    }

    public void MoveObstacle ( Vector3 pos )
    {
        transform.position = pos;
    }
}
