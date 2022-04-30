using UnityEngine;

namespace Testovoe
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private MeshRenderer currentMeshRenderer;
        [SerializeField] private float speed;
        [SerializeField] private float maxFlyTime;
        [SerializeField] private int damage;

        private Vector3 direction;

        private bool shooted = false;

        private float currentFlyTime = 0f;
        private bool isHitted = false;
        public void Shoot(Vector3 inDirecton)
        {
            direction = inDirecton;
            shooted = true;
        }
        private void Start()
        {
            ColorDataItem currentData = RewardManager.GetCurrentColorItemData();
            if(currentData!= null)
            {
                currentMeshRenderer.material.color = currentData.color;
            }
            RaycastHit hit;
            if(Physics.Raycast(transform.position, direction, out hit, 300f))
            {
                IEnemy enemyHit = hit.transform.gameObject.GetComponent<IEnemy>();
                if(enemyHit != null)
                {
                    enemyHit.SetDamage(damage);
                    isHitted = true;
                }
            }

        }
        private void FixedUpdate()
        {
            if (shooted)
            {
                transform.position += direction * Time.fixedDeltaTime * speed;
            }
            currentFlyTime = Time.fixedDeltaTime;
            if(currentFlyTime >= maxFlyTime)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Gamer>())
                return;
            IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
            if(enemy != null)
            {
                if (!isHitted)
                {
                    enemy.SetDamage(damage);
                    isHitted = true;
                }
            }
            Destroy(gameObject);
        }
    }
}