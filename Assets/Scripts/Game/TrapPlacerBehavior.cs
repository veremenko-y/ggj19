using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapPlacerBehavior : MonoBehaviour
{
    public Transform TrapButtonPrefab;
    public bool HasSprinkler;
    public bool HasRake;

    public Sprite SprinklerMenuSprite;
    public Sprite RakeMenuSprite;

    public Transform SprinklerPrefab;
    public Transform RakePrefab;

    public Transform SprinklerPrefabPreview;
    public Transform RakePrefabPreview;

    private string SelectedTrap = null;
    private Transform objectToPlace = null;
    private Canvas canvas;
    private List<Transform> buttons = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();

        var button = Instantiate(TrapButtonPrefab, canvas.transform);
        button.name = "Sprinkler";
        button.GetComponent<Image>().sprite = SprinklerMenuSprite;
        var rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(32, -32);
        button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick("Sprinkler"));
        buttons.Add(button);
    }

    void OnButtonClick(string type)
    {
        SelectedTrap = type;
        if (SelectedTrap == "Sprinkler")
        {
            objectToPlace = Instantiate(SprinklerPrefabPreview);
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
                    DestroyPreviewObject();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            DestroyPreviewObject();
        }
    }

    private void DestroyPreviewObject()
    {
        SelectedTrap = null;
        Destroy(objectToPlace.gameObject);
        objectToPlace = null;
    }
}
