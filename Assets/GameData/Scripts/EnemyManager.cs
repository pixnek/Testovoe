using System.Collections.Generic;
using UnityEngine;

namespace Testovoe
{
    public class EnemyManager : MonoBehaviour
    {
        private static EnemyManager Instance;
        [SerializeField] private GameObject prefabEnemy;
        [SerializeField] private float spawnTime;

        private float reloadSpawnTime = 0f;

        private static List<IEnemy> enemies = new List<IEnemy>();

        private static Vector3 lastCharaterPosition;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        public static void AddEnemy(IEnemy inEnenmy)
        {
            enemies.Add(inEnenmy);
        }
        public static void RemoveEnemy(IEnemy inEnemy)
        {
            enemies.Remove(inEnemy);
        }
        public void CreateEnemy()
        {
            GameObject insertEnemy = Instantiate(prefabEnemy);
            insertEnemy.transform.position = Gamer.Position - Gamer.Forward * UnityEngine.Random.Range(5f, 40f) - Gamer.Right * UnityEngine.Random.Range(-40f, 40f);
            insertEnemy.GetComponent<IEnemy>().GoToTarget(lastCharaterPosition);
        }
        private void Update()
        {
            if(reloadSpawnTime <= 0f)
            {
                CreateEnemy();
                reloadSpawnTime = spawnTime;
            }
            else
            {
                reloadSpawnTime -= Time.deltaTime;
            }
        }
        public static void ChangeTargetPosition(Vector3 newPosition)
        {
            lastCharaterPosition = newPosition;
            foreach (var item in enemies)
            {
                item.GoToTarget(newPosition);
            }
        }
    }
}