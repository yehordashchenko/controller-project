# Nástroj na testovanie vstupu z ovládačov (Unity)

## Popis

Tento projekt predstavuje jednoduché interaktívne prostredie vytvorené v Unity, ktorého cieľom je testovanie a analýza vstupných zariadení, ako sú herné ovládače, klávesnica a myš.

Aplikácia umožňuje sledovať spracovanie vstupu a porovnávať správanie rôznych zariadení v rámci jednej hry.

Projekt obsahuje implementáciu vlastnej abstrakčnej vrstvy vstupu, ktorá oddeľuje spracovanie vstupu od hernej logiky.

---

## Funkcionalita

Aplikácia umožňuje:

- pohyb postavy a ovládanie kamery
- streľbu a interakciu so scénou
- spracovanie dead zón (Axial, Radial, Scaled Radial)
- vizualizáciu vstupu z analógových stickov
- testovanie driftu analógových stickov
- detekciu aktuálneho vstupného zariadenia
- nastavenie:
  - veľkosti dead zóny
  - citlivosti (sensitivity)
  - vibrácií (intenzita a trvanie)

---

## Ovládanie

### Klávesnica a myš
- WASD – pohyb
- Myš / Šipky – kamera
- Medzerník – skok
- Ľavé tlačidlo myši / Ľavý Control – streľba

### Ovládač
- Ľavý stick – pohyb
- Pravý stick – kamera
- Tlačidlo A / Cross – skok
- RT / R2 – streľba

---

## Požiadavky

- Unity verzia 2022 alebo novšia (pre otvorenie projektu)
- Windows OS
- Herný ovládač (odporúčané)

---

## Spustenie projektu

### Možnosť 1 – Unity projekt
1. Stiahnite alebo naklonujte repozitár
2. Otvorte projekt v Unity Hub
3. Otvorte hlavnú scénu
4. Spustite tlačidlom **Play**

### Možnosť 2 – Spustiteľný súbor (.exe)
1. Build je dostupný na stiahnutie v sekcii **Releases** tohto repozitára.
2. Spustite súbor `.exe`

---

## Štruktúra projektu

- `PlayerController` – herná logika
- `InputReader` – abstrakčná vrstva vstupu
- `TargetStand` – interaktívne objekty na testovanie
- `StickVisualizer` – vizualizácia vstupu
- `InputDebugPanel` – zobrazovanie údajov o vstupe

---

## Technický popis

Projekt využíva Unity Input System a implementuje vlastnú vrstvu spracovania vstupu:
Input System → InputReader → PlayerController → Game Logic

Tento prístup umožňuje jednoduchú podporu rôznych zariadení bez nutnosti meniť hernú logiku.

---

## Autor - Yehor Dashchenko

Študentský projekt – praktická časť bakalárskej práce

---

## Poznámka

Projekt slúži na vzdelávacie a testovacie účely.
