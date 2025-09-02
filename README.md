# Minesweeper WPF

A modern, fully-featured **Minesweeper** game built in **C# using WPF**. This project is a WPF adaptation of the classic Minesweeper game, featuring customizable board size, mine count, and background themes.

---

## Features

- **Customizable Game Board**  
  - Set board size and number of mines from the settings page.  

- **Modern UI**  
  - Clean WPF interface with custom-styled buttons and background images.  
  - Smooth tile reveal with click events.  
  - Right-click to place flags with dynamic mine counter.  

- **Timer**  
  - Tracks elapsed time in the game.  
  - Timer display updates every second.  

- **Multiple Pages**  
  - **Main Menu**: Start Game, Open Settings, Exit.  
  - **Game Board**: Play Minesweeper with reset and back options.  
  - **Settings**: Change board size, mine count, and background preview.  

- **Global Theme Support**  
  - Background images applied globally across pages.  
  - User can preview backgrounds before saving changes.  

- **Engine**  
  - Mines are randomly generated on first click.  
  - Flood fill algorithm reveals empty tiles automatically.  
  - Win/loss detection with events to update UI.

---

## Installation

1. Clone the repository:  
   ```bash
   git clone https://github.com/YourUsername/Minesweeper-WPF.git

