# Orders Api

## Despliegue para pruebas
1. Para correr ambos proyectos se necesita tener Docker instalado en sus máquinas (Revisar la documentación de Docker para la instalación en su SO). 
2. Una vez instalado se debe abrir la carpeta del proyecto en la Terminal/Consola del sistema y ejecutar la siguiente linea de codigo.

``` shell
$ docker-compose up --build
```
3. Detener los contenedores presionando Ctrl + C en su consola.
4. Descomprimir el archivo "data.zip" en la carpeta "data" del directorio del repositorio.
5. Esperar hasta que la base de datos se encuentre completamente desplegada y luego entrar a la direcciones:
   
| Acción                   | Endpoint                             | Método |
| :----------------------- | :----------------------------------- | :----: |
| Listar ordenes           | http://localhost:9200/api/orders     |  GET   |
| Listar items de la orden | http://localhost:9200/api/orderitems |  GET   |
| Listar productos         | http://localhost:9200/api/products   |  GET   |
| Listar proveedores       | http://localhost:9200/api/suppliers  |  GET   |
| Agregar orden            | http://localhost:9200/api/orders     |  POST  |