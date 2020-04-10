using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovementController : MonoBehaviour
{
    public Transform[] points;
    
    public float speedMove = 5f;
    public float speedTurn = 10f;
    public float stoppingAngle = 0.1f;
    public float stoppingDistance = 0.1f;

    private Vector3 targetPoint;
    private Vector3 targetCorner;
    private int targetPointIndex;
    private int targetCornerIndex;

    private CharacterController characterController;
    private NavMeshPath path;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Простраиваем путь к первой Point
        targetPointIndex = 0;
        targetPoint = points[targetPointIndex].position;
        path = new NavMeshPath();
        NavMesh.CalculatePath(characterController.transform.position, targetPoint, NavMesh.AllAreas, path);

        // Фиксируем первый угол из NavMeshPath как целевой, чтобы двигаться к нему в Update
        targetCornerIndex = 0;
        targetCorner = path.corners[targetCornerIndex];
    }

    void Update()
    {
        // Игнорируем координату y, чтобы двигаться вдоль XOZ
        var targetPointWithoutZ = new Vector3(this.targetPoint.x, 0, this.targetPoint.z);
        var targetCornerWithoutZ = new Vector3(this.targetCorner.x, 0, this.targetCorner.z);
        var enemyWithoutZ = new Vector3(characterController.transform.position.x, 0, characterController.transform.position.z);

        // Проверяем смотрит ли Enemy на Point
        Quaternion direction = Quaternion.LookRotation(targetPointWithoutZ - enemyWithoutZ);
        if (Quaternion.Angle(characterController.transform.rotation, direction) <= stoppingAngle)
        {
            // Проверяем находится ли Enemy вблизи целевого Corner
            if (Vector3.Distance(enemyWithoutZ, targetCornerWithoutZ) <= stoppingDistance)
            {
                // Проверяем является ли целевой Corner последним в NavMeshPath
                if (targetCornerIndex == path.corners.Length - 1)
                {
                    // Простраиваем путь к следующей Point
                    targetPointIndex = (targetPointIndex + 1) % points.Length;
                    targetPoint = points[targetPointIndex].position;
                    NavMesh.CalculatePath(characterController.transform.position, targetPoint, NavMesh.AllAreas, path);

                    // Фиксируем первый Corner из NavMeshPath как целевой, чтобы двигаться к нему
                    targetCornerIndex = 0;
                    this.targetCorner = path.corners[targetCornerIndex];
                }
                else 
                {
                    // Фиксируем соедующий Corner из NavMeshPath как целевой, чтобы двигаться к нему
                    targetCornerIndex++;
                    this.targetCorner = path.corners[targetCornerIndex];
                }
            }
            else 
            {
                // Передвигаем Enemy к corner
                var movement = (targetCornerWithoutZ - enemyWithoutZ).normalized;
                characterController.SimpleMove(movement * speedMove);
            }
        }
        else 
        { 
            // Поворачиваем Enemy, чтобы смотрел на point
            characterController.transform.rotation = Quaternion.Lerp(characterController.transform.rotation, direction, Time.deltaTime * speedTurn);
        }

    }

}
