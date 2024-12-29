using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Ui
{
	[RequireComponent(typeof(UIDocument))]
	public abstract class UiController : MonoBehaviour
	{
		protected VisualElement _root;

		virtual protected void Awake()
		{
			_root = GetComponent<UIDocument>().rootVisualElement;
			AssignElements();
			SubscribeToEvents();
		}
		protected abstract void AssignElements();
		protected abstract void SubscribeToEvents();
		protected abstract void UnsubscribeToEvents();

		public void DisplayScreen()
		{
			_root.style.display = DisplayStyle.Flex;
		}
		public void HideScreen()
		{
			_root.style.display = DisplayStyle.None;
		}
	}
}