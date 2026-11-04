using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
  [Header("Boutique")]
  [SerializeField] private string turretName = "Tourelle";
  [SerializeField] private int cost = 10;
  
  [Header("Tir")]
  [SerializeField] private float range = 5f;
  [SerializeField] private float fireRate = 2f;
  [SerializeField] private int damage = 2;
  [SerializeField] private GameObject projectilPrefab;
  
  [Header("Rotation")]
  [SerializeField] private Transform head;
  [SerializeField] private float turnSpeed = 8f;
  [SerializeField] private float angleOffset = 0f;

  private Enemy target;
  private float fireCountdown = 0f;
  
  public string GetName()
  {
    return turretName;
  }
  
  public int GetCost()
  {
    return cost;
  }

  private void Update()
  {
    FindTarget();
    if (target != null) return;

    Aim();
    
    fireCountdown -= Time.deltaTime;
    if (fireCountdown <= 0f)
    {
      Shoot();
      fireCountdown = 1f / fireRate;
    }

  }

  private void FindTarget()
  {
    target = null;
    
    Collider[] colliders = Physics.OverlapSphere(head.position, range);

    foreach (Collider col in colliders)
    {
      Enemy enemy = col.GetComponent<Enemy>();
      if (enemy != null)
      {
        target = enemy;
        return;
      }  
    }
  }

  private void Aim()
  {
    if (head == null) return;
    
    Vector3 direction = target.transform.position - head.position;
    direction.y = 0f;
    
    Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, angleOffset, 0);
    head.rotation = Quaternion.Slerp(head.rotation, targetRotation, Time.deltaTime * turnSpeed);

  }

  private void Shoot()
  {
    if (projectilPrefab == null)
    {
      target.TakeDamage(damage);
      return;
    }

    Vector3 start = transform.position + Vector3.up;
    if (head != null)
    {
      start = head.position;
    }  
    
    GameObject bullet = Instantiate(projectilPrefab, start, Quaternion.identity);
    Projectile projectile = bullet.GetComponent<Projectile>();
    projectile.Init(target, damage);
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(head.position, range);
  }
  
}
