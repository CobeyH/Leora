using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothColour : MonoBehaviour
{
    public Color baseColor;

    Material mat;

    bool isDecoy = false;

    // Start is called before the first frame update
    void Start()
    {
        mat = gameObject.GetComponent<ParticleSystemRenderer>().material;
        isDecoy = gameObject.GetComponent<Goal>().IsDecoy;
        if (isDecoy)
        {
            mat.color = Color.white;
            mat.SetColor("_EmissionColor", Color.gray);
        }
        else
        {
            mat.color = baseColor;
            mat.SetColor("_EmissionColor", baseColor);
        }
    }
}
