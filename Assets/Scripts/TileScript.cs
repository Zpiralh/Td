﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{

    public Point GridPosition { get; private set; }

    public bool IsEmpty { get; private set; }

    private Color32 fullColor = new Color32(255, 0, 0, 100);

    private Color32 emptyColor = new Color32(0, 0, 255, 200);

    public SpriteRenderer SpriteRenderer { get; set; }

    public bool Debugging { get; set; }

    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

	// Use this for initialization
	void Start ()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
        ColorTile(fullColor);

        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (IsEmpty && !Debugging)
            {
                ColorTile(emptyColor);
            }
            if (!IsEmpty && !Debugging)
            {
                ColorTile(fullColor);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    private void OnMouseExit()
    {
        if (!Debugging)
        {
            ColorTile(Color.white);
        }
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {


        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        tower.transform.SetParent(transform);

        IsEmpty = false;

        ColorTile(Color.white);

        GameManager.Instance.BuyTower();
    }

    public void ColorTile(Color newColor)
    {
        SpriteRenderer.color = newColor;
    }
}
