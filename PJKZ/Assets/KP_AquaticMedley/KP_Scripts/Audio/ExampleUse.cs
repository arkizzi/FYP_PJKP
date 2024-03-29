﻿using UnityEngine;
using System.Collections;

public class ExampleUse : MonoBehaviour {

	public SimpleBeatDetection beatProcessor;

	private SpriteRenderer myMeshRenderer;
	private Color beatedColor;
	public float smoothnessChange;

	void Start () {
		beatProcessor.OnBeat += OnBeat;
		myMeshRenderer = GetComponent<SpriteRenderer> ();
		beatedColor = Color.black;
	}

	void Update () {

		beatedColor = Color.Lerp (beatedColor, Color.black, smoothnessChange*Time.deltaTime);

		myMeshRenderer.material.color = beatedColor;
	}

	void OnBeat(){
		beatedColor = Color.yellow;
	}
}
