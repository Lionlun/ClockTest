using System;
using TMPro;
using UnityEngine;

public class ConfirmAlarmButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI minutes;
    [SerializeField] TextMeshProUGUI hours;
	public static Action<int, int> OnConfirmButtonPressed;
	public static Action OnConfirmButtonPressedNoData;

	public int HoursAlarm;
    public int MinutesAlarm;
    public bool IsAlarmSet;

   public void OnButtonPressed() 
    {
        if (hours.text != null && minutes.text != null)
        {
			OnConfirmButtonPressed?.Invoke(int.Parse(hours.text), int.Parse(minutes.text));
			OnConfirmButtonPressedNoData?.Invoke();
		}
	}
}
