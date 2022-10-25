using UnityEngine;

[System.Serializable]
public class LightData
{
    public AreaOfEffect area;

    public ActivationType activation;

    public bool returnsLux = false;

    public bool startsOn;

    public int lightIntensity = 1;

    [HideInInspector]
    public bool canRotate;
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
