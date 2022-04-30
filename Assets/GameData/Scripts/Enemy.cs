using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Testovoe
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] protected NavMeshAgent currentNavMeshAgent;
        [SerializeField] protected int health;
        [SerializeField] protected int damage;
        [SerializeField] protected float reloadTime;
        [SerializeField] protected GameObject noFullHPObject;

        private float currentReloadTime = 0f;

        private IEnemyTarget currentTarget = null;
        private Coroutine punchCo = null;

        private void Start()
        {
            EnemyManager.AddEnemy(this);
        }
        public void Death()
        {
            RewardManager.CreateReward(transform.position);
            Destroy(gameObject);
        }

        public virtual void GoToTarget(Vector3 targetPosition)
        {
            currentNavMeshAgent.SetDestination(targetPosition);
        }

        public void SetDamage(int damage)
        {
            health -= damage;
            noFullHPObject.SetActive(true);
            if (health <= 0)
            {
                Death();
            }
        }
        private void OnDestroy()
        {
            EnemyManager.RemoveEnemy(this);
        }
        private void OnTriggerEnter(Collider other)
        {
            IEnemyTarget enemyTarget = other.gameObject.GetComponent<IEnemyTarget>();
            if (enemyTarget != null)
            {
                currentTarget = enemyTarget;
                currentNavMeshAgent.isStopped = true;
                TryPunch();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            IEnemyTarget exitTarget = other.gameObject.GetComponent<IEnemyTarget>();
            if(exitTarget == currentTarget)
            {
                currentTarget = null;
                currentNavMeshAgent.isStopped = false;
            }
        }
        private void TryPunch()
        {
            if(punchCo == null)
            {
                punchCo = StartCoroutine(PunchCoroutine());
            }
        }
        private IEnumerator PunchCoroutine()
        {
            while (true)
            {
                while(currentReloadTime > 0f)
                {
                    currentReloadTime -= Time.deltaTime;
                    yield return null;
                }
                if(currentTarget != null)
                {
                    currentTarget.SetDamage(damage);
                    currentReloadTime = reloadTime;
                }
                else
                {
                    punchCo = null;
                    break;
                }
                yield return null;
            }
        }
    }
}