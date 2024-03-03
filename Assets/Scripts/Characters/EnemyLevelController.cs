using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Interactables;

public class EnemyLevelController : MonoBehaviour
{
    [SerializeField] private ItemHolder[] allItems;
    [SerializeField] private Renderer meshForVisible;


    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        SetNewItem();
    }

    private void OnDestroy() 
    {
    }

    public void SetNewItem()
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            if (GetComponent<EnemyController>().GetCurrentLevel() >= allItems[i].targetLevel)
            {
                for (int j = 0; j < allItems[i].diactiveItems.Length; j++)
                {
                    allItems[i].diactiveItems[j].SetActive(false);
                }
                for (int j = 0; j < allItems[i].activeItems.Length; j++)
                {
                    allItems[i].activeItems[j].SetActive(true);
                }
            }
        }
    }
}
