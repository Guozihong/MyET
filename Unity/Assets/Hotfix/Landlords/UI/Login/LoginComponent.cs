using UnityEngine;
using UnityEngine.UI;
using ETModel;
using System;

namespace ETHotfix {
    [ObjectSystem]
    public class LoginComponentSystem : AwakeSystem<LoginComponent>
    {
        public override void Awake(LoginComponent self)
        {
            self.Awake();
        }
    }

    public class LoginComponent : Component 
    {
        private InputField password, account;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<Component>().GameObject.GetComponent<ReferenceCollector>();
            this.account = rc.Get<GameObject>("Account").GetComponent<InputField>();
            this.password = rc.Get<GameObject>("Password").GetComponent<InputField>();
            rc.Get<GameObject>("LoginButton").GetComponent<Button>().onClick.Add(OnLogin);
            rc.Get<GameObject>("RegistButton").GetComponent<Button>().onClick.Add(OnRegist);
        }

        private void OnRegist()
        {
            LandlordsLoginHelper.Login(this.account.text, this.password.text, LoginType.REGIST);
        }

        private void OnLogin() {
            LandlordsLoginHelper.Login(this.account.text, this.password.text, LoginType.LOGIN);
        }
    }
}