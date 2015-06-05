Moviestorm Companion.
---------------------

Version 0.2.9

This little tool is intended to provide Moviestorm users with some convenient functionalities. 

Currently it's in a very early state of development and there are only a few functions implemented:

* It provides information about:
	1. Addons and packages installed and their status (enabled/disabled)
	2. Movies in the Moviestorm user folder, including:
		- Name of the movies
		- For the currently selected movie:
			- List of addons used/required
			- List of props used, with info about the number of instances and the package it belongs to.
				The props shown can be filtered by addon (on the addons list, select an addon and right-click and click 'Filter Used Props')
			- List of media files (sound and video clips, dialogs, images).

* It can set enabled only those addons used in the movie selected by the user. This function can be of any use when working on and/or trying to render a movie especially long/complex. The original list of enabled addons can be restored, provided the toolbox application is not closed. Anyway, it creates a backup of the properties file every time this function is used.

* It scans the directories under the selected movie folder in search of media files and can delete those not actually used in the movie. This can be of use if you edit/recreate the TTS dialogue audio files or if you copy a movie as a starting point for another one (e.g., to split a very long and/or complex movie).




Installation
------------
The app is developed under .NET Framework 4.0 and requires no installation, though you may need to download and install the framework files from Microsoft. 
Just create a folder, unzip the contents there and create a direct link to the application (MSCompanion.exe) at will.

