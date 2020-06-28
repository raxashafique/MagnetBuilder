namespace MagnetBuilder.Core.StateMachine
{
	public interface IState
	{
		void Update();

		void OnEnter();

		void OnExit();
	}
}