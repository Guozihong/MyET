namespace ETModel
{
	[ObjectSystem]
	public class UserSystem : AwakeSystem<User, long>
	{
		public override void Awake(User self, long a)
		{
			self.Awake(a);
		}
	}

	public sealed class User : Entity
	{
		public long UserId { get; private set; }
		
		public long MapActorId { get; set; }

		public long GateAppId { get; set; }

        public bool IsMatching { get; set; }

		public void Awake(long UserId)
		{
			this.UserId = UserId;
		}
		
		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();

            MapActorId = 0;
            GateAppId = 0;
            IsMatching = false;
		}
	}
}