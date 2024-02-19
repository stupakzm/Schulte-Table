using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject gridNumber;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject ModesButton;
    [SerializeField] private GameObject ResetInGameButton;
    [SerializeField] private TextMeshProUGUI[] numbersToFindText;
    [SerializeField] private Transform numbersGridParent;
    [SerializeField] private GameObject statisticsView;
    [SerializeField] private TextMeshProUGUI statisticsViewText;
    [SerializeField] private TextMeshProUGUI currentModeText;

    private int startPositionX = 300;
    private int startPositionY = 250;
    private int offset = 150;
    private int missClick;
    private List<int> numbers;
    private Queue<int> numbersToFind;
    private Stack<int> numbersToFindReverse;
    private float currentTime;
    private bool won;
    private bool started;
    private string lightSettings;
    private List<GridNumber> gridNumberObjects = new List<GridNumber>();
    private Mode mode;

    private void Start() {
        InstantiateGridNumbers();
        statisticsView.SetActive(false);
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
        AsignNumberListToFind();
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
        ChangeLightMode();
    }

    private void GenerateGrid() {
        AsignNumberList();
        for (int i = 0; i < 25; i++) {
            int index = Random.Range(0, numbers.Count - 1);
            gridNumberObjects[i].SetNumber(numbers[index]);
            gridNumberObjects[i].SetMode(mode);
            gridNumberObjects[i].gameObject.SetActive(true);
            numbers.RemoveAt(index);
        }
    }

    public void ChangeLightMode() {
        lightSettings = PlayerPrefs.GetString(Const.LIGHT_SETTINGS);
        for (int i = 0; i < 25; i++) {
            if (lightSettings == Const.DARK_MODE) {
                gridNumberObjects[i].SetDark();
            }
            else {
                gridNumberObjects[i].SetLight();
            }
        }
    }

    private void AsignNumberList() {
        numbers = new List<int>();
        for (int i = 1; i <= 25; i++) {
            numbers.Add(i);
        }
    }

    private void AsignNumberListToFind() {
        numbersToFind = new Queue<int>();
        for (int f = 1; f <= 25; f++) {
            numbersToFind.Enqueue(f);
        }
        numbersToFindReverse = new Stack<int>();
        for (int f = 1; f <= 25; f++) {
            numbersToFindReverse.Push(f);
        }
    }

    public bool CompareNumberPressed(int pressedNumer) {
        if (mode == Mode.Reverse) {
            if (numbersToFindReverse.Peek() == pressedNumer) {
                numbersToFindReverse.Pop();
                if (numbersToFindReverse.TryPeek(out int res)) {
                    ShowNumberToFindReverse();
                }
                else {
                    WonView();
                }
                return true;
            }
            else {
                missClick++;
            }
        }
        else {
            if (numbersToFind.Peek() == pressedNumer) {
                numbersToFind.Dequeue();
                if (numbersToFind.TryPeek(out int result))
                    ShowNumberToFind();
                else {
                    WonView();
                }
                if (mode == Mode.DynamicShuffle) {
                    GenerateGrid();
                }
                return true;
            }
            else {
                missClick++;
            }
        }
        return false;
    }

    private void WonView() {
        won = true;
        statisticsView.SetActive(true);
        ModesButton.SetActive(true);
        ResetInGameButton.SetActive(false);
        if (currentTime < Statistics.GetBestTime(mode))
            Statistics.SetBestTime(mode, currentTime);
        Statistics.SetAttemptCount(mode);
        statisticsViewText.text = "You won in " + currentTime.ToString("#.0") + " seconds." +
            "\nIncorrect touches - " + missClick + "\nYour best time in "+mode.ToString()+" mode is " + Statistics.GetBestTime(mode).ToString("#.0") + " seconds.";
    }

    public void ShowNumberToFind() {
        for (int i = 0; i < numbersToFindText.Length; i++) {
            numbersToFindText[i].text = numbersToFind.Peek().ToString();
        }
    }

    public void ShowNumberToFindReverse() {
        for (int i = numbersToFindText.Length - 1; i >= 0; i--) {
            numbersToFindText[i].text = numbersToFindReverse.Peek().ToString();
        }
    }

    public void Reset() {
        started = false;
        statisticsView.SetActive(false);
        StartButton.SetActive(true);
        ModesButton.SetActive(true);
        currentTime = 0;
        missClick = 0;
        won = false;
        AsignNumberListToFind();
        GenerateGrid();
    }

    public void UpdateCurrentModeText() {
        currentModeText.text = "Current mode is " + mode.ToString() + ".";
    }

    public void Started() {
        started = true;
        if (mode == Mode.Reverse)
            ShowNumberToFindReverse();
        else ShowNumberToFind();
    }

    public void SetMode(int mode) {
        this.mode = (Mode)mode;
        Reset();
    }
}
