using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private int startPositionX = 300;
    private int startPositionY = 250;
    private int offset = 150;
    private List<int> numbers;

    [SerializeField] private GameObject gridNumber;
    [SerializeField] private Transform canvas;

    private void Awake() {

    }

    private void Start() {
        AsignNumberList();
        InstantiateGridNumbers();
    }

    public void InstantiateGridNumbers() {
        for (int y = startPositionY; y >= -350; y -= offset) {
            for (int x = startPositionX; x >= -300; x -= offset) {
                GameObject currentGridNumber = Instantiate(gridNumber, canvas);
                currentGridNumber.transform.localPosition = new Vector3(x, y, 0);
                int index = Random.Range(0, numbers.Count - 1);
                currentGridNumber.GetComponent<GridNumber>().SetNumber(numbers[index]);
                numbers.RemoveAt(index);
            }
        }
    }

    private void AsignNumberList() {
        numbers = new List<int>();
        for (int i = 0; i <= 25; ++i) {
            numbers.Add(i);
        }
    }
}
