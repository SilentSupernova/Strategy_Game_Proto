﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reboot
{
    public class WorldUI : MonoBehaviour
    {
        [SerializeField] private GameManager m_GameManager;
        [SerializeField] private Renderer m_TileMarker;

        [SerializeField] private Color m_DefaultColor;
        [SerializeField] private Color m_MoveColor;
        [SerializeField] private Color m_AttackColor;

        Dictionary<Tile, Renderer> m_TileMarkers;

        private void Start()
        {
            m_GameManager.OnGameBegin += InitMarkers;
        }

        private void InitMarkers()
        {
            m_TileMarkers = new Dictionary<Tile, Renderer>();

            foreach(Tile tile in m_GameManager.Tiles)
            {
                Renderer tileMarker = Instantiate(m_TileMarker);
                tileMarker.transform.parent = this.transform;
                tileMarker.transform.position = tile.transform.position + (Vector3.back * 0.00001f);
                m_TileMarkers.Add(tile, tileMarker);
            }
        }

        private void OnEnable()
        {
            m_GameManager.OnPlayerUpdateTiles += OnTilesUpdated;
        }

        private void OnDisable()
        {
            m_GameManager.OnPlayerUpdateTiles += OnTilesUpdated;
        }

        private void OnTilesUpdated()
        {
            foreach(Tile tile in m_GameManager.Tiles)
            {
                m_TileMarkers[tile].material.color = m_DefaultColor;
                Vector3 tilePos = tile.transform.position;
                tilePos.z -= 0.0001f;
                m_TileMarkers[tile].transform.position = tilePos;
            }
            switch (m_GameManager.PlayerWithTurn.CurrentOrder)
            {
                case OrderType.NONE:
                break;
                case OrderType.MOVE:
                foreach (Tile tile in m_GameManager.PlayerWithTurn.MoveableTiles)
                {
                    m_TileMarkers[tile].material.color = m_MoveColor;
                    Vector3 tilePos = tile.transform.position;
                    tilePos.z -= 0.0002f;
                    m_TileMarkers[tile].transform.position = tilePos;
                }
                break;
                case OrderType.ATTACK:
                foreach (Tile tile in m_GameManager.PlayerWithTurn.AttackableTiles)
                {
                    m_TileMarkers[tile].material.color = m_AttackColor;
                    Vector3 tilePos = tile.transform.position;
                    tilePos.z -= 0.0002f;
                    m_TileMarkers[tile].transform.position = tilePos;
                }
                break;
            }
        }
    }
}