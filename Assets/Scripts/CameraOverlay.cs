﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverlay : MonoBehaviour {
    [Range(0.2f,1f)]
    public float overlayWidth = 0.5f;
    [Range(0.2f, 1f)]
    public float overlayHeight = 0.5f;
    [Range(-100,100)]
    public float xPos = 0;
    [Range(-100, 100)]
    public float yPos = 0;

    void Start () {
		
	}

    /// <summary>
    /// Make a mini map of the entire maze
    /// </summary>
    void Update () {
        Camera.main.rect = new Rect(xPos, yPos, overlayWidth, overlayHeight);
        Camera.main.clearFlags = CameraClearFlags.Depth;
	}
}
