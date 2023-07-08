namespace CommonComponents.Interfaces
{
	public enum GameState
	{
		Running,
		Paused,
		Loading,
		Cutscene
	}
	public delegate void GameStateChange (GameState newState);

    public interface IGameStateSubscriber
	{
		void OnStateChange(GameState newState);
	}
}