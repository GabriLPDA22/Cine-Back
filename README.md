# Cine-Back 🎬

Este proyecto es el backend de un sistema de gestión de cine desarrollado en **C#**, diseñado para ejecutarse en un entorno Dockerizado. Se encuentra implementado en una instancia de **AWS EC2** configurada a través de **Cloud9**. El backend está preparado para realizar operaciones **fetch** y viene con todo lo necesario para desplegarse fácilmente.

---

## 🚀 Tecnologías utilizadas

- **C#**: Lenguaje principal del proyecto.
- **.NET Core**: Framework utilizado para construir la API.
- **Swagger**: Documentación interactiva de la API.
- **Docker**: Contenedorización para garantizar la portabilidad del proyecto.
- **AWS EC2**: Infraestructura para la implementación del sistema.
- **Cloud9**: Entorno de desarrollo en la nube para gestionar y ejecutar el proyecto.

---

## 🌐 Despliegue en AWS

Este proyecto está configurado para ejecutarse en una instancia de **AWS EC2**. Gracias a **Cloud9**, puedes gestionar fácilmente el código y el entorno sin necesidad de configuraciones adicionales en tu máquina local.

### ¿Qué hace este proyecto único?

- **Contenedorizado con Docker**: Todo lo necesario para ejecutar el backend está empaquetado en un contenedor Docker, garantizando un entorno consistente.
- **Soporte para Fetch**: Preparado para realizar solicitudes HTTP desde el frontend u otros servicios.
- **Swagger**: Documentación interactiva lista para usar, accesible desde cualquier navegador.

---

## 🛠️ Requisitos previos

Asegúrate de que tu instancia de **AWS EC2** tenga lo siguiente instalado y configurado:

1. **Docker**: Para ejecutar contenedores.
2. **Cloud9**: Para gestionar y editar el código.
3. **.NET SDK**: Necesario para construir y ejecutar el proyecto (opcional si no usas Docker directamente).

### Cómo instalar Docker en tu instancia EC2

Si Docker no está instalado, ejecuta lo siguiente en tu terminal de **Cloud9**:

```bash
sudo apt-get update
sudo apt-get install -y docker.io
sudo usermod -aG docker $USER
