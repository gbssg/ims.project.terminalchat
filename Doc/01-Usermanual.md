# TerminalChat - Developer setup

## Requirements

To run or further develop this project you have to have .NET 10 installed.
This project was originally developed on Windows 11, you may have to tweak something if you want to develop it on a different OS.

## Setup

Clone Repository to preferred folder:  

```bash
git clone "https://github.com/gbssg/ims.project.terminalchat"
```
To continue developing this project, you can now open the cloned project in your IDE of choice.

If the .NET 10 SDK is missing, you can download it from: [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

To run the project, move into the client folder: 
```bash
cd ims.project.terminalchat/Code/TerminalChatV1/TerminalChatV1
```

If you want to run or build the server, move into this folder:

```bash
cd ims.project.terminalchat/Code/TerminalChatV1/TerminalChatServerV1
```
first run:

```bash
dotnet restore
```
to make sure all dependencies are intact.

To build the server or client:

```bash
dotnet build
```
To run the server or client:

```bash
dotnet run
```


