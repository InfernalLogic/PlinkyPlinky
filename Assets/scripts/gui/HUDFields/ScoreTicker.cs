﻿using UnityEngine;
using System;
using System.Collections;
using System.Globalization;

public class ScoreTicker : HUDField
{
  [SerializeField]
  private GUIStyle label_style;
  [SerializeField]
  private ScalingRect money_display_rect;
  [SerializeField]
  private SavedBool show_exponents;

  private TextAnchor original_alignment;
  private string formatter = "G";

  void Awake()
  {
    ResizeText();
    original_alignment = label_style.alignment;
    if (show_exponents.IsTrue())
      formatter = "0.000E+0";
  }

  void OnEnable()
  {
    HUDEvents.OnScreenResize += ResizeText;
  }

  protected override void DisplayGUIElements()
  {
    DisplayBackgroundBox();
    DisplayCurrentMoney();
  }

  void DisplayBackgroundBox()
  {
    GUI.Box(new Rect(0, 0, display_rect.GetRect().width, display_rect.GetRect().height), "", background_style);
  }

  void DisplayCurrentMoney()
  {
    GUI.Label(money_display_rect.GetRect(), "$: ", label_style);

    string money = MoneyTracker.Instance.GetCurrentMoney().ToString(formatter);

    label_style.alignment = TextAnchor.MiddleRight;
    GUI.Label(money_display_rect.GetRect(), money, label_style);
    label_style.alignment = original_alignment;
  }

  public void ResizeText()
  {
    label_style.fontSize = (int)Screen.height / 15;
  }

  public void ToggleExponents()
  {
    if (formatter.Length < 2)
    {
      show_exponents.Set(true);
      formatter = "0.000E+0";
    }
    else
    {
      show_exponents.Set(false);
      formatter = "G";
    }
  }
}