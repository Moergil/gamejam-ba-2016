using UnityEngine;
using System.Collections;

public enum E_FocusMode { Game, Menu }
public class FoucsPoint : MonoBehaviour
{
    private Vector3 _startPos;

    [SerializeField]
    private Vector3 _offset = new Vector3 ( 0f, 100f, 0f );

    void Start ( )
    {
        _startPos = transform.position;
    }

    public E_FocusMode Mode
    {
        set
        {
            switch ( value )
            {
                case E_FocusMode.Game:
                    transform.position = _startPos;
                    break;
                case E_FocusMode.Menu:
                    transform.position += _offset;
                    break;
            }
        }
    }
}
