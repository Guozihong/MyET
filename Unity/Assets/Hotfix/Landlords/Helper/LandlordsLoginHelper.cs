namespace ETHotfix
{
    public enum LoginType {
        LOGIN,
        REGIST
    }
    public static class LandlordsLoginHelper
    {
        public static async ETModel.ETVoid Login(string account, string password, LoginType loginType) {
            var netOuterComponent = ETModel.Game.Scene.GetComponent<ETModel.NetOuterComponent>();
            // model层session
            var modelSession = netOuterComponent.Create(ETModel.GlobalConfigComponent.Instance.GlobalProto.Address);
            //新建个ETHotfix层session，基于model层session进行通信
            var realmSession = ComponentFactory.Create<Session, ETModel.Session>(modelSession);
            R2C_LandlordsLogin r2CLogin = null;
            switch (loginType) {
                case LoginType.LOGIN:
                    r2CLogin = (R2C_LandlordsLogin)await realmSession.Call(new C2R_LandlordsLogin() { Account = account, Password = password });
                    break;
                case LoginType.REGIST:
                    r2CLogin = (R2C_LandlordsLogin)await realmSession.Call(new C2R_LandlordsRegist() { Account = account, Password = password });
                    break;
            }
            realmSession.Dispose();
            if (r2CLogin.Error != 0) {
                Log.Error(r2CLogin.Message);
                return;
            }

            // model层session
            var gateSession = netOuterComponent.Create(r2CLogin.Address);
            ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;

            // 在ETHotfix层保留一份方便后面使用
            Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);

            var g2CLoginGate = (G2C_LandlordsLoginGate)await SessionComponent.Instance.Session.Call(new C2G_LandlordsLoginGate() { Key = r2CLogin.Key });

            Log.Info("登陆成功！");

            var player = ETModel.ComponentFactory.CreateWithId<ETModel.Player>(g2CLoginGate.PlayerId);
            ETModel.PlayerComponent.Instance.MyPlayer = player;

            // Game.EventSystem.Run();
        }
    }
}
