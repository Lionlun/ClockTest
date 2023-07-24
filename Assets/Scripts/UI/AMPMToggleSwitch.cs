using UnityEngine;
using UnityEngine.UI;

public class AMPMToggleSwitch : MonoBehaviour
{
	[SerializeField] RectTransform uiHandleRectTransform;
	[SerializeField] Alarm alarm;
    private Button toggle;
    private Vector2 handlePosition;
	private bool isPM;


	private void Awake()
	{
		toggle = GetComponent<Button>();
	}
	void OnEnable()
    {
		toggle.onClick.AddListener(OnPress);
	}
	void OnDisable()
	{
		toggle.onClick.RemoveListener(OnPress);
	}

	private void Start()
	{
		handlePosition = uiHandleRectTransform.anchoredPosition;
		isPM = false;
	}

	void OnPress()
    {
		isPM = !isPM;
		uiHandleRectTransform.anchoredPosition = isPM ? handlePosition * -1 : handlePosition;

        if (isPM)
        {
			SendPM();
		}
        else
		{
			SendAM();
		}
    }

	public void SendPM()
	{
		alarm.SwitchToPM();
		Debug.Log("PM");
	}

	public void SendAM()
	{
		alarm.SwitchToAM();
		Debug.Log("AM");
	}
}
