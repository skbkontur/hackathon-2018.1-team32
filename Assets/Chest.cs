﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour, IPointerClickHandler {

	
	SpriteRenderer m_SpriteRenderer;
	private bool destroyed;
	static float t = 0.0f;
	
	void Start () {
		m_SpriteRenderer = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (destroyed)
		{
			t += 3 * Time.deltaTime;
			var a = Mathf.Lerp(1.0f, 0.0f, t);
			m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, a);
			if (a == 0)
			{
				Destroy(gameObject);
				t = 0.0f;
			}
		}		
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		destroyed = true;
		
	}
}
