using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroStatus
{
    public int m_HP;
    public int m_Damage;

    public HeroStatus(int in_hp, int in_Damage)
    {
        m_HP = in_hp;
        m_Damage = in_Damage;
    }
}
