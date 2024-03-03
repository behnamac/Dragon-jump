using UnityEngine;

namespace Structs
{
    [System.Serializable]
    public struct LevelUpData
    {
        public int level;
        public float muscleValue;
        public GameObject[] itemsToActivate;
    }
}