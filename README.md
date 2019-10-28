# MLAPI
DNA test for subjects

Esta solucion cuenta con una API para detectar si una arreglo de 6 strings contienen suficientes cadenas para comprobar si el sujeto siendo testeado es un mutante.
Enviando un Json mediante un metodo POST a http://ismutantapi-dev.us-west-2.elasticbeanstalk.com/mutant/ se puede verificar el resultado.

El Json debe estar constituido de la siguiente forma: 
debe conter un elemeto que represent un arreglo de 6 strings, dichos string deben estar compuestos por 6 caracters que puede ser entre A, C, G y T.

Por Ejemplo

{
"dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"],
}

Con el contenido se constituye una matriz 2d Si se llegase a detectar 2 o mas instancias donde hayan 4 characteres iguales seguidos ya sea vertiral, horizontal o diagonal se determina que el sujeto poseedor de dicho ADN es un mutante.
