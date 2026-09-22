# Eye-Tracking VR Escape Room

[![Unity](https://img.shields.io/badge/Unity-2021.3%20LTS-000000?logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-.NET-239120?logo=csharp&logoColor=white)](#)
[![VR](https://img.shields.io/badge/VR-HTC%20Vive%20Pro%20Eye-5865F2)](#)

A VR escape room where most of the puzzles are solved by looking at things — no pointing, no button-mashing, just where your eyes go.

*Originally built as my bachelor's thesis project at the Technical University of Košice (2024).*

## The problem

Controllers are the default way to interact in VR, but they're not the only way, and they can get in the way of immersion — you're miming actions with a plastic wand instead of just reaching for or looking at what you want. Eye tracking has existed for decades, but it's mostly used passively (diagnostics, foveated rendering) rather than as something the player actively interacts *with*.

This project asks a simple question: what if you made gaze a first-class input? Can a player "type" a code by looking at digits? Can a lock respond to a sustained stare instead of a trigger pull? The game is a four-room escape room built specifically to answer that, using an HTC Vive Pro Eye headset with built-in eye tracking.

## Architecture
<img width="2400" height="1352" alt="architecture" src="https://github.com/user-attachments/assets/871c7243-67c0-4a78-a6e4-2af08d19d408" />

Two interaction pipelines run side by side:

- **Standard** — Unity's XR Interaction Toolkit handles locomotion (teleport + continuous movement), controller-based grabbing, and the inventory's socket system. This is the "off-the-shelf" part of the VR setup.
- **Custom** — a gaze pipeline built from scratch for this project. `EyeTracking.cs` reads per-eye gaze data from the Vive Pro Eye through Vive's SRanipal SDK every frame. `EyeGazeController.cs` then casts a ray from that gaze direction and determines what the player is looking at, replacing the SDK's own raycasting with logic tailored to the game. Every gaze-reactive object inherits from a shared `GazeInteractable` base class that tracks hover state, dwell time and gaze distance, and drives a colour-coded outline so the player always knows what's interactive and how to activate it.

Both pipelines converge on the same gameplay layer — a puzzle doesn't care whether it was solved by hand or by stare, it just reacts to state changes.

## Tech stack

- **Engine / language:** Unity 2021.3 LTS, C#
- **VR stack:** OpenXR, Unity XR Interaction Toolkit, SteamVR
- **Eye tracking:** HTC Vive Pro Eye (built-in Tobii sensors) via the Vive SRanipal SDK
- **Target hardware:** HTC Vive Pro Eye headset + controllers

## What I built

- **A reusable gaze-interaction framework** — `GazeInteractable` base class that any object inherits to become gaze-reactive, supporting three distinct interaction models used throughout the game:
  - **Trigger-confirm** — look at an object, pull the controller trigger to confirm (e.g. selecting a digit on the TV remote).
  - **Dwell-time** — hold your gaze on an object for a set duration to activate it, with live progress feedback (the keypad's keys, the pattern-lock tiles).
  - **Smooth pursuit** — track a moving target with your eyes to trigger an action (installing a light bulb by following an orbiting point with your gaze).
- **Six gaze-driven puzzles** on top of that framework: a 3×3 pattern lock, a 4-digit TV remote code, a numeric keypad door lock, a vault that stays open only while you look at its sensor, a shelf that collapses after repeated glances, and a light-bulb installation sequence.
- **A hybrid interaction model**: gaze drives selection and attention-based puzzles, while the XR Interaction Toolkit drives physical actions — picking up, carrying and placing objects (gems, a remote, a book with a hidden code) through an inventory system with dedicated 3D sockets.
- **A four-room level** built around those mechanics: a tutorial room, a central hub with four puzzles, a dark room unlocked by installing a light, and a final room gated by a code — all converging on collecting four gems to unlock the exit.

## Video
<img width="640" height="360" alt="Video Project 1" src="https://github.com/user-attachments/assets/9049eb44-ff69-4d5b-be2b-6adbe8b8bf9a" />

## Running it

Requires an HTC Vive Pro Eye (or another SRanipal-compatible eye tracker), SteamVR, and the Vive SRanipal runtime. Open the project in Unity 2021.3 LTS, load `Assets/Scenes/Level.unity`, connect your headset, and press Play.
