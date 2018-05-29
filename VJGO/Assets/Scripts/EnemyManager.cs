using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyManager : TurnManager {

    EnemyMover m_enemyMover;
    EnemySensor m_enemySensor;
    EnemyAttack m_enemyAttack;

    ShootArrow m_skeletonArrow;

    Board m_board;
    bool m_isDead = false;
    public bool IsDead {  get { return m_isDead; } }

    public UnityEvent deathEvent;

    protected override void Awake()
    {
        base.Awake();

        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_enemyMover = GetComponent<EnemyMover>();
        m_enemySensor = GetComponent<EnemySensor>();
        m_enemyAttack = GetComponent<EnemyAttack>();
        m_skeletonArrow = GetComponent<ShootArrow>();


    }

    public void PlayTurn()
    {
        if (m_isDead)
        {
            FinishTurn();
            return;
        }

        switch (m_enemyMover.movementType)
        {
            case MovementType.Ranged:
                StartCoroutine(PlayRangedTurnRoutine());
                break;
            case MovementType.Stationary:
                StartCoroutine(PlayTurnRoutine());
                break;
            case MovementType.Spinner:
                StartCoroutine(PlayTurnRoutine());
                break;
            case MovementType.Patrol:
                StartCoroutine(PlayTurnRoutine());
                break;
        }
    }

    IEnumerator PlayTurnRoutine()
    {
        if (m_gameManager != null && !m_gameManager.IsGameOver)
        {
            m_enemySensor.UpdateSensor();

            yield return new WaitForSeconds(0f);
            if (m_enemySensor.FoundPlayer)
            {
                m_gameManager.LoseLevel();

                Vector3 playerPosition = new Vector3(m_board.PlayerNode.Coordinate.x, 0f, m_board.PlayerNode.Coordinate.y);
                m_enemyMover.Move(playerPosition, 0f);

                while (m_enemyMover.isMoving)
                {
                    yield return null;
                }
                m_enemyAttack.Attack();
                
            }
            else
            {
                m_enemyMover.MoveOneTurn();
            }
        }
    }

    IEnumerator PlayRangedTurnRoutine()
    {
        if (m_gameManager != null && !m_gameManager.IsGameOver)
        {
            m_enemySensor.UpdateSensor();

            yield return new WaitForSeconds(0f);
            if (m_enemySensor.FoundPlayer)
            {
                m_gameManager.LoseLevel();

                if (m_skeletonArrow != null)
                {
                    Vector3 playerPosition = new Vector3(m_board.PlayerNode.Coordinate.x, 0f, m_board.PlayerNode.Coordinate.y);

                    m_skeletonArrow.ShootArrowAt(playerPosition);
                }
                while (m_skeletonArrow.isMoving)
                {
                    yield return null;
                }
                m_enemyAttack.Attack();

            }
            else
            {
                m_enemyMover.MoveOneTurn();
            }
        }
    }

    public void Die()
    {
        if (m_isDead)
        {
            return;
        }
        m_isDead = true;

        if (deathEvent != null)
        {
            deathEvent.Invoke();
        }
    }
}
