# Sistema Gestor de Clientes

## 1. Integrantes del Grupo
-Franco Joel Coward Corrales 
-Alejandro Rodriguez Fuentes 
-Paulo Cesar Vargas Valenciano
-Jexon Salazar Cruz 
## 2. Repositorio del Proyecto

Enlace al repositorio:
https://github.com/FrancoCoward99/PP1_SC-701_GrupoF
## 3. Especificación Básica del Proyecto

### 3.a Arquitectura del Proyecto

El sistema fue desarrollado utilizando una arquitectura por capas, con el objetivo de garantizar separación de responsabilidades, mantenibilidad y escalabilidad.
La solución contiene tres proyectos principales:

### Capa de Presentación UI
Proyecto: `PP1_SC-701_GrupoF`
- Implementado con ASP.NET Core MVC
- Contiene:
  - Controllers
  - Views
  - JavaScript (DataTables + AJAX)
- Maneja la interacción con el usuario
- Consume los servicios de la capa de negocio

### Capa de Lógica de Negocio (BLL)
Proyecto: `PP1_SC-701_GrupoFBLL`
Contiene:
- DTOs (ClienteDto, TelefonoDto,)
- Servicios (ClienteServicio)
- Interfaces (IClienteServicio)
- Validaciones de negocio
- Reglas como:
  - Identificación obligatoria
  - Identificación única
  - Identificación alfanumérica
  - Validación de formato de teléfono

Esta capa aplica las reglas del negocio antes de interactuar con la base de datos.
###  Capa de Acceso a Datos (DAL)
Proyecto: `PP1_SC-701_GrupoFDAL`
Contiene:
- Entidades (Cliente, Telefono)
- Repositorios
- Interfaces (IClienteRepositorio)
- Métodos CRUD

Esta capa se encarga exclusivamente del acceso a datos.
La UI nunca accede directamente a la base de datos, respetando la separación por capas.
## 3.b Libraries / Paquetes NuGet Utilizados

- Microsoft.AspNetCore.Mvc
- AutoMapper
- AutoMapper.Extensions.Microsoft.DependencyInjection
- jQuery
- DataTables
- SweetAlert2
## 3.c Principios SOLID Aplicados

###  Single Responsibility Principle (SRP)
Cada clase tiene una única responsabilidad:
- ClienteServicio → reglas de negocio
- ClienteRepositorio → acceso a datos
- Controller → manejo de solicitudes HTTP
### Open/Closed Principle (OCP)
El sistema puede extenderse mediante nuevas implementaciones de interfaces sin modificar las clases existentes.
### Liskov Substitution Principle (LSP)
### Interface Segregation Principle (ISP)
Se utilizan interfaces específicas como:
- IClienteServicio
- IClienteRepositorio
### Dependency Inversion Principle (DIP)
Las dependencias se inyectan mediante constructor (Dependency Injection), evitando dependencias directas entre capas.
## Patrones de Diseño Utilizados
### Repository Pattern
Encapsula el acceso a datos y abstrae la base de datos del resto del sistema.
### Service Layer Pattern
La lógica de negocio se centraliza en la capa BLL.
###DTO Pattern
Se utilizan objetos de transferencia de datos para desacoplar las entidades del modelo de presentación.
## Consideraciones Generales

- Se respetan convenciones de naming:
  - Clases: PascalCase
  - Métodos: PascalCase
  - Variables: camelCase
- Correcta indentación y organización por carpetas.
- Separación clara entre capas.
- Validaciones implementadas en la capa de negocio.
- Uso de AJAX para mejorar experiencia de usuario sin recargar la página.
## Funcionalidades Implementadas
- Listado de clientes
- Ver detalle de cliente
- Registrar cliente
- Modificar cliente
- Eliminar cliente
- Agregar teléfonos
- Eliminar teléfonos
- Validaciones de negocio (identificación única y alfanumérica)
