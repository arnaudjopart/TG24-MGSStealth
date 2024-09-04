using UnityEngine;

public class Player : MonoBehaviour, IManageHealth
{
    private float m_health;

    public Player()
    {
        
    }
    public float GetHealth()
    {
        return m_health;
    }

    public void SetHealth(float _amount)
    {
        m_health = _amount;
    }
}
