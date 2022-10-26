using UnityEngine;

[System.Serializable]
public class LightData
{
    public AreaOfEffect area;

    public ActivationType activation;

    public bool returnsLux = false;

    public bool startsOn;

    public int maxIntensity = 1;
    public float rangeMultiplier = 1;

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
