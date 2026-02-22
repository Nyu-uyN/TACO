# 🌮 T.A.C.O. — Tag Auto-Combo Optimizer

![Version](https://img.shields.io/badge/Version-1.0.0-blue.svg)
![Game](https://img.shields.io/badge/Game-Next%20Space%20Rebels-orange.svg)
![Loader](https://img.shields.io/badge/Loader-MelonLoader-green.svg)

**T.A.C.O.** is a strategic optimization tool for **Next Space Rebels**. 

In the world of StarTube, your rocket's performance is only half the battle. The other half is gaming the algorithm. Stop guessing which tags work best: T.A.C.O. mathematically calculates and automatically selects the most profitable tag combination the second you open the upload screen.

---

## ✨ Features

* **⚡ Instant Optimization**: Calculates the best combo among all available tags in milliseconds.
* **📊 Data-Driven**: Uses the game's internal `CalculateSubs` logic to ensure 100% accuracy.
* **🧠 Context-Aware**: 
    * Adjusts to your current **Player Rank**.
    * Respects available **Tag Slots** (up to 5).
    * Accounts for **Audience Disinterest** (fatigue) by checking tag history.
* **🎮 Seamless Integration**: Native UI synchronization. Tags are selected with original sounds and visual feedback.

---

## 🛠 How It Works

T.A.C.O. operates as a "Quality of Life" injection by hooking into the `UIVideoMaker` class:

1.  **Extraction**: Scans the `TagInstance` pool generated after your flight.
2.  **Brute-Force Analysis**: Evaluates every possible unique combination of tags.
3.  **Validation**: Simulates subscriber gain using the game's native math, including rarity multipliers and penalties.
4.  **Injection**: Programmatically invokes `UIVideoMaker.SelectTag()` to populate the UI.

---

## 📥 Installation

1.  Download and install [MelonLoader](https://github.com/LavaGang/MelonLoader/releases).
2.  Launch the game once to create the `/Mods` folder.
3.  Download the latest `TACO.dll` from the [Releases](#) section.
4.  Drop the `.dll` into your `Next Space Rebels/Mods` directory.
5.  Launch the game and start your orbital StarTube domination.

---

## 📋 Requirements

* **Game**: Next Space Rebels (Steam/PC).
* **Runtime**: .NET Framework 4.7.2.
* **Architecture**: Windows x64.

---

## 👨‍💻 Author

* **Nyu** — *Core logic and implementation.*

---

> *"The revolution will not be televised. It will be optimized."*
