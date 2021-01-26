# Einleitung 

Das Reihentestungstool Bayern ist eine kostenlose Software, die die
Generierung von Barcodes über
<https://covidtestbayern.sampletracker.eu/> erleichtern soll.

Die Software ist lizensiert unter den GNU GPL v3.0 und der Quellcode ist
einsehbar unter <https://github.com/Wulfheart/CovidBulkBavaria>.

# Funktionsweise

Das Portal <https://covidtestbayern.sampletracker.eu> ist eine
Schnittstelle, mit der IDs und Barcodes für Covid19-Testungen erstellt
werden können. Das Reihentestungstool dockt an dieselbe Schnittstelle an
und holt sich ebenfalls die IDs. Aus diesen IDs werden wie in der
Webanwendung Bar- und QR-Codes generiert.

# Installation

Der Installationsvorgang setzt grundlegende technische Kenntnisse wie
Herunterladen von ZIP-Dateien und Entpacken derselbigen voraus.

## Installation des .NET Frameworks 4.7.2

<img src="media/image1.png" style="width:6.29931in;height:3.14167in" />Bitte
stellen Sie sicher, dass Sie das .NET Framework 4.7.2 installiert haben.
Falls es nicht installiert ist, laden Sie es sich unter
<https://dotnet.microsoft.com/download/dotnet-framework/net472>
herunter.

Wählen Sie hierfür bitte „Download .NET Framework 4.7.2 Runtime“ aus.
Führen Sie daraufhin die heruntergeladene Datei aus. Falls Sie bereits
das .NET Framework 4.7.2 installiert haben sollten, wird der Installer
eine Benachrichtigung anzeigen.

## Installation des Reihentestungstools

Laden Sie unter
<https://github.com/Wulfheart/CovidBulkBavaria/releases/latest> die
Datei mit der Endung .zip (bspw. Reihentestunstool\_0\_3\_0.zip)
herunter und entpacken Sie sie lokal. Führen Sie nun die Datei mit dem
Namen „setup“ bzw. „setup.exe“ aus. Danach sollte eine Anwendung mit der
Bezeichnung „ReihentestungstoolBayern“ auf Desktop und Startmenü
verfügbar sein.

Falls bei der Installation Schwierigkeiten auftreten sollten, nehmen Sie
bitte entweder über die Github Issues oder die Emailadresse Kontakt auf.

# Aufbau der Exceldatei

Der grundlegende Baustein zur Generierung von Barcodes ist eine
Exceldatei. Eine Beispieldatei ist unter
<https://github.com/Wulfheart/CovidBulkBavaria/releases/latest> als
„Mitarbeiter.xlsx“ zu finden. Bitte beachten Sie, dass die
Funktionalität nur mit Excel 365 und 2019 getestet wurde. Es sollte
jedoch auch bei ältern Version von Excel funktionieren.

## Benötigtes Arbeitsblatt

Das Reihentestungstool wird nur das Arbeitsblatt mit dem Namen
„Personen“ (Groß- und Kleinschreibung beachten) in Betracht ziehen. Alle
anderen eventuell vorhandenen Arbeitsblätter werden ignoriert. Falls das
Arbeitsblatt „Personen“ nicht vorhanden ist, dann wird es zu Fehlern im
Programmablauf kommen.

## Erste Zeile

Die erste Zeile in dem Arbeitsblatt „Personen“ wird nicht in der
Auswertung berücksichtigt und dient nur zur Orientierung.

## Nicht einstellbare Eingaben

Das Reihentestungstool verwendet die Exceldatei als Alternative zur
manuellen Eingabe im Webportal. Es gibt jedoch Parameter, die fest im
Programm verankert sind. Folgende Eingaben sind betroffen:

-   „Mein Aufenthaltsort in den nächsten 14 Tagen ist identisch zu
    Heimatadresse“ ist automatisch immer „Ja“

-   „Heimatadresse Land“ ist immer „Deutschland“

-   „Testgrund“ ist immer „Ich werde am Ort eines akuten
    Ausbruchsgeschehens getestet (§ 3 RVO)“

## Bedeutungen der einzelnen Spalten

Hinweise: ALLE Spalten von A bis M müssen pro angemeldete Testung (also
Zeile) ausgefüllt werden

### Nachname

Der Nachname der Person, die getestet werden soll.

### Vorname

Der Vorname der Person, die getestet werden soll.

### Geschlecht

Das Geschlecht der Person, die getestet werden soll. Da das Webportal
auch nur „Männlich“ und „Weiblich“ unterstützt, gilt dasselbe für das
Reihentestungstool. Es Schreibweisen beider Geschlechter unterstützt.
Wichtig ist nur, dass der erste Buchstabe jeweils „M“ bzw. „W“ ist.
Groß- und Kleinschreibung ist hierfür egal.

### Geburtstag

Das Geburtsdatum der Person, die getestet werden soll. Muss im Format
TT.MM.JJJJ sein.

### Adresse

Die Adresse der Person, die getestet werden soll. Bsp.: Ackerleite 7

### PLZ

Die Postleitzahl der Person, die getestet werden soll.

### Stadt

Der Name der Stadt, in der die Person, die getestet werden soll, wohnt.

### Email

Die Emailadresse, an die das Testergebnis geschickt werden soll.

### Telefonnummer

Die Telefonnummer, die für Rückfragen verwendet werden soll. Sollte mit
+49 beginnen.

### Land

Das Herkunftsland der Person, die getestet werden soll. Es handelt sich
hierbei um den 2-Letter ISO Code.

Erlaubte Werte: "DE", "AF", "EG", "AX", "AL", "DZ", "AS", "AD", "AO",
"AI", "AQ", "AG", "GQ", "AR", "AM", "AW", "AZ", "ET", "AU", "BS", "BH",
"BD", "BB", "BY", "BE", "BZ", "BJ", "BM", "BT", "BO", "BQ", "BA", "BW",
"BV", "BR", "IO", "BN", "BG", "BF", "BI", "CL", "CN", "CK", "CR", "CW",
"DK", "CD", "DE", "DM", "DO", "DJ", "EC", "SV", "CI", "ER", "EE", "SZ",
"FK", "FO", "FJ", "FI", "FR", "GF", "PF", "TF", "GA", "GM", "GE", "GH",
"GI", "GD", "GR", "GL", "GP", "GU", "GT", "GG", "GN", "GW", "GY", "HT",
"HM", "HN", "HK", "IN", "ID", "IM", "IQ", "IR", "IE", "IS", "IL", "IT",
"JM", "JP", "YE", "JE", "JO", "VG", "VI", "KY", "KH", "CM", "CA", "CV",
"KZ", "QA", "KE", "KG", "KI", "CC", "CO", "KM", "XK", "HR", "CU", "KW",
"LA", "LS", "LV", "LB", "LR", "LY", "LI", "LT", "LU", "MO", "MG", "MW",
"MY", "MV", "ML", "MT", "MA", "MH", "MQ", "MR", "MU", "YT", "MX", "FM",
"MD", "MC", "MN", "ME", "MS", "MZ", "MM", "NA", "NR", "NP", "NC", "NZ",
"NI", "NL", "NE", "NG", "NU", "KP", "MP", "MK", "NF", "NO", "OM", "AT",
"TL", "PK", "PS", "PW", "PA", "PG", "PY", "PE", "PH", "PN", "PL", "PT",
"PR", "CG", "RE", "RW", "RO", "RU", "MF", "SB", "ZM", "WS", "SM", "BL",
"ST", "SA", "SE", "CH", "SN", "RS", "SC", "SL", "ZW", "SG", "SX", "SK",
"SI", "SO", "ES", "LK", "SH", "KN", "LC", "PM", "VC", "ZA", "SD", "GS",
"KR", "SS", "SR", "SJ", "SY", "TJ", "TW", "TZ", "TH", "TG", "TK", "TO",
"TT", "TD", "CZ", "TN", "TR", "TM", "TC", "TV", "UG", "UA", "HU", "UY",
"UZ", "VU", "VA", "VE", "AE", "US", "GB", "VN", "WF", "CX", "EH", "CF",
"CY"

### Übermittlung an GSA

Eingabe für das Feld „Der Test wurde vom Gesundheitsamt veranlasst und
ich stimme der Übermittlung meiner Daten an das Gesundheitsamt zu.“. 1
bedeutet „Ja“, 0 bedeutet „Nein“.

### Ersttestung

Eingabe für das Feld „Das ist meine Erst-Testung“. 1 bedeutet „Ja“, 0
bedeutet „Nein“

### Symptome

Eingabe für das Feld „Ich habe Corona Symptome“. 1 bedeutet „Ja“, 0
bedeutet „Nein“

## Tipp

Sie können bei Email und Telefon auch überall die gleiche Nummer
angeben, um die Ergebnisse zentral an einer Stelle zu sammeln und dann
mit dem CovidTestOutlook-Collector
(<https://github.com/Wulfheart/CovidTestSammler>) auszuwerten.

# Verwendung des Programms

Starten Sie das Reihentestungstool wie Sie jedes normale andere Programm
auch starten würden.

<img src="media/image2.png" style="width:6.29931in;height:3.76042in" />Die
Ansicht ist folgendermaßen:

Wählen Sie die Excel-Datei mit Ihren Daten aus, indem Sie auf „Excel
Datei auswählen“ klicken. Unter „Anzahl an Testungen“ können Sie
auswählen, wie oft ein Code pro Person erstellt werden soll. Dies kann
unter anderem nützlich sein, wenn Sie mit mehreren Reihentestungen
rechnen. Die Zahl muss mindestens 1 sein.

Klicken Sie nun auf den Button „Erstellen“. Es öffnet sich ein Fenster,
in dem Sie auswählen müssen, wo Sie das PDF-Dokument mit allen
generierten Codes speichern wollen.

Daraufhin beginnt das Programm zu arbeiten. Falls Fehler auftreten,
werden Sie ausgegeben. Zusätzlich wird der Fortschritt in einem Balken
angezeigt und unten im Log wird auch gezeigt, was genau das Programm
gerade macht. Falls Sie sich mit Fehlern an den Entwickler melden,
inkludieren Sie bitte diese Meldungen.

Wenn das Programm fertig ist, dann erscheint ein Dialogfenster und
Lognachricht mit dem Text „Vorgang abgeschlossen. PDF erstellt unter
/Pfad/zu/Ihrer.pdf.
