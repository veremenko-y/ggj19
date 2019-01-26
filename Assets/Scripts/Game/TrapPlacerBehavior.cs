using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapPlacerBehavior : MonoBehaviour
{
    public Transform TrapButtonPrefab;

    public Sprite SprinklerMenuSprite;
    public Transform SprinklerPrefab;
    public Transform SprinklerPrefabPreview;
    public int SprinklerPrice = 200;

    public bool HasRake = false;
    public Sprite RakeMenuSprite;
    public Transform RakePrefab;
    public Transform RakePrefabPreview;
    public int RakePrice = 400;

    private string SelectedTrap = null;
    private Transform objectToPlace = null;
    private int priceToPay = 0;
    private Canvas canvas;
    private Text pointsText;
    private List<Transform> buttons = new List<Transform>();
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        canvas = GetComponentInChildren<Canvas>();
        pointsText = canvas.GetComponentInChildren<Text>();

        var button = Instantiate(TrapButtonPrefab, canvas.transform);
        button.name = "Sprinkler";
        button.GetComponent<Image>().sprite = SprinklerMenuSprite;
        var rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, -64);
        button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick("Sprinkler"));
        buttons.Add(button);


        button = Instantiate(TrapButtonPrefab, canvas.transform);
        button.name = "Rake";
        button.GetComponent<Image>().sprite = RakeMenuSprite;
        rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(64, -64);
        button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick("Rake"));
        buttons.Add(button);
    }

    void OnButtonClick(string type)
    {
        SelectedTrap = type;
        if (SelectedTrap == "Sprinkler")
        {
            objectToPlace = Instantiate(SprinklerPrefabPreview);
            priceToPay = SprinklerPrice;
        }
        else if (SelectedTrap == "Rake")
        {
            objectToPlace = Instantiate(RakePrefabPreview);
            priceToPay = RakePrice;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToPlace != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100))
            {
                var hitPosition = hit.point;
                objectToPlace.transform.position = new Vector3(hitPosition.x, 0, hitPosition.z);
                if (Input.GetMouseButton(0) &&
                    hit.transform.name == "Ground")
                {
                    Instantiate(SprinklerPrefab, objectToPlace.transform.position, Quaternion.identity);
                    gameManager.Points -= priceToPay;
                    DestroyPreviewObject();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            DestroyPreviewObject();
        }

        pointsText.text = $"Points: {gameManager.Points}";

        buttons[0].GetComponent<Button>().interactable = gameManager.Points >= SprinklerPrice;
        buttons[1].GetComponent<Button>().interactable = gameManager.Points >= RakePrice;
    }

    private void DestroyPreviewObject()
    {
        SelectedTrap = null;
        Destroy(objectToPlace.gameObject);
        objectToPlace = null;
    }
}
