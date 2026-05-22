Daime Almao, Informatica Seccion "A".

1.¿Por qué un sistema de delivery usa Queue para los pedidos pero Stack para la bitácora? ¿Qué problema surgiría si invertimos las estructuras?

R. Si invertimos las estructuras todo seria un caos, ya que entonces tendriamos los pedidos funcionando con LIFO y la bitacora con FIFO. Lo que significaria que si un cliente ordenase su comida y fuera el ultimo que pidio, le entregarian su comida primero que a la primera persona que llego, lo cual no tendria mucho sentido, pasa lo mismo con la bitacora usando FIFO, si hubo un error en esta, para corregirlo tendriamos que buscar entre toodas las existentes para encontrarla, ya que seria la primera que se ingreso.

2.¿Por qué es obligatorio verificar Count == 0 antes de Dequeue() o Pop()? ¿Qué ocurre en ejecución si se omite?

R. Lo utilizamos para verificar que exista un elemento antes de (por ejemplo) descolarlo, ya que si intentamos descolar un espacio vacio, el programa se romperia y daria un error. 

3.En el método Deshacer, ¿por qué es necesario analizar el texto con .StartsWith() antes de revertir? ¿Qué error lógico evitaría esto?

R. Analizamos primero prara asegurarnos que tipo de accion vamos a deshacer, evitando errores como por ejemplo eliminar algun registro que no existe.

4.¿Qué ventaja tiene entregar mediante Fork + Pull Request en lugar de un archivo comprimido? ¿Cómo facilita la la retroalimentación?

R. Es mejor ya que asi podemos comparar facilmente entre un repositorio y otro.