using UnityEngine;
using System.Collections;
using ByteSheep.Events;

[System.Serializable]
public class EnvCollisionEvent : AdvancedEvent<ObstacleBase> { }

public class EnvTrigger : MonoBehaviour
{
    public EnvCollisionEvent TrgEvent;

    private Collider _col;

    #region Mono

    void Awake ( )
    {
        _col = GetComponent<Collider> ( );

        if ( _col != null )
            _col.isTrigger = true;
    }

    #endregion

    #region Trigger handlers

    void OnTriggerEnter ( Collider col )
    {
        if ( col.tag == "Player" )
        {
            //  Fire event
            TrgEvent.Invoke ( null );

            // Destroy collider
            Destroy ( _col );

            Debug.Log ( "EnvTriger -> On Enter" );
        }
    }

    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube ( transform.position, Vector3.one);
    }
}


