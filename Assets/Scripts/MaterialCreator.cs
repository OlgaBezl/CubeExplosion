using UnityEngine;

public class MaterialCreator : MonoBehaviour
{
    private string _shaderName = "Standard";
    private Shader _shader;

    private void Start()
    {
        _shader = Shader.Find(_shaderName);
    }

    public Material CreateRandomMaterial()
    {
        Material newMaterial = new Material(_shader);
        newMaterial.color = Random.ColorHSV();

        return newMaterial;
    }
}
