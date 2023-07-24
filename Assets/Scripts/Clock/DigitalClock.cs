using System;
using TMPro;
using UnityEngine;

public class DigitalClock : MonoBehaviour
{
	public TextMeshProUGUI TimeText;
    [HideInInspector] public float CurrentSeconds;
	[HideInInspector] public float CurrentMinutes;
	[HideInInspector] public float CurrentHours;

    void Update()
    {
		HandleTime();
		DisplayTime();
	}

	public void SetTime(DateTime time)
	{
		CurrentSeconds = time.Second;
		CurrentMinutes = time.Minute;
		CurrentHours = time.Hour;
	}

	void HandleTime()
	{
		if (CurrentSeconds >= 60)
		{
			CurrentSeconds = 0;
			CurrentMinutes += 1;
		}

		if (CurrentMinutes >= 60)
		{
			CurrentMinutes = 0;
			CurrentHours += 1;
		}
		if (CurrentHours >= 24)
		{
			CurrentHours = 0;
		}
		CurrentSeconds += Time.deltaTime;
	}

	void DisplayTime()
	{
		TimeText.text = String.Format("{0:00}:{1:00}.{2:00}", CurrentHours, CurrentMinutes, CurrentSeconds);
	}
}
