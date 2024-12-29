using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Ui
{
	public class EndScreenController : UiController
    {
        private const string _BUTTON_CONTINUE_NAME = "ButtonContinue";

        private Button _btnContinue;


        protected override void AssignElements()
        {
            _btnContinue = _root.Q<Button>(_BUTTON_CONTINUE_NAME);
        }

        protected override void SubscribeToEvents()
        {
            _btnContinue.clicked += OnButtonContinueClick;
        }

        protected override void UnsubscribeToEvents()
        {
            _btnContinue.clicked -= OnButtonContinueClick;
        }

        private void OnButtonContinueClick()
        {
            Debug.Log("test");
        }
    }
}