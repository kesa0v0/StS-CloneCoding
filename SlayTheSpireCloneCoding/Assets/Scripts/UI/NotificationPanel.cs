using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class NotificationPanel : MonoBehaviour
{
    [SerializeField] TMP_Text notificationTMP;

    public void Show(string message)
    {
        notificationTMP.text = message;
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad)) // Notificaiton 창이 커졌다가
            .AppendInterval(0.9f) // 0.9초 기다리고
            .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad)); // Notification 창이 작아지기
    }

    void Start() => ScaleZero(); // 시작시 없애기

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;

    [ContextMenu("ScaleZero")]
    public void ScaleZero() => transform.localScale = Vector3.zero;
}
