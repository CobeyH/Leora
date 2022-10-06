using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Menu Event Channel")]
public class MenuEventChannelSO : ScriptableObject
{
    public UnityAction<MenuType> OnEventRaised;
    public void RaiseEvent(MenuType menu)
    {
        OnEventRaised?.Invoke(menu);
    }
}
