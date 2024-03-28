using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamage
{
    [SerializeField] HealthPool health;
    [SerializeField] AttackPool attack;
    [SerializeField] SpeedPool speed;
    [SerializeField] EnemyMovement move;
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] Material baseMaterial;
    [SerializeField] Material hitMaterial;
    [SerializeField] int PointVal;

    public SpeedPool Spd => speed;
    public AttackPool Atk => attack;

    // Start is called before the first frame update
    void Start()
    {
        MaxStats();
        mesh.material = baseMaterial;

        if (mesh == null)
        {
            mesh = GetComponent<SkinnedMeshRenderer>();
        }

        health.OnDepleted += Health_OnDepleted;
    }
    private void Health_OnDepleted()
    {
        PointsManager.instance.AddPoints(PointVal);
        UIManager.Instance.UpdateScore();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        move.Movement();
    }

    public void TakeDamage(float damage)
    {
        health.UseResource(damage);
        if (health.IsValid)
        {
            //StartCoroutine(FlashDamage());
            Debug.Log("Is Hit");
        }
    }

    public void MaxStats()
    {
        attack.SetMax();
        health.SetMax();
        speed.SetMax();
    }

    //private IEnumerator FlashDamage()
    //{
    //    mesh.material = hitMaterial;
    //    yield return new WaitForSeconds(0.25f);
    //    mesh.material = baseMaterial;
    //}
}
