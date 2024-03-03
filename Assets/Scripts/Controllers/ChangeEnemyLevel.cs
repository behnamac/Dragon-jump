using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Player;

public class ChangeEnemyLevel : MonoBehaviour
{
    public static ChangeEnemyLevel Instance;

    [SerializeField] private float distanceForChange = 10;
    [SerializeField, Range(0, 100)] private float possibilityOfRaisingLevel;
    [SerializeField] private int upLevelValue = 10;
    [SerializeField] private int downLevelValue = 5;

    List<EnemyController> allEnemies;
    private void Awake()
    {
        Instance = this;

        allEnemies = new List<EnemyController>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allEnemies.Count; i++)
        {
            float enemyZPos = allEnemies[i].transform.position.z;
            float playerZPos = PlayerLevelController.Instance.transform.position.z;

            float distance = Mathf.Abs(enemyZPos - playerZPos);

            if (distance < distanceForChange) 
            {
                ChangeLevel(allEnemies[i]);
            }
        }
    }

    private void ChangeLevel(EnemyController enemy) 
    {
        var possibility = Random.Range(0, 100);
        int level = PlayerLevelController.Instance.CurrentLevel;

        if (possibility <= possibilityOfRaisingLevel)
        {
            level += upLevelValue;
        }
        else 
        {
            level -= downLevelValue;

            level = Mathf.Clamp(level, 1, level);
        }

        enemy.SetNewLevel(level);

        allEnemies.Remove(enemy);
    }

    public void AddEnemy(EnemyController enemy) 
    {
        allEnemies.Add(enemy);
    }
}
