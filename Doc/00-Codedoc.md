# Code-Doc

## Content

- [Code-Doc](#code-doc)
  - [Content](#content)
  - [Remark](#remark)
  - [Client](#client)
    - [ClientTcpConnection](#clienttcpconnection)
      - [`void ListenToServer(Server _server)`](#void-listentoserverserver-_server)
      - [`void TcpReciveThread(NetworkStream _stream)`](#void-tcprecivethreadnetworkstream-_stream)
      - [`void ProcessPackage(string _receivedString)`](#void-processpackagestring-_receivedstring)
    - [Dataclasses](#dataclasses)
      - [`public class SetupUser`](#public-class-setupuser)
  - [| Name | string | -           |](#-name--string--------------)
      - [`public class LocalUser`](#public-class-localuser)
  - [| setupUsers | List | list of SetupUser names |](#-setupusers--list--list-of-setupuser-names-)
      - [`public class User`](#public-class-user)
  - [| UserJoinDate | DateTime | Creation date of this user |](#-userjoindate--datetime--creation-date-of-this-user-)
      - [`public class Channel`](#public-class-channel)
  - [| ChannelDescription | string | Custom description of this channel |](#-channeldescription--string--custom-description-of-this-channel-)
      - [`public class Message`](#public-class-message)
  - [| Body       | string   | Content of the message                        |](#-body--------string----content-of-the-message------------------------)
      - [`public class MessageLog`](#public-class-messagelog)
      - [`public class Server`](#public-class-server)
      - [`public class ServerList`](#public-class-serverlist)
    - [FileManager](#filemanager)
      - [`string directoryPath`](#string-directorypath)
      - [`string setupUserPath`](#string-setupuserpath)
      - [`string serverListPath`](#string-serverlistpath)
      - [`void SetupAppDir() {}`](#void-setupappdir-)
      - [`void CreateServerProfile(Server server) {}`](#void-createserverprofileserver-server-)
      - [`string GetServerProfilePath(Guid serverUUID) {}`](#string-getserverprofilepathguid-serveruuid-)
      - [`string GetMessageLogPath(Guid serverUUID) {}`](#string-getmessagelogpathguid-serveruuid-)
    - [ReadWriteData](#readwritedata)
      - [`public void UpdateServerList`](#public-void-updateserverlist)
      - [`public ServerList ReadServerList`](#public-serverlist-readserverlist)
      - [`public void UpdateServerProfile`](#public-void-updateserverprofile)
      - [`public Server ReadServerProfile`](#public-server-readserverprofile)
      - [`public void UpdateSetupUserList`](#public-void-updatesetupuserlist)
      - [`public void OverrideSetupUserList`](#public-void-overridesetupuserlist)
      - [`public LocalUser ReadSetupUserList`](#public-localuser-readsetupuserlist)
      - [`public MessageLog ReadMessageLog`](#public-messagelog-readmessagelog)
      - [`public void UpdateMEssageLog`](#public-void-updatemessagelog)
    - [SetupLocalUser](#setuplocaluser)
      - [`public void UserSetupPrompt`](#public-void-usersetupprompt)
      - [`public void CreateSetupUser`](#public-void-createsetupuser)
    - [User](#user)
    - [Server Classes](#server-classes)
      - [`public class ServerList`](#public-class-serverlist-1)
      - [`public class Server`](#public-class-server-1)
        - [`public void UpdateChannel`](#public-void-updatechannel)
        - [`public string GetLocalIpAddress`](#public-string-getlocalipaddress)
      - [`public class Channel`](#public-class-channel-1)
    - [ServerDataCrud](#serverdatacrud)
      - [`public string directoryPath`](#public-string-directorypath)
      - [`public string serverListPath`](#public-string-serverlistpath)
      - [`public void SettupAppDir`](#public-void-settupappdir)
      - [`public ServerList GetServers`](#public-serverlist-getservers)
      - [`public string GetServerAsJson`](#public-string-getserverasjson)
      - [`public void AddServer`](#public-void-addserver)
      - [`public void DeleteServer`](#public-void-deleteserver)
      - [`public void UpdateServer`](#public-void-updateserver)
      - [`public Server GetServer`](#public-server-getserver)
    - [ServerSetup](#serversetup)
      - [`public Server ServerSetupPromptByJSON`](#public-server-serversetuppromptbyjson)
      - [`public Server ServerSetupPrompt`](#public-server-serversetupprompt)
      - [`public Channel SetupChannel`](#public-channel-setupchannel)
      - [`public List<Channel> SetupChannelRecursive`](#public-listchannel-setupchannelrecursive)
      - [`static int ReadInt`](#static-int-readint)
      - [`public string ReadString`](#public-string-readstring)

-----
## Remark
Not all features may be in use in the current version of the code.


## Client
### ClientTcpConnection
#### `void ListenToServer(Server _server)`

**Description:**  
using `System.Net.Sockets;`'s TCP client, the function trys to connect to the server given in the parameters.

**Parameter:**  
| Name    | Type   | Description                                                                                              |
| ------- | ------ | -------------------------------------------------------------------------------------------------------- |
| _server | Server | Contains several relevant information about he given server, like IP, Port, amount of text channels etc. |

#### `void TcpReciveThread(NetworkStream _stream)`

**Description:**
Using `System.threading;` creates a passively running thread that receives messages from given `NetworkStream` but isn't output anywhere.

#### `void ProcessPackage(string _receivedString)`

**Description:**  
Using `System.Text.Json;` try to deserialize Json String to message or server Object 

### Dataclasses
----------------------------------

#### `public class SetupUser`

**Parameters:**  
| Name | Type   | Description |
| ---- | ------ | ----------- |
| Name | string | -           |
---
#### `public class LocalUser`

**Parameters:**  
| Name       | Type            | Description             |
| ---------- | --------------- | ----------------------- |
| setupUsers | List<SetupUser> | list of SetupUser names |
---
#### `public class User`

**Parameters:**  
| Name         | Type     | Description                |
| ------------ | -------- | -------------------------- |
| UserName     | string   | The name of this user      |
| UserJoinDate | DateTime | Creation date of this user |
---
#### `public class Channel`

**Parameters:**  
| Name               | Type   | Description                        |
| ------------------ | ------ | ---------------------------------- |
| ChannelId          | int    | Unique Identifier                  |
| ChannelName        | string | Name of this channel               |
| ChannelDescription | string | Custom description of this channel |
---
#### `public class Message`

**Parameters:**  
| Name       | Type     | Description                                   |
| ---------- | -------- | --------------------------------------------- |
| serverUUID | Guid     | Unique identifier                             |
| serverIP   | string   | Ip address of the server                      |
| ChannelId  | int      | id of the channel where the maessage was sent |
| Sender     | User     | user that sent the message                    |
| Timestamp  | DateTime | Time when the message was sent                |
| Body       | string   | Content of the message                        |
---

#### `public class MessageLog`

**Parameter**
| Name     | Type          | Description      |
| -------- | ------------- | ---------------- |
| Messages | List<Message> | list of messages |

#### `public class Server`

**Parameter**
| Name     | Type          | Description      |
| -------- | ------------- | ---------------- |
| Messages | List<Message> | list of messages |

#### `public class ServerList`

**Parameter**
| Name       | Type         | Description                        |
| ---------- | ------------ | ---------------------------------- |
| ServerList | Instance     | test implementation of a singelton |
| servers    | List<Server> | list of servers                    |

### FileManager
-----------------------------------------------------------

#### `string directoryPath`

**Description:**

The path to the App-directory using `Path.Combine();`, `Environment.GetFolderPath()`, `Environment.SpecialFolder.ApplicationData` and adds TerminalChatCLI at the end.


#### `string setupUserPath`

**Description:**  

The path to the setupuserlist using `Path.Combine();`, `Environment.GetFolderPath()`, `Environment.SpecialFolder.ApplicationData` and adds TerminalChatCLI and `user.json` at the end.


#### `string serverListPath`

**Description:**  

The path to the serverlist using `Path.Combine();`, `Environment.GetFolderPath()`, `Environment.SpecialFolder.ApplicationData` and adds TerminalChatCLI and `serverlist.json` at the end.


#### `void SetupAppDir() {}`

**Description:**  

Parameter and returnless function that checks if the [Appdirectory](#filemanager-directorypath), [setupuserlist.json](#filemanager-serverlistpath) and [serverlist.json](#filemanager-serverlistpath) exists, if not they will be created.

**Usage:**  
This function is used to generate the AppData file and folder structure, and is run on the start of the programm.


#### `void CreateServerProfile(Server server) {}`

**Description:**  
A returnless function that creates an empty serverprofile folder with the nessesary files contained (`serverprofile.josn`, `messagelog.json`), the directory is named after the parameters UUID of the Server so there are no duplicates.

**Parameters:**    
| Name   | Type   | Description                                             |
| ------ | ------ | ------------------------------------------------------- |
| server | Server | The Server class is described in [Dataclasses/Server]() |

**Usage:**  
Everytime a new serverprofile is created aka. every time the user connects to a new server and gets sent the serverprofile, a new serverprofile gets created if there isn't one already. This function gets called when theres a serverprofile missing. 


#### `string GetServerProfilePath(Guid serverUUID) {}`

**Description:**  
This function returns the path of the specefied server by the UUID in the parameter, It crafts the path with the serverUUID according to the determined pattern and searches for it and returns it if it was found.

**Parameters:**    
| Name       | Type | Description                                               |
| ---------- | ---- | --------------------------------------------------------- |
| serverUUID | Guid | The Id of the serverprofle which path has been requested. |

**Returns:**  
| Type   | Description                    |
| ------ | ------------------------------ |
| string | ther Path to the serverprofile |
| string | "file not found"               |

**Usage:**  
The function is used in the CRUD functions to recieve the paths to write and read from specified files.


#### `string GetMessageLogPath(Guid serverUUID) {}`

**Description:**  
This function returns the path of the messagelog of the specefied server by the UUID in the parameter, It crafts the path with the serverUUID according to the determined pattern and searches for it and returns it if it was found.

**Parameters:**    
| Name       | Type | Description                                               |
| ---------- | ---- | --------------------------------------------------------- |
| serverUUID | Guid | The Id of the serverprofle which path has been requested. |

**Returns:**  
| Type   | Description                 |
| ------ | --------------------------- |
| string | ther Path to the messagelog |
| string | "file not found"            |

**Usage:**  
The function is used in the CRUD functions to recieve the paths to write and read from specified files.


### ReadWriteData

#### `public void UpdateServerList`

**Description:**  
In theory, takes a Server as an input looks up the server by the ServerUUID and replaces the server in the serverlist.

**Usage:**  
Used to eddit server profiles.

#### `public ServerList ReadServerList`

**Description:** 
Reads Server list from json and outputs a ServerList object. (no errorhandling implemented)

**Usage:**  
Used to GET serverlist information.

#### `public void UpdateServerProfile`

**Description:**
Overrides Server in server porfile based on ServerUUID. (no errorhandling implemented)

**Usage:**  
Used to UPDATE already exiting profiles.

#### `public Server ReadServerProfile`

**Description:**  
Returns Server Object based on given ServerUUID. (no errorhandling implemented)

**Usage:**
Used to GET a server by ServerUUID.

#### `public void UpdateSetupUserList`

**Description:**  
Adds a SetupUser to save, overrides last saved User. Said to be cofigurable in the future.

**Usage:**
Used to save SetupUser after creation.

#### `public void OverrideSetupUserList`

**Description:**  
Testing Function to test override.

**Usage:**  
Used for testing, now no more.  

#### `public LocalUser ReadSetupUserList`

**Description:**  
Returns Current localUsere aka. SetupUser.

**Usage:**  
GET the Username to sign messages and to be displayed.

#### `public MessageLog ReadMessageLog`

**Description:**
Reading out of JSON file to messageLog Object.

**Usage:**  
Used to be displayed the full Message log on command.

#### `public void UpdateMEssageLog`

**Description:**  
Adds Message Object into MessageLog Json.

**Usage:**  
Used to insert New messages into messageLits/MessageLog.

### SetupLocalUser
---------------------------------------------

#### `public void UserSetupPrompt`

**Description:**  
Prompts the user to coose to create a new username or select the most recent one.

#### `public void CreateSetupUser`

**Description:**  
Function creates a new UserObject and prompts the user to enter a name and checks if it fits into the requrements. e. x. if the name is too long or too short. 

### User

**Description:**
Fully comented out code, used to test SetupUser calss before gettin implemented differently.

### Server Classes

#### `public class ServerList`

**Parameters:**

| Name    | Type         | Description     |
| ------- | ------------ | --------------- |
| Servers | List<Server> | List of servers |

#### `public class Server`

**Parameters:**  
| Name        | Type          | Description                              |
| ----------- | ------------- | ---------------------------------------- |
| Ip          | string        | Ip address of the server                 |
| Port        | int           | Port on the Hostdevice                   |
| Name        | string        | Name of the server                       |
| Description | string        | Custom description of the server         |
| UUID        | Guid          | Universally unique identifier            |
| Channels    | List<Channel> | List of channels contained in the server |

**Description:**
Two seperate consturctors one for having a singular channels in the server object, one to add a list full of channels. 

##### `public void UpdateChannel`

**Description:**
Looks for a channel to update and overrides one by matching UUID.

##### `public string GetLocalIpAddress`

**Description:**
Used to get the local Ip address of the host device to later open up a server/ port to allow network traffic.

#### `public class Channel`

**Parameters:**  
| Name        | Type   | Description                      |
| ----------- | ------ | -------------------------------- |
| Name        | string | Name of the channel              |
| Description | string | short description of the channel |
| UUID        | Guid   | UUID to identify each channel    |

### ServerDataCrud

#### `public string directoryPath`
**Description:** is a variable that contains the path to the app directory.

#### `public string serverListPath`
**Description:** is a variable that contains tha path to the server list foulder.

#### `public void SettupAppDir`
**Description:** checks if directories exists and creates new ones if needed.

#### `public ServerList GetServers`
**Description:** Simply reads JSON file and outputs the ServerList object.

#### `public string GetServerAsJson`
**Description:** Outputs a json string of the Serverlist.

#### `public void AddServer`
**Description:** Adds server to the serverlist. (no errorhandling, may not work)

#### `public void DeleteServer`
**Description:** Takes Server as argument to delete server from Serverlist (no errorhandling, may not work)

#### `public void UpdateServer`
**Description:** Takes a Server and overrides Already existing ServerProfile in server list.

#### `public Server GetServer`
**Description:** Gets Server By UUID.

### ServerSetup

#### `public Server ServerSetupPromptByJSON`
**Description:** Nonfunctional way to create Server instead of entering values manually. (does not work)

#### `public Server ServerSetupPrompt`
**Description:** Manual way to create a server, prompts the user to create a server value by value.

#### `public Channel SetupChannel`
**Description:** Recursively used function to create channels.

#### `public List<Channel> SetupChannelRecursive`
**Description:** Generates a given amount of channels.

#### `static int ReadInt`
**Description:** Takes a range of integer and includes errorhandling.

#### `public string ReadString`
**Description:** Takes a string with a given range of length and includes errorhandling.