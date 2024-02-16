using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using System.Globalization;

public class GridNumber : MonoBehaviour, IPointerDownHandler {
    private Button button;
    private TextMeshProUGUI text;
    private int textInt;
    private GameController gameController;
    private Mode mode;
    private Color textDarkColor = new Color(39,34,60);
    private string textColorLight = "AFB3C0";
    private string textColorDark = "27223C";
    private string buttonColorLight = "FFFFFF";
    private string buttonColorDark = "4B505F";

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
         bool pressedCorrectly = gameController.CompareNumberPressed(textInt);
        if (pressedCorrectly) {
            if (mode == Mode.Disappear) {
                gameObject.SetActive(false);
            }
            else if (mode == Mode.Reaction) {
                gameObject.SetActive(false);
                //in gameController activate text + in reverse color style of next number
            }
            else if (mode == Mode.Memory) {
                //disable text after 3 seconds
            }
        }
        else {
            if (mode == Mode.Memory) {
                //enable text for 3 seconds
            }
        }
    }

    public void SetMode(Mode mode) {
        this.mode = mode;
    }

    public void SetLight() {
        text.color = FromHex(textColorDark);
        button.image.color = FromHex(buttonColorLight);
    }

    public void SetDark() {
        text.color = FromHex(textColorLight);
        button.image.color = FromHex(buttonColorDark);
    }

    public Color FromHex(string hex) {
        if (hex.Length < 6) {
            throw new System.FormatException("Needs a string with a length of at least 6");
        }

        var r = hex.Substring(0, 2);
        var g = hex.Substring(2, 2);
        var b = hex.Substring(4, 2);
        string alpha;
        if (hex.Length >= 8)
            alpha = hex.Substring(6, 2);
        else
            alpha = "FF";

        return new Color((int.Parse(r, NumberStyles.HexNumber) / 255f),
                        (int.Parse(g, NumberStyles.HexNumber) / 255f),
                        (int.Parse(b, NumberStyles.HexNumber) / 255f),
                        (int.Parse(alpha, NumberStyles.HexNumber) / 255f));
    }
}
