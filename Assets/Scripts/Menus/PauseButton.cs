using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public MenuEventChannelSO ToggleMenuChannel;
    [SerializeField]
    private Sprite[] icons = new Sprite[2];
    private int currentIcon = 0;
    private Image img;
    void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    void OnEnable()
    {
        ToggleMenuChannel.OnEventRaised += TogglePauseButton;
    }

    void OnDisable()
    {
        ToggleMenuChannel.OnEventRaised -= TogglePauseButton;
    }

    void TogglePauseButton(MenuType menu)
    {
        if (menu != MenuType.PauseMenu) return;
        currentIcon = (currentIcon + 1) % 2;
        img.sprite = icons[currentIcon];
    }

    public void SendPauseEvent()
    {
        if (ToggleMenuChannel != null)
        {
            ToggleMenuChannel.RaiseEvent(MenuType.PauseMenu);
        }
    }

}
