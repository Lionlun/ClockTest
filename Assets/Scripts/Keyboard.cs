using System.Collections;
using UnityEngine;
using System;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Key keyPrefab;

    [Range(0,1f)]
    [SerializeField] private float widthPercent;
	[Range(0, 1f)]
	[SerializeField] private float heightPercent;
	[Range(0, .5f)]
	[SerializeField] private float bottomOffset;

    [SerializeField] private KeyboardLine[] lines;
    [Range(0,2f)]
    [SerializeField] private float keyToLineRatio;
	[Range(-1, 1)]
	[SerializeField] private float keyXSpacing;

    public static Action<int> OnKeyPressed;

	IEnumerator Start()
    {   
        CreateKeys();
		yield return null;
		UpdateRecctTransform();
	}

    void Update()
    {
        ScaleKeyboard();
		UpdateRecctTransform();
        PlaceKeys();
	}

    private void UpdateRecctTransform()
    {
        float width = widthPercent * Screen.width;
        float height = heightPercent * Screen.height;

        rectTransform.sizeDelta = new Vector2(width, height);

        Vector2 position;
        position.x = Screen.width / 2;
        position.y = bottomOffset * Screen.height + height / 2;

        rectTransform.position = position;
    }

    void CreateKeys()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                int key = lines[i].keys[j];
                Key keyInstance = Instantiate(keyPrefab, rectTransform);
                keyInstance.SetKey(key);

                keyInstance.GetButton().onClick.AddListener(() => KeyPressedCallback(key));
            }
        }
    }

    void PlaceKeys()
    {
        int lineCount = lines.Length;
        float lineHeight = rectTransform.rect.height / lineCount;
        float keyWidth = lineHeight * keyToLineRatio;
        float xSpacing = keyXSpacing * lineHeight;

        int currentKeyIndex = 0;

        for (int i = 0; i < lineCount; i++)
        {
            float halfKeyCount = (float)lines[i].keys.Length / 2;
            float startX = rectTransform.position.x - (keyWidth + xSpacing) * halfKeyCount + (keyWidth+xSpacing)/2;

            float lineY = rectTransform.position.y + rectTransform.rect.height / 2 - lineHeight / 2 - i * lineHeight;
            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                float keyX = startX + j * (keyWidth+xSpacing);
                Vector2 keyPosition = new Vector2(keyX, lineY);
                RectTransform keyRectTransform  = rectTransform.GetChild(currentKeyIndex).GetComponent<RectTransform>();
                keyRectTransform.position = keyPosition;

                keyRectTransform.sizeDelta = new Vector2(keyWidth, keyWidth);
                currentKeyIndex++;
            }
        }

    }

    void KeyPressedCallback(int key)
    {
        Debug.Log("Key pressed: " + key);
        OnKeyPressed?.Invoke(key);
    }

    void ScaleKeyboard()
    {
		if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
		{
			keyXSpacing = 1;
			rectTransform.localScale = new Vector3(4f, rectTransform.localScale.y, rectTransform.localScale.z);
		}
		else
		{
			keyXSpacing = -0.88f;
			rectTransform.localScale = new Vector3(3f, rectTransform.localScale.y, rectTransform.localScale.z);
		}
	}
}

[System.Serializable]
public struct KeyboardLine
{
    public int[] keys;
}
