using UnityEngine;
using System.Runtime.InteropServices;

public class PianoSoundManager : IPianoSound {
	[DllImport("fluidsynth")]
	static extern void initialize();

	[DllImport("fluidsynth")]
	static extern void loadSF2(string sf2Path);

	[DllImport("fluidsynth")]
	static extern void noteOn(int noteNumber, int velocity);

	[DllImport("fluidsynth")]
	static extern void noteOff(int noteNumber);

	[DllImport("fluidsynth")]
	static extern void allNotesOff();

	[DllImport("fluidsynth")]
	static extern void cleanup();


	public PianoSoundManager() { }

	public void Initialize() {
		initialize();
	}

	public void PlayNote(int noteNumber, int velocity) {
		noteOn(noteNumber, velocity);
	}

	public void StopNote(int noteNumber) {
		noteOff(noteNumber);
	}

	public void LoadSoundResource(string resourcePath) {
		loadSF2(resourcePath);
	}

	public void StopAllNotes() {		
		allNotesOff();
	}

	public void Cleanup() {		
		cleanup();
	}
}
