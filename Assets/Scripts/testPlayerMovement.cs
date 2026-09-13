using UnityEngine;
using PurrNet;

public class testPlayerMovement : NetworkBehaviour
{

    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed = 5f;

    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        Debug.Log($"OnSpawned fired. isOwner={isOwner}, asServer={asServer}");
        if (isOwner) joystick = FindAnyObjectByType<Joystick>();
        Debug.Log($"Joystick assigned: {joystick != null}");
    }

    private void Update()
    {
        if(!isOwner || joystick == null) return;

        Vector3 move = new Vector3(joystick.Direction.x, joystick.Direction.y, 0f);
        transform.position += move * (moveSpeed * Time.deltaTime);
    }
}
