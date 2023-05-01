using UnityEngine;
using UnityEngine.UI;

public class CursorSettings : MonoBehaviour
{
    [Header("Main Settings")]

    public SpriteRenderer cursorActive;
    public SpriteRenderer cursorDeactive;
    public Canvas cursorMenu;
    public static CursorSettings cursor;
    public Transform cursorMid;

    [Header("Cursor Menu")]

    public Button towerGunButton;
    public Button towerLaserButton;
    public Button towerIceButton;

    private void Start()
    {
        cursor = this;
        cursorMenu.enabled = false;

    }

    public void CallCursorMenu(bool menuActive)
    {
        if (menuActive == true)
            cursorMenu.enabled = true;
        else if (menuActive == false)
            cursorMenu.enabled = false;
    }

    public void ChangeCursorImage(bool canBuild)
    {
        if(canBuild == true)
        {
            cursorActive.enabled = true;
            cursorDeactive.enabled = false;
        }
        else
        {
            cursorActive.enabled = false;
            cursorDeactive.enabled = true;
        }
    }
}
