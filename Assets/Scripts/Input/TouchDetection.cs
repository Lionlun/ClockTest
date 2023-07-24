using System;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
	public static event Action OnTouch = delegate { };
	public static event Action OnEndTouch = delegate { };
	public static event Action<Vector3> OnTouchPosition = delegate { };

	public Vector3 CurrentDirection;
	public float Distance;

	Vector3 startPosition;
	Vector3 currentPosition;

	void Update()
	{
		if (Input.touchCount > 0)
		{
			HandleTouch();
		}

		GetDirection();
		GetDistnace();
	}

	void HandleTouch()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == UnityEngine.TouchPhase.Began)
			{
				SendTouch();
				startPosition = touch.position;
				SendTouchPosition(startPosition);

			}

			if (touch.phase == UnityEngine.TouchPhase.Ended)
			{
				SendEndTouch();
			}

			currentPosition = touch.position;
		}
	}

	void GetDirection()
	{
		if (Input.touchCount > 0)
		{
			var horizontalDirection = startPosition.x - currentPosition.x;
			var verticalDirection = startPosition.y - currentPosition.y;

			CurrentDirection = new Vector3(horizontalDirection, 0, verticalDirection);
		}
	}

	void GetDistnace()
	{
		Distance = Vector2.Distance(startPosition, currentPosition);
	}

	void SendTouch()
	{
		OnTouch?.Invoke();
	}

	void SendTouchPosition(Vector3 position)
	{
		OnTouchPosition?.Invoke(position);
	}

	void SendEndTouch()
	{
		OnEndTouch?.Invoke();
	}
}
