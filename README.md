# WindowsUnited Gewinnspiel 2018 - Auslosung mit .NET

Liebes Community-Mitglied

Dieser Quellcode steht unter der MIT-Lizenz.
Das bedeutet, dass du selbst mit dem Projekt arbeiten kannst. Du findest eine Projektdatei für Visual Studio mit welcher du das Programm kompillieren kannst. Alles was du benötigst ist Visual Studio: https://visualstudio.microsoft.com/de/

Das Projekt verwendet das .NET Core Framework und kann daher auch unter Linux und macOS kompilliert und ausgeführt werden

## Wichtiges
Du musst natürlich deine eigene Wordpress-Domain angeben. Dafür öffne die Datei '\Components\winner2018\Winner2018Manager.cs' im Projektvezeichnis. Dort findest du die folgende Codezeile:
```
const string WORDPRESS_API_URL = "https://DEINE-WORDPRESS-DOMAIN.de/wp-json/wp/v2";
```

Ersetze in dieser Zeile DEINE-WORDPRESS-DOMAIN gegen die Domain deines wordpress-Blogs.
Zum Beispiel:
```
const string WORDPRESS_API_URL = "https://beispiel.wordpressblog.de/wp-json/wp/v2";
```