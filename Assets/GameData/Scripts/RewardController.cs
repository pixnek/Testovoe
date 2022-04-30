using UnityEngine;

namespace Testovoe
{
    public class RewardController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Rigidbody currentRigidbody;
        [SerializeField] private float startImpulseForce;

        private ColorDataItem currentColorDataItem;
        public ColorDataItem CurrentColorDataItem { get => currentColorDataItem; }
        // Start is called before the first frame update
        void Start()
        {
            currentRigidbody.AddForce(new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f, UnityEngine.Random.Range(-1f, 1f)) * startImpulseForce, ForceMode.Impulse);
        }
        public void SetData(ColorDataItem inColorDataItem)
        {
            currentColorDataItem = inColorDataItem;
            meshRenderer.material.color = currentColorDataItem.color;
        }
        private void OnTriggerEnter(Collider other)
        {
            Gamer gamer = other.gameObject.GetComponent<Gamer>();
            if (gamer != null)
            {
                RewardManager.PutReward(this);
                Destroy(gameObject);
            }
        }
    }
}