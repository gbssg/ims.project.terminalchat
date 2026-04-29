# TerminalChat - Code-Doc

## Content

- [TerminalChat - Code-Doc](#terminalchat---code-doc)
  - [Content](#content)
  - [Concept](#concept)
    - [Architecture](#architecture)
      - [Program structure](#program-structure)
  - [Improvements](#improvements)
    - [Current state - Github Issues](#current-state---github-issues)
  - [UX Philosophy](#ux-philosophy)
  
## Concept

### Architecture

The client-side is structured as seen in the class-diagramm, the dashed lines represent future features and how they would be connected to the programm. 

![client concept diagramm](../Dokumente/Diagramme/client-concept-diagramm.svg)

The server-side is structured as seen in the class-diagramm, it soely consists of forwarding messages and distributing the server architecture to the clients, the server config is the anly thing that's stored on the server.

![server concept diagramm](../Dokumente/Diagramme/server-concept-diagramm.svg)

#### Program structure

```
TerminalChatV1/    
├── .vs/  
├── TerminalChatServerV1/  
│   ├── Properties/  
│   ├── Program.cs  
│   ├── ServerClasses.cs  
│   ├── ServerDataCrud.cs  
│   ├── ServerTcpConnection.cs  
│   ├── TerminalChatServer.csproj  
│   └── TerminalChatServer.csproj.user  
├── TerminalChatV1/  
│   ├── ClientTcpConnection.cs  
│   ├── DataClasses.cs  
│   ├── FileManager.cs  
│   ├── Program.cs  
│   ├── ReadWriteData.cs  
│   ├── SetupLocalUser.cs  
│   ├── TerminalChatUser.csproj  
│   └── TerminalChatUser.csproj.user  
└── TerminalChat.sln  
```

## Improvements

### Current state (q1 2026) - Github Issues

Completed:
- [Most Planning aspects](https://github.com/gbssg/ims.project.terminalchat/issues?q=is%3Aissue%20is%3Aclosed%20label%3Adoc)

Open:
- [Userguid, Userinstallation, etc.](https://github.com/gbssg/ims.project.terminalchat/issues?q=is%3Aissue%20is%3Aopen%20label%3Adoc)
- [Client Interface](https://github.com/gbssg/ims.project.terminalchat/issues/24)
  - [Infobox](https://github.com/gbssg/ims.project.terminalchat/issues/69)
  - [Messagebox](https://github.com/gbssg/ims.project.terminalchat/issues/68)
  - [Textbox](https://github.com/gbssg/ims.project.terminalchat/issues/67)
- [Client Controller](https://github.com/gbssg/ims.project.terminalchat/issues/23)

To improve or add:
- Add encryption
- Add voicecall support
- Add support for dotfiles (customization)
- improve or add UI-designs

## UX Philosophy

The performance of the application should be prioritized, it should feel responsive and lightweight, it shouldn't try to compete with commercial enterprise-grade solutions, but more like a polished OSS product. 
