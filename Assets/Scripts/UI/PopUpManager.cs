using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] AlarmSetPopup alarmSetPopup;
	private void OnEnable()
	{
		ConfirmAlarmButton.OnConfirmButtonPressedNoData += CreatePopup;
	}

	private void OnDisable()
	{
		ConfirmAlarmButton.OnConfirmButtonPressedNoData -= CreatePopup;
	}

    void CreatePopup()
    {
        var popup = Instantiate(alarmSetPopup);
        popup.gameObject.transform.SetParent(transform);
    }
}
