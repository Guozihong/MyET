using ETModel;

namespace ETHotfix {
    [ObjectSystem]
    public class SessionUserComponentSystem : DestroySystem<SessionUserComponent>
    {
        /// <summary>
        /// session销毁时会触发，因为SessionUserComponent挂载在session上
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public override async void Destroy(SessionUserComponent self)
        {
            try {
                Game.Scene.GetComponent<UserComponent>().Remove(self.User.UserId);

                var realmInnerIpEndPoint = StartConfigComponent.Instance.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
                var realSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmInnerIpEndPoint);
                await realSession.Call(new G2R_LandlordsOffline() { UserId = self.User.UserId });

                self.User.Dispose();
                self.User = null;

            } catch (System.Exception e) {
                Log.Trace(e.ToString());
            }
        }
    }

}