using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Gate)]
    public class ActorLocation_LandlordsKickPlayerHandler : AMActorLocationRpcHandler<User, Actor_LandlordsKickPlayer, ActorLocation_EmptyResponse>
    {
        protected override async ETTask Run(User user, Actor_LandlordsKickPlayer message, Action<ActorLocation_EmptyResponse> reply)
        {
            RunAsync(user, message, reply).Coroutine();
        }

        private async ETVoid RunAsync(User user, Actor_LandlordsKickPlayer message, Action<ActorLocation_EmptyResponse> reply) {
            var respone = new ActorLocation_EmptyResponse();
            try
            {
                Game.Scene.GetComponent<NetOuterComponent>().Remove(user.GateAppId);

                reply(respone);
            }
            catch (Exception e) {
                ReplyError(respone, e, reply);
            }
        }
    }
}
