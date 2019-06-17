using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class G2R_LandloresOfflineHandler : AMRpcHandler<G2R_LandlordsOffline, EmptyResponse>
    {
        protected override void Run(Session session, G2R_LandlordsOffline message, Action<EmptyResponse> reply)
        {
            RunAsync(session, message, reply).Coroutine();
        }

        private async ETVoid RunAsync(Session session, G2R_LandlordsOffline message, Action<EmptyResponse> reply) {
            EmptyResponse respone = new EmptyResponse();
            try
            {
                Game.Scene.GetComponent<OnlineComponent>().Remove(message.UserId);

                reply(respone);
            }
            catch (Exception e) {
                ReplyError(respone, e, reply);
            }
        }
    }
}
