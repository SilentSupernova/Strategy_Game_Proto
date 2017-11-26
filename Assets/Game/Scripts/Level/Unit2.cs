﻿using Navigation;
using System;
using UnityEngine;

public class Unit2 : MonoBehaviour
{
    [SerializeField] private Agent m_Agent;

    public Agent Agent
    {
        get
        {
            return m_Agent;
        }
    }

    public void Select()
    {
        Game_Manager.Instance.SelectUnit(this);
    }

    public void MoveTo(AStarNode targetNavNode, Action callback)
    {
        m_Agent.MoveTo(targetNavNode, callback);
    }
}