using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyText;
    private int key;

    public void SetKey(int key)
    {
        this.key = key;
        keyText.text = key.ToString();
    }

    public Button GetButton()
    {
        return GetComponent<Button>();
    }
}
