using System;
using UnityEngine;

public class ToggleAlarmButton : MonoBehaviour
{
	public static event Action OnPress = delegate { };
	public void SendOnPress()
	{
		OnPress?.Invoke();
	}
}
