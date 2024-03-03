using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Interfaces;

public class EnemyPostLevel : MonoBehaviour, IInteractable
{
    [SerializeField] Transform particelHit;
    [SerializeField] float force = 100;
    private ActiveRagdollSystem activeRagdollSystem;

    private Vector3 _axisForce;
    private Animator[] _animators;
    private void Awake()
    {
        activeRagdollSystem = GetComponent<ActiveRagdollSystem>();

        _animators = GetComponentsInChildren<Animator>();
    }
    public void InteractWithTrigger(PlayerController playerController)
    {
        if (playerController.PlayerLevelController.ItemNumber == 0)
            GameManager.Instance.ChangeGameState(Enums.GameState.Win);

        int punchN = playerController.PlayerAnimationController.PunchNumber;
        _axisForce = punchN == 0 ? Vector3.left : Vector3.right;
        playerController.PlayerAnimationController.ActivePunchAnimation();
        playerController.PlayerLevelController.DowngradeItem();

        var particel = Instantiate(particelHit, transform.position + Vector3.up, Quaternion.identity);
        Destroy(particel.gameObject, 2);

        Invoke(nameof(ActiveRagdoll), 0.1f);
    }

    public void InteractWithCollision(PlayerController playerController)
    {

    }

    void ActiveRagdoll() 
    {
        for (int i = 0; i < _animators.Length; i++)
        {
            _animators[i].enabled = false;
        }
        activeRagdollSystem.ActiveRagdollForce(_axisForce, force);
    }
}
