using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMaterial : MonoBehaviour {

    public Color32 DefaultColor;
    public Material material;
    private Color32 colorChange;

    private void Start()
    {
        colorChange = new Color32(152, 152, 152, 255);
        ChangeColor();
    }

    public void MaterialGrey()
    {
        colorChange = new Color32(152, 152, 152, 255);
        ChangeColor();
    }
    public void MaterialRed()
    {
        colorChange = new Color32(255, 0, 0, 255);
        ChangeColor();
    }
    public void MaterialBlue()
    {
        colorChange = new Color32(0, 0, 255, 255);
        ChangeColor();
    }
    public void MaterialYellow()
    {
        colorChange = new Color32(255, 255, 0, 255);
        ChangeColor();
    }
    public void MaterialWhite()
    {
        colorChange = new Color32(0, 0, 0, 0);
        ChangeColor();
    }
    private void ChangeColor()
    {
        material.color = colorChange;
    }
}
