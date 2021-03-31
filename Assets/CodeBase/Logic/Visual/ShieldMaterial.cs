using UnityEngine;

public class ShieldMaterial : MonoBehaviour
{
    private const string _fresnelColorName = "Color_57489fd0e47d40f08ce1987e29ddbd2f";
    private const string _mainColorName = "Color_6ccb84ce8a384ad58be0afc83dd4d518";
    private const string _vertexOffset = "Vector1_09a52f16d7f14c16801adcf7e9d41371";
    public Material MainMaterial;
    public Light LightDepend;
    public Gradient Fresnel;
    public Gradient Main;

    public float StrobeDuration = 2f;

    private Coroutine _gradientChangeColor;

    void Update()
    {
        float t = Mathf.PingPong(Time.time / StrobeDuration, 1f);

        Color fresnelResult = Fresnel.Evaluate(t);
        Color mainResult = Main.Evaluate(t);

        MainMaterial.SetColor(_fresnelColorName, fresnelResult);
        MainMaterial.SetFloat(_vertexOffset, Mathf.Lerp(-3f, 3f, t));
        LightDepend.color = fresnelResult;

        MainMaterial.SetColor(_mainColorName, mainResult);
    }
}