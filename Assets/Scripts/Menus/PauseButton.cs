using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    GameController gameController;
    [SerializeField]
    private Sprite[] icons = new Sprite[2];
    private Image img;
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        img = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (gameController.IsGamePaused())
        {
            img.sprite = icons[1];
        }
        else
        {
            img.sprite = icons[0];
        }
    }

    public void HandlePauseButton()
    {
        gameController.TogglePauseMenu();
    }

}
