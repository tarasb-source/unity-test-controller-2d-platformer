using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    private Controller controller; 
    private Collider2D collider2d;
    private bool playerOnPlatform;


    private void Start()
    {
        collider2d = GetComponent<Collider2D>();
        controller = GameObject.FindWithTag("Player").GetComponent<Controller>();
    }

    private void Update()
    {
        if (playerOnPlatform && controller != null)
        {
            if (controller._moveInput.y < 0 && controller.jumpWasPressed)
            {
                collider2d.enabled = false;
                StartCoroutine(EnableCollider());
            }
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider2d.enabled = true;
    }

    private void setPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<Controller>();
        if (player != null)
        {
            playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        setPlayerOnPlatform(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        setPlayerOnPlatform(collision, false);
    }

}
