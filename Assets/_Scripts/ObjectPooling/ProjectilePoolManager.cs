
using UnityEngine;

public class ProjectilePoolManager : GenericPoolManager<PooledProjectile>
{
    public static new ProjectilePoolManager Instance => (ProjectilePoolManager)GenericPoolManager<PooledProjectile>.Instance;
    [SerializeField] private bool isShooting = false;
    public void Initiate(Transform spawnPosition, Transform targetPosition)
    {
        if (!isShooting)
        {
            PooledProjectile projectile = ProjectilePoolManager.Instance.Get();
            projectile.transform.position = spawnPosition.position;
            projectile._target = targetPosition;
            projectile._damage = PlayerController.Instance._currentDamage;
            projectile.gameObject.SetActive(true);
        }

        isShooting = true;  
    }

    public void ResetAttack()
    {
        isShooting = false;
    }
}