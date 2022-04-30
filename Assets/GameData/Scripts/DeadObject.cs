using UnityEngine;

namespace Testovoe
{
    public class DeadObject : MonoBehaviour
    {
        [SerializeField] private float lifeTime;

        private float currentTime = 0f;
        // Update is called once per frame
        void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= lifeTime)
                Destroy(gameObject);
        }
    }
}