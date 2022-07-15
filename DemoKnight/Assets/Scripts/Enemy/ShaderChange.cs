using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderChange : MonoBehaviour
{
    public Material material;

    public void ShaderVisibility(float thickness)
    {
        material.color = Color.red;//SetColor("Color", Color.red);
    }
}
