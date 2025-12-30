# CatApi

API REST en .NET (C#) usando Clean Architecture ligera y DDD sencillo.

## Estructura de Carpetas

```
CatApi/
│
├── Api/                  # Controllers y configuración de la API
│   └── Controllers/
│
├── Application/          # Casos de uso, servicios, DTOs
│   ├── Interfaces/
│   └── Services/
│
├── Domain/               # Entidades, contratos, lógica de dominio
│   ├── Entities/
│   └── Contracts/
│
├── Infrastructure/       # Integraciones externas, repositorios, clientes HTTP
│   ├── Clients/
│   └── Repositories/
│
├── Tests/                # Proyecto de pruebas unitarias
│   └── Application/
│
└── CatApi.sln            # Solución principal
```

## Explicación de cada capa

- **Api**:  
  Controladores HTTP, configuración de DI, middlewares. No contiene lógica de negocio.

- **Application**:  
  Servicios (casos de uso), interfaces de dependencias, DTOs. Orquesta la lógica de negocio usando el dominio y las dependencias.

- **Domain**:  
  Entidades, contratos y lógica de negocio pura. No depende de ninguna otra capa.

- **Infrastructure**:  
  Implementaciones concretas de interfaces (repositorios, clientes HTTP, etc). Aquí se integra con la API externa y el repositorio en memoria.

- **Tests**:  
  Proyecto de pruebas unitarias para los servicios de Application, usando mocks para dependencias.

## Principios
- SOLID
- Separación de capas
- Inyección de dependencias
- Repositorio en memoria para usuarios
- Cliente HTTP desacoplado para TheCatApi

## Comenzar
1. Completa la configuración de DI y el startup de la API
2. Implementa los servicios y controladores restantes
3. Agrega lógica de negocio y pruebas unitarias

---

Este proyecto es una base para pruebas técnicas y puede ser extendido según necesidades.
