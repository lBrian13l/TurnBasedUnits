using UnityEngine;
using TurnBasedUnits.Helpers;

namespace TurnBasedUnits.UI
{
    public class StatPlate : MonoBehaviour
    {
        [SerializeField] private Bar[] _bars;

        public void Init()
        {
            foreach (Bar bar in _bars)
                bar.Init();
        }

        public void UpdateBar(int value, StatType type)
        {
            foreach (Bar bar in _bars)
            {
                if (bar.Type == type)
                {
                    bar.UpdateUi(value);
                    return;
                }
            }
        }
    }
}
