using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    GameController gameController;
    [SerializeField]
    private Sprite[] icons = new Sprite[2];
    private int currentSpriteIndex = 0;
    private Image img;
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        img = gameObject.GetComponent<Image>();
    }

    public void HandlePauseButton()
    {
        gameController.TogglePauseMenu();
        currentSpriteIndex = (currentSpriteIndex + 1) % 2;
        img.sprite = icons[currentSpriteIndex];
    }

}
