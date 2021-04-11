using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartsCounterUI : MonoBehaviour, PlayerUI.IInjectable
{
  public List<HeartUI> hearts;

  private PlayerHPModule hp;
  private int renderedCurrentHP;
  private int renderedMaxHP;

  private void OnValidate()
  {
    GetComponentsInChildren(hearts);
  }

  public void Inject(PlayerUI playerUI)
  {
    UpdateUnit(playerUI.SelectedUnitController);
  }

  public void UpdateUnit(PlayerUnitController unitController)
  {
    if (hp)
      hp.OnHpUpdate -= OnHpUpdate;

    hp = unitController.di.hp;
    hp.OnHpUpdate += OnHpUpdate;
    UpdateUI();
  }

  //private void Update()
  //{
  //  int currentHp = hp.CurrentHP;
  //  //if (currentHp != renderedCurrentHP) {
  //  UpdateUI();
  //  //}
  //}

  private void OnHpUpdate(PlayerHPModule.HpChange hpChange)
  {
    if (hpChange == PlayerHPModule.HpChange.Update)
      UpdateUI();
    else
      UpdateUIAfterDamage();
  }

  private void UpdateUIAfterDamage()
  {
    int currentHp = hp.CurrentHP;
    int maxHp = hp.MaxHP;
    int hpLost = renderedCurrentHP - currentHp;
    for (int i = 0; i < hpLost; i++)
    {
      int heartIndex = renderedCurrentHP - i - 1;
      HeartUI heartUI = hearts[heartIndex];
      heartUI.PlayLost();
    }
    int emptyHearts = renderedMaxHP - renderedCurrentHP;
    for (int i = 0; i < emptyHearts; i++)
    {
      int heartIndex = renderedMaxHP - i - 1;
      HeartUI heartUI = hearts[heartIndex];
      heartUI.PlayEmpty();
    }
    renderedCurrentHP = currentHp;
    renderedMaxHP = maxHp;
  }

  private void UpdateUI()
  {
    int heartsToRender = hp.MaxHP;
    int currentHp = hp.CurrentHP;
    bool isRegen = !hp.IsInCombatMode();
    for (int i = 0; i < hearts.Count; i++)
    {
      HeartUI heartUI = hearts[i];
      if (i < heartsToRender)
      {
        heartUI.Show();
        if (i < currentHp)
          heartUI.PlayFull();

        else if (isRegen)
          heartUI.PlayRegen();

        else
          heartUI.PlayEmpty();
      }
      else
        heartUI.Hide();
    }
    renderedCurrentHP = currentHp;
    renderedMaxHP = heartsToRender;
  }

  private void OnDisable()
  {
    if (hp)
      hp.OnHpUpdate -= OnHpUpdate;
  }
}
