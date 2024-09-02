# Gestión de Adquisiciones

## Contenido
* **AdquisicionesFront** - Contiene los archivos web para la interfaz de usuario, incluyendo HTML, CSS y JavaScript para la gestión de adquisiciones. Aquí se puede encontrar el código para mostrar, crear, modificar y eliminar registros, así como realizar búsquedas y visualizar historiales de cambios.
* **AdquisicionesAPI** - Contiene la API construida en .NET Core que proporciona los endpoints necesarios para la gestión de adquisiciones. Incluye funcionalidades para crear, leer, actualizar y eliminar registros de adquisiciones, así como la visualización del historial de cambios de cada registro.

## Requisitos
- .NET 6.0 SDK o superior
- Navegador web moderno (para la interfaz de usuario)

## Ejecución de la API

1. **Clonar el repositorio:**
    ```bash
    git clone https://github.com/cizaquita/AdquisicionesA.git
    ```

2. **Navegar al directorio de la API:**
    ```bash
    cd AdquisicionesA/AdquisicionesAPI
    ```

3. **Restaurar las dependencias:**
    ```bash
    dotnet restore
    ```

4. **Ejecutar la API:**
    ```bash
    dotnet run
    ```
    La API se ejecutará por defecto en `http://localhost:7062` (puede variar según la configuración).

5. **Probar los Endpoints:**
    - La documentación de Swagger para la API estará disponible en `http://localhost:7062/swagger` una vez que la API esté en ejecución.

## Ejecución del Front-End

1. **Navegar al directorio del front-end:**
    ```bash
    cd AdquisicionesA/AdquisicionesFront
    ```
2. **Configurar apiUrl en el archivo `js/app.js`**
    Editar URL de acuerdo a la configuración, la API se ejecutará por defecto en `http://localhost:7062`

3. **Abrir el archivo `index.html` en un navegador web:**
    Puedes abrir directamente el archivo `index.html` en tu navegador para visualizar la interfaz de usuario.

## Uso

- **Consultar Adquisiciones:** Visualiza los registros existentes y realiza búsquedas usando el campo de filtro en la tabla.
- **Crear Adquisiciones:** Usa el formulario para agregar nuevos registros a la lista.
- **Modificar Adquisiciones:** Edita registros existentes y guarda los cambios.
- **Eliminar Adquisiciones:** Elimina registros no deseados con la opción de eliminar.
- **Ver Historial de Cambios:** Consulta los detalles de las modificaciones realizadas en los registros.

## Contribuciones

Las contribuciones son bienvenidas. Si deseas contribuir al proyecto, por favor sigue estos pasos:

1. Haz un fork del repositorio.
2. Crea una rama con tu nueva funcionalidad o corrección de errores.
3. Envía un pull request describiendo los cambios realizados.

## Documentación Adicional

Para una explicación más detallada del diseño y la funcionalidad de la aplicación, así como para ver ejemplos de uso, consulta el [video de demostración](link-al-video) y la documentación de la API disponible en Swagger.

## Contacto

Para preguntas o comentarios, por favor abre un issue en este repositorio.
