
# TODO

*Je me suis mis le défi de faire une version barebones aussi vite que possible, d'où la version rudimentaire que vous voyez.*
*N'étant pas familier avec la notation RPN, il se peut que j'aie mal compris certains aspects...*

A des fins de simplicité, **le code actuel ne traite que des entiers**.
Plutôt que d'avoir une route pour ajouter des valeurs à la stack et une autre pour les opérations, **j'ai mis une seule saisie utilisateur**. Le back va ainsi distinguer les saisies de nombres des saisies d'utilisateurs et les traiter en conséquence.
Coté stack, le code actuel permet d'ajouter des entiers dans une stack, de la consulter et de la vider
Coté opération, le code actuel permet:
- les additions
- les soustractions
- les divisions
- les mutliplications