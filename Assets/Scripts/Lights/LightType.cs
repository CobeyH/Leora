using UnityEngine;

[System.Serializable]
public class LightData
{
    public AreaOfEffect area;
    public ActivationType activation;
    public bool canRotate;
    public bool returnsLux = false;
    public bool startsOn;
}
public enum AreaOfEffect
{
    Point,
    Directional
}

public enum ActivationType
{
    PlayerControlled,
    AlwaysOn
}