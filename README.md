# TerminalChat

## About

TerminalChat is a lightweight, Discord/IRC-like, self-hostable chat application that runs both the server and client directly in the terminal. It is minimal and intentionally old-school, combining the feel of classic command-line tools with a small set of modern features.

Messages are not stored on the server; all data is kept client-side in simple JSON files and directories. This eliminates the need for database setup and reduces storage overhead.

Communication is handled directly over standard TCP/IP connections. No external APIs are used—only straightforward socket-based networking.

## Usage

TerminalChat is for anyone who wants a simple, self-managed chat solution without relying on corporate services. It’s lightweight, easy to set up, and keeps you in control of your own data. The target user is someone who understands where data is typically hosted and prefers to run services within their own local network. 

## Status

This project is unfinished and no longer under active development.

## Other documentation

- [Documentation](Doc/00-Codedoc.md)  
- [Manual](Doc/01-Usermanual.md)


## Features

* No server-side logs 
* Client JSON-based data storage

### Planned Features

* Pure TCP/IP connection
* Responsive interface for different terminal sizes
* Encrypted connections for secure messaging
* Configurable settings interface
* Custom themes / template layouts
 
## Limitations

* No built-in message encryption
* No explicit File/Media sharing
* No voice/video calls (possible future feature)

## Tech stack

* Language: C# .net, the core application is built entirely in C# for cross-platform compatibility.
* Networking: TCP/IP connections, messages are sent directly over the network. 
* Data Storage: Simple JSON files, all client-side data is stored locally.
* Interface: Terminal-based UI, minimalistic and lightweight, specifically for command-line use.

## Current state

The program is capable of:
- Managing Files
- Reading from JSON files
- Writing to JSON files
- Creating necessary classes for basic TCP/IP connection

The program can't:
- PRovied a user interface
- Communicate between server and client
- Encrypt messages



