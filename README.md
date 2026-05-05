# CatWatch

API REST para gestión de gatos, construida con ASP.NET Core 10.

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Ejecutar

```bash
dotnet run --project CatWatch
```

La API queda disponible en `https://localhost:7xxx` (el puerto exacto se muestra en consola).

## Endpoints

| Método | Ruta        | Descripción          |
|--------|-------------|----------------------|
| GET    | /api/cats   | Lista todos los gatos |

## Documentación

En modo desarrollo, la especificación OpenAPI está disponible en `/openapi/v1.json`.
