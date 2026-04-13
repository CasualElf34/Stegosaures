# 🦕 Stegosaures

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download)
[![Avalonia UI](https://img.shields.io/badge/Avalonia-11.3-ed2d7b?logo=avalonia)](https://avaloniaui.net/)
[![SQLite](https://img.shields.io/badge/SQLite-3.0-003B57?logo=sqlite)](https://www.sqlite.org/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

**Stegosaures** est une application desktop moderne et performante spécialisée dans la stéganographie d'image. Elle permet de dissimuler des messages textuels au sein d'images PNG en utilisant la technique du **Least Significant Bit (LSB)**.

---

## ✨ Fonctionnalités

- 🔐 **Encodage Sécurisé** : Cachez n'importe quel message texte dans une image PNG sans altération visible à l'œil nu.
- 🔓 **Décodage Rapide** : Extrayez instantanément les messages cachés des images traitées.
- 📊 **Historique Local** : Suivez l'ensemble de vos opérations (encodage/décodage) grâce à une base de données SQLite intégrée.
- 🎨 **Interface Moderne** : Développée avec Avalonia UI, offrant une expérience fluide et un design élégant.
- 🚀 **Multiplateforme** : Compatible avec Windows, macOS et Linux.

---

## 🛠️ Stack Technique

- **Langage** : C# 13 / .NET 10
- **Framework UI** : [Avalonia UI](https://avaloniaui.net/) (Cross-platform XAML framework)
- **State Management** : MVVM Pattern (CommunityToolkit.Mvvm)
- **Base de données** : Entity Framework Core + SQLite
- **Algorithme** : Bit manipulation (LSB) sur les canaux BGR.

---

## 🔬 Comment ça marche ? (LSB Steganography)

L'algorithme utilise le bit de poids faible (le bit le plus à droite) de chaque canal de couleur (Bleu, Vert, Rouge) pour stocker les bits du message.

- Chaque pixel peut stocker **3 bits** d'information (1 par canal).
- L'altération d'un seul bit (0 ou 1) modifie la couleur de manière imperceptible pour l'œil humain (variation de ±1/255).
- Le message est terminé par un caractère nul (`\0`) pour marquer la fin lors du décodage.

---

## 🚀 Installation & Lancement

### Prérequis

- [SDK .NET 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) ou supérieur.
- Un IDE comme Visual Studio 2022, JetBrains Rider ou VS Code.

### Installation

1. Clonez le dépôt :
   ```bash
   git clone https://github.com/votre-compte/stegosaures.git
   cd stegosaures
   ```

2. Restaurez les dépendances :
   ```bash
   dotnet restore
   ```

3. Lancez l'application :
   ```bash
   dotnet run --project SteganographyLSB.csproj
   ```

---

## 📱 Utilisation

1. **Encode** :
   - Choisissez une image source (.png recommandé).
   - Saisissez votre message secret.
   - Cliquez sur **Encode** et enregistrez l'image résultante.
2. **Decode** :
   - Importez une image contenant un message caché.
   - Cliquez sur **Decode** pour révéler le secret.
3. **History** :
   - Consultez la liste de vos anciennes opérations.

---

## 📁 Structure du Projet

```text
├── Converters/      # Logic de conversion pour l'UI
├── Models/          # Entités de données et DB Context (SQLite)
├── Services/        # Cœur de l'application (Algo LSB)
├── ViewModels/      # Logique métier et navigation
├── Views/           # Interfaces XAML (Avalonia)
└── history.db       # Base de données locale
```

---

## 👨‍💻 Auteurs

Projet réalisé dans le cadre du module **Cyber-Sécurité**.
- Groupe d'étudiants (Mael G. & Team)

---

## 📄 Licence

Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.
*Note : Projet à but pédagogique développé pour le cours Court-26-27.*