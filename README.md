# Лабораторная работа 7. PostgreSQL и Entity Framework Core

Предметная область: авиакомпания (сущность Flight).
Технологии: ASP.NET Core Minimal API, Entity Framework Core (Code First), PostgreSQL, Docker, Render.

## Таблица API

| Метод | URL | Назначение |
|-------|-----|------------|
| GET | / | проверка приложения |
| GET | /api/info | информация о приложении |
| GET | /api/flights | получить все рейсы |
| GET | /api/flights/{id} | получить рейс |
| GET | /api/flights/search?search=... | поиск по городу назначения |
| POST | /api/flights | создать рейс |
| PUT | /api/flights/{id} | изменить рейс |
| DELETE | /api/flights/{id} | удалить рейс |
| GET | /api/flights/expensive | LINQ: рейсы дороже 100 |
| GET | /api/flights/sorted | LINQ: сортировка по цене |
| GET | /api/flights/available | LINQ: доступные рейсы |
| GET | /api/flights/from/{origin} | LINQ: рейсы из города |

## Миграции

1. InitialCreate
2. AddCreatedAt
3. SeedFlights
4. AddDescription

Миграции применяются автоматически при запуске приложения.

## Пример запроса (POST /api/flights)

```json
{
  "number": "B2-606",
  "origin": "Minsk",
  "destination": "Paris",
  "price": 999,
  "isAvailable": true
}
```

## Запуск

```
dotnet run
```

Строка подключения к базе берётся из `ConnectionStrings:DefaultConnection`
(локально) или из переменной окружения `DATABASE_URL` (на Render).

## Ссылки

GitHub:
https://github.com/threes3xs3x/wt-lab7-7postgres-efcore-sudinko

Render:
https://wt-lab7-7postgres-efcore-sudinko.onrender.com