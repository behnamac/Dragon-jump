using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class CharacterLevelBase : MonoBehaviour
    {
        [SerializeField] protected int currentLevel = 1;
        [SerializeField] protected Text levelText;

        protected virtual void Start()
        {
            UpdateLevelText();
        }

        protected virtual void UpdateLevelText()
        {
            levelText.text = currentLevel+ " lvl";
        }
    }
}