using UnityEngine;

public class AlarmSound : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Clip;

	private void OnEnable()
	{
		Alarm.OnAlarmTrigger += PlaySound;
	}
	private void OnDisable()
	{
		Alarm.OnAlarmTrigger -= PlaySound;
	}

	public void StopSound()
	{
		Source.Stop();
	}

	void PlaySound()
    {
        Source.Play();
    }
}
