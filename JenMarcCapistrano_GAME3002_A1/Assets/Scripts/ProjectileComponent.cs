using UnityEngine.Assertions;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vInitialVelocity = Vector3.zero;

    private Rigidbody m_rb = null;
    private GameObject m_landingDisplay = null;

    private bool m_bIsGrounded = true;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rb, "No RigidBody Attached");

        CreateLandingDisplay();
    }

    private void Update()
    {
        UpdateLandingPosition();
    }

    private void CreateLandingDisplay()
    {
        m_landingDisplay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        m_landingDisplay.transform.position = Vector3.zero;
        m_landingDisplay.transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);

        m_landingDisplay.GetComponent<Renderer>().material.color = Color.blue;
        m_landingDisplay.GetComponent<Collider>().enabled = false;
    }

    public void OnLaunchProjectile()
    {
        if (!m_bIsGrounded)
        {
            return;
        }

        m_landingDisplay.transform.position = GetLandingPosition();
        m_bIsGrounded = false;

        transform.rotation = CalculationTools.CalcUtils.UpdateProjectileFacingRotation(m_landingDisplay.transform.position, transform.position);
        m_rb.velocity = m_vInitialVelocity;

    }

    private void UpdateLandingPosition()
    {
        if (m_landingDisplay != null && m_bIsGrounded)
        {
            m_landingDisplay.transform.position = GetLandingPosition();
        }
    }

    private Vector3 GetLandingPosition()
    {
        // t = 2 * (vf - vi) / g)
        float fTime = 2.0f * (0.0f - m_vInitialVelocity.y) / Physics.gravity.y;

        Vector3 vFlatVel = m_vInitialVelocity;
        vFlatVel.y = 0.0f;
        vFlatVel *= fTime;


        return transform.position + vFlatVel;
    }

    #region INPUT_FUNCTIONS
    public void OnMoveForward(float fVal) 
    {
        m_vInitialVelocity.z += fVal;
    }

    public void OnMoveBackward(float fVal) 
    {
        m_vInitialVelocity.z -= fVal;
    }

    public void OnMoveRight(float fVal) 
    {
        m_vInitialVelocity.x += fVal;
    }

    public void OnMoveLeft(float fVal)
    {
        m_vInitialVelocity.x -= fVal;
    }

    #endregion
}
