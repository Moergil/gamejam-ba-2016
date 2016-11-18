using UnityEngine;
using System.Collections;

public enum E_FocusMode { Game, Menu }
public class FoucsPoint : MonoBehaviour
{
    private Vector3 _startPos;

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
                    transform.position += Vector3.up * 100f;
                    break;
            }
        }
    }
}
