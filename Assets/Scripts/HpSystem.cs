using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem
{
    public int m_HP = 5;

    public int CalcHP(int hp)
    {
        m_HP += hp;

        return m_HP;
    }
}
