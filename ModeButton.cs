using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int mode;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetMode);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void SetMode()
    {
        gameManager.mode = mode;
        gameManager.isModeScreen = false;
        gameManager.difSelectScreen.gameObject.SetActive(true);
        gameManager.modeSelectScreen.gameObject.SetActive(false);
    }
}
