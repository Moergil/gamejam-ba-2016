﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake ( )
    {
        if ( Instance == null )
            Instance = this;
        else if ( Instance != this )
            Destroy ( gameObject );
    }
}
