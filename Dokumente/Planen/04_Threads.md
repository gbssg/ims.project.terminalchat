## Was ist ein "Thread"

Ein "thread" ist eine art von subproozess, von denen es mehrere in einem Prozess hat  

Prozess  
└─ Thread

Man nutzt "threads" um subprozesse parallel zu verarbeiten.

In meinem Anwendungsfall muss ich mehrere input- und anzeigestellen überwachen wie zum 
Beispiel der Textinput, das Senden und Empfangen von Nachrichten, der MessageLog, die Infobox und die InfoBox Optionen.

All diese Funktionen werden parallel miteinander agieren dammit die Applikation im 
ganzen flüssiger lauft und nicht alles in einem Loop passiert.