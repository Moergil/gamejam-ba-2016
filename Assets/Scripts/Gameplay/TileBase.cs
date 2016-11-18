using System;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;
public class TileBase : MonoBehaviour
{
    [SerializeField]
    private bool _randomizeOnStart = true;

    public bool _isFreePlaceable = false;

    public float yScaleRatio = 0.6f;

    void Start ( )
    {
        if ( _randomizeOnStart )
            RandomizeInitRotation ( );

        AssignTextures ( );
    }

    private void AssignTextures ( )
    {
        var mat = GetComponent<Renderer> ( ).material;
        var name = gameObject.name;
        if ( name.Contains ( "brick" ) )
        {
            var res = Regex.Match ( name, @"\d+" ).Value;
            int index = -1;

            var success = Int32.TryParse ( res, out index );

            if ( success && index < GameManager.Instance.tilesTextures.Length )
                mat.SetTexture ( "_MainTex", GameManager.Instance.tilesTextures[ index ] );
        }
    }

    private void RandomizeInitRotation ( )
    {
        transform.Rotate ( Vector3.up, Random.Range ( 0, 3 ) * 90, Space.World );
    }
}
