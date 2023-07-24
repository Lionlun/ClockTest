using UnityEngine;

public class UIActivationManager : MonoBehaviour
{
    [SerializeField] private GameObject timeContainer;
	[SerializeField] private GameObject alarmConfirmationButton;
	[SerializeField] private GameObject secondsNeedle;
	[SerializeField] private GameObject stopAlarmButton;
	[SerializeField] private Keyboard keyboard;
	[SerializeField] private ToggleAlarmButton toggleAlarmButton;
	[SerializeField] private DragDetector dragDetector;
	[SerializeField] private AMPMToggleSwitch amPmSwitch;

	private bool isAlarmActive;

	private void OnEnable()
	{
		ToggleAlarmButton.OnPress += ToggleAlarm;
		ConfirmAlarmButton.OnConfirmButtonPressedNoData += ToggleAlarm;
		Alarm.OnAlarmTrigger += ActivatStopAlarmButton;
		DragDetector.OnDragStarted += DeactivateKeyBoard;
	}
	private void OnDisable()
	{
		ToggleAlarmButton.OnPress -= ToggleAlarm;
		ConfirmAlarmButton.OnConfirmButtonPressedNoData -= ToggleAlarm;
		Alarm.OnAlarmTrigger -= ActivatStopAlarmButton;
		DragDetector.OnDragStarted -= DeactivateKeyBoard;
	}

	public void ActivateKeyBoard()
	{
		keyboard.gameObject.SetActive(true);
	}
	public void DeactivateKeyBoard()
	{
		keyboard.gameObject.SetActive(false);
	}
	public void ActivatStopAlarmButton()
	{
		stopAlarmButton.SetActive(true);
	}
	public void DeactivateStopAlarmButton()
	{
		stopAlarmButton.SetActive(false);
	}

	private void ToggleAlarm()
	{
		isAlarmActive = !isAlarmActive;
		timeContainer.gameObject.SetActive(isAlarmActive);
		alarmConfirmationButton.gameObject.SetActive(isAlarmActive);
		amPmSwitch.gameObject.SetActive(isAlarmActive);
		toggleAlarmButton.gameObject.SetActive(!isAlarmActive);
		secondsNeedle.gameObject.SetActive(!isAlarmActive);
		keyboard.gameObject.SetActive(false);
	}
}
