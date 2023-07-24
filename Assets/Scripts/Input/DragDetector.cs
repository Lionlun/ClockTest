using System;
using UnityEngine;

public class DragDetector : MonoBehaviour
{
	public static event Action OnTouch = delegate { };
	public static event Action OnEndTouch = delegate { };
	public static event Action OnDragStarted = delegate { };
	public static event Action OnDragEnded = delegate { };

	public bool IsDragActive = false;

    Vector2 screenPosition;
    Vector3 worldPosition;
    Draggable lastDragged;

	private void Awake()
	{
        DragDetector[] controllers = FindObjectsOfType<DragDetector>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
	}

	private void OnEnable()
	{
        OnEndTouch += Drop;
	}

	private void OnDisable()
	{
		OnEndTouch -= Drop;
	}

	private void Update()
	{
		if (Input.touchCount > 0)
		{
			HandleDrag();
		}

		if (IsDragActive)
		{
			Drag();
		}
	}

	void HandleDrag()
	{
		foreach (Touch touch in Input.touches)
		{
			CheckPosition(touch);
			CheckDragPhase(touch);
		}
	}

	void InitDrag()
    {
		OnDragStarted?.Invoke();
        IsDragActive = true;  
    }

    void Drag()
    {
		var world2Dposition = new Vector2(worldPosition.x, worldPosition.y);
		var lastDragged2DPosition = new Vector2(lastDragged.transform.position.x, lastDragged.transform.position.y);
		var direction = world2Dposition - lastDragged2DPosition;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		lastDragged.transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    }

    void Drop()
    {
		OnDragEnded?.Invoke();
		IsDragActive = false;
    }

    void SendEndTouch()
    {
		OnEndTouch?.Invoke();
	}

	void CheckDragPhase(Touch touch)
	{
		if (touch.phase == UnityEngine.TouchPhase.Began)
		{
			RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

			if (hit.collider != null)
			{
				Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
				if (draggable != null)
				{
					lastDragged = draggable;
					InitDrag();
				}
			}
		}

		if (IsDragActive && touch.phase == UnityEngine.TouchPhase.Ended)
		{
			SendEndTouch();
		}
	}

	void CheckPosition(Touch touch)
	{
		screenPosition = touch.position;
		worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
	}
}
