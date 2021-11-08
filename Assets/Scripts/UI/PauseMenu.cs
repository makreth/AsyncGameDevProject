using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Canvas canvas;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (active)
            {
                DisableMenu();
            }
            else
            {
                EnableMenu();
            }
        }
    }

    void EnableMenu()
    {
        canvas.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        active = true;
    }

    void DisableMenu()
    {
        canvas.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        active = false;
    }

    public void OnPressResumeButton()
    {
        DisableMenu();
    }
}
