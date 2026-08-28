using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EmissionContr : MonoBehaviour
{
    private Material _material;
    //[SerializeField] private Light2D _light;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        _material = spriteRenderer.material;

        _material.DisableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor", Color.black);
    }

    //public void PlayEmission() 
    //{
    //    _material.DisableKeyword("_EMISSION");
    //    _material.SetColor("_EmissionColor", Color.yellow * 5f);

    //    Debug.Log("Emission On!");
    //}
}
