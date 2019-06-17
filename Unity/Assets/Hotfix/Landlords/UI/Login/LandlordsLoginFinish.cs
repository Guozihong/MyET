using ETModel;
using System;
using UnityEngine;

namespace ETHotfix
{
    [Event(EventIdType.LandlordsLoginFinish)]
    public class LandlordsLoginFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.LandlordsLogin);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.LandlordsLogin.StringToAB());
        }
    }
}
