# TerminalChat
## About
Terminal Chat is a lightweight, discord/IRC like, self-hostable chat setup that runs both server and client right in your terminal. It’s minimal and a bit old-school by design, mixing the feel of classic command-line tools with just enough modern features to make it useful.

Messages aren’t stored on the server at all only on the client side, saved in simple JSON files and directories. That means no databases to set up or manage which eats up your storage.

Everything runs directly over standard TCP/IP connections. No APIs, just straightforward TCP/IP communication.

## Usage
Terminal Chat is for anyone who wants a simple, self-managed chat solution without relying on corporate services. It’s lightweight, easy to set up, and keeps you in control of your own data. The target user is somebody who's aware of where data is usualy hosted, stored and likes to host services in their local network. 

## Codedoc

[Documentation](Doc\00-Codedoc.md)
[Manunal](Doc/01-Usermanual.md)

## Current state

The Program is capable of:
- Managing Files
- Reading from Json-files
- Writeing to Json-files
- Createing necessary classes for basic Ip/TCP connection

The Program can't:
- Display an interface
- communicate between server and client
- Encrypt

## Features

* No server-side logs 
* client JSON-based data storage

### Planned Features

* Self-hostable "chat" server and client, in terminal
* Pure TCP/IP "connection"
* Responsive interface for different terminal sizes
* Encrypted connections for secure messaging
* Configurable settings interface
* Custom themes / template layouts
* Split-terminal functionality for multitasking
 
## Limitations

* No built-in encryption (yet)
* No explicit File/Media shareing
* No voice/video calls (mabey future voicecalls)

## Tech stack

* Language: C# .net, the core application is built entirely in C# for cross-platform compatibility.

* Networking: TCP/IP connections, messages are sent directly over the network. 

* Data Storage: Simple JSON files, all client-side data is stored locally.

* Interface: Terminal-based UI, minimalistic and lightweight, specifically for command-line use.

## Status

Not in development.


