using UnityEngine;
using UnityEngine.SceneManagement;

namespace Testovoe
{
    public class Gamer : MonoBehaviour, IEnemyTarget
    {
        private static Gamer Instance;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private int startHealth;

        public delegate void ChangeHP(int currentHP);
        public static ChangeHP OnChangeHP;

        private int currentHealth;
        public static int CurrentHP
        {
            get
            {
                if (Instance != null)
                {
                    return Instance.currentHealth;
                }
                return 0;
            }
            set
            {
                if(Instance != null)
                {
                    if (Instance != null)
                    {
                        if(Instance.currentHealth != value)
                        {
                            Instance.currentHealth = value;
                            OnChangeHP?.Invoke(Instance.currentHealth);
                        }
                    }
                }
            }
        }
        public static Vector3 Position
        {
            get
            {
                if(Instance != null)
                {
                    return Instance.transform.position;
                }
                return Vector3.zero;
            }
        }
        public static Vector3 Forward
        {
            get
            {
                if (Instance != null)
                {
                    return Instance.transform.forward;
                }
                return Vector3.forward;
            }
        }
        public static Vector3 Right
        {
            get
            {
                if (Instance != null)
                {
                    return Instance.transform.right;
                }
                return Vector3.right;
            }
        }
        
        public void SetDamage(int inDamage)
        {
            CurrentHP -= inDamage;
            if(CurrentHP <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
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
        // Start is called before the first frame update
        void Start()
        {
            CurrentHP = startHealth;
        }
        public static Vector3? GetShootTarget()
        {
            if (Instance != null)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(Instance.mainCamera.transform.position, Instance.mainCamera.transform.forward, out raycastHit, 100f))
                {
                    return raycastHit.point;
                }
            }
            return null;
        }
    }
}