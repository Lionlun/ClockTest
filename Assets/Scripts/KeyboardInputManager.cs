using UnityEngine;
using TMPro;

public class KeyboardInputManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI minutesText;
	[SerializeField] private TextMeshProUGUI hoursText;
	[SerializeField] private Keyboard keyboard;
	private string defaultText = "00";
	private string noText = "";
	private bool isHourTextSelected = false;
	private bool isMinuteTextSelected = false;

	private void OnEnable()
	{
		Keyboard.OnKeyPressed += KeyPressedCallback;
		BackspaceKey.OnBackspaceKeyPressed += BackspaceKeyPressedCallback;
	}

	private void OnDisable()
	{
		Keyboard.OnKeyPressed -= KeyPressedCallback;
		BackspaceKey.OnBackspaceKeyPressed -= BackspaceKeyPressedCallback;
	}

	private void Update()
	{
        CheckText();
	}

	void KeyPressedCallback(int key)
	{
		CheckHourDefaultText();

		if (isHourTextSelected)
		{
			hoursText.text += key.ToString();

			ReplaceHourPreviousText(key);
		}
	
		else
		{
			CheckMinuteDefaultText();

			if (isMinuteTextSelected)
			{
				minutesText.text += key.ToString();
				ReplaceMinutePreviousText(key);
			}
		}
	}

	void CheckHourDefaultText()
	{
		if (hoursText.text == defaultText)
		{
			hoursText.text = noText;
		}
	}
	void ReplaceHourPreviousText(int key)
	{
		if (hoursText.text.Length > 2)
		{
			hoursText.text = noText;
			hoursText.text += key.ToString();
		}
	}

	void CheckMinuteDefaultText()
	{
		if (minutesText.text == defaultText)
		{
			minutesText.text = noText;
		}
	}

	void ReplaceMinutePreviousText(int key)
	{
		if (minutesText.text.Length > 2)
		{
			minutesText.text = noText;
			minutesText.text += key.ToString();
		}
	}

	void BackspaceKeyPressedCallback()
	{
		if(hoursText.text.Length > 0 && minutesText.text == defaultText)
		{
			hoursText.text = hoursText.text.Substring(0, hoursText.text.Length - 1);
		}
		if(minutesText.text != defaultText && hoursText.text.Length >= 2)
		{
			minutesText.text = minutesText.text.Substring(0, minutesText.text.Length - 1);	
		}
	}

	void CheckText()
	{
		if (hoursText.text.Length > 0)
		{
			if (int.Parse(hoursText.text) > 12)
			{
				hoursText.text = "12";
			}
		}

		if (minutesText.text.Length > 0)
		{
			if (int.Parse(minutesText.text) >= 60)
			{
				minutesText.text = "59";
			}
		}
	}
	public void SetHoursTextSelected() 
	{
		isHourTextSelected = true;
	}
	public void SetMinutesTextSelected()
	{
		isMinuteTextSelected = true;
	}

	public void SetHoursTextDeselected()
	{
		isHourTextSelected = false;
	}
	public void SetMinutesTextDeselected()
	{
		isMinuteTextSelected = false;
	}
}
