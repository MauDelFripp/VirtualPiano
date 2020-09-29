public interface IPianoSound {
    
	void Initialize();
	void PlayNote(int noteNumber, int velocity);
	void StopNote(int noteNumber);
	void LoadSoundResource(string resourcePath);
	void StopAllNotes();
	void Cleanup();
}