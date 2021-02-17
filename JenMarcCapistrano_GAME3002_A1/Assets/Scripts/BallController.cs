using UnityEngine.Assertions;
using UnityEngine;

public class BallController : MonoBehaviour
{


  

    private BallPhysics m_ball = null;
   

  

    // Start is called before the first frame update
    void Start()
    {
        m_ball = GetComponent<BallPhysics>();
        Assert.IsNotNull(m_ball, "No BallPhysics Attached!");

       
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
            
            m_ball.transform.position = new Vector3(0f, 1f, -8f);
            m_ball.OnKickBall();
            
        }

        if (Input.GetKey(KeyCode.R))
        {
            // MoveForward
            m_ball.OnMoveForward(0.1f);
    
        }

        if (Input.GetKey(KeyCode.F))
        {
            // MoveBackward
            m_ball.OnMoveBackward(0.1f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // MoveRight
            m_ball.OnMoveRight(0.1f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // MoveLeft
            m_ball.OnMoveLeft(0.1f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            // MoveUp
            m_ball.OnMoveUp(0.1f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            // MoveDown
            m_ball.OnMoveDown(0.1f);
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            //Quit
            Application.Quit();
        }

    }


   
}
