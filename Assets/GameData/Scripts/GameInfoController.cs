using TMPro;
using UnityEngine;

namespace Testovoe
{
    public class GameInfoController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpInfo;

        private void Start()
        {
            SetHPInfo(Gamer.CurrentHP);
            Gamer.OnChangeHP += SetHPInfo;
        }
        public void SetHPInfo(int inHP)
        {
            hpInfo.text = (Mathf.Max(inHP, 0)).ToString();
        }
        private void OnDestroy()
        {

            Gamer.OnChangeHP -= SetHPInfo;
        }
    }
}