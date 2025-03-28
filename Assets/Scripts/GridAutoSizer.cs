using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridAutoSizer : MonoBehaviour
{
    [SerializeField]
    private int columns = 3;
    [SerializeField]
    private int rows = 5;

    private GridLayoutGroup gridLayout;

    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        ResizeGrid();
    }

    void ResizeGrid()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float width = rect.rect.width / columns;
        float height = rect.rect.height / rows;
        gridLayout.cellSize = new Vector2(width, height);
    }
}
