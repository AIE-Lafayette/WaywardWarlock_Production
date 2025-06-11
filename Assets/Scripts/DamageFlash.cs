using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField]
    private Material _redMaterial;
    [SerializeField]
    private float _flashDuration = 0.1f;

    private SkinnedMeshRenderer _rend;
    private Material _originalMaterial;
    private Color _originalColor;
    private Coroutine _flashCoroutine;
    private Color _flashColor = Color.red;
    

    private void Start()
    {
        _rend = GetComponentInChildren<SkinnedMeshRenderer>();
        if(_rend != null)
        {
            _originalMaterial = _rend.materials[1];
            _originalColor = _originalMaterial.GetColor("_Color");
        }
    }
    public void PlayerDamageEffect()
    {
        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);

        _flashCoroutine = StartCoroutine(FlashandFade());
    }

    private IEnumerator FlashandFade()
    {
        _originalMaterial.SetColor("_BaseColor", _flashColor);

        float timer = 0;
        while(timer < _flashDuration)
        {
            float lerp = timer / _flashDuration;

            Color currentColor = Color.Lerp(_flashColor, _originalColor, lerp);
            _originalMaterial.SetColor("_BaseColor", currentColor);
            timer += Time.deltaTime;
            yield return null;
        }

        _originalMaterial.SetColor("_Color", _originalColor);
    }






}
