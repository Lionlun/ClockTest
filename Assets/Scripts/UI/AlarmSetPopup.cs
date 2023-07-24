using UnityEngine;

public class AlarmSetPopup : MonoBehaviour
{
	[SerializeField] RectTransform rectTransform;
	private float speed = 1.5f;
	private float lifetime = 1.5f;
	
    void Start()
    {
		transform.position = Vector3.zero;
		Destroy(gameObject, lifetime);
	}

	void Update()
    {
		Move();
	}

	void Move()
	{
		Vector2 position = rectTransform.anchoredPosition;

		position.y += speed * Time.deltaTime;
		rectTransform.anchoredPosition = position;
	}
}
