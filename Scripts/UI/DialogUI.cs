using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DialogUI : Singleton<DialogUI>
{
   public GameObject ButtonPref;

   public GameObject Panel;

   public TextMeshProUGUI Text;

   private Queue<GameObject> ButtonsPool = new();

   private Event NotEnoughtMoney = new();
   
   private Event Loose = new();

   private PlayerStats _playerStats => Game.Instance.PlayerStats;

   private void Start()
   {
      CreateNotEnoughtMoneyEvent();
      CreateLooseEvent();
   }

   public void OpenDialog(Event activeEvent)
   {
      Refresh();      
      Panel.SetActive(true);
      Text.text = activeEvent.Text;
      foreach (var choice in activeEvent.Choices)
      {
         var button = Instantiate(ButtonPref, Panel.transform);
         ButtonsPool.Enqueue(button);
         
         var actionButton = button.GetComponent<ActionButton>();
         actionButton.Init(choice.Name, () =>
         {
            choice.GetEvent()?.OnEventStart();
            choice.Action?.Invoke();
         });
      }
   }

   public void OpenNotEnoughtMoney() => 
      OpenDialog(NotEnoughtMoney);

   public void OpenLoose(string message)
   {
      Loose.Text = message;
      OpenDialog(Loose);
   }

   public void CloseDialog()
   {
      Panel.SetActive(false);
   }

   private void Refresh()
   {
      while (ButtonsPool.TryDequeue(out var button))
      {
         Destroy(button);
      }
   }

   private void CreateNotEnoughtMoneyEvent()
   {
      NotEnoughtMoney.Text = "Не хватает денег.";
      NotEnoughtMoney.Choices = new ();
      var unityEvent = new UnityEvent();
      unityEvent.AddListener(CloseDialog);
      NotEnoughtMoney.Choices.Add(new Choice("Ок", unityEvent));
   }
   
   private void CreateLooseEvent()
   {
      Loose.Choices = new ();
      var unityEvent = new UnityEvent();
      unityEvent.AddListener(Restart);
      Loose.Choices.Add(new Choice("Начать заново.", unityEvent));
   }

   private void Restart()
   {
      SceneManager.LoadScene("SampleScene");
   }
}
