using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    public bool IsPaused = false;

    public PlayerController PlrController;

    public GameObject PauseUI;

    void Start()
    {
        LockCursor();
    }

    void Update () {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Pause();
        } else
        {
            if (IsPaused == false)
            {
                LockCursor();
            }
        }
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
        if (IsPaused == true)
        {
            PauseUI.SetActive(true);
            PlrController.enabled = false;
            UnlockCursor();
        }
        else
        {
            PauseUI.SetActive(false);
            PlrController.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            LockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
