using UnityEngine;
using System.Collections;

namespace Testovoe
{

    /// <summary>
    /// This is not a functional weapon script. It just shows how to implement shooting and reloading with buttons system.
    /// </summary>
    public class WeaponFromExample : MonoBehaviour
    {
        [SerializeField] private GameObject bulletStartPlace;
        [SerializeField] private GameObject shootSoundEffect;
        [SerializeField] private Bullet bulletPrefab;
        public FP_Input playerInput;

        public float shootRate = 0.15F;

        private float delay;

        void Update()
        {
            if (playerInput.Shoot())                         //IF SHOOT BUTTON IS PRESSED (Replace your mouse input)
                if (Time.time > delay)
                    Shoot();
        }

        void Shoot()
        {
            var insertBullet = Instantiate(bulletPrefab);
            insertBullet.transform.position = bulletStartPlace.transform.position;
            Vector3? target = Gamer.GetShootTarget();
            insertBullet.Shoot(target != null ? (target.Value - bulletStartPlace.transform.position).normalized : bulletStartPlace.transform.forward);
            Instantiate(shootSoundEffect).transform.position = bulletStartPlace.transform.position;
            Debug.Log("Shoot");

            delay = Time.time + shootRate;
        }
    }
}