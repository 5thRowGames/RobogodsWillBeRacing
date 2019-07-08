//This class is auto-generated by the Atto framework. You shouldn't edit it.


using UnityEngine;

 public static partial class Core
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void BindCommonServices()
	{
		Atto.Bind<ISettingsService, AttoSettingsProvider>();
		if (Settings.Current.autoBindCommonServices)
{
			Atto.Bind<IDatabaseService,BinaryDatabaseProvider>();
			Atto.Bind<IDataChannelService,UriDataChannelProvider>();
			Atto.Bind<IEventService,SimpleEventProvider>();
			Atto.Bind<IInputService,SimpleInputProvider>();
			Atto.Bind<ILogService,UnityConsoleLogProvider>();
			Atto.Bind<ISerializationService,JsonSerializationProvider>();
			Atto.Bind<ISettingsService,AttoSettingsProvider>();
			Atto.Bind<ISoundService,SimpleSoundProvider>();
			Atto.Bind<IStorageService,FileStorageProvider>();
}
	}

	public static IDatabaseService Database { get { if (Database_ == null) {Database_ = Atto.Get<IDatabaseService>();} return Database_ ; } }
	static IDatabaseService Database_ ;

/*This service is hidden from API use, use Atto.Get<IService>() to access it from other services.
	public static IDataChannelService DataChannel { get { if (DataChannel_ == null) {DataChannel_ = Atto.Get<IDataChannelService>();} return DataChannel_ ; } }
	static IDataChannelService DataChannel_ ;
*/

	public static IEventService Event { get { if (Event_ == null) {Event_ = Atto.Get<IEventService>();} return Event_ ; } }
	static IEventService Event_ ;

	public static IInputService Input { get { if (Input_ == null) {Input_ = Atto.Get<IInputService>();} return Input_ ; } }
	static IInputService Input_ ;

	public static ILogService Log { get { if (Log_ == null) {Log_ = Atto.Get<ILogService>();} return Log_ ; } }
	static ILogService Log_ ;

	public static ISceneService Scene { get { if (Scene_ == null) {Scene_ = Atto.Get<ISceneService>();} return Scene_ ; } }
	static ISceneService Scene_ ;

	public static ISerializationService Serialization { get { if (Serialization_ == null) {Serialization_ = Atto.Get<ISerializationService>();} return Serialization_ ; } }
	static ISerializationService Serialization_ ;

	public static ISettingsService Settings { get { if (Settings_ == null) {Settings_ = Atto.Get<ISettingsService>();} return Settings_ ; } }
	static ISettingsService Settings_ ;

	public static ISoundService Sound { get { if (Sound_ == null) {Sound_ = Atto.Get<ISoundService>();} return Sound_ ; } }
	static ISoundService Sound_ ;

	public static IStorageService Storage { get { if (Storage_ == null) {Storage_ = Atto.Get<IStorageService>();} return Storage_ ; } }
	static IStorageService Storage_ ;

}