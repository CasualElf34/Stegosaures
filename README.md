Stegosaures
Application de Stéganographie LSB (Projet Cyber)
Ce projet est une application desktop multiplateforme (Windows, Linux, Mac) développée en C# avec Avalonia UI. Elle permet de cacher un message dans une image (encodage) ou d’extraire un message caché (décodage) grâce à la technique du Least Significant Bit (LSB).

Fonctionnalités principales
Encodage LSB : Cachez un message texte dans une image PNG.
Décodage LSB : Retrouvez un message caché dans une image PNG.
Interface moderne : Navigation simple entre les pages d’encodage et de décodage.
Aucune connexion requise : Application 100% locale.
Installation
Prérequis
.NET 10.0 SDK ou supérieur (télécharger ici)
Windows, Linux ou MacOS
Récupérer le projet
Lancer en mode développement
Générer l’exécutable Windows
L’exécutable se trouvera dans bin/Release/net10.0/win-x64/publish/SteganographyLSB.exe

Utilisation
Encode : Sélectionnez une image source, saisissez le message à cacher, puis encodez.
Decode : Sélectionnez une image encodée, puis extrayez le message caché.
Structure du projet
Views : Pages Encode et Decode
ViewModels : Logique de présentation
Services : Service LSB (encodage/décodage)
Models : Modèles de données
Converters : Converters pour l’UI
Auteurs
Projet réalisé dans le cadre du module Cyber
Groupe de 2 à 3 personnes
Ressources utiles
Documentation Avalonia
Documentation C#
Licence
Projet pédagogique, usage non commercial.