using UnityEngine;

namespace Testovoe
{
    public interface IEnemy
    {
        void GoToTarget(Vector3 targetPosition);
        void Death();
        void SetDamage(int damage);
    }
}