using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class R2G_LandlordsKickPlayerHandler : AMRpcHandler<R2G_LandlordsKickPlayer, EmptyResponse>
    {
        protected override void Run(Session session, R2G_LandlordsKickPlayer message, Action<EmptyResponse> reply)
        {
            RunAsync(session, message, reply).Coroutine();
        }

        private async ETVoid RunAsync(Session session, R2G_LandlordsKickPlayer message, Action<EmptyResponse> reply) {
            var respone = new EmptyResponse();
            try
            {
                User user = Game.Scene.GetComponent<UserComponent>().Get(message.UserId);
                Game.Scene.GetComponent<NetOuterComponent>().Remove(user.GateAppId);

                reply(respone);
            }
            catch (Exception e) {
                ReplyError(respone, e, reply);
            }
        }
    }
}
