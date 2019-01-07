﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class JoyStick : ScrollRect
{

    public TankMovement m_movement;
    // 半径
    public float _mRadius = 1f;

    // 距离
    public  float Dis = 1.5f;

    public void SetPlayerMovement(GameObject player)
    {
        Debug.Log("current option : "+player.name);

        m_movement = player.GetComponent<TankMovement>();
    }

    protected override void Start()
    {
        base.Start();

        // 能移动的半径 = 摇杆的宽 * Dis
        _mRadius = content.sizeDelta.x * Dis;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        // 获取摇杆，根据锚点的位置。
        var contentPosition = content.anchoredPosition;

        // 判断摇杆的位置 是否大于 半径
        if (contentPosition.magnitude > _mRadius)
        {
            // 设置摇杆最远的位置
            contentPosition = contentPosition.normalized * _mRadius;
            SetContentAnchoredPosition(contentPosition);
        }

        // 最后 v2.x/y 就跟 Input中的 Horizontal Vertical 获取的值一样 
        var v2 = content.anchoredPosition.normalized;

        m_movement.m_MovementInputValue = v2.y;
        m_movement.m_TurnInputValue = v2.x;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        m_movement.m_MovementInputValue = 0;
        m_movement.m_TurnInputValue = 0;
        SetContentAnchoredPosition(Vector2.zero);
    }
}
