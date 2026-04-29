# TerminalChat

## About

TerminalChat is a lightweight, Discord/IRC-like, self-hostable chat application that runs both the server and client directly in the terminal. It is minimal and intentionally old-school, combining the feel of classic command-line tools with a small set of modern features.

Messages are not stored on the server, all data is kept client-side in simple JSON files and directories. This eliminates the need for database setup and reduces storage overhead.

Communication is handled directly over standard TCP/IP connections. No external APIs are used—only straightforward socket-based networking.

## Usage

TerminalChat is for anyone who wants a simple, self-managed chat solution without relying on corporate services. It’s lightweight, easy to set up, and keeps you in control of your own data. The target user is someone who understands where data is typically hosted and prefers to run services within their own local network. 

## Status

This project is unfinished and no longer under active development.

## Other documentation

- [Documentation](Doc/00-Codedoc.md)
- [Manual](Doc/01-Usermanual.md)

### Project capabilities
| Feature                     | Status | 
| --------------------------- | ------ | 
| Clientside data storage     | Done   | 
| Data stored as JSON         | Done   | 
| TCP/IP communication        | Done   | 
| Interactive user interface  | Idea   | 
| Encrypted connection        | Idea   | 
| Custom themes               | Idea   | 
| built in File/Media sharing | Idea   | 
| Voice- / Videochats         | Idea   | 

## Tech stack
| Component  | Technology     | Purpouse                              |
| ---------- | -------------- | ------------------------------------- |
| Backend    | c# / .Net 10 core | Core server and client implementation |
| Networking | TCP sockets    | Communication layer                   |
| Database   | JSON           | Hand made JSON CRUD functions         |
| Interface  | Terminal       | Minimal usage, light weight           |

