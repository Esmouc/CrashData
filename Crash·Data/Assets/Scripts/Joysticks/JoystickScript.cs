using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

	private int touchId = -1;

	private PointerEventData data;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (touchId != -1) {

			Vector3 touchPos = Camera.main.ScreenToWorldPoint (data.position);

			transform.position = transform.parent.position + Vector3.ClampMagnitude (new Vector3 (touchPos.x, touchPos.y, transform.position.z)
				- transform.parent.position, 0.4f);

		}
	
	}

	public Vector2 GetDirection ()
	{
		return (transform.position - transform.parent.position).normalized;
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		Debug.Log (this.gameObject.name + " Was Clicked.");
		touchId = eventData.pointerId;
		data = eventData;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log (this.gameObject.name + " Was UNClicked.");
		touchId = -1;
		transform.position = transform.parent.position;
	}
}
