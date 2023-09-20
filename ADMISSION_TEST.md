# Contratación - Lobby Wars

## Descripción

Estamos en la era de las "demandas", todo el mundo quiere ir a los tribunales con su abogado Saul e intentar conseguir un montón de dólares como si llovieran sobre
Manhattan.

Las leyes han cambiado mucho últimamente y los gobiernos se han digitalizado. Es entonces cuando entra en juego *Signaturit*.

El ayuntamiento mediante el uso de *Signaturit* mantiene un registro de firmas legales de cada parte implicada en los contratos que se realizan.

Durante un juicio, la justicia sólo verifica las firmas de las partes implicadas en el contrato para decidir quién gana. Para ello, asignan puntos a las
diferentes firmas en función del papel de quien haya firmado.

Por ejemplo, si el demandante tiene un contrato que está firmado por un notario obtiene 2 puntos, si el demandado tiene en el contrato la firma de sólo un validador obtiene sólo 1 punto, por lo que la parte demandante gana el juicio.

### Roles

* King (K): 5 points
* Notary (N): 2 points
* Validator (V): 1 points

Tenga en cuenta que cuando un Rey firma, las firmas de los validadores por su parte no tienen ningún valor.

## Primera fase

Queremos que automatice este proceso, dado un contrato con las 2 partes implicadas y sus firmas, e indique cuál gana la prueba.

Haz un programa que reciba ambos contratos como entrada (por ejemplo **KN** vs **NNV** ) y dé como salida el ganador. Deberíamos poder interactuar desde consola o *HTTP*.

## Segunda fase

A veces el contrato no tiene todos los signos, por lo que lo representamos utilizando el carácter **#**. Teniendo en cuenta que sólo una firma por parte puede estar vacía para ser válida, determine cuál es la firma mínima necesaria para ganar el juicio dado un contrato con las firmas de la conocida parte de la oposición.

Por ejemplo, para el caso **N#V** vs. **NVV**, debería retornar **N**.

Hacer un programa que reciba ambos contratos como entrada (por ejemplo **N#V** vs. **NVV** ) y dé como salida la firma necesaria para ganar. Deberíamos ser capaz de interactuar desde la consola o HTTP.