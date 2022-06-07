using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem
{
    // 나의 HP
    public int m_HP = 5;

    // HP 계산 후 반환
    public int CalcHP(int hp)
    {
        m_HP += hp;

        return m_HP;
    }
}
