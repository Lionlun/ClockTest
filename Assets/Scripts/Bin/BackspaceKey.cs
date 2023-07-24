using System;
using UnityEngine;
using UnityEngine.UI;

public class BackspaceKey : MonoBehaviour
{
	public static Action OnBackspaceKeyPressed;
	Button button;
	
	void Start()
    {
        button = GetComponent<Button>();
		button.onClick.AddListener(() => BackspaceKeyPressed());
	}

	void BackspaceKeyPressed()
	{
		OnBackspaceKeyPressed?.Invoke();
	}

	public Button GetButton()
	{
		return GetComponent<Button>();
	}
}
