using UnityEngine.Assertions;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    private ProjectileComponent m_projectile = null;


    // Start is called before the first frame update
    void Start()
    {
        m_projectile = GetComponent<ProjectileComponent>();
        Assert.IsNotNull(m_projectile, "No ProjectileComponent Attached");
    }

    // Update is called once per frame
    void Update()
    {
        HandleUserInput();
    }
    private void HandleUserInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Launch
            m_projectile.OnLaunchProjectile();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // MoveForward
            m_projectile.OnMoveForward(0.1f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // MoveBackward
            m_projectile.OnMoveBackward(0.1f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // MoveRight
            m_projectile.OnMoveRight(0.1f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // MoveLeft
            m_projectile.OnMoveLeft(0.1f);
        }


    }
}
