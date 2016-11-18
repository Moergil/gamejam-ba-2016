using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent ( typeof ( Light ) )]
public class LightColorInterpolator : MonoBehaviour
{
    private Light _light;
    public Color[ ] colors;
    public bool startOnStart = true;
    public float duration = 0.5f;
    public float delayAfterComplete = 0.1f;
    public Vector2 intensityRange = new Vector2 ( 0f, 1f );
    private bool _canEval = true;

    void Awake ( )
    {
        _light = GetComponent<Light> ( );
    }

    void Start ( )
    {
        if ( startOnStart )
            StartAction ( );
    }

    public void StartAction ( )
    {
        if ( _light == null )
            return;

        if ( _canEval )
        {
            if ( colors.Length > 0 )
            {
                _light.DOColor ( colors[ Random.Range ( 0, colors.Length ) ], duration ).SetDelay ( delayAfterComplete ).OnComplete ( ( ) => StartAction ( ) );
                _light.DOIntensity ( Random.Range ( intensityRange.x, intensityRange.y ), duration ).SetDelay ( delayAfterComplete );
            }
            else
                _light.DOIntensity ( Random.Range ( intensityRange.x, intensityRange.y ), duration ).SetDelay ( delayAfterComplete ).OnComplete ( ( ) => StartAction ( ) );

        }
    }

    public void StopAction ( )
    {
        _canEval = false;
    }
}
