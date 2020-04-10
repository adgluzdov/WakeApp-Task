using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public float speedMove = 5f;
    public float speedTurn = 10f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        var vertical = Joystick.Instance.Vertical;
        var horizontal = Joystick.Instance.Horizontal;
        var movement = Vector3.forward * vertical + Vector3.right * horizontal;
        characterController.SimpleMove(movement * speedMove);
        if (movement.magnitude > 0 && movement != Vector3.zero)
        {
            var direction = Quaternion.LookRotation(movement);
            characterController.transform.rotation = Quaternion.Lerp(characterController.transform.rotation, direction, Time.deltaTime * speedTurn);
        }
        else 
        {
            if (PlayerManager.Instance.targetEnemy != null) {
                var direction = Quaternion.LookRotation(PlayerManager.Instance.targetEnemy.transform.position - characterController.transform.position);
                characterController.transform.rotation = Quaternion.Lerp(characterController.transform.rotation, direction, Time.deltaTime * speedTurn);
            }
        }
    }
}
