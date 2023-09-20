# Contrataci�n - Lobby Wars

## Descripci�n

Estamos en la era de las "demandas", todo el mundo quiere ir a los tribunales con su abogado Saul e intentar conseguir un mont�n de d�lares como si llovieran sobre
Manhattan.

Las leyes han cambiado mucho �ltimamente y los gobiernos se han digitalizado. Es entonces cuando entra en juego *Signaturit*.

El ayuntamiento mediante el uso de *Signaturit* mantiene un registro de firmas legales de cada parte implicada en los contratos que se realizan.

Durante un juicio, la justicia s�lo verifica las firmas de las partes implicadas en el contrato para decidir qui�n gana. Para ello, asignan puntos a las
diferentes firmas en funci�n del papel de quien haya firmado.

Por ejemplo, si el demandante tiene un contrato que est� firmado por un notario obtiene 2 puntos, si el demandado tiene en el contrato la firma de s�lo un validador obtiene s�lo 1 punto, por lo que la parte demandante gana el juicio.

### Roles

* King (K): 5 points
* Notary (N): 2 points
* Validator (V): 1 points

Tenga en cuenta que cuando un Rey firma, las firmas de los validadores por su parte no tienen ning�n valor.

## Primera fase

Queremos que automatice este proceso, dado un contrato con las 2 partes implicadas y sus firmas, e indique cu�l gana la prueba.

Haz un programa que reciba ambos contratos como entrada (por ejemplo **KN** vs **NNV** ) y d� como salida el ganador. Deber�amos poder interactuar desde consola o *HTTP*.

## Segunda fase

A veces el contrato no tiene todos los signos, por lo que lo representamos utilizando el car�cter **#**. Teniendo en cuenta que s�lo una firma por parte puede estar vac�a para ser v�lida, determine cu�l es la firma m�nima necesaria para ganar el juicio dado un contrato con las firmas de la conocida parte de la oposici�n.

Por ejemplo, para el caso **N#V** vs. **NVV**, deber�a retornar **N**.

Hacer un programa que reciba ambos contratos como entrada (por ejemplo **N#V** vs. **NVV** ) y d� como salida la firma necesaria para ganar. Deber�amos ser capaz de interactuar desde la consola o HTTP.