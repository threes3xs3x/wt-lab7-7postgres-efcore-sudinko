# Лабораторная работа 6. REST API (Minimal API)

Предметная область: авиакомпания (сущность Flight).

## Таблица API

| Метод | URL | Назначение |
|-------|-----|------------|
| GET | /api/flights | получить список |
| GET | /api/flights/{id} | получить объект |
| POST | /api/flights | создать объект |
| PUT | /api/flights/{id} | изменить объект |
| DELETE | /api/flights/{id} | удалить объект |

## Пример запроса (POST /api/flights)

```json
{
  "number": "B2-606",
  "origin": "Minsk",
  "destination": "Paris",
  "price": 350,
  "isAvailable": true
}
```

## Пример ответа (201 Created)

```json
{
  "id": 6,
  "number": "B2-606",
  "origin": "Minsk",
  "destination": "Paris",
  "price": 350,
  "isAvailable": true
}
```


## Запуск

```
dotnet run
```