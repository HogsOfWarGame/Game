using UnityEngine;

namespace Characters.Player
{
    public class Shotgun : Weapon
    {
        [SerializeField] private GameObject bulletPre;
        [SerializeField] private int pelletsCount = 10;
        [SerializeField] private float spreadAngle = 20f;

        public override void Fire()
        {
            
        }
    }
}