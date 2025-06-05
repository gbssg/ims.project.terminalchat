### Server Storage solution

Um dem Client die nötigen Informationen über den Server wissen bekommt der Server einen Server-
Storage, indem er Daten wie die Strucktur des Servers oder wie viele kanäle es gibt, welche namen 
diese haben, welche themen sie besprechen und auch wie viele und welche user online sind, speichert.

Um diese Daten zu speichern nutze ich simple JSON files da diese einfach zu lesen und bearbeiten sind 
und sie sind einfacher zu verwalten und nehmen weniger platz als eine ganze Datenbank.

Es wird voraussichtlich 2 JSON files auf der Serverseite haben:
- serverConfig.json
  - enthält daten über die Serverstrucktur, abrufbar von clientside wenn sie dem server beitreten.
- userData.json
  - enthält daten über nutzer die online oder offline sind, nutzer werden beim beitreten des servers hinzugefügt, werden bei dem verlassen des servers offline gesetzt. diese daten werden an clients gesendet wenn das JSON geupdatet wird aka neue nutzer joinen, offline gehen, oder online kommen.