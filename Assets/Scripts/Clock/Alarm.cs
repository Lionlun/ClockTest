using System;
using UnityEngine;

public class Alarm : MonoBehaviour
{
	public static Action OnAlarmTrigger = delegate { };
	[SerializeField] DigitalClock clock;
	private int hours;
	private int minutes;
	private bool isAlarmSet;
	private int hoursCorrectionTime;

	private void OnEnable()
	{
		ConfirmAlarmButton.OnConfirmButtonPressed += SetDigitalAlarm;
	}
	private void OnDisable()
	{
		ConfirmAlarmButton.OnConfirmButtonPressed -= SetDigitalAlarm;
	}

    void Update()
    {
		CheckAlarm();
	}
	public void SwitchToAM()
	{
		hoursCorrectionTime = 0;
	}
	public void SwitchToPM()
	{
		hoursCorrectionTime = 12;
	}

	void SetDigitalAlarm(int hours, int minutes)
	{
		isAlarmSet = true;
		this.hours = hours + hoursCorrectionTime;
		this.minutes = minutes;
	}

	void CheckAlarm()
	{
		if (isAlarmSet)
		{
			if (Mathf.FloorToInt(clock.CurrentHours) == hours && Mathf.FloorToInt(clock.CurrentMinutes) == minutes)
			{
				PlayAlarm();
			}
		}
	}

	void PlayAlarm()
	{
		OnAlarmTrigger?.Invoke();
		isAlarmSet = false;
	}
}
