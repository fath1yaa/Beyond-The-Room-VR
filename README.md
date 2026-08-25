# Beyond-The-Room-VR — VR Escape Room Simulation
A VR escape room built as the final project for a Real-Time Computer Graphics course. Players wear a Meta Quest 3 headset and see a live 360° camera feed of the physical room, with a speech-recognition AI providing real-time hints when they ask for help.

Demo video: [add link — YouTube/Drive]
Screenshots: [add 2–3 images here]

# My Role
Lead technical developer on a team of 3. I was responsible for the full technical implementation — front-end and back-end systems, hardware setup, and debugging — while teammates focused on documentation.

# Tech Stack
Engine: Unity
Headset: Meta Quest 3
Camera: Insta360 X4 (360° passthrough)
Camera feed pipeline: OBS Studio
Speech recognition: Vosk (offline speech-to-text model)

# Key Technical Challenges
Real-time 360° feed sync: Getting the Insta360 X4's live feed, routed through OBS Studio, to display inside the Meta Quest 3 headset view without noticeable lag.
In-game voice hints: Integrating the Vosk speech recognition model so players could ask for hints out loud and get a real-time spoken/text response, without needing an internet connection.

# Status
Fully working demo delivered as the final course project.
