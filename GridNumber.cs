using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridNumber : MonoBehaviour, IPointerDownHandler {
    private Button button;
    private TextMeshProUGUI text;
    private int textInt;
    private GameController gameController;

    private void Awake() {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    public void SetNumber(int number) {
        textInt = number;
        text.text = number.ToString();
    }

    public void OnPointerDown(PointerEventData eventData) {
         gameController.CompareNumberPressed(textInt);
    }
}
