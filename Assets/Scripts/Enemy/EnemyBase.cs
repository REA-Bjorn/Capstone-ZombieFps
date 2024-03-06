using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour,IDamage
{

    [SerializeField] HealthPool health;

    [SerializeField] MeshRenderer mesh;

    [SerializeField] Color baseColor;

    // Start is called before the first frame update
    void Start()
    {
        health.SetMax();

       if (mesh == null)
        {
            mesh = GetComponent<MeshRenderer>();
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
        }
    }

    private IEnumerator FlashDamage()
    {
        mesh.material.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        mesh.material.color = baseColor;
    }
}
