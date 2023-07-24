using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using System;


public class APICall : MonoBehaviour
{
	public static DateTime DateTime;

	[SerializeField] AnalogClock analogClock;
	[SerializeField] DigitalClock digitalClock;

	private readonly int delay = 2;
	private readonly int oneHour = 3600;
	private readonly string firstRequestSource = "https://timeapi.io/api/Time/current/zone?timeZone=Europe/Moscow";
	private readonly string secondRequestSource = "http://worldtimeapi.org/api/timezone/Europe/Moscow";

	private void Start()
	{
		StartCoroutine(GetFirstRequest(firstRequestSource));
		StartCoroutine(GetSecondRequest(secondRequestSource)); 
	}
	IEnumerator GetFirstRequest(string uri)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
			yield return webRequest.SendWebRequest();

			switch (webRequest.result)
			{
				case UnityWebRequest.Result.ConnectionError:
					Debug.Log("ERROR");
					break;

				case UnityWebRequest.Result.DataProcessingError:
					Debug.LogError(String.Format("Something went wrong: {0}", webRequest.error));
					break;

				case UnityWebRequest.Result.Success:
					Debug.Log("First Request");
					SecondTimeAPI time = JsonConvert.DeserializeObject<SecondTimeAPI>(webRequest.downloadHandler.text);
					DateTime = time.DateTime;
					digitalClock.SetTime(DateTime);
					analogClock.SetTime(DateTime);
					break;
			}
		}
		yield return new WaitForSeconds(oneHour);
		StartCoroutine(GetFirstRequest(firstRequestSource));
	}
	IEnumerator GetSecondRequest(string uri)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
			yield return new WaitForSeconds(delay);

			yield return webRequest.SendWebRequest();

			switch (webRequest.result)
			{
				case UnityWebRequest.Result.ConnectionError:

					Debug.Log("API Connection Failed");
					break;

				case UnityWebRequest.Result.DataProcessingError:

					Debug.LogError(String.Format("Something went wrong: {0}", webRequest.error));
					break;

				case UnityWebRequest.Result.Success:

					Debug.Log("Second Request");
					FirstTimeAPI time = JsonConvert.DeserializeObject<FirstTimeAPI>(webRequest.downloadHandler.text);
					DateTime = time.Datetime;
					digitalClock.SetTime(DateTime);
					analogClock.SetTime(DateTime);
					break;
			}
		}
		yield return new WaitForSeconds(oneHour);
		StartCoroutine(GetSecondRequest(secondRequestSource));
	}
}

