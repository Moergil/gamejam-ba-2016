using UnityEngine;
using System.Collections;

public class ObstacleBase : MonoBehaviour
{
    public bool IsMoveable { get { return this is IIsMoveAble; } }

    #region API

    public virtual void Activate ( )
    {
        Debug.Log ( "ACTIVATE" );
    }

    #endregion
}
