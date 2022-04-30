using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Testovoe
{
    public class RewardManager : MonoBehaviour
    {
        private static RewardManager Instance;

        [SerializeField] private RewardController rewardPrefab;
        [SerializeField] private ColorData rewardColors;
        [SerializeField] private TextMeshProUGUI rewardInfo;
        [SerializeField] private GameObject pickUpSoundEffect;

        private static ColorDataItem lastReward = null;

        private Dictionary<ColorDataItem, int> colorsInfo = new Dictionary<ColorDataItem, int>();

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
        private void Start()
        {
            Clear();
        }
        public static void Clear()
        {
            if(Instance != null)
            {
                Instance.ClearLocal();
            }
        }
        public void ClearLocal()
        {
            colorsInfo.Clear();
            foreach (var colorDataItem in rewardColors.colors)
            {
                colorsInfo.Add(colorDataItem, 0);
            }
            lastReward = null;
            SetCurrentRewardInfo();
        }
        public static void CreateReward(Vector3 position)
        {
            if(Instance != null)
            {
                var reward = Instantiate(Instance.rewardPrefab);
                reward.SetData(Instance.rewardColors.colors[UnityEngine.Random.Range(0, Instance.rewardColors.colors.Count)]);
                reward.transform.position = position;
            }
        }
        public static void PutReward(RewardController inReward)
        {
            lastReward = inReward.CurrentColorDataItem;
            if(Instance != null)
            {
                Instance.colorsInfo[lastReward]++;
                Instance.SetCurrentRewardInfo();
                Instantiate(Instance.pickUpSoundEffect).transform.position = inReward.gameObject.transform.position;
            }
        }
        public static ColorDataItem GetCurrentColorItemData()
        {
            return lastReward;
        }
        public void SetCurrentRewardInfo()
        {
            string infoText = "";
            foreach(var colorItemInfo in colorsInfo)
            {
                infoText += colorItemInfo.Key.viewName + ": " + colorItemInfo.Value+"\r\n";
            }
            rewardInfo.text = infoText;
        }
    }
}