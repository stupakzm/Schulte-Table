using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject gridNumber;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject ModesButton;
    [SerializeField] private TextMeshProUGUI[] numbersToFindText;
    [SerializeField] private Transform numbersGridParent;
    [SerializeField] private GameObject statisticsView;
    [SerializeField] private TextMeshProUGUI statisticsViewText;

    private int startPositionX = 300;
    private int startPositionY = 250;
    private int offset = 150;
    private int missClick;
    private List<int> numbers;
    private Queue<int> numbersToFind;
    private static float bestTime = float.MaxValue;
    private const string BEST_TIME = "BestTime";
    private const string LIGHT_SETTING = "DarkMode";
    private const string LIGHT_MODE = "LightMode";
    private const string DARK_MODE = "DarkMode";
    private float currentTime;
    private bool won;
    private bool started;
    private string lightSettings;
    private List<GridNumber> gridNumberObjects = new List<GridNumber>();
    private Mode mode;

    private void Start() {
        InstantiateGridNumbers();
        statisticsView.SetActive(false);
        //  if (PlayerPrefs.GetFloat(BEST_TIME) > 0)
        bestTime = PlayerPrefs.GetFloat(BEST_TIME, float.MaxValue);
        lightSettings = PlayerPrefs.GetString(LIGHT_SETTING, LIGHT_MODE);
        float screenHeight = Screen.height;
        float newScale = screenHeight / 1080;
        numbersGridParent.localScale = new Vector3(newScale, newScale, 1);
    }

    private void Update() {
        if (!won && started) {
            currentTime += 1 * Time.deltaTime;
            statisticsViewText.text = "Time : " + currentTime.ToString("#.0") + " seconds";
        }
    }

    public void InstantiateGridNumbers() {
        AsignNumberList();
        for (int y = startPositionY; y >= -350; y -= offset) {
            for (int x = startPositionX; x >= -300; x -= offset) {
                GameObject currentGridNumberObject = Instantiate(gridNumber, numbersGridParent);
                currentGridNumberObject.transform.localPosition = new Vector3(x, y, 0);
                int index = Random.Range(0, numbers.Count - 1);
                var currentGridNumber = currentGridNumberObject.GetComponent<GridNumber>();
                gridNumberObjects.Add(currentGridNumber);
                currentGridNumber.SetNumber(numbers[index]);
                currentGridNumber.SetGameController(this);
                numbers.RemoveAt(index);
            }
        }
        numbersToFind = new Queue<int>();
        for (int f = 1; f <= 25; f++) {
            numbersToFind.Enqueue(f);
        }
    }

    private void AsignNumberList() {
        numbers = new List<int>();
        for (int i = 1; i <= 25; i++) {
            numbers.Add(i);
        }
    }

    public bool CompareNumberPressed(int pressedNumer) {
        if (numbersToFind.Peek() == pressedNumer) {
            numbersToFind.Dequeue();
            if (numbersToFind.TryPeek(out int result))
                ShowNumberToFind();
            else {
                won = true;
                statisticsView.SetActive(true);
                ModesButton.SetActive(true);
                bestTime = currentTime < bestTime ? currentTime : bestTime;
                PlayerPrefs.SetFloat(BEST_TIME, bestTime);
                statisticsViewText.text = "You won in " + currentTime.ToString("#.0") + " seconds." +
                    "\nIncorrect touches - " + missClick + "\nYour best time in this mode is " + bestTime.ToString("#.0") + " seconds.";
            }
            return true;
        }
        else {
            missClick++;
        }
        return false;
    }

    public void ShowNumberToFind() {
        for (int i = 0; i < numbersToFindText.Length; ++i) {
            numbersToFindText[i].text = numbersToFind.Peek().ToString();
        }
    }

    public void Reset() {
        started = false;
        statisticsView.SetActive(false);
        StartButton.SetActive(true);
        currentTime = 0;
        missClick = 0;
        won = false;
        AsignNumberList();
        numbersToFind = new Queue<int>();
        for (int i = 0; i < 25; i++) {
            int index = Random.Range(0, numbers.Count - 1);
            gridNumberObjects[i].SetNumber(numbers[index]);
            gridNumberObjects[i].SetMode(mode);
            if (lightSettings == DARK_MODE) {
                gridNumberObjects[i].SetDark();
            }
            else {
                gridNumberObjects[i].SetLight();
            }
            numbers.RemoveAt(index);
            numbersToFind.Enqueue(i + 1);
        }
        ShowNumberToFind();
    }

    public void Started() {
        started = true;
    }

    public void SetMode(int mode) {
        this.mode = (Mode)mode;
        Reset();
    }
}
