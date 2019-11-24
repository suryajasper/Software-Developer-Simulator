using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageMenus : MonoBehaviour
{
    public GameObject FirstMenu;
    public GameObject NewGameMenu;
    // Start is called before the first frame update
    void Start()
    {
        FirstMenu.SetActive(true);
        NewGameMenu.SetActive(false);
    }
    public void switchToNewGameMenu()
    {
        FirstMenu.SetActive(false);
        NewGameMenu.SetActive(true);
    }
    public void switchToMainMenu()
    {
        FirstMenu.SetActive(true);
        NewGameMenu.SetActive(false);
    }
}
