using System;
using InControl;
using UnityEngine;

[RequireComponent( typeof(InControlInputModule) )]
public class InControlInputModuleSetter : MonoBehaviour
{
   public InControlInputModule inControlInputModule;
   
   private MyPlayerActions myPlayerActions;

   private void OnEnable()
   {
      myPlayerActions = MyPlayerActions.BindBoth();

      inControlInputModule.MoveAction = myPlayerActions.Move;
      inControlInputModule.SubmitAction = myPlayerActions.Submit;
      inControlInputModule.CancelAction = myPlayerActions.Cancel;
   }

   private void OnDisable()
   {
      myPlayerActions.Destroy();
   }
}
