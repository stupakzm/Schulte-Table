using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridNumber : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI text;

    private void Awake() {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetNumber(int number) {
        text.text = number.ToString();
    }
}
