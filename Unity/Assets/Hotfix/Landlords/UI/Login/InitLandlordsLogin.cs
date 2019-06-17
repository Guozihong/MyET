using ETModel;
using System;
using UnityEngine;

namespace ETHotfix
{
    [Event(EventIdType.InitLandlordsLogin)]
    public class InitLandlordsLogin : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Add(this.Create());
        }

        private UI Create()
        {
            UI loginUI = null;
            try {
                var resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.LandlordsLogin.StringToAB());
                var bundleGameObject = resourcesComponent.GetAsset(UIType.LandlordsLogin.StringToAB(), UIType.LandlordsLogin) as GameObject;
                var gameObject = GameObject.Instantiate(bundleGameObject);
                loginUI = ComponentFactory.Create<UI, string, GameObject>(UIType.LandlordsLogin, gameObject, false);
                loginUI.AddComponent<LoginComponent>();
            } catch (Exception e) {
                Log.Error(e);
            }

            return loginUI;
        }
    }
}
