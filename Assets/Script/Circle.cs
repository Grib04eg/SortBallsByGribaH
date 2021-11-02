using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    [SerializeField] GameObject topHalfPrefab;
    [SerializeField] GameObject trailPrefab;
    [SerializeField] Color[] spriteColors;
    public RectTransform topHalf;
    public Skewer skewer;
    public int id;
    public int position;
    TrailRenderer trail;

    public void Init(Transform topCirclesContainer, Skewer skewer, int id, int position)
    {
        this.skewer = skewer;
        this.id = id;
        this.position = position;
        trail = Instantiate(trailPrefab, transform).GetComponent<TrailRenderer>();
        trail.startColor = spriteColors[id];
        Color color = new Color(spriteColors[id].r, spriteColors[id].g, spriteColors[id].b, 0f);
        trail.endColor = color;
        GetComponent<Image>().color = spriteColors[id];
        topHalf = Instantiate(topHalfPrefab, topCirclesContainer).GetComponent<RectTransform>();
        topHalf.GetComponent<Image>().color = spriteColors[id];
        GetComponent<RectTransform>().localPosition = Vector3.up * (95f * position - 135f);
        topHalf.position = transform.position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
        Destroy(topHalf.gameObject);
    }

    private void Update()
    {
        topHalf.position = transform.position;
    }

    public void Swap(Skewer skewer)
    {
        this.skewer.RemoveCircle(this);
        skewer.AttachCircle(this);

    }

    public void Move(Vector3 position)
    {
        trail.sortingOrder = 2;
        GetComponent<RectTransform>().DOKill(true);
        transform.DOKill(true);
        var dotween = transform.DOMove(position, 0.3f).SetEase(Ease.InOutCirc);
        dotween.onUpdate += () => { topHalf.position = transform.position; };
        dotween.onComplete += () => { ShowDown(); trail.sortingOrder = 0; };
    }

    public void ShowUp()
    {
        transform.DOKill(true);
        var pos = transform.parent.position + Vector3.up * (1.2f);
        transform.DOMove(pos, 0.3f).SetEase(Ease.InOutCirc).onUpdate += () => { topHalf.position = transform.position; };
    }

    public void ShowDown()
    {
        transform.DOKill(true);
        transform.parent = skewer.bottomContainer;
        topHalf.parent = skewer.topContainer;
        GetComponent<RectTransform>().DOLocalMove(Vector3.up * (95f * position - 135f), 0.3f).SetEase(Ease.InOutCirc).onUpdate += () => { topHalf.position = transform.position; };
    }
}
