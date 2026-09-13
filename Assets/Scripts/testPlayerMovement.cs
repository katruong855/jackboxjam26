using UnityEngine;
using PurrNet;

public class testPlayerMovement : NetworkBehaviour
{

    [SerializeField] private Joystick joystick;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float maxVelocity = 5f;

    private NetworkRigidbody2D rb;

    private static readonly Color[] playerColors =
    {
        Color.red, Color.green, Color.blue, Color.yellow
    };

    private void Awake()
    {
        rb = this.GetComponent<NetworkRigidbody2D>();
        // Debug.Log(rb != null);
    }

    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        // Debug.Log($"OnSpawned fired. isOwner={isOwner}, asServer={asServer}");
        if (isOwner) joystick = FindAnyObjectByType<Joystick>();    // assign joystick (player) to this player prefab
        // Debug.Log($"Joystick assigned: {joystick != null}");

        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.color = playerColors[(int)owner.Value.id.value % playerColors.Length];
    }

    private void FixedUpdate()
    {
        if(!isOwner || joystick == null) return;    // makes sure there is a joystick (player) linked to this prefab player object

        Vector2 input = joystick.Direction;
        rb.AddForce(input * acceleration, ForceMode2D.Force);

        if (rb.linearVelocity.magnitude > maxVelocity) rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity; // clamp max velocity
    }
}
