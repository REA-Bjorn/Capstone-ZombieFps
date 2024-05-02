using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyVisual : MonoBehaviour
{
    [SerializeField] List<Material> _modelMaterial;
    [SerializeField] SkinnedMeshRenderer _renderer;

    [SerializeField] private Renderer r_eyeMat;
    [SerializeField] private Renderer l_eyeMat;

    public void UpdateEnemyEyes(float healthPercentage)
    {
        r_eyeMat.material.SetColor("_EmissionColor", Color.red * healthPercentage);
        l_eyeMat.material.SetColor("_EmissionColor", Color.red * healthPercentage);
    }

    public void SetRandomMaterial()
    {
        _renderer.material = _modelMaterial[Random.Range(0, _modelMaterial.Count)];
    }
}
