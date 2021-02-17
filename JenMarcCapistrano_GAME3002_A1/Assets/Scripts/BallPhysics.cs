using UnityEngine.Assertions;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vTargetPos;
    [SerializeField]
    private Vector3 m_vInitialVel;
    // [SerializeField]
    // private bool m_bDebugKickBall = false;

    //UI ELEMENTS
    // Score
    private int m_iScore = 0;
    // Ball Left
    private int m_iBallLeft = 5;
    private UI m_interface = null;



    private Rigidbody m_rb = null;
    private GameObject m_TargetDisplay = null;
    private GameObject m_GoalDisplay = null;

    private bool m_bIsGrounded = true;
    [SerializeField]
    private  bool m_bIsKicked = false;

    private float m_fDistanceToTarget = 0.0f;
    
    
    private Vector3 vDebugHeading;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rb, "No RigidBody Attached");
        CreateLandingDisplay();
        CreateGoal();
        transform.position = new Vector3(0f, 1f, -8f);
        m_fDistanceToTarget = (m_TargetDisplay.transform.position - transform.position).magnitude;
        m_interface = GetComponent<UI>();
        if (m_interface != null)
        {
            m_interface.OnRequestUpdateUI(m_iScore, m_iBallLeft);
        }

       

    }

   

    // Update is called once per frame
    void Update()
    {
        if (m_bIsKicked&&m_bIsGrounded)
        {
            m_iBallLeft--;
            
            m_bIsKicked = false;
        }
        
       if (m_interface != null)
          {
                m_interface.OnRequestUpdateUI(m_iScore, m_iBallLeft);
        }
        

        if (transform.position.y <= 0.5f)
        {
            m_bIsGrounded = true;
          
           
        }

        if (m_TargetDisplay != null && m_bIsGrounded)
        {
            m_TargetDisplay.transform.position = m_vTargetPos;
            vDebugHeading = m_vTargetPos - transform.position;
            
        }



        //// Update target pos
        //if (m_bDebugKickBall)
        //{
        //    m_bDebugKickBall = false;
        //    OnKickBall();
        //}

        if (m_iBallLeft==0)
        {
            if (m_interface != null)
            {
                m_interface.OnRequestGameOverUI();
            }
            Time.timeScale = 0;
        }

        if (m_iScore >= 3)
        {
            if (m_interface != null)
            {
                m_interface.OnRequestWinUI();
            }
            Time.timeScale = 0;
        }


    }

    private void CreateLandingDisplay()
    {
        m_TargetDisplay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        m_TargetDisplay.transform.position = Vector3.zero;
        m_TargetDisplay.transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);
        m_TargetDisplay.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);


        m_TargetDisplay.GetComponent<Renderer>().material.color = Color.red;
        m_TargetDisplay.GetComponent<Collider>().enabled = false;
    }

    private void CreateGoal()
    {
        m_GoalDisplay = GameObject.CreatePrimitive(PrimitiveType.Cube);
        m_GoalDisplay.transform.position = new Vector3(0.0f, 5.0f, 46.0f);
        m_GoalDisplay.transform.localScale = new Vector3(18.0f, 8.0f, 1.0f);
        //m_GoalDisplay.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);


        m_GoalDisplay.GetComponent<Renderer>().enabled = false;
        m_GoalDisplay.GetComponent<Collider>().enabled = true;
        m_GoalDisplay.GetComponent<Collider>().isTrigger = true;
        m_GoalDisplay.name = "Goal";
    }


    public void OnKickBall()
    {
        // H = Vi^2 * sin^2(theta) / 2g
        // R = 2Vi^2 * cos(theta) * sin(theta) / g
        // Vi = sqrt(2gh) / sin(tan^-1(4h/R))
        // theta = tan^-1(4h/R)
        // Vy = V * sin(theta)
        // Vx = V * cos(theta)

        
        m_bIsGrounded = false;
        m_bIsKicked = true;
        


        // deltaD = Df-Di
        m_fDistanceToTarget = (m_TargetDisplay.transform.position - transform.position).magnitude;
        // Max Height
        float fMaxHeight = m_TargetDisplay.transform.position.y;
        // Range
        float fRange = m_fDistanceToTarget * 2;
        // Theta
        float fTheta = Mathf.Atan((4 * fMaxHeight) / (fRange));

        // Horizontal theta
        float fThetaX =  Mathf.Atan((m_TargetDisplay.transform.position.x-transform.position.x) /(m_fDistanceToTarget)) - (2 * Mathf.PI);

        // Initial Velocity Magnitude
        float fInitVelMag = Mathf.Sqrt((2 * Mathf.Abs(Physics.gravity.y) * fMaxHeight)) / Mathf.Sin(fTheta);

        // Initial velocity y-component
        m_vInitialVel.y = fInitVelMag * Mathf.Sin(fTheta);
        // Initial velocity z-component
         m_vInitialVel.z = fInitVelMag * Mathf.Cos(fTheta) * Mathf.Cos(fThetaX);
        // Initial velocity x-component
        m_vInitialVel.x = fInitVelMag * Mathf.Cos(fTheta) * Mathf.Sin(fThetaX);
      

        transform.rotation = CalculationTools.CalcUtils.UpdateProjectileFacingRotation(m_TargetDisplay.transform.position, transform.position);
        m_rb.velocity = m_vInitialVel;

        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Goal")
    //    {
    //        m_iScore++;
    //    }

    //}

    private void OnTriggerEnter(Collider other)
    {
        m_iScore++;
    }


    private void OnDrawGizmos()
    {

       
        if (m_bIsGrounded)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(0f, 1f, -8f), transform.position + vDebugHeading);
            
        }
       
        
    }

    #region INPUT_FUNCTIONS
    public void OnMoveForward(float fVal)
    {
        m_vTargetPos.z += fVal;
    }

    public void OnMoveBackward(float fVal)
    {
        m_vTargetPos.z -= fVal;
    }

    public void OnMoveRight(float fVal)
    {
        m_vTargetPos.x += fVal;
    }

    public void OnMoveLeft(float fVal)
    {
        m_vTargetPos.x -= fVal;
    }

    public void OnMoveUp(float fVal)
    {
        m_vTargetPos.y += fVal;
    }

    public void OnMoveDown(float fVal)
    {
        m_vTargetPos.y -= fVal;
    }

    #endregion


}

