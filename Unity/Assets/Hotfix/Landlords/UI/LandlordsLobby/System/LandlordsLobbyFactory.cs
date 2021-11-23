using ETModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
	public class LandlordsLobbyFactory
	{
		public static UI Create()
		{
			try
			{
				Debug.LogError("--LandlordsLobbyFactory--Create--");

				ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
				resourcesComponent.LoadBundle(UIType.LandlordsLobby.StringToAB());
				GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.LandlordsLobby.StringToAB(), UIType.LandlordsLobby);
				GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

				UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.LandlordsLobby, gameObject, false);

				ui.AddComponent<LandlordsLoginComponent>();
				return ui;
			}
			catch (Exception e)
			{
				Log.Error(e);
				return null;
			}
		}
	}
}