using UnityEngine;

public class GradientColorChange : MonoBehaviour
{
    private const string _fresnelColorName = "Color_57489fd0e47d40f08ce1987e29ddbd2f";
    private const string _mainColorName = "Color_6ccb84ce8a384ad58be0afc83dd4d518";
    public Light LightDepend;
    public Material Material;

    void Update()
    {
        Color fresnel = Material.GetColor(_fresnelColorName);
        Material.SetColor(_fresnelColorName,AlignToColor(fresnel, GetRandomValue()));

        LightDepend.color = fresnel;

        Color main = Material.GetColor(_mainColorName);
        Material.SetColor(_mainColorName, AlignToColor(main, GetRandomValue()));
    }

    private Color AlignToColor(Color color, float changeValue)
    {
        color.r = color.r >= 1 ? 0 : color.r + changeValue;
        color.g = color.g >= 1 ? 0 : color.g + changeValue;
        color.b = color.b >= 1 ? 0 : color.b + changeValue;
        return color;
    }

    private float GetRandomValue()
    {
        return Random.Range(0f, 0.01f);
    }
}