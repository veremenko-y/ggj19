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

    public bool HasWall = false;
    public Sprite WallMenuSprite;
    public Transform WallPrefab;
    public Transform WallPrefabPreview;
    public int WallPrice = 100;

    private string SelectedTrap = null;
    private Transform objectToPlace = null;
    private Transform objectToAdd = null;
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

        button = Instantiate(TrapButtonPrefab, canvas.transform);
        button.name = "Wall";
        button.GetComponent<Image>().sprite = WallMenuSprite;
        rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(64*2, -64);
        button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick("Wall"));
        buttons.Add(button);
    }

    void OnButtonClick(string type)
    {
        SelectedTrap = type;
        if (SelectedTrap == "Sprinkler" && gameManager.Points >= SprinklerPrice)
        {
            objectToPlace = Instantiate(SprinklerPrefabPreview);
            objectToAdd = SprinklerPrefab;
            priceToPay = SprinklerPrice;
        }
        else if (SelectedTrap == "Rake" && gameManager.Points >= RakePrice)
        {
            objectToPlace = Instantiate(RakePrefabPreview);
            objectToAdd = RakePrefab;
            priceToPay = RakePrice;
        }
        else if (SelectedTrap == "Wall" && gameManager.Points >= WallPrice)
        {
            objectToPlace = Instantiate(WallPrefabPreview);
            objectToAdd = WallPrefab;
            priceToPay = WallPrice;
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
                SetAllowedColor(hit.transform.name == "Ground");
                if (Input.GetMouseButton(0) &&
                    hit.transform.name == "Ground")
                {
                    Instantiate(objectToAdd, objectToPlace.transform.position, Quaternion.identity);
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
        buttons[2].GetComponent<Button>().interactable = gameManager.Points >= WallPrice;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            OnButtonClick("Sprinkler");
        if (Input.GetKeyDown(KeyCode.Alpha2))
            OnButtonClick("Rake");
        if (Input.GetKeyDown(KeyCode.Alpha3))
            OnButtonClick("Wall");
    }

    private void DestroyPreviewObject()
    {
        SelectedTrap = null;
        if(objectToPlace != null)
            Destroy(objectToPlace.gameObject);
        objectToPlace = null;
    }

    private void SetAllowedColor(bool allowed)
    {
        if(objectToPlace != null)
        {
            var renderer = objectToPlace.GetComponent<SpriteRenderer>();
            Color c = allowed ? new Color(1, 1, 1, 1) : new Color(1, 0, 0, .5f);
            renderer.color = c;
        }
    }
}
