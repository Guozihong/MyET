using ETModel;

namespace ETHotfix {
    public static class OnlineComponentSystem {
        public static async ETVoid KickPlayer(this OnlineComponent self, long userId) {
            long gateAppId = Game.Scene.GetComponent<OnlineComponent>().Get(userId);
            if (gateAppId != 0) {
                // 方式一 通过ActorLocationSenderComponent
                // var actorMessageSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(userId);
                // await actorMessageSender.Call(new Actor_LandlordsKickPlayer() { UserId = userId });

                // 方式二 通过gateAppId获取对应app地址
                var ipEndPoint = StartConfigComponent.Instance.GetInnerAddress(IdGenerater.GetAppId(gateAppId));
                var gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(ipEndPoint);
                await gateSession.Call(new R2G_LandlordsKickPlayer() { UserId = userId });
            } 
        }
    }

}