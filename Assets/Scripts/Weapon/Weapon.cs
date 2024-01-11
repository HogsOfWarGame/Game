using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected string Name;
    [SerializeField] protected float Damage;
    [SerializeField] protected float Range;
    [SerializeField] protected float FireRate;
    [SerializeField] protected int MagazineSize;
    [SerializeField] protected int CurrentAmmo;
    [SerializeField] protected float ReloadTime;

    private bool isReloading = false;
    private float lastFireTime;

    public abstract void Fire();

    public void TryFire()
    {
        if (isReloading || Time.time - lastFireTime < 1f / FireRate || CurrentAmmo <= 0)
        {
            return;
        }

        lastFireTime = Time.time;
        CurrentAmmo--;
        Fire();
    }

    public IEnumerator Reload()
    {
        if (CurrentAmmo == MagazineSize || isReloading)
        {
            yield break;
        }

        isReloading = true;
        yield return new WaitForSeconds(ReloadTime);
        CurrentAmmo = MagazineSize;
        isReloading = false;
    }
}
