using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        //Bir Harekete Başlandığında Önceki Hareketi Durdur
        public void StartAction(IAction action)
        {
            if (currentAction == action)
            {
                return;
            }
            if (currentAction != null)
            {
                currentAction.Cancel();
            } 
                currentAction = action;
            
        }
        public void CancelAction()
        {
            StartAction(null);
        }
    }
}
