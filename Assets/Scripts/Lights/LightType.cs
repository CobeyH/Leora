using UnityEngine;

[System.Serializable]
public class LightData
{
    public AreaOfEffect area;
    public ActivationType activation;
    public bool canDim;
    public bool canRotate;
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