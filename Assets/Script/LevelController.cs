using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject skewerPrefab;
    [SerializeField] GameObject circlePrefab;
    [SerializeField] Transform skewersContainer;
    [SerializeField] Transform circlesTopContainer;
    [SerializeField] CanvasGroup winGroup;

    List<Skewer> skewers = new List<Skewer>();

    public void Init(LevelData data)
    {
        winGroup.blocksRaycasts = false;
        winGroup.alpha = 0f;
        for (int i = 0; i < skewers.Count; i++)
        {
            skewers[i].Destroy();
        }
        skewers = new List<Skewer>();
        for (int i = 0; i < data.Skewers.Length; i++)
        {
            var skewer = Instantiate(skewerPrefab, skewersContainer).GetComponent<Skewer>();
            skewers.Add(skewer);
            skewer.Init(data.Skewers[i], circlesTopContainer, this);
        }
    }

    public void CalculateWin()
    {
        bool win = true;
        foreach (var skewer in skewers)
        {
            if (!skewer.Completed())
                win = false;
        }
        if (win)
        {
            winGroup.blocksRaycasts = true;
            winGroup.DOFade(1f, 0.2f).SetDelay(1f);
        }
    }
}
[System.Serializable]
public class LevelData
{
    public SkewerData[] Skewers;
}
[System.Serializable]
public class SkewerData
{
    public int[] Circles;
}
