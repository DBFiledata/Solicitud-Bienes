# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Agregada la vista **En Proceso** (`EnProceso.aspx`) en el menú de navegación lateral para los usuarios de Recepción Final / Otras Recepciones (`NestedMasterOtrasRecepciones.master`), permitiendo que el rol de Presupuesto (Paso 8 y 10) y SAP (Paso 11) accedan a esta vista para realizar sus respectivas acciones del flujo.
- Implementado el Flujo de Presupuesto y Preparación de O/C (Fase 2/4, Pasos 8 al 12) en `EnProceso.aspx.cs` habilitando selectivamente paneles de acciones, filtrando dinámicamente las opciones de etapas y estatus especiales según el paso actual (Paso 8 y 10 para Presupuesto, Paso 9 y 12 para Analista, Paso 11 para SAP), y permitiendo que SAP registre la Orden de Compra en el panel de proveedor sin errores de autorización.

### Fixed
- Reestructurado el flujo del Paso 5 (Supervisor de Cotizadores) y Paso 6 (Analista de Compras) resolviendo el orden invertido de las acciones: la confirmación de atención se realiza ahora en Por Recibir (`Asignado.aspx`) del Paso 5 manteniendo la solicitud en etapa 8 (Cotizador) y estatus 14 (En atención), mientras que la asignación de cotizadores se realiza en En Proceso (`EnProceso.aspx`). Al confirmar el cotizador asignado, el registro avanza automáticamente al Paso 6 (etapa 9, estatus 4 "Enviado").
- Corregido bug en el selector de cotizadores del Paso 5 (`dListaCotizadores`): se modificó la consulta en el Stored Procedure `dListaCotizadores` para que filtre por `idTipo = 4` and `idRolesF = 9` (Analistas de Compras) en lugar de `idTipo = 5` (Supervisores), asegurando que el Supervisor asigne correctamente a los analistas encargados de cotizar en el Paso 6.
- Ajustado el Paso 6 (Analista de Compras, `idTipo: 4`, `idRolesF: 9`) para recibir la solicitud en su vista `PorRecibir.aspx` y gestionar los proveedores en `EnProceso.aspx` habilitando el panel `panelAsignarProveedor`.
- Restringidas las bandejas `PorRecibir.aspx` y `EnProceso.aspx` para que los Analistas de Compras (`rolesF == 9`) visualicen de forma exclusiva únicamente aquellas solicitudes que tengan asignadas a su ID de usuario (`idCotizadores`), evitando accesos a bandejas genéricas del rol.
- Corregido el filtrado de la vista En Proceso (`EnProceso.aspx.cs`) para el Supervisor de Cotizadores (`rolesF == 8`) de manera que visualice todas las solicitudes activas de la etapa 8 en curso, sin filtrar por su propio ID de usuario cotizador.
- Corregido bug de filtrado por unidad en la vista de solicitudes **En Proceso** ([EnProceso.aspx.cs](file:///C:/Users/derek/Documents/S.B/Solicitud-Bienes/sitemaCompras/viewsGeneral/EnProceso.aspx.cs)) para los roles funcionales globales (Presupuesto `rolesF == 3` y SAP `rolesF == 5`), asegurando que no se restrinjan por la unidad del usuario activo y puedan ver solicitudes de cualquier departamento de la institución.
- Corregida excepción `System.InvalidCastException` ("Object cannot be cast from DBNull to other types") al presionar el botón 'Enviar' o 'Estatus Especiales' en [EnProceso.aspx.cs](file:///C:/Users/derek/Documents/S.B/Solicitud-Bienes/sitemaCompras/viewsGeneral/EnProceso.aspx.cs#L2327) al evaluar `row["idEstatus"]` cuando vale `DBNull.Value` (mensajes del chat común sin estatus asociado).
- Ocultado el botón 'Enviar ✉️' (`btnSiguienteEtapa`) en la vista **En Proceso** (`EnProceso.aspx.cs`) para el Supervisor de Cotizadores (`type == 5` / `rolesF == 8`), obligando a que la transición de la solicitud hacia la etapa 9 (Paso 6) se realice de forma automática y exclusiva al confirmar la asignación del cotizador en el modal respectivo.
- Corregido el refresco y carga de comentarios en la sección de **Mensajes y alertas** en [EnProceso.aspx.cs](file:///C:/Users/derek/Documents/S.B/Solicitud-Bienes/sitemaCompras/viewsGeneral/EnProceso.aspx.cs#L1844) limpiando los controles dinámicos de `panelChat` antes de renderizar para evitar duplicados o fallos de pintado entre postbacks de la misma página.
- Corregido el flujo de aprobación del DGAF Vo. Bo. (`userRoleF == 2`) en [porAprobar.aspx.cs](file:///C:/Users/derek/Documents/S.B/Solicitud-Bienes/sitemaCompras/viewsGeneral/porAprobar.aspx.cs#L958) para derivar condicionalmente según la fase de la solicitud: en la primera vuelta (Paso 3) la envía al Jefe de Compras (Paso 4, etapa `9`), y en la segunda vuelta (Paso 7b) la envía a Presupuesto (Paso 8, etapa `3`), evitando que se estanque el flujo entre los pasos 3 y 4.
- Corregido problema de botones duplicados en la vista de solicitudes **En Proceso** ([EnProceso.aspx.cs](file:///C:/Users/derek/Documents/S.B/Solicitud-Bienes/sitemaCompras/viewsGeneral/EnProceso.aspx.cs)) para todos los roles reestructurando `Page_Load` para configurar la visibilidad de los paneles en cada petición (incluyendo postbacks) en lugar de hacerlo únicamente en la carga inicial.
- Habilitada la opción **SAP** en el modal de despacho del Paso 10 — Presupuesto al corregir el procedimiento almacenado `gestionarComentariuosSolicitudes` en `COMPRAS_AN-SPs.sql` (y aplicando la actualización a la base de datos) para que inserte el parámetro de estado en la columna `idEstatus` de la tabla `comentariuos_solicitudes`, permitiendo que la validación de `tieneReserva` funcione correctamente.
- Corregida la redirección en la aprobación del Jefe de Compras (`userRoleF == 9`) en `porAprobar.aspx.cs` para que derive dinámicamente según la fase del flujo de la solicitud: a la etapa `8` (Supervisor de Cotizadores) en la primera vuelta (cuando no hay cotizador asignado) o a la etapa `2` (DGAF Vo. Bo. / Paso 7) en la segunda vuelta.
- Simplificado el filtro de bandejas de solicitudes en `Enviados.aspx.cs` para ocultar cualquier solicitud que corresponda a la etapa del usuario activo (`rowEtapa == stageName`), previniendo que se muestren en Enviados solicitudes pendientes en su bandeja (como ocurría con DGAF Vo. Bo. en el Paso 7).

## [1.0.7] - 2026-06-19

### Added
- Habilitado el panel de botones de envío (`panelBtnsOthers`) y el modal de selección de etapas (`mpeEtapas`) en `EnProceso.aspx.cs` para el rol de Analista de Compras (Paso 5, `idTipo = 4`, `rolesF = 9`), replicando la misma interfaz de transición de etapas del Paso 4 (Supervisor de Cotizadores).

### Fixed
- Corregido bug de visualización duplicada en el Paso 5 (Analistas de Compras) donde las solicitudes aparecían al mismo tiempo en `PorRecibir`, `EnProceso` y `Enviados`.
- Modificado el método `filtros` en `PorRecibir.aspx.cs` para que por defecto filtre por estatus `4` (Enviado) en lugar de retornar todos los estatus (`null`), y se programó la remoción de solicitudes cuyo último comentario pertenezca al usuario de sesión (evitando que una solicitud ya recibida o enviada vuelva a aparecer en su bandeja de entrada).
- Corregido el filtrado de `EnProceso.aspx.cs` para el rol de Compras (`userRoleF = 9`), apuntando correctamente a la etapa `9` con estatus `15` (Recibido) en lugar de la etapa `8` (Cotizador).
- Agregado filtro en memoria al DataSet de `Enviados.aspx.cs` para ocultar solicitudes que todavía estén bajo la etapa del usuario actual (`rolesF`) con estatus en curso (`Recibido`, `En Proceso`, `En atención`), asegurando que solo aparezcan una vez hayan sido enviadas a la siguiente etapa.
- Resuelta excepción crítica `System.Web.HttpException` ("The IListSource does not contain any data sources") al ingresar a `Enviados.aspx` provocada por un error de MySqlException en el stored procedure `filtroSolicitudesEnviados` (el cual tenía el parámetro `proveedors` en lugar de `proveedor`, generando una falla en la vinculación por nombre de parámetros). Se aplicó el script de migración `SQL/update_sps_desacoplamiento.sql` para restaurar y corregir la firma de los stored procedures en la base de datos local.
- Corregido bug en `porAprobar.aspx.cs` que impedía que una solicitud aprobada por la Dirección Administrativa (`test_DGAF`, `rolesF = 2`) avanzara a la etapa del Jefe de Compras (`rolesF = 9`). Ahora el campo `Session["etapaDocs"]` (etapa destino) se calcula dinámicamente según el rol funcional (`rolesF`) de la sesión.
- Corregida la recarga de grillas en `porAprobar.aspx.cs` (`btnConfirmarPorceso_Click` y `btnEliminarSolicitud_Click`) para que refresquen la bandeja usando los filtros correspondientes al rol funcional actual del usuario de sesión, en lugar de forzar siempre la bandeja de la unidad (`etapa = 1`, `estatus = 2`).
- Configurado que al aprobar una solicitud como Jefe de Compras (`rolesF = 9`), se asigne la etapa 9 (Compras) con el estatus `15` (Recibido). Esto hace que desaparezca de la bandeja de "Por Aprobar" del Jefe de Compras y habilite directamente el botón de "Asignar Cotizador" en `PorRecibir.aspx` para la Recepción de Compras.
- Corregido bug en `Asignado.aspx.cs` que impedía al Supervisor de Cotizadores (`test_Sup_Cotiz`, `idTipo = 5, rolesF = 8`) ver las solicitudes de la etapa 8 en su bandeja de `Asignado.aspx` al no tener un cotizador asignado aún. Ahora los usuarios con rol funcional de cotizador (`rolesF == 8`) pueden ver tanto las solicitudes asignadas a ellos individualmente como aquellas que están pendientes de asignación (sin cotizador registrado).
- Habilitada la visibilidad condicional del botón de asignación (`Button3`) en `Asignado.aspx.cs` para el rol de cotizador (`rolesF == 8`) cuando la solicitud está en estado no asignada, y agregada la recarga automática del grid tras confirmar la asignación del cotizador.

## [1.0.6] - 2026-05-20

Esta versión introduce el desacoplamiento de las solicitudes del usuario creador y del aprobador, vinculándolas directamente a su departamento/unidad de origen.

### Orden de ejecución de scripts para la migración:
1. `SQL/pre_migracion_desacoplamiento.sql` (Ejecutar **antes** de la migración de base de datos)
2. `SQL/migrar_dirc_dep.sql` (Migración de catálogo y estructura de departamentos)
3. `SQL/post_migracion_reasignacion.sql` (Reasignación de unidades a usuarios y solicitudes viejas tras el truncate)
4. `SQL/update_sps_desacoplamiento.sql` (Actualización de los 12 stored procedures modificados)

### Added
- Columna `idUnidades` agregada directamente a la tabla `solicitudes` (`SQL/pre_migracion_desacoplamiento.sql`) para desacoplar históricamente las solicitudes del departamento al que pertenece su creador.
- Tablas temporales de backup `temp_usuaruios_unidades_backup` y `temp_solicitudes_unidades_backup` para facilitar la migración de datos.

### Changed
- Actualizados 12 stored procedures (`SQL/update_sps_desacoplamiento.sql`) para apuntar a la unidad de la solicitud (`solicitudes.idUnidades`) en lugar de resolver la unidad transitivamente a través del usuario (evitando discrepancias históricas si los usuarios cambian de departamento):
  - Filtros y búsquedas modificados: `filtroSolicitudes`, `filtroSolicitudesConfirmadas`, `filtroSolicitudesCotizadorEnproceso`, `filtroSolicitudesEnviados`, `filtroSolicitudesEscaneadasRSalida`, `filtroSolicitudesUnidadEnProceso` y `listaAsignaciones`.
  - Reportes y visualizaciones modificados: `infoSolicitudes` y `reporteBody`.
  - Inserciones y réplicas modificados: `gestionarSolicitudes`, `crearReplicaSolicitud` y `crearResolucionClon`.

### Fixed
- Corregido error de diseño donde el nombre de la unidad emisora en los filtros (`filtroSolicitudes`, `filtroSolicitudesConfirmadas`, etc.) se resolvía mediante el `Aprobador` de la solicitud en lugar del creador original, mostrando departamentos erróneos si el aprobador pertenecía a otra área.

## [1.0.5] - 2026-05-20

### Added
- Nueva tabla `rolesF` en la base de datos (`SQL/crear_tabla_rolesF.sql`): catálogo de 12 roles de flujo (UNIDAD, DIR. ADM, PRESUPUESTO, LEGAL, SAP, CONTROL FISCAL, PRESIDENCIA, COTIZADOR, COMPRAS, VICE PRESIDENCIA, SECRETARIA GENERAL, CONTRATACIONES). Replica la estructura de `etapas_solicitudes` para modelar el flujo de aprobación por rol funcional.
- Columna `idRolesF` agregada a la tabla `usuaruios` (`SQL/agregar_rolesF_a_usuarios.sql`): FK nullable hacia `rolesF` con `ON DELETE SET NULL` / `ON UPDATE CASCADE`. Permite asignar un rol de flujo a cada usuario sin romper registros existentes.

### Security
- Corregida falla de control de acceso en el Stored Procedure `login`: usuarios con `idEstatus = 2` (Inactivo) podían autenticarse correctamente porque el WHERE del SP solo validaba credenciales (`idUsuaruios` + `passwd`) sin filtrar por estatus. Se agrega la condición `AND idEstatus = 1` al SP. Script de corrección: `SQL/fix_login_idEstatus.sql`.

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

[Unreleased]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.7...HEAD
[1.0.7]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.6...v1.0.7
[1.0.6]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.5...v1.0.6
[1.0.5]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.4...v1.0.5
[1.0.4]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.3...v1.0.4
[1.0.3]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.2...v1.0.3
[1.0.2]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/Drek003/S.Bienes-Compras/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/Drek003/S.Bienes-Compras/releases/tag/v1.0.0
