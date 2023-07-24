using System;
using TMPro;
using UnityEngine;

public class AnalogClock: MonoBehaviour
{
	public float SecondsCurrentTime;
    public float MinutesCurrentTime;
    public float HoursCurrentTime;
	[SerializeField] GameObject secondsNeedle, minutesNeedle, hoursNeedle;
	[SerializeField] int hoursCoeficient = 30;
	[SerializeField] TextMeshProUGUI hoursText;
	[SerializeField] TextMeshProUGUI minutesText;
	private bool isAlarm;
	private bool isDragged;

	private void OnEnable()
	{
		ToggleAlarmButton.OnPress += ToggleAlarm;
		ToggleAlarmButton.OnPress += ResetClock;
		ConfirmAlarmButton.OnConfirmButtonPressedNoData += ToggleAlarm;
		DragDetector.OnDragStarted += SetDraggedTrue;
		DragDetector.OnDragEnded += SetDraggedFalse;
	}
	private void OnDisable()
	{
		ToggleAlarmButton.OnPress -= ToggleAlarm;
		ToggleAlarmButton.OnPress -= ResetClock;
		ConfirmAlarmButton.OnConfirmButtonPressedNoData -= ToggleAlarm;
		DragDetector.OnDragStarted -= SetDraggedTrue;
		DragDetector.OnDragEnded -= SetDraggedFalse;
	}

	void Update()
	{
		if (isAlarm && isDragged)
		{
			ConvertMinutesAngleToTime();
			ConverHoursAngleToTime();
		}
		UpdateTime();
	}

	public void ResetClock()
	{
		secondsNeedle.transform.localRotation = Quaternion.Euler(0, 0, 0);
		minutesNeedle.transform.localRotation = Quaternion.Euler(0, 0, 0);
		hoursNeedle.transform.localRotation = Quaternion.Euler(0, 0, 0);
	}

	public void SetTime(DateTime time)
	{
		SecondsCurrentTime = time.TimeOfDay.Seconds;
		MinutesCurrentTime = time.TimeOfDay.Minutes;
		HoursCurrentTime = time.TimeOfDay.Hours;

		ConvertToAmPm();
		MoveNeedles();
	}

	void UpdateTime()
	{
		SecondsCurrentTime += Time.deltaTime;
		MinutesCurrentTime += Time.deltaTime / 60;
		HoursCurrentTime += Time.deltaTime / 360;

		ConvertToAmPm();

		if (!isAlarm)
		{
			MoveNeedles();
		}
	}

	void ConvertMinutesAngleToTime()
	{
		var convertedMinutes = Mathf.RoundToInt((Quaternion.Inverse(minutesNeedle.transform.localRotation).eulerAngles.z) / 6);
		minutesText.text = convertedMinutes.ToString();
	}
	
	void ConverHoursAngleToTime()
	{
		var convertedHours = Mathf.RoundToInt((Quaternion.Inverse(hoursNeedle.transform.localRotation).eulerAngles.z) / 30);

		if (convertedHours == 0)
		{
			convertedHours = 12;
		}
			hoursText.text = convertedHours.ToString();
	}
	private void ToggleAlarm()
	{
		isAlarm = !isAlarm;
	}


	void SetDraggedFalse()
	{
		isDragged = false;
	}
	void SetDraggedTrue()
	{
		isDragged = true;
	}

	void ConvertToAmPm()
	{
		if (HoursCurrentTime > 12)
		{
			HoursCurrentTime -= 12;
		}
	}

	void MoveNeedles()
	{
		secondsNeedle.transform.localRotation = Quaternion.Euler(0, 0, Mathf.RoundToInt(-SecondsCurrentTime) * 6);
		minutesNeedle.transform.localRotation = Quaternion.Euler(0, 0, -MinutesCurrentTime * 6);
		hoursNeedle.transform.localRotation = Quaternion.Euler(0, 0, -HoursCurrentTime * hoursCoeficient);
	}
}
