# TaskFlow API

TaskFlow est une API REST développée en ASP.NET Core pour gérer des projets et des tâches. Elle supporte l'authentification JWT, l'accès restreint par utilisateur, et offre une documentation Swagger complète.

##  Stack Technique

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core + SQL Server (LocalDB)
- JWT Authentication
- Swagger / OpenAPI
- Architecture en couches avec Repository pattern

##  Fonctionnalités

### Utilisateurs
- `POST /api/users/register` – Inscription
- `POST /api/users/login` – Connexion (retourne un JWT)

### Projets (authentifié)
- `GET /api/projects` – Liste tous les projets de l'utilisateur
- `POST /api/projects` – Ajout d’un projet
- `GET /api/projects/{id}` – Détail d’un projet
- `PUT /api/projects/{id}` – Modification d’un projet
- `DELETE /api/projects/{id}` – Suppression d’un projet

### Tâches (authentifié)
- `GET /api/tasks` – Liste toutes les tâches de l’utilisateur
- `POST /api/tasks` – Ajout d’une tâche à un projet
- `GET /api/tasks/{id}` – Détail d’une tâche
- `PUT /api/tasks/{id}` – Modification d’une tâche
- `DELETE /api/tasks/{id}` – Suppression d’une tâche

## Authentification

- Utilise JWT (Bearer Token)
- Swagger permet de tester l'API après authentification (`Authorize`)

##  Installation

### Prérequis

- .NET 8 SDK
- SQL Server Express / LocalDB

### Lancer le projet

```bash
git clone https://github.com/zakaribel-dev/TaskFlowSolution.git
cd TaskFlow.WebApi
dotnet run
```

Accéder à Swagger : [http://localhost:5020/swagger](http://localhost:5020/swagger)

##  Test rapide

1. `POST /api/users/register`
2. `POST /api/users/login` → récupérer le token
3. Cliquer sur "Authorize" dans Swagger, coller `Bearer {token}`
4. Utiliser tous les endpoints protégés

