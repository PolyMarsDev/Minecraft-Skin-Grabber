using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotation : MonoBehaviour
{

    void Update()
    {
		if (EventSystem.current.currentSelectedGameObject == null) {
			transform.Rotate (0.0f, -Input.GetAxisRaw ("Horizontal") * 125.0f * Time.deltaTime, 0.0f, Space.Self);
		}
    }
}
