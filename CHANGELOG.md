# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Creado `viewsAdmin/busquedaDocumentos.aspx` y `busquedaDocumentos.aspx.cs`: módulo de búsqueda y gestión de solicitudes exclusivo para el panel de administración (tipos de usuario 1 y 7). Replica la funcionalidad de `viewsGeneral/busqueda.aspx` pero usa `superAdmin.master` como master page fija, elimina el `Page_PreInit` dinámico que causaba conflictos de layout, y permite acceso irrestricto a todas las solicitudes sin filtros por unidad o usuario.

### Changed
- Mejorado el manejo de excepciones en `Controlers/BaseDatos.cs` (método `gestionarUsuarios`) y `viewsAdmin/Users.aspx.cs` (`btnRegistrar_Click`) para propagar y mostrar el mensaje de error real de la base de datos en los alertas de diagnóstico, reemplazando el error silencioso ("Error al ingresar al proceso").

### Fixed
- Corregido error HTTP 404 en el menú lateral del panel de administración (`superAdmin.master`): el enlace "Solicitudes" ahora apunta a la página `busquedaDocumentos.aspx` que existe en `viewsAdmin/`. El intento anterior de redirigir a `viewsGeneral/busqueda.aspx` causaba pérdida de sesión (redirect a login) por el chequeo de tipos de usuario (2–6) y corrupción del layout por conflicto de master pages.

- Resuelto error al crear usuarios donde la capa de datos fallaba por el error *`The user specified as a definer ('root'@'%') does not exist`*. Se corrigieron masivamente todos los Stored Procedures en el archivo de base de datos (`compras.sql`) para que usen `DEFINER='root'@'localhost'`, parcheando la base de datos activa local a través de `fix_procedimientos.sql`.

## [1.0.0] - 2026-04-01

### Added
- Primera versión del sistema `sitemaCompras` (Solicitud de Bienes) subida al repositorio.
- Módulo de creación de solicitudes (`RegistroSolicitud.aspx`) para roles Solicitante (tipo 2) y Solicitante-Aprobador (tipo 3).
- Módulo de aprobación de unidad (`porAprobar.aspx`) con control por `idAutorizaciones` para tipo 2 y botones siempre activos para tipo 3.
- Módulo de gestión post-aprobación (`Enviados.aspx`) para Recepción de Compras (tipo 4), Cotizador (tipo 5) y Recepción final (tipo 6).
- Módulo de búsqueda avanzada de solicitudes (`viewsGeneral/busqueda.aspx`).
- Panel de administración (`viewsAdmin`) con gestión de usuarios, unidades, directores administrativos, proveedores, etapas, estatus y tipos de solicitud.
- Sistema de autenticación por formularios con redirección por tipo de usuario.
- Generación de reportes en PDF usando Telerik RadSpreadProcessing.
- Sistema de chat/comentarios por solicitud con notificaciones por correo (`Mailer.cs`).
- Soporte para adjuntar archivos PDF a las solicitudes.
- Documentación técnica: diagnóstico de flujo de trabajo (`diagnostico_flujo_solicitudes.md`) y flujo propuesto (`Flujo de Trabajo de Solicitud.md`).

[Unreleased]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/Drek003/S.Bienes-Compras/releases/tag/v1.0.0
