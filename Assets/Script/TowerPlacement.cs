using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacement : MonoBehaviour
{
    [Header("Main")]

    public Camera mainCamera;
    public Tilemap tilemap;
    public Tile usedTile;

    private Vector3Int mousePositionInt32;
    private bool isPlaceFree = false;
    private CursorSettings cursor;

    [Header("Cursor Settings")]

    public CursorSettings cursorPrefab;

    private bool cursorMenuActive;

    [Header("Towers")]

    public GameObject towerGunPrefab;
    public GameObject towerLaserPrefab;
    public GameObject towerIcePrefab;



    private void Start()
    { 
        cursor = Instantiate(cursorPrefab, new Vector3(0, 0, 0), transform.rotation);
        cursorMenuActive = false;
        cursor.towerGunButton.onClick.AddListener(delegate { CreateTower(towerGunPrefab); });
        cursor.towerLaserButton.onClick.AddListener(delegate { CreateTower(towerLaserPrefab); });
        cursor.towerIceButton.onClick.AddListener(delegate { CreateTower(towerIcePrefab); });
    }

    void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (GameController.gameController.gameIsActive && !cursorMenuActive)
        {
            mousePositionInt32 = ConvertMousepositionToInt(mousePosition.x, mousePosition.y);
            UpdateCursor();
        }

        float dist = Vector3.Distance(mousePosition, cursor.cursorMid.position);
        Debug.Log(dist);

        if (Input.GetMouseButtonDown(0) && isPlaceFree)
        {
            cursorMenuActive = true;
            cursor.CallCursorMenu(cursorMenuActive);
        }

        if (Input.GetMouseButtonDown(1) || dist > 1.5f)
        {
            cursorMenuActive = false;
            cursor.CallCursorMenu(cursorMenuActive);
        }
    }

    void UpdateCursor()
    {
        if (cursor == null)
            return;

        cursor.transform.position = mousePositionInt32;

        isPlaceFree = tilemap.GetTile(mousePositionInt32) == null;
        cursor.ChangeCursorImage(isPlaceFree);
    }

    void CreateTower(GameObject tower)
    {
        int towerCost = GameController.gameController.towerCost;
        int pScore = GameController.gameController.score;

        if (pScore >= towerCost )
        {
            tilemap.SetTile(mousePositionInt32, usedTile);
            Instantiate(tower, mousePositionInt32, transform.rotation);
            GameController.gameController.AddScore(-towerCost);
            GameController.gameController.UpdateTowerCost();
            cursorMenuActive = false;
            cursor.CallCursorMenu(cursorMenuActive);
        }
    }

    private Vector3Int ConvertMousepositionToInt(float x, float y)
    {
        int x1 = (int) x;
        int y1 = (int) y;
        Vector3Int vector3Int = new Vector3Int(x1, y1);

        return vector3Int;
    }
}
