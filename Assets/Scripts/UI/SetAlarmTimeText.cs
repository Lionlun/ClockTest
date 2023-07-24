using TMPro;
using UnityEngine;

public class SetAlarmTimeText : MonoBehaviour
{
	private TextMeshProUGUI timeText;
	private string defaultValue = "00";

	private void OnEnable()
	{
		timeText = GetComponent<TextMeshProUGUI>();
		ResetText();
	}

    void Update()
    {
		CheckNoText();
		CheckWrongValues();
	}

	void CheckNoText()
	{
		if (timeText.text == "")
		{
			timeText.text = defaultValue;
		}
	}

	void CheckWrongValues()
	{
		if (timeText.text == "60")
		{
			timeText.text = defaultValue;
		}
	}

	void ResetText()
	{
		timeText.text = defaultValue;
	}
}
