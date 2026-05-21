# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Nueva tabla `rolesF` en la base de datos (`SQL/crear_tabla_rolesF.sql`): catálogo de 12 roles de flujo (UNIDAD, DIR. ADM, PRESUPUESTO, LEGAL, SAP, CONTROL FISCAL, PRESIDENCIA, COTIZADOR, COMPRAS, VICE PRESIDENCIA, SECRETARIA GENERAL, CONTRATACIONES). Replica la estructura de `etapas_solicitudes` para modelar el flujo de aprobación por rol funcional.
- Columna `idRolesF` agregada a la tabla `usuaruios` (`SQL/agregar_rolesF_a_usuarios.sql`): FK nullable hacia `rolesF` con `ON DELETE SET NULL` / `ON UPDATE CASCADE`. Permite asignar un rol de flujo a cada usuario sin romper registros existentes.

### Security
- Corregida falla de control de acceso en el Stored Procedure `login`: usuarios con `idEstatus = 2` (Inactivo) podían autenticarse correctamente porque el WHERE del SP solo validaba credenciales (`idUsuaruios` + `passwd`) sin filtrar por estatus. Se agrega la condición `AND idEstatus = 1` al SP. Script de corrección: `SQL/fix_login_idEstatus.sql`.

## [released]

## [1.0.4] - 2026-05-20

### Refactor
- Refactorizadas 375 excepciones silenciadas (`catch(Exception){}`) distribuidas en 30 archivos del sistema. Se implementó logging centralizado mediante la nueva clase estática `Controlers/ErrorHandler.cs` usando `System.Diagnostics.Trace`, protección de transacciones con `BeginTransaction()`/`Commit()`/`Rollback()` en operaciones multi-paso de BaseDatos (6 métodos en 3 vistas), diferenciación entre "sin resultados" y "error de consulta" en métodos `lista*`/`datos*`, y mensajes de error descriptivos al usuario en español. El único catch preservado sin modificar es el de `Mailer.cs` por diseño.

## [1.0.3] - 2026-05-13

### Fixed
- Resuelto error crítico de timeout (pantalla en blanco o "Fatal error encountered attempting to read the resultset") en la vista de Administrador (`viewsAdmin/busquedaDocumentos.aspx`). Se optimizó el Stored Procedure `filtroSolicitudes` eliminando la variable temporal `@ids` en la subconsulta para evitar N+1 queries y permitir el uso de índices por el optimizador de MySQL.
- Se incrementó preventivamente el tiempo de espera (`Cmd.CommandTimeout = 300;`) en el constructor de `Controlers/BaseDatos.cs` para evitar bloqueos en consultas pesadas sin filtros.
- Corregido bug de responsividad en la tabla de solicitudes de `viewsAdmin/busquedaDocumentos.aspx`: la tabla desbordaba el ancho de la vista y la columna "Titulo" no hacía salto de línea. Causas: (1) el wrapper externo del `ViewTabla` usaba `container-fluid p-0` en lugar de `table-responsive` (clase Bootstrap que aplica `overflow-x: auto` para scroll horizontal), y (2) la regla CSS `.word-break-cell` tenía `min-width: 120px` que forzaba ancho mínimo en la columna Titulo empeorando el desbordamiento. Se alineó la estructura con la vista de referencia `viewsGeneral/busqueda.aspx` que ya maneja responsividad correctamente.

## [1.0.2] - 2026-05-07

### Added
- Nuevo motor de documentos basado en plantillas Word: clase `Controlers/ReporteComprasDocx.cs` que procesa archivos `.docx` con marcadores `{{CAMPO}}`, genera tablas dinamicas de renglones y exporta a HTML embebible via `HtmlFormatProvider` de Telerik Document Flow. Soporta fallback automatico con generacion de `plantilla_default.docx` si no existe una plantilla activa en el sistema.

- Modulo administrativo de gestion de plantillas (`viewsAdmin/PlantillaSolicitud.aspx`): permite al administrador subir archivos `.docx` personalizados, ver el historial de plantillas y generar la plantilla por defecto. Cada nueva plantilla subida se marca automaticamente como "activa" y las futuras solicitudes la utilizaran.

- Metodos de acceso a datos en `BaseDatos.cs` (`guardarPlantillaSolicitud`, `obtenerPlantillaActiva`, `listaPlantillasSolicitud`) para persistir y consultar plantillas desde la tabla `plantillas_solicitud`.

- Link de navegacion "Plantilla Solicitud" en el menu lateral del panel de administracion (`superAdmin.master`).

- Script SQL `crear_tabla_plantillas.sql` con la definicion de la tabla `plantillas_solicitud` y los procedimientos almacenados necesarios para gestion de plantillas.

### Changed
- Migrada la generacion de documentos de Solicitud de Bienes desde Telerik Spreadsheet (PDF hardcodeado) a plantillas Word (.docx) dinamicas en los 9 archivos de vistas de estados: `RegistroSolicitud`, `porAprobar`, `busqueda`, `Enviados`, `EnProceso`, `Asignado`, `Entregado`, `PorRecibir` y `busquedaDocumentos` (admin). El codigo Telerik original permanece comentado (no eliminado) para facilitar reversion si fuera necesario.

- Actualizado `sitemaCompras.csproj`: registrados los nuevos archivos (`ReporteComprasDocx.cs`, `PlantillaSolicitud.aspx`, `PlantillaSolicitud.aspx.cs`, `PlantillaSolicitud.aspx.designer.cs`) y agregadas las referencias a `WindowsBase` y `PresentationCore` requeridas por la API de Telerik Document Flow.

### Fixed
- Corregido bug donde los renglones de la tabla (`{{TABLA_RENGLONES}}`) no se renderizaban en el PDF. El sistema ahora usa doble estrategia: (A) reemplaza el marcador de texto si existe, (B) detecta la tabla por sus encabezados (RENGLON, CANTIDAD, DESCRIPCION) y llena las filas vacias con los datos del DataSet.

- Corregido bug donde `{{DESCRIPCION_USO}}` se acumulaba en una sola celda/parrafo ignorando la estructura de la tabla. El nuevo metodo `BuscarDescripcionEnTabla` detecta la fila con el marcador, la limpia, y distribuye el texto en las filas vacias de ancho completo que estan debajo usando `DistribuirEnChunks` para repartir el texto proporcionalmente por renglon (efecto texto escrito sobre lineas).

- Corregidos caracteres ilegibles (mojibake) en `PlantillaSolicitud.aspx`: reemplazados emojis UTF-8 por texto ASCII estandar y corregida estructura HTML.

- Recreado `ReporteComprasDocx.cs` tras ser eliminado accidentalmente del proyecto. Reconstruido con todos los fixes integrados.

### Deprecated
- La generacion de PDF mediante Telerik RadSpreadProcessing (`ReporteCompras.createexcellficha` + `PdfFormatProvider`) ha sido reemplazada por el nuevo motor de plantillas Word. El codigo legado esta comentado en todos los archivos afectados y puede reactivarse eliminando los bloques de comentario `/* ... */`.

## [1.0.1] - 2026-05-03

### Added
- Creado `viewsAdmin/busquedaDocumentos.aspx` y `busquedaDocumentos.aspx.cs`: modulo de busqueda y gestion de solicitudes exclusivo para el panel de administracion (tipos de usuario 1 y 7). Replica la funcionalidad de `viewsGeneral/busqueda.aspx` pero usa `superAdmin.master` como master page fija, elimina el `Page_PreInit` dinamico que causaba conflictos de layout, y permite acceso irrestricto a todas las solicitudes sin filtros por unidad o usuario.

### Changed
- Mejorado el manejo de excepciones en `Controlers/BaseDatos.cs` (metodo `gestionarUsuarios`) y `viewsAdmin/Users.aspx.cs` (`btnRegistrar_Click`) para propagar y mostrar el mensaje de error real de la base de datos en los alertas de diagnostico, reemplazando el error silencioso ("Error al ingresar al proceso").

### Fixed
- Corregido error HTTP 404 en el menu lateral del panel de administracion (`superAdmin.master`): el enlace "Solicitudes" ahora apunta a la pagina `busquedaDocumentos.aspx` que existe en `viewsAdmin/`.

- Resuelto error al crear usuarios donde la capa de datos fallaba por el error `The user specified as a definer ('root'@'%') does not exist`. Se corrigieron masivamente todos los Stored Procedures para que usen `DEFINER='root'@'localhost'` via `fix_procedimientos.sql`.

- Resuelto problema al crear una nueva solicitud en `RegistroSolicitud.aspx` (error "No se pudo salvar la el registro") causado por una violacion de restriccion not-null en la base de datos. archivo: `fix_procedimientos.sql`

- Resuelto error al intentar guardar un nuevo renglon de contenido para las solicitudes (mensaje "No se pudo salvar la el renglon"). Se reescribio el Procedimiento Almacenado `gestionarDetallesSolicitudes`. archivo: `update_sp.sql`

## [1.0.0] - 2026-04-01

### Added
- Primera version del sistema `sitemaCompras` (Solicitud de Bienes) subida al repositorio.
- Modulo de creacion de solicitudes (`RegistroSolicitud.aspx`) para roles Solicitante (tipo 2) y Solicitante-Aprobador (tipo 3).
- Modulo de aprobacion de unidad (`porAprobar.aspx`) con control por `idAutorizaciones` para tipo 2 y botones siempre activos para tipo 3.
- Modulo de gestion post-aprobacion (`Enviados.aspx`) para Recepcion de Compras (tipo 4), Cotizador (tipo 5) y Recepcion final (tipo 6).
- Modulo de busqueda avanzada de solicitudes (`viewsGeneral/busqueda.aspx`).
- Panel de administracion (`viewsAdmin`) con gestion de usuarios, unidades, directores administrativos, proveedores, etapas, estatus y tipos de solicitud.
- Sistema de autenticacion por formularios con redireccion por tipo de usuario.
- Generacion de reportes en PDF usando Telerik RadSpreadProcessing.
- Sistema de chat/comentarios por solicitud con notificaciones por correo (`Mailer.cs`).
- Soporte para adjuntar archivos PDF a las solicitudes.
- Documentacion tecnica: diagnostico de flujo de trabajo (`diagnostico_flujo_solicitudes.md`) y flujo propuesto (`Flujo de Trabajo de Solicitud.md`).

[Unreleased]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.4...HEAD
[1.0.4]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.3...v1.0.4
[1.0.3]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.2...v1.0.3
[1.0.2]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/Drek003/S.Bienes-Compras/releases/tag/v1.0.0
