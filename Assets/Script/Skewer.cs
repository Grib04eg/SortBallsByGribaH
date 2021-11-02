using com.adjust.sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skewer : MonoBehaviour
{
    [SerializeField] GameObject circlePrefab;
    public Transform bottomContainer;
    public Transform topContainer;

    [HideInInspector] Transform circlesTopContainer;
    LevelController controller;
    List<Circle> circles;
    public void Init(SkewerData data, Transform circlesTopContainer, LevelController controller)
    {
        this.controller = controller;
        this.circlesTopContainer = circlesTopContainer;
        circles = new List<Circle>();
        if (data.Circles != null)
        {
            for (int i = 0; i < data.Circles.Length; i++)
            {
                if (data.Circles.Length >= i + 1)
                {
                    CreateCircle(data.Circles[i]);
                }
            }
        }
    }

    public void CreateCircle(int id)
    {
        var circle = Instantiate(circlePrefab, bottomContainer).GetComponent<Circle>();
        circle.Init(topContainer, this, id, circles.Count);
        circles.Add(circle);
    }

    public void AttachCircle(Circle circle)
    {
        circle.position = circles.Count;
        circles.Add(circle);
        circle.skewer = this;
        circle.transform.SetParent(circlesTopContainer);
        circle.topHalf.SetParent(circlesTopContainer);
        circle.Move(transform.position + Vector3.up * (1.2f));
    }

    public void Destroy()
    {
        Destroy(gameObject);
        for (int i = 0; i < circles.Count; i++)
        {
            circles[i].Destroy();
        }
    }
    static Skewer clicked;
    public void OnClick()
    {
        if (clicked)
        {
            if (clicked != this && circles.Count < 4 && (circles.Count == 0 || circles[circles.Count - 1].id == clicked.circles[clicked.circles.Count - 1].id))
            {
                clicked.circles[clicked.circles.Count - 1].Swap(this);
                controller.CalculateWin();
                clicked = null;
            } 
            else
            {
                clicked.circles[clicked.circles.Count - 1].ShowDown();
                clicked = null;
            }
        }
        else if (circles.Count > 0)
        {
            circles[circles.Count - 1].ShowUp();
            clicked = this;
        }
    }

    internal void RemoveCircle(Circle circle)
    {
        circles.Remove(circle);
    }

    public bool Completed()
    {
        if (circles.Count == 0)
            return true;
        if (circles.Count == 4)
        {
            bool completed = true;
            int id = circles[0].id;
            for (int i = 1; i < 4; i++)
            {
                if (circles[i].id != id)
                    completed = false;
            }
            return completed;
        }
        return false;
    }
}
