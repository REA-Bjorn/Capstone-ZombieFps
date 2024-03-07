using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamage
{

    [SerializeField] HealthPool health;

    [SerializeField] SkinnedMeshRenderer mesh;

    [SerializeField] Material baseMaterial;

    [SerializeField] Material hitMaterial;

    // Start is called before the first frame update
    void Start()
    {
        mesh.material = baseMaterial;

        health.SetMax();

        if (mesh == null)
        {
             mesh = GetComponent<SkinnedMeshRenderer>();
        }

        health.OnDepleted += Health_OnDepleted;
    }
    private void Health_OnDepleted()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health.UseResource(damage);
        if (health.IsValid)
        {
            StartCoroutine(FlashDamage());
            Debug.Log("Is Hit");
        }
    }

    private IEnumerator FlashDamage()
    {
        mesh.material = hitMaterial;
        yield return new WaitForSeconds(0.25f);
        mesh.material = baseMaterial;
    }
}
