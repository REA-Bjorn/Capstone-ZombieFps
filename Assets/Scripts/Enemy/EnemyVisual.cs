using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] List<Material> _modelMaterial;
    [SerializeField] SkinnedMeshRenderer _renderer;

    public void SetRandomMaterial()
    {
        _renderer.material = _modelMaterial[Random.Range(0, _modelMaterial.Count)];
    }
}
